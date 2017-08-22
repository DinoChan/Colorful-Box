using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;

namespace ColorfulBox
{
    public class ColorPoint : DependencyObject
    {


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
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorPoint), new PropertyMetadata(default(Color), OnColorChanged));

        private static void OnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorPoint target = obj as ColorPoint;
            Color oldValue = (Color)args.OldValue;
            Color newValue = (Color)args.NewValue;
            if (oldValue != newValue)
                target.OnColorChanged(oldValue, newValue);
        }

        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {

        }



        /// <summary>
        /// 获取或设置IsPrimary的值
        /// </summary>  
        public bool IsPrimary
        {
            get { return (bool)GetValue(IsPrimaryProperty); }
            set { SetValue(IsPrimaryProperty, value); }
        }

        /// <summary>
        /// 标识 IsPrimary 依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsPrimaryProperty =
            DependencyProperty.Register("IsPrimary", typeof(bool), typeof(ColorPoint), new PropertyMetadata(false, OnIsPrimaryChanged));

        private static void OnIsPrimaryChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorPoint target = obj as ColorPoint;
            bool oldValue = (bool)args.OldValue;
            bool newValue = (bool)args.NewValue;
            if (oldValue != newValue)
                target.OnIsPrimaryChanged(oldValue, newValue);
        }

        protected virtual void OnIsPrimaryChanged(bool oldValue, bool newValue)
        {
        }
    }
}
