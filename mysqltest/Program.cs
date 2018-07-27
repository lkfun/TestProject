using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace lk
{
    class Program
    {
        static void Main(string[] args)
        {
            MysqlHelperLk hp = new MysqlHelperLk();

            DataTable dt = hp.GetData("select * from user");
            IList<User> u = DataTableToObject.ConvertTo<User>(dt);
            string json = JsonConvert.SerializeObject(u);
            Console.WriteLine(json);
            Console.ReadKey();
        }
    }
    class User
    {
        public string host { get; set; }
        public string user { get; set; }
        public string plugin { get; set; }
    }
}
