﻿using Monitor.Helper;
using Monitor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using Monitor.View;
using IBM.Data.DB2;
using DB2DataAccess;
using System.Data.SqlClient;
using System.Data;

namespace Monitor.ViewModel
{
    class MainWindowVM : ViewModelBase
    {
        private ScanCodeEntities repo = new ScanCodeEntities();
        private NameValueCollection textResource = ConfigurationManager.GetSection("textResource") as NameValueCollection;
        private string ngBarcode = ConfigurationManager.AppSettings["ngBarcode"];
        /// <summary>
        /// 保存从.order文件中读取的订单条目
        /// </summary>
        private List<OrderLine> orderLines = new List<OrderLine>();
        private List<Order> orders;
        private ObservableCollection<Route> routes;
        private int orderIndex;//当前分解订单序号，模拟分户信号。首户不发送切户信号
        private int orderCount;//当天订单数量
        public int OrderCount 
        {
            get
            {
                return orderCount;
            }
            private set
            {
                orderCount = value;
                RaisePropertyChanged("OrderCount");
            }
        }
        private string showProgressBar;
        public string ShowProgressBar 
        {
            get
            {
                return showProgressBar;
            }
            set
            {
                showProgressBar = value;
                RaisePropertyChanged("ShowProgressBar");
            }
        }

        private int barSequence = 0;//订单内条烟顺序
        private int qrSequence = 0;
        private int cartonCount;//当天条烟数量
        public int CartonCount 
        {
            get
            {
                return cartonCount;
            }
            private set
            {
                cartonCount = value;
                RaisePropertyChanged("CartonCount");
            }
        }

        public int Delay { get; set; }

        private int currentOrderNumber;
        public int CurrentOrderNumber
        {
            get
            {
                return currentOrderNumber;
            }
            set
            {
                currentOrderNumber = value;
                RaisePropertyChanged("CurrentOrderNumber");
            }
        }
        private int currentCartonNumber;
        public int CurrentCartonNumber
        {
            get
            {
                return currentCartonNumber;
            }
            set
            {
                currentCartonNumber = value;
                RaisePropertyChanged("CurrentCartonNumber");
            }
        }
        private string currentNumberVisibility;
        public string CurrentNumberVisibility
        {
            get
            {
                return currentNumberVisibility;
            }
            set
            {
                currentNumberVisibility = value;
                RaisePropertyChanged("CurrentNumberVisibility");
            }
        }
        private bool firstQR = true;
        
        private SerialPort shiftSignalPort = new SerialPort(ConfigurationManager.AppSettings["shiftCOM"]);
        private SerialPort qrCodePort = new SerialPort(ConfigurationManager.AppSettings["qrcodeCOM"]);
        private byte[] result = new byte[1024];//接收从网口来的barcode

        private Socket clientSocket;

        /// <summary>
        /// 队列同步锁
        /// </summary>
        private object syncObj = new object();
        /// <summary>
        /// 缓存从串口读取的条码和二维码
        /// </summary>
        private Queue<BarCode> barCodes = new Queue<BarCode>();
        private Queue<QRCode> qrCodes = new Queue<QRCode>();

        /// <summary>
        /// 记录日志
        /// </summary>
        private StreamWriter swLog = new StreamWriter("errorlog.txt");

        /// <summary>
        /// 绑定到UI的当前订单
        /// </summary>
        private Order currentOrder;
        public Order CurrentOrder
        {
            get
            {
                return currentOrder;
            }
            set
            {
                currentOrder = value;
                RaisePropertyChanged("CurrentOrder");
            }
        }
        private Order barcodeCurrentOrder;//条码的切户需要延时

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }

        /// <summary>
        /// 绑定到UI的当前订单二维码和条码
        /// </summary>
        public ObservableCollection<string> CurrentQRCodes { get; set; }
        public ObservableCollection<string> CurrentBarcodes { get; set; }

        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand UpCommand { get; set; }
        public DelegateCommand DownCommand { get; set; }
        public DelegateCommand ModifyRouteCommand { get; set; }
        public DelegateCommand SubmitToDBCommand { get; set; }
        
