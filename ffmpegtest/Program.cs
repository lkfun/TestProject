using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ffmpegtest
{
    class Program
    {
        private readonly string tempmusic = "tempmusic.mp3";
        private readonly string tempvideo = "tempvideo.mp4";
        static void Main(string[] args)
        {
            Program a = new Program();
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + a.tempmusic);
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + a.tempvideo);
            Console.WriteLine("请输入视频文件夹地址");
            string videopath = Console.ReadLine();
            Console.WriteLine("请输入音频文件夹地址");
            string musicpath = Console.ReadLine();
            Console.WriteLine("请输入输出文件夹地址");
            string outputpath = Console.ReadLine();
            DirectoryInfo di = new DirectoryInfo(videopath);
            DirectoryInfo dimusic = new DirectoryInfo(musicpath);
            FileInfo[] fis = di.GetFiles();
            FileInfo[] fismusic = dimusic.GetFiles();
            string videoname;
            string musicname;
            int countmusic = fismusic.Length;
            for (int i = 0; i < fis.Length; i++)
            {
                videoname= "\"" + fis[i].FullName+"\"";
                musicname= "\"" + fismusic[i% countmusic].FullName + "\"";
                a.combineVideo(videoname, musicname, fis[i].Name,outputpath);
            }
            Console.ReadKey();
        }
        public void ConvertVideo(string Arguments)
        {
            Process p = new Process();//建立外部调用线程
            p.StartInfo.FileName = System.AppDomain.CurrentDomain.BaseDirectory+@"ffmpeg.exe";//要调用外部程序的绝对路径
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
                Console.WriteLine(output.Data);
            }
        }

        private void combineVideo(string videoname, string musicname, string outputname, string outputpath) {
            Program a = new Program();
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + outputname);
            a.ConvertVideo("-i " + videoname + " -i " + musicname + " -filter_complex amix=inputs=2:duration=first:dropout_transition=0 -vol 80 " + tempmusic);
            a.ConvertVideo("-i " + videoname + " -vcodec copy -an " + tempvideo);
            a.ConvertVideo(@"-i .\" + tempvideo + @" -i .\" + tempmusic + " -acodec copy " + "\""+outputpath+"/"+ outputname+"\"");
            Console.WriteLine("==========done=========");
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + tempmusic);
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + tempvideo);
        }
    }
}
