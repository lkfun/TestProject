using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace lkfunWebService.Controllers
{
    public class Md5
    {
        private string[] words = { "!", "\"", "#", "$", "%", "&", "'", "(", ")", "+", ",", "-", ".", "/", ":", ";", "<", "=", ">", "?", "@", "[", "\\", "]", "^", "_", "`", "{", "|", "}", "~" };
        public string Md5Str { get; set; }
        public string inText { get; set; }
        public string type { get; set; }
        public Md5(string inText) {
            this.inText = inText;
            this.Md5Str=this.Generatemd5(inText);
        }
        public Md5(string inText, string type)
        {
            this.inText = inText;
            this.type = type;
            this.Md5Str = this.Generatemd5(inText);
        }
        private string Generatemd5(string inText)
        {
            string md5Str = "";
            StringBuilder sb = new StringBuilder();
            md5Str = Md5Encrypt(inText);
            for (int i = 0; i < md5Str.Length; i += 16)
            {
                int length = i >= md5Str.Length ? md5Str.Length - i : 16;
                string str = "0x" + md5Str.Substring(i, length);
                ulong hex = Convert.ToUInt64(Convert.ToUInt64(str, 16));
                if (type==null || (type != "1"&& type != "2"))
                {
                    //大小写混合
                    sb.Append(HexTo62(hex));
                }
                else if (type=="1")
                {
                    //小写
                    sb.Append(HexTo36(hex).ToLower());
                }
                else if (type == "2")
                {
                    //大写
                    sb.Append(HexTo36(hex));
                }
            }
            return sb.ToString();
        }
        private string HexTo62(ulong hex)
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
        private string HexTo26(ulong hex)
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
    }
}
