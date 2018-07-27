using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ConsoleAccessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string mdbFile = @"G:\Projects\Visual Studio Project\Phillip\AccessProject\mdb\Logger.mdb";           //默認路徑  

            MDBHelp mdbHelp = new MDBHelp(mdbFile);
            DataTable dt = null;
            try
            {
                mdbHelp.Open();     // 打開數據庫  

                dt = mdbHelp.GetDataTable("select* from events where datatime > '2005/08/23 08:47:46.734'");

                for (int i = 0; i < dt.Rows.Count; i ++) {
                    foreach (DataColumn item in dt.Columns)
                    {
                        Console.Write(item.ColumnName+":"+ dt.Rows[i][item].ToString()+"\t");
                    }
                    Console.WriteLine();
                }

                mdbHelp.Close();    // 關閉數據庫  
            }
            finally
            {
                mdbHelp = null;
            }
            Console.ReadKey();
        }
    }
}
