using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;

namespace ColorfulBox
{
    public class ColorAndRadiusToTransformXBridge : DependencyObject
    {
        /// <summary>
        ///     标识 AttachedElement 依赖属性。
        /// </summary>
        public static readonly DependencyProperty AttachedElementProperty =
            DependencyProperty.Register("AttachedElement", typeof(FrameworkElement), typeof(ColorAndRadiusToTransformXBridge), new PropertyMetadata(null, OnAttachedElementChanged));

        /// <summary>
        ///     标识 Color 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorAndRadiusToTransformXBridge), new PropertyMetadata(default(Color), OnColorChanged));

        /// <summary>
        ///     标识 TranslateX 依赖属性。
        /// </summary>
        public static readonly DependencyProperty TranslateXProperty =
            DependencyProperty.Register("TranslateX", typeof(double), typeof(ColorAndRadiusToTransformXBridge), new PropertyMetadata(0d));


        /// <summary>
        ///     获取或设置AttachedElement的值
        /// </summary>
        public FrameworkElement AttachedElement
        {
            get { return (FrameworkElement)GetValue(AttachedElementProperty); }
            set { SetValue(AttachedElementProperty, value); }
        }


        /// <summary>
        ///     获取或设置Color的值
        /// </summary>
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }


        /// <summary>
        ///     获取或设置TranslateX的值
        /// </summary>
        public double TranslateX
        {
            get { return (double)GetValue(TranslateXProperty); }
            set { SetValue(TranslateXProperty, value); }
        }

        private static void OnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as ColorAndRadiusToTransformXBridge;
            var oldValue = (Color)args.OldValue;
            var newValue = (Color)args.NewValue;
            if (oldValue != newValue)
                target.OnColorChanged(oldValue, newValue);
        }

        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {
            UpdateTransformX();
        }


        private static void OnAttachedElementChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as ColorAndRadiusToTransformXBridge;
            var oldValue = (FrameworkElement)args.OldValue;
            var newValue = (FrameworkElement)args.NewValue;
            if (oldValue != newValue)
                target.OnAttachedElementChanged(oldValue, newValue);
        }

        protected virtual void OnAttachedElementChanged(FrameworkElement oldValue, FrameworkElement newValue)
        {
            if (oldValue != null)
                oldValue.SizeChanged -= OnSizeChanged;

            if (newValue != null)
                newValue.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTransformX();
        }


        private void UpdateTransformX()
        {
            if (AttachedElement == null)
                return;

            var hsvColor = Microsoft.Toolkit.Uwp.ColorHelper.ToHsv(Color);

            var radius = Math.Min(AttachedElement.ActualWidth, AttachedElement.ActualHeight) / 2;
            TranslateX = hsvColor.S * radius;
        }
    }
}
