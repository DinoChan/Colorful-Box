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
    public class HsvColorBridge : DependencyObject, INotifyPropertyChanged
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
            DependencyProperty.Register("Color", typeof(Color), typeof(HsvColorBridge),
                new PropertyMetadata(Color.FromArgb(0, 0, 0, 0), OnColorChanged));

        private static void OnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            HsvColorBridge target = obj as HsvColorBridge;
            Color oldValue = (Color) args.OldValue;
            Color newValue = (Color) args.NewValue;
            if (oldValue != newValue)
                target.OnColorChanged(oldValue, newValue);
        }

        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {


            _isIgnoreColorChanged = true;
            try
            {
                var hsv = newValue.ToHsvEx();
                if (H != hsv.H)
                    H = hsv.H;

                if (S != hsv.S)
                    S = hsv.S;

                if (V != hsv.V)
                    V = hsv.V;
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
                OnHsvChanged();
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
                OnHsvChanged();
            }
        }


        private double _v;

        /// <summary>
        /// 获取或设置 V 的值
        /// </summary>
        public double V
        {
            get { return _v; }
            set
            {
                if (_v == value)
                    return;

                _v = value;
                OnPropertyChanged("V");
                OnHsvChanged();
            }
        }

        private int _a;

     

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        private void OnHsvChanged()
        {
            if (_isIgnoreColorChanged)
                return;

            Color = ColorExtensions.FromHsvEx(H, S, V);
        }


    }
}
