using Monitor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static SerialPort qrPortSend = new SerialPort("COM3");       
        static SerialPort shiftPortSend = new SerialPort("COM1");
        static StreamWriter swBar = new StreamWriter("bar.txt");
        static StreamWriter swQR = new StreamWriter("qr.txt");    
       
        static StreamReader sr;

        static char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        delegate void TriggerHandle(int i);
        static event TriggerHandle triggerEvent;

        static void Main(string[] args)
        {
            string orderFolder = @"c:\order";
            if (!Directory.Exists(orderFolder))
            {
                Directory.CreateDirectory(orderFolder);
            }
            string dayFolder = Path.Combine(orderFolder, DateTime.Today.ToString("yyyyMMdd"));
            if (!Directory.Exists(dayFolder))
            {
                Directory.CreateDirectory(dayFolder);
            }
            string[] orderFiles = Directory.GetFiles(dayFolder, "*.Order", SearchOption.TopDirectoryOnly);
            sr = new StreamReader(orderFiles[0]);

            List<OrderLine> orderLines = new List<OrderLine>();          
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                OrderLine ol = new OrderLine() { OrderNumber = temp[1], RetailerId = temp[2], Retailer = temp[3], BrandId = temp[4], Brand = temp[5], Count = int.Parse(temp[6]), FJROut = temp[13] };
                if (ol.FJROut == "002")
                {
                    orderLines.Add(ol);
                }
            }
            sr.Close();
            IEnumerable<Order> orders = from ol in orderLines group ol by ol.OrderNumber into g select new Order() { Number = g.Key, OrderLines = g.ToList(), TotalCount = g.Sum(l => l.Count) };
            int totalCount = orderLines.Sum(o => o.Count);
            int total = orders.Sum(l => l.TotalCount);
            string[] names = SerialPort.GetPortNames();

            qrPortSend.BaudRate = 57600;
            qrPortSend.DataBits = 8;
            qrPortSend.Parity = Parity.None;
            qrPortSend.StopBits = StopBits.One;
            qrPortSend.WriteBufferSize = 1024 * 1;
            qrPortSend.Open();
           
            shiftPortSend.BaudRate = 9600;
            shiftPortSend.DataBits = 8;
            shiftPortSend.Parity = Parity.None;
            shiftPortSend.StopBits = StopBits.One;
            shiftPortSend.Open();

            int c0 = 0;
            foreach (Order order in orders)
            {
                //发送切户信号
                if (c0 > 0)
                {
                    shiftPortSend.Write(new byte[] { 194 }, 0, 1);//194的十六进制是c2
                    Thread.Sleep(500);
                }
                c0++;
                //将订单行项目随机排序
                OrderLine[] randomLines = order.OrderLines.OrderBy(l => Guid.NewGuid()).ToArray();
                foreach (OrderLine orderline in randomLines)
                {                   
                    string barcode = orderline.BrandId;
                    for (int i = 0; i < orderline.Count; i++)
                    {
                        string qrcode = string.Format("http://scdzyc.cn/q/{0}", new string(constant.OrderBy(c => Guid.NewGuid()).Take(7).ToArray()));
                        if (order.Number == "DZ10000005591490" && i == 1 )
                        {
                            qrcode = "NG";
                        }
                        Console.WriteLine(qrcode);
                        qrPortSend.Write(qrcode + ",");                                                        
                        Thread.Sleep(100);
                    }                    
                }                
            }
            swBar.Close();
            swQR.Close();            
        }        
    }
}