        public MainWindowVM()
        {           
            Message = textResource["welcome"];
            ShowProgressBar = "Hidden";
            CurrentNumberVisibility = "Hidden";
            Delay = int.Parse(ConfigurationManager.AppSettings["delay"]);
            //当前订单和当前订单的二维码条码
            CurrentOrder = new Order();
            CurrentQRCodes = new ObservableCollection<string>();
            CurrentBarcodes = new ObservableCollection<string>();

            StartCommand = new DelegateCommand(Start);
            UpCommand = new DelegateCommand(Up);
            DownCommand = new DelegateCommand(Down);
            ModifyRouteCommand = new DelegateCommand(ModifyRoute);
            SubmitToDBCommand = new DelegateCommand(SubmitToDB, CanSubmitToDB);     

            //保存已接收的二维码条码到本地SQL Server数据库
            Thread threadSaveQR = new Thread(SaveQRCode) { IsBackground = true };
            threadSaveQR.Start();
            Thread threadSaveBar = new Thread(SaveBarCode) { IsBackground = true };
            threadSaveBar.Start();

            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            App.Current.MainWindow.Closed += MainWindow_Closed;
        }       

        private void Start(object o)
        {
            if (InitComPort() == false)
            {
                return;
            }/*
            if (InitSocket() == false)
            {
                return;
            }  */          
            Message = textResource["waitingOrders"];
            Thread receivOrders = new Thread(InitOrdersTest) { IsBackground = true };
            receivOrders.Start();
            //InitOrdersTest();
        }

        #region 初始化COM口，接收二位码
        private bool InitComPort()
        {
            shiftSignalPort.BaudRate = 9600;
            shiftSignalPort.DataBits = 8;
            shiftSignalPort.Parity = Parity.None;
            shiftSignalPort.StopBits = StopBits.One;
            try
            {
                shiftSignalPort.Open();
            }
            catch (Exception ex)
            {
                swLog.WriteLine(ex.Message);
                swLog.Flush();
                Message = string.Format(textResource["openShiftComFail"], shiftSignalPort.PortName);
                return false;
            }

            qrCodePort.BaudRate = 57600;
            qrCodePort.DataBits = 8;
            qrCodePort.Parity = Parity.None;
            qrCodePort.StopBits = StopBits.One;
            try
            {
                qrCodePort.Open();
            }
            catch (Exception ex)
            {
                swLog.WriteLine(ex.Message);
                swLog.Flush();
                Message = string.Format(textResource["openQrComFail"], qrCodePort.PortName);
                return false;
            }

            shiftSignalPort.DataReceived += shiftSignalPort_DataReceived;
            qrCodePort.DataReceived += qrCodePort_DataReceived;

            return true;
        }

