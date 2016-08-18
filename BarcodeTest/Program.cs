using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarcodeTest
{
    class Program
    {
        private static Queue<BarCode> barcodes = new Queue<BarCode>();
        private static ScanCodeEntities repo = new ScanCodeEntities();
        private static byte[] result = new byte[1024];
        private static Socket clientSocket;
        private static int count;

        static void Main(string[] args)
        {           
            IPAddress ip = IPAddress.Parse(ConfigurationManager.AppSettings["barcodeIP"]);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(ip, 23));

            Thread receiveThread = new Thread(ReceiveBarcode) { IsBackground = true };
            receiveThread.Start();

            Thread saveThread = new Thread(SaveBarcode) { IsBackground = true };
            saveThread.Start();

            Console.ReadLine();
        }

        private static void ReceiveBarcode()
        {
            int length = 15;
            while (true)
            {
                int receiveLength = clientSocket.Receive(result, length, 0);
                string code = Encoding.ASCII.GetString(result, 0, receiveLength).Trim();
                BarCode barcode = new BarCode() { Code = code, OrderNumber = "testOrder", DateTime = DateTime.Now, RevisedCode = "0000000000000", Sequence = -1 };
                Console.WriteLine(string.Format("Code: {0}, Count: {1}", code, ++count));
                barcodes.Enqueue(barcode);
                Thread.Sleep(1);
            }
        }

        private static void SaveBarcode()
        {
            while (true)
            {
                if (barcodes.Count > 0)
                {
                    BarCode barcode = barcodes.Dequeue();
                    repo.BarCodes.Add(barcode);
                    repo.SaveChanges();                    
                }
                Thread.Sleep(1);
            }
        }
    }

    
}
