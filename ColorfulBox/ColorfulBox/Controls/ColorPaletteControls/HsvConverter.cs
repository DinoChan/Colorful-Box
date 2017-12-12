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
    public class HsvConverter : IColorConverter
    {
        public HsvModel Model { get; set; }

        public Color ToColor(Color color, double value)
        {
          var hsv=  color.ToHsvEx();
            switch (Model)
            {
                case HsvModel.Hue:
                    return ColorExtensions.FromHsvEx(value, hsv.S, hsv.V);
                case HsvModel.Saturation:
                    return ColorExtensions.FromHsvEx(hsv.H, value, hsv.V);
                case HsvModel.Value:
                    return ColorExtensions.FromHsvEx(hsv.H, hsv.S, value);
            }

            return Color.FromArgb(0, 0, 0, 0);
        }
    }
}
