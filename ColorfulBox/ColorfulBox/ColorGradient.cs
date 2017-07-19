using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ColorfulBox
{
    public class ColorGradient : Slider
    {
        public ColorGradient()
        {
            DefaultStyleKey = typeof(ColorGradient);
        }

        /// <summary>
        /// 获取或设置Color的值
        /// </summary>  
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// 标识 Color 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorGradient), new PropertyMetadata(Color.FromArgb(0, 0, 0, 0), OnColorChanged));

        private static void OnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorGradient target = obj as ColorGradient;
            Color oldValue = (Color)args.OldValue;
            Color newValue = (Color)args.NewValue;
            if (oldValue != newValue)
                target.OnColorChanged(oldValue, newValue);
        }


        /// <summary>
        /// 获取或设置ColorConverter的值
        /// </summary>  
        public IColorConverter ColorConverter
        {
            get { return (IColorConverter)GetValue(ColorConverterProperty); }
            set { SetValue(ColorConverterProperty, value); }
        }

        /// <summary>
        /// 标识 ColorConverter 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorConverterProperty =
            DependencyProperty.Register("ColorConverter", typeof(IColorConverter), typeof(ColorGradient), new PropertyMetadata(null, OnColorConverterChanged));

        private static void OnColorConverterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorGradient target = obj as ColorGradient;
            IColorConverter oldValue = (IColorConverter)args.OldValue;
            IColorConverter newValue = (IColorConverter)args.NewValue;
            if (oldValue != newValue)
                target.OnColorConverterChanged(oldValue, newValue);
        }

        /// <summary>
        ///     获取或设置MaximumColor的值
        /// </summary>
        public Color MaximumColor
        {
            get { return (Color)GetValue(MaximumColorProperty); }
            set { SetValue(MaximumColorProperty, value); }
        }

        /// <summary>
        ///     标识 MaximumColor 依赖属性。
        /// </summary>
        public static readonly DependencyProperty MaximumColorProperty =
            DependencyProperty.Register("MaximumColor", typeof(Color), typeof(ColorGradient), new PropertyMetadata(default(Color)));

        /// <summary>
        ///     获取或设置MinimumColor的值
        /// </summary>
        public Color MinimumColor
        {
            get { return (Color)GetValue(MinimumColorProperty); }
            set { SetValue(MinimumColorProperty, value); }
        }

        /// <summary>
        ///     标识 MinimumColor 依赖属性。
        /// </summary>
        public static readonly DependencyProperty MinimumColorProperty =
            DependencyProperty.Register("MinimumColor", typeof(Color), typeof(ColorGradient), new PropertyMetadata(default(Color)));


        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {
            UpdateMaximumAndMinimumColor();
        }

        protected virtual void OnColorConverterChanged(IColorConverter oldValue, IColorConverter newValue)
        {
            UpdateMaximumAndMinimumColor();
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            UpdateMaximumAndMinimumColor();
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            UpdateMaximumAndMinimumColor();
        }


        private void UpdateMaximumAndMinimumColor()
        {
            if (ColorConverter == null)
                return;

            MinimumColor = ColorConverter.ToColor(Color, Minimum);
            MaximumColor = ColorConverter.ToColor(Color, Maximum);
        }
    }
}
