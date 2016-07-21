using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2DataAccess
{
    public class DB2
    {
        private DB2Connection connection;

        public DB2(string connectionString)
        {            
            connection = new DB2Connection(connectionString);
            connection.Open();
        }

        public void Close()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        public DataTable GetDataTable(string sql)
        {            
            DB2Command cmd = new DB2Command(sql, connection);
            DB2DataAdapter adapter = new DB2DataAdapter(cmd);
            DataSet set = new DataSet();
            adapter.Fill(set);
            return set.Tables[0];
        }

        public DataRow GetDataRow(string sql)
        {
            DataTable table = GetDataTable(sql);
            DataRow row;
            if (table.Rows.Count != 0)
            {
                row = table.Rows[0];
            }
            else
            {
                row = null;
            }
            return row;
        }

        public void Insert(string sql)
        {
            DB2Command cmd = new DB2Command(sql, connection);
            cmd.ExecuteNonQuery();
        }

        public void Update(string sql)
        {
            DB2Command cmd = new DB2Command(sql, connection);
            cmd.ExecuteNonQuery();
        }
    }
}
