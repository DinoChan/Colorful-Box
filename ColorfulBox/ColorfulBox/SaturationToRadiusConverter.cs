using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ColorfulBox
{
    public class SaturationToRadiusConverter : DependencyObject, IValueConverter
    {



        /// <summary>
        /// 获取或设置Radius的值
        /// </summary>  
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        /// <summary>
        /// 标识 Radius 依赖属性。
        /// </summary>
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(SaturationToRadiusConverter), new PropertyMetadata(0d, OnRadiusChanged));

        private static void OnRadiusChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SaturationToRadiusConverter target = obj as SaturationToRadiusConverter;
            double oldValue = (double)args.OldValue;
            double newValue = (double)args.NewValue;
            if (oldValue != newValue)
                target.OnRadiusChanged(oldValue, newValue);
        }

        protected virtual void OnRadiusChanged(double oldValue, double newValue)
        {
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (double)value * this.Radius / 100d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

     
    }
}
