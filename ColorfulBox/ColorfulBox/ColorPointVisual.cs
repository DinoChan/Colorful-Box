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
    [TemplateVisualState(Name = IsSelectedName, GroupName = SelectedStates)]
    [TemplateVisualState(Name = UnSelectedName, GroupName = SelectedStates)]

    public class ColorPointVisual : Control
    {
        private const string ThumbElementName = "ThumbElement";
        private const string SelectedStates = "SelectedStates";
        private const string IsSelectedName = "IsSelected";
        private const string UnSelectedName = "UnSelected";

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


        /// <summary>
        /// 获取或设置IsSelected的值
        /// </summary>  
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// 标识 IsSelected 依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(ColorPointVisual), new PropertyMetadata(false, OnIsSelectedChanged));

        private static void OnIsSelectedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorPointVisual target = obj as ColorPointVisual;
            bool oldValue = (bool)args.OldValue;
            bool newValue = (bool)args.NewValue;
            if (oldValue != newValue)
                target.OnIsSelectedChanged(oldValue, newValue);
        }

        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            UpdateVisualStates(true);
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualStates(false);
        }

        private void UpdateVisualStates(bool useTransitions)
        {

            VisualStateManager.GoToState(this, IsSelected ? IsSelectedName : UnSelectedName, useTransitions);
        }
    }
}
