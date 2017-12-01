using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ColorfulBox
{
    public class HsvColorBridge : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isIgnoreColorChanged;



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
            DependencyProperty.Register("HsvColor", typeof(HsvColor), typeof(HsvColorBridge), new PropertyMetadata(default(HsvColor), OnHsvColorChanged));

        private static void OnHsvColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            HsvColorBridge target = obj as HsvColorBridge;
            HsvColor oldValue = (HsvColor)args.OldValue;
            HsvColor newValue = (HsvColor)args.NewValue;
            if (oldValue.Equals(newValue) == false)
                target.OnHsvColorChanged(oldValue, newValue);
        }

        protected virtual void OnHsvColorChanged(HsvColor oldValue, HsvColor newValue)
        {
            if (_isIgnoreColorChanged)
                return;

            _isIgnoreColorChanged = true;
            try
            {
                var hsv = newValue;

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

            _isIgnoreColorChanged = true;
            try
            {
                HsvColor = new HsvColor {A = 1, H = H, S = S, V = V};
            }
            finally
            {
                _isIgnoreColorChanged = false;
            }
        }


    }
}
