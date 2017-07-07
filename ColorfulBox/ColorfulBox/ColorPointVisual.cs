using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace ColorfulBox
{
    [TemplatePart(Name = ThumbElementName, Type = typeof(Thumb))]
    public class ColorPointVisual : Control
    {
        private const string ThumbElementName = "ThumbElement";

        /// <summary>
        ///     标识 ColorPoint 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorPointProperty =
            DependencyProperty.Register("ColorPoint", typeof(ColorPoint), typeof(ColorPointVisual), new PropertyMetadata(null, OnColorPointChanged));

    
       

    
        public ColorPointVisual()
        {
            DefaultStyleKey = typeof(ColorPointVisual);
        }


        /// <summary>
        ///     获取或设置ColorPoint的值
        /// </summary>
        public ColorPoint ColorPoint
        {
            get { return (ColorPoint)GetValue(ColorPointProperty); }
            set { SetValue(ColorPointProperty, value); }
        }


        private static void OnColorPointChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as ColorPointVisual;
            var oldValue = (ColorPoint)args.OldValue;
            var newValue = (ColorPoint)args.NewValue;
            if (oldValue != newValue)
                target.OnColorPointChanged(oldValue, newValue);
        }

        protected virtual void OnColorPointChanged(ColorPoint oldValue, ColorPoint newValue)
        {
        }


     
    }
}
