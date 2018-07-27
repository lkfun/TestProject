using System;
using System.Diagnostics;
using System.IO;

namespace ffmpegtest
{
    class Program
    {
        private readonly string tempmusic = "tempmusic.mp3";
        private readonly string tempmusic2 = "tempmusic2.mp3";
        private readonly string tempmusic3 = "tempmusic3.mp3";
        private readonly string tempvideo = "tempvideo.mp4";
        private readonly string tempvideo2 = "tempvideo2.mp4";
        string videoname;
        string musicname;
        string videopath;
        string musicpath;
        string outputpath;
        string flag;
        int db=0;
        string outputname;
        static void Main(string[] args)
        {
            try
            {
                Program a = new Program();
                a.deleteTempFile(a);
                Console.WriteLine("请输入视频文件夹地址");
                a.videopath = Console.ReadLine();
                Console.WriteLine("请输入音频文件夹地址");
                a.musicpath = Console.ReadLine();
                Console.WriteLine("请输入输出文件夹地址");
                a.outputpath = Console.ReadLine();
                a.flag = "";
                string dbstr = "";
                while (a.flag.ToLower() != "y" && a.flag.ToLower() != "n")
                {
                    Console.WriteLine("是否保留视频原声？（Y或N）");
                    a.flag = Console.ReadLine();
                } 
                do
                {
                    Console.WriteLine("请输入缩放音量值：建议值:[-20,20]");
                    dbstr = Console.ReadLine();
                } while (int.TryParse(dbstr, out a.db) == false&& dbstr!="");
                DirectoryInfo di = new DirectoryInfo(a.videopath);
                DirectoryInfo dimusic = new DirectoryInfo(a.musicpath);
                FileInfo[] fis = di.GetFiles();
                FileInfo[] fismusic = dimusic.GetFiles();
                int countmusic = fismusic.Length;

                #region 控制台操作
                Console.Clear();
                ConsoleColor colorBack = Console.BackgroundColor;
                ConsoleColor colorFore = Console.ForegroundColor;
                //(0,0)(Left,Top) 第一行
                Console.WriteLine("***********开始输出*************");
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                for (int i = 0; i < Console.WindowWidth - 3; i++)
                {
                    //(0,1) 第二行
                    Console.Write(" ");
                }
                Console.WriteLine(" ");
                Console.BackgroundColor = colorBack;
                #endregion

                for (int i = 0; i < fis.Length; i++)
                {

                    a.videoname = "\"" + fis[i].FullName + "\"";
                    a.musicname = "\"" + fismusic[i % countmusic].FullName + "\"";
                    a.outputname = fis[i].Name;
                    a.combineVideo(a);
                    a.deleteTempFile(a);
                    #region 控制台操作
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(i * (Console.WindowWidth - 2) / fis.Length, 1);
                    Console.Write(new string(' ', (Console.WindowWidth - 2) / fis.Length + 1));
                    Console.BackgroundColor = colorBack;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(0, 2);
                    Console.WriteLine((1 + i) * 100 / fis.Length + "%");
                    #endregion
                }
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("完成！");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            Console.ReadKey();
        }
        public void ConvertVideo(string Arguments)
        {
            Process p = new Process();//建立外部调用线程
            p.StartInfo.FileName = System.AppDomain.CurrentDomain.BaseDirectory + @"ffmpeg.exe";//要调用外部程序的绝对路径
            p.StartInfo.Arguments = Arguments;//参数(这里就是FFMPEG的参数了)
            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的...这是我耗费了2个多月得出来的经验...mencoder就是用standardOutput来捕获的)
            p.StartInfo.CreateNoWindow = false;//不创建进程窗口
            p.ErrorDataReceived += new DataReceivedEventHandler(Output);//外部程序(这里是FFMPEG)输出流时候产生的事件,这里是把流的处理过程转移到下面的方法中,详细请查阅MSDN
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程
            p.Dispose();//释放资源
        }
        private void Output(object sendProcess, DataReceivedEventArgs output)
        {
            if (!String.IsNullOrEmpty(output.Data))
            {
                //Console.WriteLine(output.Data);
            }
        }

        private void combineVideo(Program a)
        {
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + outputname);
            if (a.flag == "y")
            {
                a.ConvertVideo("-i " + a.videoname + " -i " + a.musicname + " -filter_complex amix=inputs=2:duration=first:dropout_transition=0  " + a.tempmusic);
            }
            else
            {
                a.ConvertVideo("-i " + a.videoname + " -vol 0 " + a.tempmusic2);
                a.ConvertVideo("-i " + a.tempmusic2 + " -i " + a.musicname + " -filter_complex amix=duration=shortest  " + a.tempmusic);
            }
            //Console.WriteLine("音量增减："+a.db);
            a.ConvertVideo("-i " + a.tempmusic + " -vcodec copy -af \"volume = "+a.db+"dB\" " + a.tempmusic3);
            a.ConvertVideo("-i " + a.videoname + " -vcodec copy -an " + a.tempvideo);
            a.ConvertVideo(@"-i .\" + a.tempvideo + @" -i .\" + a.tempmusic3 + " -acodec copy " + "\"" + a.outputpath + "/" + a.outputname + "\"");
            //Console.WriteLine("==========done=========");
        }
        private void deleteTempFile(Program a) {

            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + a.tempmusic);
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + a.tempmusic2);
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + a.tempmusic3);
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + a.tempvideo);
        }
    }
}