        /// <summary>
        /// 收到切户信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shiftSignalPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort port = sender as SerialPort;
                //int a = port.ReadChar();
                //int b = port.ReadChar();//63
                int a = port.ReadByte();//194
                string code = Convert.ToString(a, 16).Trim();
                //string code = port.ReadExisting();?
                //byte[] buffer = new byte[1024];
                //port.Read(buffer, 0, 1024);
                //string code = port.ReadLine();
                if (code == "c2")
                {
                    barSequence = 0;
                    qrSequence = 0;

                    if (orderIndex == OrderCount)
                    {
                        orderIndex = 1;
                    }
                    orderIndex++;
                    App.Current.Dispatcher.Invoke(delegate() { CurrentQRCodes.Clear(); });                    
                    CurrentOrder = orders[orderIndex];//new Order() { Number = orders.ElementAt(orderIndex).Number, Retailer = orders.ElementAt(orderIndex).Retailer, TotalCount = orders.ElementAt(orderIndex).TotalCount };

                    Thread.Sleep(Delay);
                    App.Current.Dispatcher.Invoke(delegate() { CurrentBarcodes.Clear(); });
                    barcodeCurrentOrder = orders[orderIndex];                    

                    CurrentOrderNumber++;
                }
            }
            catch (Exception ex)
            {
                swLog.WriteLine(ex.Message);
                swLog.WriteLine(ex.StackTrace);
                swLog.Flush();
            }
        }

        private void qrCodePort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (firstQR)
                {
                    CurrentOrderNumber = 1;
                    CurrentNumberVisibility = "Visible";
                    firstQR = false;
                }
                CurrentCartonNumber++;

                SerialPort port = sender as SerialPort;
                string code = port.ReadTo(",");
                App.Current.Dispatcher.Invoke(new AddCodeToCollectionEvent(AddQRCodeToCurrent), code);
                lock (syncObj)
                {
                    QRCode qrcode;
                    if (code == "NG")
                    {
                        qrcode = new QRCode() { URL = code, Code = code, OrderNumber = CurrentOrder.Number, DateTime = DateTime.Now, NationCustCode = CurrentOrder.RetailerId, Sequence = qrSequence++ };                    
                    }
                    else
                    {
                        qrcode = new QRCode() { URL = code, Code = code.Split(new string[] { "/q/" }, StringSplitOptions.RemoveEmptyEntries)[1], OrderNumber = CurrentOrder.Number, DateTime = DateTime.Now, NationCustCode = CurrentOrder.RetailerId, Sequence = qrSequence++ };
                    }
                    qrCodes.Enqueue(qrcode);
                }
            }
            catch (Exception ex)
            {
                swLog.WriteLine(ex.Message);
                swLog.WriteLine(ex.StackTrace);
                swLog.Flush();
                //swLog.Close();
            }
        }
        #endregion

        #region 初始化网口，接收条码
        public bool InitSocket()
        {
            //设定服务器IP地址  
            IPAddress ip = IPAddress.Parse(ConfigurationManager.AppSettings["barcodeIP"]);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Message = textResource["connectingBarScaner"];
                clientSocket.Connect(new IPEndPoint(ip, 23)); //配置服务器IP与端口                  
            }
            catch
            {
                Message = textResource["connectBarScanerFail"];
                return false;
            }
            Thread thread = new Thread(ReceiveBarcode) { IsBackground = true };
            thread.Start();
            return true;
        }

        private void ReceiveBarcode()
        {
            //通过clientSocket接收数据              
            int length = 15;//条码13位加两位换行
            while (true)
            {
                int receiveLength = clientSocket.Receive(result, length, 0);
                string code = Encoding.ASCII.GetString(result, 0, receiveLength).Trim();
                App.Current.Dispatcher.Invoke(new AddCodeToCollectionEvent(AddBarCodeToCurrent), code);
                BarCode barcode = new BarCode() { Code = code, OrderNumber = barcodeCurrentOrder.Number, DateTime = DateTime.Now, Sequence = barSequence++ };
                barCodes.Enqueue(barcode);
            }
        }
        #endregion

        #region 等待接收从欧康发来的订单信息
        private void InitOrders()
        {
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            string myip = IpEntry.AddressList[2].ToString();
            IPAddress ip = IPAddress.Parse(myip);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(ip, 8089));  //绑定IP地址：端口  
            server.Listen(10);
            int bufferSize = 1024 * 1024;
            Socket client = server.Accept();
            byte[] buffer = new byte[bufferSize];
            int size;

            string orderFolder = ConfigurationManager.AppSettings["orderFolder"];
            if (!Directory.Exists(orderFolder))
            {
                Directory.CreateDirectory(orderFolder);
            }
            string dayFolder = Path.Combine(orderFolder, DateTime.Today.ToString("yyyyMMdd"));
            Directory.CreateDirectory(dayFolder);
            string zipName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".rar";
            string zipFile = Path.Combine(dayFolder, zipName);
            //创建文件流，然后让文件流来根据路径创建一个文件
            FileStream fs = new FileStream(zipFile, FileMode.Create);

            client.ReceiveBufferSize = bufferSize;
            Thread.Sleep(1000);
            size = client.Receive(buffer, buffer.Length, SocketFlags.None);
            fs.Write(buffer, 0, size);
            fs.Close();
            byte[] b = { 0, 0, 0, 0, 79, 68, 82, 59, 13, 10, 35, 35, 02 };
            client.Send(b);
            client.Close();
            server.Close();

            //unzip
            ZipHelper.UnRAR(dayFolder, dayFolder, zipName);

            string orderFile = Directory.GetFiles(dayFolder, "*.Order")[0];
            StreamReader sr = new StreamReader(orderFile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                OrderLine ol = new OrderLine() { OrderNumber = temp[1], RetailerId = temp[2], Retailer = temp[3], BrandId = temp[4], Brand = temp[5], Count = int.Parse(temp[6]), RouteId = temp[9], RouteName = temp[10] };
                orderLines.Add(ol);
            }
            sr.Close();
            int totalCount = orderLines.Sum(ol => ol.Count);
            var t = from ol in orderLines group ol by ol.OrderNumber into g select new Order() { Number = g.Key, Retailer = g.First().Retailer, OrderLines = g.ToList(), TotalCount = g.Sum(l => l.Count), RouteId = g.First().RouteId, RouteName = g.First().RouteName, RetailerId = g.First().RetailerId };
            orders = t.ToList();
            var r = from o in orders group o by o.RouteId into g select new Route() { Id = g.Key, Name = g.First().RouteName, Orders = g.ToList() };
            routes = new ObservableCollection<Route>(r);
            OrderCount = Enumerable.Count(orders);
            CartonCount = orders.Sum(o => o.TotalCount);
            CurrentOrder = orders[0];
            barcodeCurrentOrder = orders[0];
            Message = textResource["readOrderSuccess"];
        }

        private void InitOrdersTest()
        {
            Thread.Sleep(1);
            string orderFile = ConfigurationManager.AppSettings["orderFilePath"];
            StreamReader sr = new StreamReader(orderFile);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                OrderLine ol = new OrderLine() { OrderNumber = temp[1], RetailerId = temp[2], Retailer = temp[3], BrandId = temp[4], Brand = temp[5], Count = int.Parse(temp[6]), RouteId = temp[9], RouteName = temp[10] };
                orderLines.Add(ol);
            }
            sr.Close();
            int totalCount = orderLines.Sum(ol => ol.Count);
            var t = from ol in orderLines group ol by ol.OrderNumber into g select new Order() { Number = g.Key, Retailer = g.First().Retailer, OrderLines = g.ToList(), TotalCount = g.Sum(l => l.Count), RouteId = g.First().RouteId, RouteName = g.First().RouteName, RetailerId = g.First().RetailerId };
            orders = t.ToList();
            var r = from o in orders group o by o.RouteId into g select new Route() { Id = g.Key, Name = g.First().RouteName, Orders = g.ToList() };
            routes = new ObservableCollection<Route>(r);
            OrderCount = Enumerable.Count(orders);
            CartonCount = orders.Sum(o => o.TotalCount);
            CurrentOrder = orders[0];
            barcodeCurrentOrder = orders[0];
            Message = textResource["readOrderSuccess"];
        }
        #endregion

        private void Up(object o)
        {
            if (CurrentOrder != null && barcodeCurrentOrder != null)
            {
                if (orderIndex > 0)
                {
                    orderIndex--;
                    CurrentOrder = orders[orderIndex];//new Order() { Number = orders.ElementAt(orderIndex).Number, Retailer = orders.ElementAt(orderIndex).Retailer, TotalCount = orders.ElementAt(orderIndex).TotalCount };
                    barcodeCurrentOrder = orders[orderIndex];
                    if (CurrentNumberVisibility == "Hidden")
                    {
                        CurrentNumberVisibility = "Visibility";                        
                    }
                    CurrentOrderNumber--;
                }
            }
        }

        private void Down(object o)
        {
            if (CurrentOrder != null && barcodeCurrentOrder != null)
            {
                if (orderIndex < orders.Count - 1)
                {
                    orderIndex++;
                    CurrentOrder = orders[orderIndex];//new Order() { Number = orders.ElementAt(orderIndex).Number, Retailer = orders.ElementAt(orderIndex).Retailer, TotalCount = orders.ElementAt(orderIndex).TotalCount };
                    barcodeCurrentOrder = orders[orderIndex];
                    if (CurrentNumberVisibility == "Hidden")
                    {
                        CurrentNumberVisibility = "Visibility";                        
                    }
                    CurrentOrderNumber++;
                }
            }
        }

        private void ModifyRoute(object o)
        {
            if (routes == null)
            {
                Message = textResource["clickStart"];
            }
            else
            {
                RouteModify window = new RouteModify(routes, orders) { Owner = App.Current.MainWindow };
                window.ShowDialog();
            }
        }

        private void SendToDB2()
        {
            Message = textResource["submittingToDB2"];
            ShowProgressBar = "Visible";
            var qrcodes = repo.QRCodes.ToList().Where(q => q.DateTime.HasValue && q.DateTime.Value.Date == DateTime.Today.Date);
            var barcodes = repo.BarCodes.ToList().Where(b => b.DateTime.HasValue && b.DateTime.Value.Date == DateTime.Today.Date);

            string connectionString = ConfigurationManager.ConnectionStrings["weixin"].ConnectionString;
            DB2 weixinDB = new DB2(connectionString);
            
            string qrCmd = "INSERT INTO \"DB2ADMIN\".\"QRCODES\"(\"URL\", \"CODE\", \"ORDERNUMBER\", \"SAVETIME\", \"SEQUENCE\", \"NATIONCUSTCODE\") VALUES('{0}', '{1}', '{2}', '{3}', {4}, '{5}')";
            foreach (QRCode qrcode in qrcodes)
            {
                string sql = string.Format(qrCmd, qrcode.URL, qrcode.Code, qrcode.OrderNumber, qrcode.DateTime.HasValue ? qrcode.DateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "0000-00-00 00:00:00", qrcode.Sequence.HasValue ? qrcode.Sequence : -1, qrcode.NationCustCode);
                weixinDB.Insert(sql);
            }            
           
            //补空
            if (orders == null)
            {
                string orderFolder = ConfigurationManager.AppSettings["orderFolder"];
                string dayFolder = Path.Combine(orderFolder, DateTime.Today.ToString("yyyyMMdd"));           
                string orderFile = Directory.GetFiles(dayFolder, "*.Order")[0];
                StreamReader sr = new StreamReader(orderFile);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] temp = line.Split(',');
                    OrderLine ol = new OrderLine() { OrderNumber = temp[1], RetailerId = temp[2], Retailer = temp[3], BrandId = temp[4], Brand = temp[5], Count = int.Parse(temp[6]), RouteId = temp[9], RouteName = temp[10] };
                    orderLines.Add(ol);
                }
                sr.Close();
                int totalCount = orderLines.Sum(ol => ol.Count);
                var t = from ol in orderLines group ol by ol.OrderNumber into g select new Order() { Number = g.Key, Retailer = g.First().Retailer, OrderLines = g.ToList(), TotalCount = g.Sum(l => l.Count), RouteId = g.First().RouteId, RouteName = g.First().RouteName, RetailerId = g.First().RetailerId };
                orders = t.ToList();
            }
           
            var barcodeOrderGroups = barcodes.GroupBy(b => b.OrderNumber);//扫描后的条码按订单号分组
            foreach (Order o in orders)
            {
                var barcodeOrderGroup = barcodeOrderGroups.First(g => g.Key == o.Number).ToList();
                if (barcodeOrderGroup.Any(b => b.Code == ngBarcode))
                {
                    //先补缝隙XXXXXX,000000,XXXXXXX
                    int begin = 0;
                    int barcodeCount = barcodeOrderGroup.Count();
                    while (begin < barcodeCount)
                    {
                        BarCode beginBarcode = barcodeOrderGroup[begin];
                        if (beginBarcode.Code == ngBarcode)
                        {
                            begin++;
                        }
                        else
                        {
                            BarCode endBarcode = barcodeOrderGroup.Last(b => b.Code == beginBarcode.Code);
                            if (endBarcode != null)
                            {
                                int last = barcodeOrderGroup.IndexOf(endBarcode);
                                string revisedCode = endBarcode.Code;
                                for (int i = begin; i <= last; i++)
                                {
                                    barcodeOrderGroup[i].RevisedCode = revisedCode;
                                }
                                begin = last + 1;
                            }
                        }
                    }
                    //查看是否全部补齐
                    if (barcodeOrderGroup.Any(b => b.RevisedCode == ngBarcode))
                    {
                        var barcodeBrandGroups = barcodeOrderGroup.Where(c => c.Code != ngBarcode).GroupBy(c => c.Code);//扫描后的条码按条码分组
                        int brandCount = barcodeBrandGroups.Count();//扫描后的品牌数量

                        if (o.TotalCount == barcodeOrderGroup.Count)//订单内条烟全部触发 应该是==
                        {
                            int index = 0; //条烟在订单内的序号                            
                            if (o.OrderLines.Count == brandCount)//全部触发且品牌无漏扫 应该是==
                            {
                                for (int i = 0; i < brandCount; i++)
                                {
                                    var barcodeBrandGroup = barcodeBrandGroups.ElementAt(i);
                                    string brandId = barcodeBrandGroup.Key;
                                    OrderLine orderLine = o.OrderLines.Find(l => l.BrandId == brandId);//订单内该品牌的实际条烟数量
                                    if (orderLine != null)
                                    {
                                        for (int j = 0; j < orderLine.Count; j++)
                                        {
                                            BarCode barcode = barcodeOrderGroup.ElementAt(index);
                                            barcode.RevisedCode = brandId;
                                            index++;
                                        }
                                    }
                                }
                            }
                            else//全部触发但是有品牌的条码全部漏扫
                            {
                                if (o.OrderLines.Count - brandCount == 1)//先只处理一个品牌全漏扫的情况
                                {

                                }
                            }
                        }
                        else//有没有触发的条烟
                        {
                            if (o.OrderLines.Count == brandCount)//有漏触发但品牌无漏扫 应该是==
                            {
                                string mark = string.Empty;
                                List<BarCode> group = new List<BarCode>();
                                List<List<BarCode>> groups = new List<List<BarCode>>();
                                foreach (BarCode barcode in barcodeOrderGroup)
                                {
                                    if (barcode.RevisedCode != mark)
                                    {
                                        groups.Add(group);
                                        group = new List<BarCode>();
                                        mark = barcode.RevisedCode;
                                    }                                    
                                    group.Add(barcode);
                                }
                                if (groups.Count > 1)
                                {
                                    for (int i = 0; i < groups.Count; i++)
                                    {
                                        if (i == 0 && groups[i].First().RevisedCode == ngBarcode)
                                        {
                                            foreach (BarCode b in groups[i])
                                            {
                                                b.RevisedCode = groups[i + 1].First().RevisedCode;
                                            }
                                        }
                                        else if (i == groups.Count - 1 && groups[i].First().RevisedCode == ngBarcode)
                                        {
                                            foreach (BarCode b in groups[i])
                                            {
                                                b.RevisedCode = groups[i - 1].First().RevisedCode;
                                            }
                                        }
                                        else
                                        {
                                            List<BarCode> g = groups[i];
                                            
                                            if (g.First().RevisedCode == ngBarcode)
                                            {
                                                string codeBefore = groups[i - 1].First().RevisedCode;
                                                string codeAfter = groups[i + 1].First().RevisedCode;
                                                //先用前面的条烟验证
                                                int actualCount = barcodes.Count(b => b.RevisedCode == codeBefore);
                                                int expectCount = o.OrderLines.Find(ol => ol.BrandId == codeBefore).Count;
                                                if (actualCount == expectCount)
                                                {
                                                    foreach (BarCode b in g)
                                                    {
                                                        b.RevisedCode = codeAfter;
                                                    }
                                                    continue;
                                                }
                                                actualCount = barcodes.Count(b => b.RevisedCode == codeAfter);
                                                expectCount = o.OrderLines.Find(ol => ol.BrandId == codeAfter).Count;
                                                if (actualCount == expectCount)
                                                {
                                                    foreach (BarCode b in g)
                                                    {
                                                        b.RevisedCode = codeBefore;
                                                    }
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else//有漏触发并且品牌无漏扫 应该是==
                            {
                            }
                        }
                    }                   
                }
                repo.SaveChanges();
            }

            string barCmd = "INSERT INTO \"DB2ADMIN\".\"BARCODES\"(\"CODE\", \"ORDERNUMBER\", \"SAVETIME\", \"SEQUENCE\") VALUES('{0}', '{1}', '{2}', {3})";
            foreach (BarCode barcode in barcodes)
            {                
                string sql = string.Format(barCmd, barcode.RevisedCode, barcode.OrderNumber, barcode.DateTime.HasValue ? barcode.DateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "0000-00-00 00:00:00", barcode.Sequence.HasValue ? barcode.Sequence : -1);
                weixinDB.Insert(sql);
            }
            weixinDB.Close();

            Sync32BitsCodes();

            Message = textResource["submitComplete"];
            ShowProgressBar = "Hidden";
        }

        private void Sync32BitsCodes()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["code32"].ConnectionString);
            connection.Open();
            string sql = string.Format("select * from BP_ORDER_BARCODE where OB_SORT_DATE = '{0}'", DateTime.Today);
            SqlDataAdapter adpter = new SqlDataAdapter(sql, connection);
            DataTable table = new DataTable();
            adpter.Fill(table);
            //DataRow row = table.Rows[0];
            adpter.Dispose();
            connection.Close();

            DB2 weixinDB = new DB2(ConfigurationManager.ConnectionStrings["weixin"].ConnectionString);
            foreach (DataRow row in table.Rows)
            {
                string code = row["OB_BCIG_BARCODE"].ToString();
                string custId = row["OB_RETAILER_CODE"].ToString();
                string sequence = row["OB_Sequence"].ToString();
                string date = DateTime.Parse(row["OB_SORT_DATE"].ToString()).ToString("yyyy-MM-dd");
                sql = string.Format("INSERT INTO \"DB2ADMIN\".\"CODES32\"(\"CODE\", \"NationCustCode\", \"DATE\", \"SEQUENCE\") VALUES('{0}', '{1}', '{2}', {3})", code, custId, date, sequence);
                weixinDB.Insert(sql);
            }
            weixinDB.Close();
        }

        private void SubmitToDB(object o)
        {
            //App.Current.Dispatcher.Invoke(SendToDB2);
            Thread sendToDB2 = new Thread(SendToDB2) { IsBackground = true };
            sendToDB2.Start();
            #region old
            /* old
            //从数据库中读取暂存的条码
            //List<QRCode> qrCodes = repo.QRCodes.ToList();
            //List<BarCode> barCodes = repo.BarCodes.ToList();

            //从暂存文本中读取
            List<QRCode> qrCodes = new List<QRCode>();
            List<BarCode> barCodes = new List<BarCode>();
            StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["qrcode"]);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                QRCode qrCode = new QRCode() { Code = line.Split('\t')[1] };
                qrCodes.Add(qrCode);
            }
            sr.Close();
            sr = new StreamReader(ConfigurationManager.AppSettings["barcode"]);
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split('\t');
                BarCode qrCode = new BarCode() { Code = temp[1], OrderNumber = temp[2] };
                barCodes.Add(qrCode);
            }
            sr.Close();
            if (qrCodes.Count != barCodes.Count)
            {
                //接收到的二维码和条码数量不一致，未知错误
                Message = textResource["barcodeQrcodeNotEq"];
                return;
            }
            int index = 0;
            var barcodeOrders = barCodes.GroupBy(bc => bc.OrderNumber); //扫描到的条码按订单号分组            
            StreamWriter logWriter = new StreamWriter(ConfigurationManager.AppSettings["log"]);
            foreach (var order in orders)//以.Order文件中读取的订单信息为基准依次比对
            {
                line = string.Format("订单：{0}，零售户：{1}，条烟总数：{3}", order.Number, order.Retailer, order.TotalCount);
                logWriter.WriteLine(line);
                var barcodeOrder = barcodeOrders.First(g => g.Key == order.Number);
                if (barcodeOrder == null)
                {
                    //没有找到，说明整个订单内的所有条烟都没有触发
                    logWriter.WriteLine("没有找到该订单的条烟，整个订单内的所有条烟都没有触发。");
                }
                else
                {
                    var brands = barcodeOrder.Where(b => b.Code != "NA").GroupBy(bc => bc.Code);//扫描的品牌集合
                    //验证一个订单中扫描后的条烟数量
                    int cartonCount = barcodeOrder.Count();
                    if (order.TotalCount == cartonCount)//全部触发
                    {
                        logWriter.WriteLine("条烟全部触发。");
                        var naBarcodes = barcodeOrder.Where(bc => bc.Code == "NA");
                        int naCount = naBarcodes.Count();
                        if (naCount != 0)
                        {
                            logWriter.WriteLine(string.Format("漏扫条数：{0}", naCount));
                            //处理NA条码

                            if (order.OrderLines.Count == brands.Count())//每个品牌至少有一条烟被扫到
                            {
                                logWriter.WriteLine("品牌无漏扫。");
                                //依次填品牌，数量即可
                                foreach (var brand in brands)
                                {
                                    string barcode = brand.Key;
                                    int count = order.OrderLines.First(ol => ol.BrandId == barcode).Count;
                                    for (int i = 0; i < count; i++)
                                    {
                                        Carton carton = new Carton() { QRCode = qrCodes[index].Code, Barcode = barcode, OrderNumber = order.Number };
                                        repo.Cartons.Add(carton);
                                    }
                                }
                                logWriter.WriteLine("漏扫已填充。");
                            }
                            else
                            {
                                logWriter.WriteLine("漏扫未填充。");
                                
                                //填充先后品牌相同的NA
                                foreach (BarCode barcode in naBarcodes)
                                {
                                    int i = barcodeOrder.ToList().IndexOf(barcode);//NA条烟在扫描序列中的位置
                                    if (i > 0 && i < barcodeOrder.Count() - 1)
                                    {
                                        BarCode before = barcodeOrder.ElementAt(i - 1);
                                        BarCode after = barcodeOrder.ElementAt(i + 1);
                                        if (before.Code == after.Code)
                                        {
                                            barcode.Code = before.Code;
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            logWriter.WriteLine("订单无漏扫。");
                            for (int i = 0; i < cartonCount; i++)
                            {
                                Carton carton = new Carton() { QRCode = qrCodes[index].Code, Barcode = barCodes[index].Code, OrderNumber = barCodes[index].OrderNumber };
                                repo.Cartons.Add(carton);
                                index++;
                            }
                        }
                    }
                    else//有没有触发
                    {
                        logWriter.WriteLine("未触发条烟个数：{0}", order.TotalCount - cartonCount);
                        if (order.OrderLines.Count == brands.Count())//每个品牌至少有一条烟被扫到
                        {
                            logWriter.WriteLine("品牌无漏触发。");
                        }
                    }
                }
                repo.SaveChanges();
                logWriter.WriteLine();
            }
            */
            #endregion
        }

        private bool CanSubmitToDB(object o)
        {
            return true;
        }        


        /// <summary>
        /// WPF需要用Dispatcher异步调用集合操作
        /// </summary>
        /// <param name="code"></param>
        private delegate void AddCodeToCollectionEvent(string code);
        private void AddQRCodeToCurrent(string code)
        {
            CurrentQRCodes.Add(code);
        }
        private void AddBarCodeToCurrent(string code)
        {
            CurrentBarcodes.Add(code);
        }

        private void SaveBarCode()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["saveInTxt"]))
            {
                using (StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["barcode"], false))
                {
                    int i = 0;
                    while (true)
                    {
                        if (barCodes.Count > 0 && !string.IsNullOrWhiteSpace(CurrentOrder.Number))
                        {
                            try
                            {
                                string code;
                                lock (syncObj)
                                {
                                    //code = barCodes.Dequeue();
                                    i++;
                                }
                                //sw.WriteLine(string.Format("{0}\t{1}\t{2}", i, code, CurrentOrder.Number));
                                sw.Flush();
                            }

                            catch (Exception ex)
                            {
                                swLog.WriteLine(ex.Message);
                                swLog.WriteLine(ex.StackTrace);
                                swLog.Flush();
                                //swLog.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                using (ScanCodeEntities repo = new ScanCodeEntities())
                {
                    while (true)
                    {
                        if (barCodes.Count > 0 && !string.IsNullOrWhiteSpace(CurrentOrder.Number))
                        {
                            try
                            {
                                BarCode barcode; 
                                lock (syncObj)
                                {
                                    barcode = barCodes.Dequeue();
                                    //Console.WriteLine(barCodes.Count);
                                    //swQueueCount.WriteLine(string.Format("dequeue:{0}", barCodes.Count));
                                }
                                //BarCode barCode = new BarCode() { Code = code, OrderNumber = CurrentOrder.Number, DateTime = DateTime.Now, Sequence = barSequence++ };
                                repo.BarCodes.Add(barcode);
                                repo.SaveChanges();
                            }

                            catch (Exception ex)
                            {
                                swLog.WriteLine(ex.Message);
                                swLog.WriteLine(ex.StackTrace);
                                swLog.Flush();
                            }
                        }
                        Thread.Sleep(1);
                    }
                }
            }
        }

        private void SaveQRCode()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["saveInTxt"]))
            {
                using (StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["qrcode"], false))
                {
                    int i = 0;
                    while (true)
                    {
                        if (qrCodes.Count > 0)
                        {
                            try
                            {
                                string code;
                                lock (syncObj)
                                {
                                    //code = qrCodes.Dequeue();
                                    i++;
                                }
                                //sw.WriteLine(string.Format("{0}\t{1}", i, code));
                                sw.Flush();
                            }
                            catch (Exception ex)
                            {
                                swLog.WriteLine(ex.Message);
                                swLog.WriteLine(ex.StackTrace);
                                swLog.Flush();
                            }
                        }
                    }
                }
            }
            else
            {
                using (ScanCodeEntities repo = new ScanCodeEntities())
                {
                    while (true)
                    {
                        if (qrCodes.Count > 0)
                        {
                            try
                            {
                                QRCode qrcode;
                                lock (syncObj)
                                {
                                    qrcode = qrCodes.Dequeue();
                                }                                
                                repo.QRCodes.Add(qrcode);
                                repo.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                swLog.WriteLine(ex.Message);
                                swLog.WriteLine(ex.StackTrace);
                                swLog.Flush();
                            }
                        }
                        Thread.Sleep(1);
                    }
                }
            }
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["delay"].Value = Delay.ToString();
            config.Save();
            swLog.Close();
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            swLog.Close();
        }
    }
}
