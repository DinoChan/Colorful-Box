using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp;

namespace ColorfulBox
{
    public class ColorPoint : DependencyObject, INotifyPropertyChanged
    {

        //public event EventHandler<PropertyEventArgs> ColorChanged;
        public event EventHandler<PropertyEventArgs> HsvColorChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnColorChanged(Color oldValue, Color newValue)
        //{
        //    ColorChanged?.Invoke(this, new PropertyEventArgs(nameof(Color), oldValue, newValue));
        //}


        //private Color _color;

        ///// <summary>
        ///// 获取或设置 Color 的值
        ///// </summary>
        //public Color Color
        //{
        //    get { return _color; }
        //    set
        //    {
        //        if (_color == value)
        //            return;

        //        var oldValue = _color;
        //        _color = value;
        //        OnPropertyChanged("Color");
        //        ColorChanged?.Invoke(this, new PropertyEventArgs(nameof(Color), oldValue, _color));
        //    }
        //}


        private Color _initializationColor;

        /// <summary>
        /// 获取或设置 InitializationColor 的值
        /// </summary>
        public Color InitializationColor
        {
            get { return _initializationColor; }
            set
            {
                if (_initializationColor == value)
                    return;

                _initializationColor = value;
                HsvColor = value.ToHsvEx();
            }
        }

        /// <summary>
        /// 获取或设置HsvColor的值
        /// </summary>  
        public HsvColor HsvColor
        {
            get { return (HsvColor)GetValue(HsvColorProperty); }
            set { SetValue(HsvColorProperty, value); }
        }

        /// <summary>
        /// 标识 HsvColor 依赖属性。
        /// </summary>
        public static readonly DependencyProperty HsvColorProperty =
            DependencyProperty.Register("HsvColor", typeof(HsvColor), typeof(ColorPoint), new PropertyMetadata(default(HsvColor), OnHsvColorChanged));

        private static void OnHsvColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ColorPoint target = obj as ColorPoint;
            HsvColor oldValue = (HsvColor)args.OldValue;
            HsvColor newValue = (HsvColor)args.NewValue;
            if (oldValue.Equals(newValue) == false)
                target.OnHsvColorChanged(oldValue, newValue);
        }

        protected virtual void OnHsvColorChanged(HsvColor oldValue, HsvColor newValue)
        {
            //Color = newValue.ToColor();
            HsvColorChanged?.Invoke(this, new PropertyEventArgs(nameof(HsvColor), oldValue, newValue));
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

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
