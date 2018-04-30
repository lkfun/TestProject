using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using System.Text;
using System.IO;

namespace WindowsServiceTest
{
    public partial class Service1 : ServiceBase
    {
        private static int numTimes=0;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        private void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.timer1.Stop();
            numTimes++;
            string filePath = @"D:/FavoriteVideo";
            string message = "";
            message=DelectDir(filePath);
            string filePath2 = @"D:\lk.log";
            string strCont = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "第" + numTimes + "次执行。 "+ message + "\n\r";
            System.IO.File.AppendAllText(filePath2, strCont);
            this.timer1.Start();
        }

        private static string DelectDir(string srcPath)
        {
            string message = "成功";
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                message=e.Message;
            }
            return message;
        }
    }
}
