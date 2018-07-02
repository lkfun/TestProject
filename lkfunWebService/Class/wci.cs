using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lkfunWebService.Controllers
{
    public class Wci
    {
        public double RpD { get; set; } = 0;
        public double ZpD { get; set; } = 0;
        public double RpN { get; set; } = 0;
        public double ZpN { get; set; } = 0;
        public double RtpD { get; set; } = 0;
        public double ZtpD { get; set; } = 0;
        public double Rmax { get; set; } = 0;
        public double Zmax { get; set; } = 0;

        public double Getwci() {
            double O = 0.85 * Math.Log(this.RpD + 1) + 0.15 * Math.Log(10 * ZpD + 1);
            double A = 0.85 * Math.Log(this.RpN + 1) + 0.15 * Math.Log(10 * ZpN + 1);
            double H = 0.85 * Math.Log(this.RtpD + 1) + 0.15 * Math.Log(10 * ZtpD + 1);
            double P = 0.85 * Math.Log(this.Rmax + 1) + 0.15 * Math.Log(10 * Zmax + 1);
            double WCI = Math.Pow((0.3 * O + 0.3 * A + 0.3 * H + 0.1 * P), 2) * 10;
            return WCI;
        }
    }
}
