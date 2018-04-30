using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] words = { "!", "\"", "#", "$", "%", "&", "'", "(", ")", "+", ",", "-", ".", "/", ":", ";", "<", "=", ">", "?", "@", "[", "\\", "]", "^", "_", "`", "{", "|", "}", "~" };
        string serviceName = "LkService";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Random rand = new Random();
            string md5Str;
            if ((bool)CheckBox_isRand.IsChecked)
            {
                md5Str = Md5Encrypt(inputTextBox.Text + rand.NextDouble());
            }
            else
            {
                md5Str = Md5Encrypt(inputTextBox.Text);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Str.Length; i += 16)
            {
                int length = i >= md5Str.Length ? md5Str.Length - i : 16;
                string str = "0x" + md5Str.Substring(i, length);
                ulong hex = Convert.ToUInt64(Convert.ToUInt64(str, 16));
                if ((bool)capital_cap.IsChecked)
                {
                    sb.Append(HexTo36(hex));
                }
                else if ((bool)capital_low.IsChecked)
                {
                    sb.Append(HexTo36(hex).ToLower());
                }
                else
                {
                    sb.Append(HexTo62(hex));
                }
            }
            string output = sb.ToString().Substring(0, 16);
            if ((bool)chkSymbol.IsChecked)
            {
                int splitIndex = rand.Next(15);
                output = output.Substring(0, splitIndex) + words[rand.Next(31)] + output.Substring(splitIndex, output.Length - splitIndex - 1);
            }
            outputTextBlock.Text = output;
        }
        static private string HexTo36(ulong hex)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            ulong mod = 0;
            while (hex > 36 || i == 0)
            {
                mod = hex % 36;
                if (mod < 10)
                    sb.Append(Convert.ToInt64(mod));
                else
                    sb.Append(Convert.ToChar(Convert.ToInt64(mod + 55)));
                i++;
                hex = hex / 36;
            }
            char[] c = sb.ToString().ToArray();
            Array.Reverse(c);
            return new string(c);
        }
        static private string HexTo62(ulong hex)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            ulong mod = 0;
            while (hex > 62 || i == 0)
            {
                mod = hex % 62;
                if (mod < 10)
                    sb.Append(Convert.ToInt64(mod));
                else if (mod < 36)
                    sb.Append(Convert.ToChar(Convert.ToInt64(mod + 55)));
                else
                    sb.Append(Convert.ToChar(Convert.ToInt64(mod + 55 + 6)));
                i++;
                hex = hex / 62;
            }
            char[] c = sb.ToString().ToArray();
            Array.Reverse(c);
            return new string(c);
        }
        static private string HexTo26(ulong hex)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            ulong mod = 0;
            while (hex > 26 || i == 0)
            {
                mod = hex % 26;
                sb.Append(Convert.ToChar(Convert.ToInt64(mod + 55)));
                i++;
                hex = hex / 26;
            }
            char[] c = sb.ToString().ToArray();
            Array.Reverse(c);
            return new string(c);
        }
        static private string Md5Encrypt(string inputStr)
        {
            byte[] result = Encoding.Default.GetBytes(inputStr);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToString();
        }

        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = CurrentDirectory + "/Service";
            System.Diagnostics.Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "install.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            lblLog.Text = "安装成功";
            System.Environment.CurrentDirectory = CurrentDirectory;
        }

        private void btnUninstall_Click(object sender, RoutedEventArgs e)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = CurrentDirectory + "/Service";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "uninstall.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            lblLog.Text = "卸载成功";
            System.Environment.CurrentDirectory = CurrentDirectory;
        }

        private void btnCheckStatus_Click(object sender, RoutedEventArgs e)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            lblCheckStatus.Text = serviceController.Status.ToString();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceController serviceController = new ServiceController(serviceName);
                serviceController.Start();
            }
            catch (Exception e2)
            {
            }
            lblStatus.Text = "服务已启动";
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceController serviceController = new ServiceController(serviceName);
            if (serviceController.CanStop)
            {
                serviceController.Stop();
                lblStatus.Text = "服务已停止";
            }
            else
                lblStatus.Text = "服务不能停止";
            }
            catch (Exception e2)
            {
            }
        }

        private void btnPauseContinue_Click(object sender, RoutedEventArgs e)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            if (serviceController.CanPauseAndContinue)
            {
                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    serviceController.Pause();
                    lblStatus.Text = "服务已暂停";
                }
                else if (serviceController.Status == ServiceControllerStatus.Paused)
                {
                    serviceController.Continue();
                    lblStatus.Text = "服务已继续";
                }
                else
                {
                    lblStatus.Text = "服务未处于暂停和启动状态";
                }
            }
            else
                lblStatus.Text = "服务不能暂停";
        }
    }
}
