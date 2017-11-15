using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ColorfulBox
{
    public class HslColorBridge : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isIgnoreColorChanged;

        /// <summary>
        /// 获取或设置Color的值
        /// </summary>  
        public Color Color
        {
            get { return (Color) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// 标识 Color 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(HslColorBridge),
                new PropertyMetadata(Color.FromArgb(0, 0, 0, 0), OnColorChanged));

        private static void OnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            HslColorBridge target = obj as HslColorBridge;
            Color oldValue = (Color) args.OldValue;
            Color newValue = (Color) args.NewValue;
            if (oldValue != newValue)
                target.OnColorChanged(oldValue, newValue);
        }

        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {
            if (_isIgnoreColorChanged)
                return;

            _isIgnoreColorChanged = true;
            try
            {
                var Hsl = newValue.ToHslEx();
               
                if (H != Hsl.H)
                    H = Hsl.H;

                if (S != Hsl.S)
                    S = Hsl.S;

                if (L != Hsl.L)
                    L = Hsl.L;
            }
            finally
            {
                _isIgnoreColorChanged = false;
            }
        }


        private double _H;

        /// <summary>
        /// 获取或设置 H 的值
        /// </summary>
        public double H
        {
            get { return _H; }
            set
            {
                if (_H == value)
                    return;

                _H = value;
                OnPropertyChanged("H");
                OnHslChanged();
            }
        }


        private double _s;

        /// <summary>
        /// 获取或设置 S 的值
        /// </summary>
        public double S
        {
            get { return _s; }
            set
            {
                if (_s == value)
                    return;

                _s = value;
                OnPropertyChanged("S");
                OnHslChanged();
            }
        }


        private double _l;

        /// <summary>
        /// 获取或设置 V 的值
        /// </summary>
        public double L
        {
            get { return _l; }
            set
            {
                if (_l == value)
                    return;

                _l = value;
                OnPropertyChanged("L");
                OnHslChanged();
            }
        }

        private int _a;

     

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        private void OnHslChanged()
        {
            if (_isIgnoreColorChanged)
                return;

            _isIgnoreColorChanged = true;
            try
            {
                Color = ColorExtensions.FromHslEx(H, S, L);
            }
            finally
            {
                _isIgnoreColorChanged = false;
            }
        }


    }
}
