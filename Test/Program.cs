using Monitor.Model;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<OrderLine> orderLines = new List<OrderLine>();
            StreamReader sr = new StreamReader("QrCode20160721141445.Order");
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
            List<Order> orders = t.ToList();
            StreamWriter sw = new StreamWriter("result.txt");
            foreach (Order order in orders.OrderBy(o => o.Number))
            {
                sw.WriteLine(string.Format("{0}, {1}", order.Number, order.TotalCount));
            }
            sw.Close();
            

            
        }
    }

    class TransferFiles
    {

        public TransferFiles()
        {

        }

        public static int SendVarData(Socket s, byte[] data) // return integer indicate how many data sent.  
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;
            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = s.Send(datasize);//send the size of data array.  

            while (total < size)
            {
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }

            return total;
        }

        public static byte[] ReceiveVarData(Socket s) // return array that store the received data.  
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            recv = s.Receive(datasize, 0, 4, SocketFlags.None);//receive the size of data array for initialize a array.  
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];

            while (total < size)
            {
                recv = s.Receive(data, total, dataleft, SocketFlags.None);
                if (recv == 0)
                {
                    data = null;
                    break;
                }
                total += recv;
                dataleft -= recv;
            }

            return data;

        }
    }  

}
