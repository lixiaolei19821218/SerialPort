using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Receive
{
    class Program
    {
        static ScanCodeEntities repo = new ScanCodeEntities();
        static string orderNumber = string.Empty;
        static List<OrderLine> orderLines = new List<OrderLine>();
        static dynamic groups;
        static int groupIndex = 0;
        static int groupCount = 0;
        static object syncObj = new object();

        static Queue<string> barCodes = new Queue<string>();
        static Queue<string> qrCodes = new Queue<string>();

        static StreamWriter swLog = new StreamWriter("errorlog.txt");
        static StreamWriter swQueueCount = new StreamWriter("queueCount.txt");

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(@"C:\Users\lei\Desktop\Work\RetailerOrder20150509105427.Order");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                OrderLine ol = new OrderLine() { OrderNumber = temp[1], Retailer = temp[3], Count = int.Parse(temp[6]) };
                orderLines.Add(ol);
            }
            int totalCount = orderLines.Sum(ol => ol.Count);
            groups = from ol in orderLines group ol by new { ol.OrderNumber, ol.Retailer } into g select new { g.Key, TotalCount = g.Sum(o => o.Count) };
            groupCount = Enumerable.Count(groups);

            SerialPort shiftSignalPort = new SerialPort("COM6");
            SerialPort qrCodePort = new SerialPort("COM2");
            SerialPort barCodePort = new SerialPort("COM4");

            shiftSignalPort.BaudRate = 9600;
            shiftSignalPort.DataBits = 8;
            shiftSignalPort.Parity = Parity.None;
            shiftSignalPort.StopBits = StopBits.One;
            shiftSignalPort.Open();

            qrCodePort.BaudRate = 9600;
            qrCodePort.DataBits = 8;
            qrCodePort.Parity = Parity.None;
            qrCodePort.StopBits = StopBits.One;
            qrCodePort.Open();

            barCodePort.BaudRate = 9600;
            barCodePort.DataBits = 8;
            barCodePort.Parity = Parity.None;
            barCodePort.StopBits = StopBits.One;
            barCodePort.Open();

            shiftSignalPort.DataReceived += shiftSignalPort_DataReceived;
            qrCodePort.DataReceived += qrCodePort_DataReceived;
            barCodePort.DataReceived += barCodePort_DataReceived;

            Thread threadSaveQR = new Thread(SaveQRCode);
            threadSaveQR.Start();
            Thread threadSaveBar = new Thread(SaveBarCode);
            threadSaveBar.Start();

            Console.ReadKey();
        }

        static void barCodePort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            /*
            SerialPort port = sender as SerialPort;
            string code = port.ReadLine();
            using (ScanCodeEntities repo = new ScanCodeEntities())
            {
                BarCode barCode = new BarCode() { Code = code, OrderNumber = orderNumber };
                repo.BarCodes.Add(barCode);
                repo.SaveChanges();
            }*/
            
            try
            {
                SerialPort port = sender as SerialPort;
                string tem = port.ReadExisting();
                string code = port.ReadLine();
                //object syncObj = new object();
                lock (syncObj)
                {
                    barCodes.Enqueue(code);
                    //Console.WriteLine(barCodes.Count);
                    //swQueueCount.WriteLine(string.Format("enqueue:{0}", barCodes.Count));
                }
            }
            catch (Exception ex)
            {
                swLog.WriteLine(ex.Message);
                swLog.WriteLine(ex.StackTrace);
                swLog.Close();
            }
        }

        static void WriteBarCode()
        {

        }

        static void qrCodePort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            /*
            SerialPort port = sender as SerialPort;
            string code = port.ReadLine();
            QRCode qrCode = new QRCode() { Code = code };
            repo.QRCodes.Add(qrCode);
            repo.SaveChanges();*/
            
            try
            {                
                SerialPort port = sender as SerialPort;
                string code = port.ReadLine();
                //object syncObj = new object();
                lock (syncObj)
                {
                    qrCodes.Enqueue(code);                    
                }
            }
            catch (Exception ex)
            {
                swLog.WriteLine(ex.Message);
                swLog.WriteLine(ex.StackTrace);
                swLog.Close();
            }
        }

        static void shiftSignalPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort port = sender as SerialPort;
                string code = port.ReadLine();
                if (code == "Shift")
                {
                    if (groupIndex == groupCount)
                    {
                        groupIndex = 0;
                    }
                    //lock (syncObj)
                    {
                        orderNumber = Enumerable.ElementAt(groups, groupIndex++).Key.OrderNumber;
                        Console.WriteLine(orderNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                swLog.WriteLine(ex.Message);
                swLog.WriteLine(ex.StackTrace);
                swLog.Close();
            }
        }

        static private void SaveToDB()
        {
            try
            {
                if (barCodes.Count > 0 && qrCodes.Count > 0)
                {
                    string barCode = barCodes.Dequeue();
                    string qrCode = barCodes.Dequeue();

                }
            }
            catch (Exception ex)
            {
                swLog.WriteLine(ex.Message);
                swLog.WriteLine(ex.StackTrace);
                swLog.Close();
            }
        }

        static private void SaveBarCode()
        {
            using (ScanCodeEntities repo = new ScanCodeEntities())
            {
                while (true)
                {
                    if (barCodes.Count > 0 && orderNumber != string.Empty)
                    {
                        try
                        {
                            string code;
                            
                            lock (syncObj)
                            {
                                code = barCodes.Dequeue();
                                //Console.WriteLine(barCodes.Count);
                                //swQueueCount.WriteLine(string.Format("dequeue:{0}", barCodes.Count));
                            }
                            BarCode barCode = new BarCode() { Code = code, OrderNumber = orderNumber };
                            repo.BarCodes.Add(barCode);
                            repo.SaveChanges();
                        }

                        catch (Exception ex)
                        {
                            swLog.WriteLine(ex.Message);
                            swLog.WriteLine(ex.StackTrace);
                            swLog.Close();
                        }

                    }
                }
            }
        }

        static private void SaveQRCode()
        {
            using (ScanCodeEntities repo = new ScanCodeEntities())
            {
                while (true)
                {
                    if (qrCodes.Count > 0)
                    {
                        try
                        {
                            string code;
                            //object syncObj = new object();
                            lock (syncObj)
                            {
                                code = qrCodes.Dequeue();
                            }
                            QRCode qrCode = new QRCode() { Code = code };
                            repo.QRCodes.Add(qrCode);
                            repo.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            swLog.WriteLine(ex.Message);
                            swLog.WriteLine(ex.StackTrace);
                            swLog.Close();
                        }
                    }

                }
            }
        }
    }
}
