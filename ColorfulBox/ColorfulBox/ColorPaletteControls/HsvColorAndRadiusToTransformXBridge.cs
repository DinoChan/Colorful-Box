using System;
using Windows.UI;
using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp;

namespace ColorfulBox
{
    public class HsvColorAndRadiusToTransformXBridge : DependencyObject
    {
        /// <summary>
        ///     标识 AttachedElement 依赖属性。
        /// </summary>
        public static readonly DependencyProperty AttachedElementProperty =
            DependencyProperty.Register("AttachedElement", typeof(FrameworkElement), typeof(HsvColorAndRadiusToTransformXBridge), new PropertyMetadata(null, OnAttachedElementChanged));

     
        /// <summary>
        ///     标识 TranslateX 依赖属性。
        /// </summary>
        public static readonly DependencyProperty TranslateXProperty =
            DependencyProperty.Register("TranslateX", typeof(double), typeof(HsvColorAndRadiusToTransformXBridge), new PropertyMetadata(0d));

        /// <summary>
        ///     标识 HsvColor 依赖属性。
        /// </summary>
        public static readonly DependencyProperty HsvColorProperty =
            DependencyProperty.Register("HsvColor", typeof(HsvColor), typeof(HsvColorAndRadiusToTransformXBridge), new PropertyMetadata(default(HsvColor), OnHsvColorChanged));


        /// <summary>
        ///     获取或设置AttachedElement的值
        /// </summary>
        public FrameworkElement AttachedElement
        {
            get => (FrameworkElement) GetValue(AttachedElementProperty);
            set => SetValue(AttachedElementProperty, value);
        }


        /// <summary>
        ///     获取或设置HsvColor的值
        /// </summary>
        public HsvColor HsvColor
        {
            get => (HsvColor) GetValue(HsvColorProperty);
            set => SetValue(HsvColorProperty, value);
        }

        /// <summary>
        ///     获取或设置TranslateX的值
        /// </summary>
        public double TranslateX
        {
            get => (double) GetValue(TranslateXProperty);
            set => SetValue(TranslateXProperty, value);
        }

        private static void OnHsvColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as HsvColorAndRadiusToTransformXBridge;
            var oldValue = (HsvColor) args.OldValue;
            var newValue = (HsvColor) args.NewValue;
            if (oldValue.Equals(newValue)==false)
                target.OnHsvColorChanged(oldValue, newValue);
        }

        protected virtual void OnHsvColorChanged(HsvColor oldValue, HsvColor newValue)
        {
            UpdateTransformX();
        }


        private static void OnAttachedElementChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as HsvColorAndRadiusToTransformXBridge;
            var oldValue = (FrameworkElement) args.OldValue;
            var newValue = (FrameworkElement) args.NewValue;
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


            var radius = Math.Min(AttachedElement.ActualWidth, AttachedElement.ActualHeight) / 2;
            TranslateX = HsvColor.S * radius;
        }
    }
}