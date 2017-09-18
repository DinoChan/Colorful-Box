using System;
using System.Collections.Generic;
using System.Diagnostics;
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


    public class ColorPointVisual : ListViewItem
    {

        /// <summary>
        ///     标识 ColorPoint 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorPointProperty =
            DependencyProperty.Register("ColorPoint", typeof(ColorPoint), typeof(ColorPointVisual), new PropertyMetadata(null, OnColorPointChanged));


        public ColorPointVisual()
        {
            DefaultStyleKey = typeof(ColorPointVisual);
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            ColorPoint = args.NewValue as ColorPoint;
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


        protected  override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualStates(false);
          
        }

        private void UpdateVisualStates(bool useTransitions)
        {
        }

  
    }
}
