using DB2DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync32Code
{
    class Program
    {
        static void Main(string[] args)
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
    }
}
