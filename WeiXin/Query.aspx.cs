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
                string qrSequence = qrRow["sequence"].ToString();
                string qrNationCustCode = qrRow["NationCustCode"].ToString();
                string qrId  = qrRow["Id"].ToString();

                sql = string.Format("select * from db2inst2.sd_co where CO_NUM = '{0}'", orderNumber);
                DataRow orderRow = v3DB.GetDataRow(sql);
                soldDate.InnerText = orderRow["BORN_DATE"].ToString();
                string custId = orderRow["cust_id"].ToString();
                sql = string.Format("select * from db2inst2.rm_cust where cust_id = '{0}'", custId);
                DataRow custRow;
                custRow = v3DB.GetDataRow(sql);                
                custCode.InnerText = custRow["NATION_CUST_CODE"].ToString().Trim();
                custName.InnerText = custRow["CUST_NAME"].ToString().Trim();
                busiAddr.InnerText = custRow["BUSI_ADDR"].ToString().Trim();

                //查询次数
                sql = string.Format("INSERT INTO \"DB2ADMIN\".\"QUERIES\"(\"IPADDRESS\", \"QUERYTIME\", \"QRCODEID\") VALUES('{0}', '{1}', '{2}')", Request.UserHostAddress, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), qrId);
                weixinDB.Insert(sql);
                //sql = string.Format("select count(*) from \"DB2ADMIN\".\"QUERIES\" where \"QRCODEID\" = {0}", qrId);
                //DataRow countRow = weixinDB.GetDataRow(sql);
                int count = int.Parse(qrRow["QUERYCOUNT"].ToString());
                sql = string.Format("UPDATE \"DB2ADMIN\".\"QRCODES\" SET \"QUERYCOUNT\" = {0} WHERE \"DB2ADMIN\".\"QRCODES\".\"ID\" = {1};", ++count, qrId);
                weixinDB.Update(sql);
                scanTimes.InnerText = count.ToString();

                //查找序号和订单号相同的barcode
                if (!string.IsNullOrWhiteSpace(qrSequence))
                {
                    sql = string.Format("select * from barcodes where orderNumber = '{0}' and sequence = {1}", orderNumber, qrSequence);
                    DataRow barRow = weixinDB.GetDataRow(sql);
                    if (barRow != null)
                    {
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

                //查找序号和客户编码相同的32位码
                if (!string.IsNullOrWhiteSpace(qrNationCustCode) && !string.IsNullOrWhiteSpace(qrSequence))
                {
                    sql = string.Format("select * from codes32 where \"NationCustCode\" = '{0}' and sequence = {1}", qrNationCustCode, int.Parse(qrSequence) + 1);
                    DataRow code32Row = weixinDB.GetDataRow(sql);
                    if (code32Row != null)
                    {
                        string code = code32Row["code"].ToString();
                        if (code.Length == 32)
                        {
                            code32.InnerHtml = string.Format("{0}&nbsp;{1}&nbsp;{2}&nbsp;{3}&nbsp;<br/>{4}&nbsp;{5}&nbsp;{6}&nbsp;{7}&nbsp;",
                                code.Substring(0, 4),
                                code.Substring(4, 4),
                                code.Substring(8, 4),
                                code.Substring(12, 4),
                                code.Substring(16, 4),
                                code.Substring(20, 4),
                                code.Substring(24, 4),
                                code.Substring(28, 4));
                        }
                    }
                }
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