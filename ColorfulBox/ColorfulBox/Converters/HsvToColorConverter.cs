using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Microsoft.Toolkit.Uwp;

namespace ColorfulBox
{
    public class HsvToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is HsvColor hsv)
                return hsv.ToArgbColor();

            return default(Color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Color color)
                return color.ToHsvEx();

            return default(HsvColor);
        }
    }
}
