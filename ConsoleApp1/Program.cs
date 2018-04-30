using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            Console.WriteLine(Convert.ToInt64(ts.TotalSeconds));
            Console.WriteLine("__________");
            string b = "0x65BA7E4552434E90";
            Decimal a = Convert.ToDecimal(Convert.ToUInt64(b,16));
            Console.WriteLine(a);
            Console.ReadKey();
        }

        static private string hexTo36(uint hex)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            uint mod = 0;
            while (hex>36 || i==0)
            {
                mod = hex % 36;
                if (mod < 10)
                {
                    sb.Append(mod);
                }
                else {
                    sb.Append(Convert.ToChar(mod + 55));
                }
                i++;
                hex = hex / 36;
            }
            char[] c = sb.ToString().ToArray();
            Array.Reverse(c);
            return new string(c);
        }
    }
}
