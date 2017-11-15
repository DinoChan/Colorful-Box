using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ColorfulBox
{
  public static class ColorExtensions
    {
        public static HsvColor ToHsvEx(this Color color)
        {
           var hsv= color.ToHsv();
            hsv.H = Math.Round(hsv.H);
            hsv.S = Math.Round(hsv.S, 2);
            hsv.V = Math.Round(hsv.V, 2);
            return hsv;
        }


        public static Color FromHsvEx(double hue, double saturation, double value, double alpha = 1)
        {
           return Microsoft.Toolkit.Uwp.Helpers.ColorHelper.FromHsv(Math.Round(hue), Math.Round(saturation, 2), Math.Round(value, 2), alpha);
        }

        public static HslColor ToHslEx(this Color color)
        {
            var hsl = color.ToHsl();
            hsl.H = Math.Round(hsl.H);
            hsl.S = Math.Round(hsl.S, 2);
            hsl.L = Math.Round(hsl.L, 2);
            return hsl;
        }


        public static Color FromHslEx(double hue, double saturation, double lightness, double alpha = 1)
        {
            return Microsoft.Toolkit.Uwp.Helpers.ColorHelper.FromHsl(Math.Round(hue), Math.Round(saturation, 2), Math.Round(lightness, 2), alpha);
        }
    }
}
