using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor.Model;

namespace CodeRecover
{
    class Program
    {
        static ScanCodeEntities repo = new ScanCodeEntities();

        static void Main(string[] args)
        {

            List<OrderLine> orderLines = new List<OrderLine>();
            StreamReader sr = new StreamReader(@"C:\Users\lei\Desktop\Work\RetailerOrder20150509105427.Order");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] temp = line.Split(',');
                OrderLine ol = new OrderLine() { OrderNumber = temp[1], RetailerId = temp[2], Retailer = temp[3], BrandId = temp[4], Brand = temp[5], Count = int.Parse(temp[6]) };
                orderLines.Add(ol);
            }
            sr.Close();
            //一天分解的条烟总数量
            int totalCount = orderLines.Sum(ol => ol.Count);
            //订单列表
            IEnumerable<Order> orders = from ol in orderLines group ol by ol.OrderNumber into g select new Order() { Number = g.Key, OrderLines = g.ToList() };
            List<Carton> cartons = new List<Carton>();
            
            //从数据库中读取暂存的条码
            //List<QRCode> qrCodes = repo.QRCodes.ToList();
            //List<BarCode> barCodes = repo.BarCodes.ToList();

            //从暂存文本中读取
            List<QRCode> qrCodes = new List<QRCode>();
            List<BarCode> barCodes = new List<BarCode>();
            sr = new StreamReader(@"qrcode.txt");
            while ((line = sr.ReadLine()) != null)
            {
                QRCode qrCode = new QRCode() { Code = line.Split('\t')[1] };
                qrCodes.Add(qrCode);
            }
            sr.Close();
            sr = new StreamReader(@"barcode.txt");
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
            }
            int index = 0;
            var barcodeOrders = barCodes.GroupBy(bc => bc.OrderNumber); //扫描到的条码按订单号分组            

            foreach (var order in orders)//以.Order文件中读取的订单信息为基准依次比对
            {
                var barcodeOrder = barcodeOrders.First(g => g.Key == order.Number);
                if (barcodeOrder == null)
                {
                    //没有找到，说明整个订单内的所有条烟都没有触发
                }
                else
                {
                    //验证一个订单中扫描后的条烟数量
                    int cartonCount = barcodeOrder.Count();
                    if (order.OrderLines.Sum(l => l.Count) == cartonCount)//全部触发
                    {                        
                        var naBarcodes = barcodeOrder.Where(bc => bc.Code == "NA");
                        if (naBarcodes.Count() != 0)
                        {
                            //处理NA条码
                            var brands = barcodeOrder.Where(b => b.Code != "NA").GroupBy(bc => bc.Code);//扫描的品牌集合
                            if (order.OrderLines.Count == brands.Count())//每个品牌至少有一条烟被扫到
                            {
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
                            }
                            else
                            {
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
                        
                    }
                }
                repo.SaveChanges();
            }
        }
    }
}