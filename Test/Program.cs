using System;
using System.Collections.Generic;
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
            IPAddress ip = IPAddress.Parse("10.89.245.30");
            IPEndPoint ipep = new IPEndPoint(ip, 8089);  
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipep);
            socket.Listen(10);

           
            Socket client = socket.Accept();


            //获得[文件名]  
            string SendFileName = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));
            //MessageBox.Show("文件名" + SendFileName);  

            //获得[包的大小]  
            string bagSize = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));
            //MessageBox.Show("包大小" + bagSize);  

            //获得[包的总数量]  
            int bagCount = int.Parse(System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client)));
            //MessageBox.Show("包的总数量" + bagCount);  

            //获得[最后一个包的大小]  
            string bagLast = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));  


            int file_name = 1;

            string fileaddr = "d:\\" + file_name.ToString() + ".zip";

            FileStream MyFileStream = new FileStream(fileaddr, FileMode.Create, FileAccess.Write);

            //        int SendedCount = 0;  

            while (true)
            {
                byte[] data = TransferFiles.ReceiveVarData(client);
                if (data.Length == 0)
                {
                    break;
                }
                else
                {
                    // SendedCount++;  
                    MyFileStream.Write(data, 0, data.Length);
                }
            }

            MyFileStream.Close();

            client.Close();  
            
            string sendMessage = "client send Message Hellp" + DateTime.Now;
            socket.Send(Encoding.ASCII.GetBytes(sendMessage));  

            string[] portNames = SerialPort.GetPortNames();
            SerialPort port2 = new SerialPort("COM4");
            port2.BaudRate = 57600;
            port2.DataBits = 8;
            port2.Parity = Parity.None;
            port2.StopBits = StopBits.One;
            //port2.ReadTimeout = 100;
            port2.Open();
            
            string t0 = port2.ReadTo(",");
            string t1 = port2.ReadTo(",");
            string t2 = port2.ReadTo(",");

            string p2 = port2.ReadExisting();
            string p1 = port2.ReadLine();

            List<SerialPort> ports = new List<SerialPort>();
            foreach (string portName in portNames)
            {
                SerialPort port = new SerialPort(portName);
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();
                ports.Add(port);
            }

            string t = ports[1].ReadExisting();
            foreach (SerialPort port in ports)
            {

            }
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
