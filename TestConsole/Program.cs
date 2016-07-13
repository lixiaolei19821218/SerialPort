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
        static SerialPort barPortSend = new SerialPort("COM5");
        static SerialPort shiftPortSend = new SerialPort("COM1");
        static StreamWriter swBar = new StreamWriter("bar.txt");
        static StreamWriter swQR = new StreamWriter("qr.txt");
        static StreamReader sr = new StreamReader(@"QrCode20160707082416.Order");

        static char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        delegate void TriggerHandle(int i);
        static event TriggerHandle triggerEvent;

        static void Main(string[] args)
        {
            List<OrderLine> orderLines = new List<OrderLine>();
            //StreamReader sr = new StreamReader(@"C:\Users\lei\Desktop\Work\RetailerOrder20150509105427.Order");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                OrderLine ol = new OrderLine() { OrderNumber = temp[1], RetailerId = temp[2], Retailer = temp[3], BrandId = temp[4], Brand = temp[5], Count = int.Parse(temp[6]) };
                orderLines.Add(ol);
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

            barPortSend.BaudRate = 9600;
            barPortSend.DataBits = 8;
            barPortSend.Parity = Parity.None;
            barPortSend.StopBits = StopBits.One;
            barPortSend.Open();

            shiftPortSend.BaudRate = 9600;
            shiftPortSend.DataBits = 8;
            shiftPortSend.Parity = Parity.None;
            shiftPortSend.StopBits = StopBits.One;
            shiftPortSend.Open();

            //triggerEvent += new TriggerHandle(Program_triggerEvent);

            foreach (Order order in orders)
            {
                //发送切户信号
                shiftPortSend.WriteLine("C2");
                Thread.Sleep(500);
                //将订单行项目随机排序
                OrderLine[] randomLines = order.OrderLines.OrderBy(l => Guid.NewGuid()).ToArray();
                foreach (OrderLine orderline in randomLines)
                {                   
                    string barcode = orderline.BrandId;
                    for (int i = 0; i < orderline.Count; i++)
                    {
                        string qrcode = string.Format("http://scdzyc.cn/q/{0}", new string(constant.OrderBy(c => Guid.NewGuid()).Take(7).ToArray()));
                        Console.WriteLine(qrcode);
                        qrPortSend.Write(qrcode + ",");
                        Console.WriteLine(barcode);
                        barPortSend.WriteLine(barcode);                       
                        Thread.Sleep(100);
                    }                    
                }                
            }
            swBar.Close();
            swQR.Close();
            /*
            SendShift();
            for (int i = 0; i < 10000000; i++)
            {
                triggerEvent(i);
            }
            */
            /*
           Thread shiftThread = new Thread(new ThreadStart(SendShift));
           shiftThread.Start();

           Thread.Sleep(3000);
           
           Thread qrThread = new Thread(new ThreadStart(SendQR));
           qrThread.Start();

           Thread.Sleep(100);

           Thread barThread = new Thread(SendBar);
           barThread.Start();*/
        }

        static void Program_triggerEvent(int i)
        {
            string code = string.Format("QRCode-{0:D16}", i);
            Console.WriteLine(code);            
            qrPortSend.WriteLine(code);            
            code = string.Format("BarCode-{0:D16}", i);
            Console.WriteLine(code);            
            barPortSend.WriteLine(code);
            Thread.Sleep(100);
            i++;
        }

        private static void SendShift()
        {
            while (true)
            {
                shiftPortSend.WriteLine("Shift");
                //Console.WriteLine("Shift");
                Thread.Sleep(3000);
            }
        }

        private static void SendQR()
        {
            int i = 0;
            while (true)
            {
                string code = string.Format("QRCode-{0:D16}", i++);
                Console.WriteLine(code);
                //swQR.WriteLine(code);
                qrPortSend.WriteLine(code);
                Thread.Sleep(100);
            }
        }

        private static void SendBar()
        {
            int i = 0;
            while (true)
            {
                string code = string.Format("BarCode-{0:D16}", i++);
                Console.WriteLine(code);
                //swBar.WriteLine(code);
                barPortSend.WriteLine(code);
                Thread.Sleep(100);
            }
        }
    }
}
