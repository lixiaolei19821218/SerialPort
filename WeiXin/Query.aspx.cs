using DB2DataAccess;
using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Query : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["qrcode"] == null)
        {

        }
        else
        {
            DB2 weixinDB = new DB2(ConfigurationManager.ConnectionStrings["weixin"].ConnectionString);

            string qrcode = Page.RouteData.Values["qrcode"].ToString();
            string sql = string.Format("select * from qrcodes where code = '{0}'", qrcode);
            DataRow qrRow = weixinDB.GetDataRow(sql);
            if (qrRow != null)
            {
                //查询V3
                DB2 v3DB = new DB2(ConfigurationManager.ConnectionStrings["v3"].ConnectionString);
                string orderNumber = qrRow["orderNumber"].ToString();
                sql = string.Format("select * from db2inst2.sd_co where CO_NUM = '{0}'", orderNumber);
                DataRow orderRow = v3DB.GetDataRow(sql);               
                string custId = orderRow["CUST_ID"].ToString();
                sql = string.Format("select * from db2inst2.rm_cust where CUST_ID = '{0}'", custId);
                DataRow custRow = v3DB.GetDataRow(sql);
                custCode.InnerText = custRow["CUST_CODE"].ToString();
                custName.InnerText = custRow["CUST_NAME"].ToString();
                busiAddr.InnerText = custRow["BUSI_ADDR"].ToString();

                //记录查询
                sql = string.Format("INSERT INTO \"DB2ADMIN\".\"QUERIES\"(\"IPADDRESS\", \"QUERYTIME\", \"QRCODEID\") VALUES('{0}', '{1}', '{2}')", Request.UserHostAddress, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), qrRow["Id"].ToString());
                weixinDB.Insert(sql);
                DataRow countRow = weixinDB.GetDataRow("select count(*) from \"DB2ADMIN\".\"QUERIES\"");
                scanTimes.InnerText = countRow[0].ToString();               

                //查询订单号相同的二维码并查找其在订单中的位置
                sql = string.Format("select * from qrcodes where orderNumber = '{0}'", orderNumber);
                DataTable qrTable = weixinDB.GetDataTable(sql);                
                int index = -1;
                for (int i = 0; i < qrTable.Rows.Count; i++)
                {
                    if (qrTable.Rows[i]["code"].ToString() == qrcode)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                {
                    sql = string.Format("select * from barcodes where orderNumber = '{0}'", orderNumber);
                    DataTable barTable = weixinDB.GetDataTable(sql);
                    if (barTable.Rows.Count > index)
                    {
                        DataRow barRow = barTable.Rows[index];
                        string barcode = barRow["code"].ToString();
                        sql = string.Format("select * from db2inst2.sd_item where stand_bar_code = '{0}'", barcode);
                        DataRow itemRow = v3DB.GetDataRow(sql);
                        if (itemRow != null)
                        {
                            sdItem.InnerText = itemRow["stand_bar_name"].ToString();
                        }
                    }
                }

                v3DB.Close();
            }

            weixinDB.Close();

            /*
            string connectionString = ConfigurationManager.ConnectionStrings["db2"].ConnectionString;
            DB2Connection cn = new DB2Connection(connectionString);
            cn.Open();
            string qrcode = Page.RouteData.Values["qrcode"].ToString();
            string sql = string.Format("select * from qrcodes where code = '{0}'", qrcode);            
            DB2Command cmd = new DB2Command(sql, cn);            
            DB2DataAdapter adp = new DB2DataAdapter(cmd);
            DataSet qrSet = new DataSet();
            adp.Fill(qrSet);
            
            if (qrSet.Tables[0].Rows.Count != 0)
            {
                DataRow qrRow = qrSet.Tables[0].Rows[0];
                string orderNumber = qrRow["orderNumber"].ToString();
                sql = string.Format("select * from db2inst2.sd_co where CO_NUM = '{0}'", orderNumber);


                sql = string.Format("select * from qrcodes where orderNumber = {0}", orderNumber);
                cmd = new DB2Command(sql, cn);
                adp = new DB2DataAdapter(cmd);
                DataSet qrsSet = new DataSet();
                adp.Fill(qrsSet);
                if (qrsSet.Tables[0].Rows.Count != 0)
                {
                    int index = qrsSet.Tables[0].Rows.IndexOf(qrRow);
                    sql = string.Format("select * from barcode where orderNumber = {0}", orderNumber);
                    cmd = new DB2Command(sql, cn);
                    adp = new DB2DataAdapter(cmd);
                    DataSet barSet = new DataSet();
                    adp.Fill(barSet);
                    if (barSet.Tables.Count > 0 && barSet.Tables[0].Rows.Count > index)
                    {
                        DataRow barRow = barSet.Tables[0].Rows[index];
                    }
                }
            }
            qrSet.Dispose();
            cn.Close();
             * */
        }
    }
}