using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Microsoft.Toolkit.Uwp.Helpers;
using ColorHelper = Microsoft.Toolkit.Uwp.Helpers.ColorHelper;

namespace ColorfulBox
{
    public class HslConverter : IColorConverter
    {
        public HslModel Model { get; set; }

        public Color ToColor(Color color, double value)
        {
          var hsl=  color.ToHslEx();
            switch (Model)
            {
                case HslModel.Hue:
                    return ColorExtensions.FromHslEx(value, hsl.S, hsl.L);
                case HslModel.Saturation:
                    return ColorExtensions.FromHslEx(hsl.H, value, hsl.L);
                case HslModel.Lightness:
                    return ColorExtensions.FromHslEx(hsl.H, hsl.S, value);
            }

            return Color.FromArgb(0, 0, 0, 0);
        }
    }
}
