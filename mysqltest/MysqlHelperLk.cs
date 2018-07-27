using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lk
{
    class MysqlHelperLk
    {
        string M_str_sqlcon = "server=192.168.0.65;user id=lk;password=zxcxzasdsa;database=mysql;Charset=utf8;port=3306"; //根据自己的设置
        public MysqlHelperLk()
        {
        }
        public MysqlHelperLk(string M_str_sqlcon)
        {
            this.M_str_sqlcon = M_str_sqlcon;
        }
        public DataTable GetData(string sql)
        {
            DataTable data = new DataTable();
            StringBuilder sb = new StringBuilder();
            MySqlConnection mysqlcon = new MySqlConnection(M_str_sqlcon);
            sb.Append(sql);
            MySqlDataAdapter da = new MySqlDataAdapter(sb.ToString(), mysqlcon);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
            da.Fill(data);
            return data;
        }
    }
}
