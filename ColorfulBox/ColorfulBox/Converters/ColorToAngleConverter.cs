using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace ColorfulBox
{
    public class ColorToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Color)
            {
                var hsvColor = Microsoft.Toolkit.Uwp.ColorHelper.ToHsv((Color)value);
                return hsvColor.H;
            }
            return 0d;
        }

   
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
