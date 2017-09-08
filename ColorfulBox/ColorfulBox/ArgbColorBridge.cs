using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;

namespace ColorfulBox
{
    public class ArgbColorBridge : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isIgnoreColorChanged;

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
            DependencyProperty.Register("Color", typeof(Color), typeof(ArgbColorBridge), new PropertyMetadata(Color.FromArgb(0, 0, 0, 0), OnColorChanged));

        private static void OnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ArgbColorBridge target = obj as ArgbColorBridge;
            Color oldValue = (Color)args.OldValue;
            Color newValue = (Color)args.NewValue;
            if (oldValue != newValue)
                target.OnColorChanged(oldValue, newValue);
        }

        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {


            _isIgnoreColorChanged = true;
            try
            {
                if (A != newValue.A)
                    A = newValue.A;

                if (R != newValue.R)
                    R = newValue.R;

                if (G != newValue.G)
                    G = newValue.G;

                if (B != newValue.B)
                    B = newValue.B;
            }
            finally
            {
                _isIgnoreColorChanged = false;
            }
        }


        private int _a;

        /// <summary>
        /// 获取或设置 A 的值
        /// </summary>
        public int A
        {
            get { return _a; }
            set
            {
                if (_a == value)
                    return;

                _a = value;
                _a = Math.Max(0, _a);
                _a = Math.Min(255, _a);
                OnPropertyChanged("A");
                OnArgbChanged();
            }
        }


        private int _r;

        /// <summary>
        /// 获取或设置 R 的值
        /// </summary>
        public int R
        {
            get { return _r; }
            set
            {
                if (_r == value)
                    return;

                _r = value;
                _r = Math.Max(0, _r);
                _r = Math.Min(255, _r);
                OnPropertyChanged("R");
                OnArgbChanged();
            }
        }


        private int _g;

        /// <summary>
        /// 获取或设置 G 的值
        /// </summary>
        public int G
        {
            get { return _g; }
            set
            {
                if (_g == value)
                    return;

                _g = value;
                _g = Math.Max(0, _g);
                _g = Math.Min(255, _g);
                OnPropertyChanged("G");
                OnArgbChanged();
            }
        }


        private int _b;

        /// <summary>
        /// 获取或设置 B 的值
        /// </summary>
        public int B
        {
            get { return _b; }
            set
            {
                if (_b == value)
                    return;

                _b = value;
                _b = Math.Max(0, _b);
                _b = Math.Min(255, _b);
                OnPropertyChanged("B");
                OnArgbChanged();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ///// <summary>
        ///// 获取或设置A的值
        ///// </summary>  
        //public int A
        //{
        //    get { return (int)GetValue(AProperty); }
        //    set { SetValue(AProperty, value); }
        //}

        ///// <summary>
        ///// 标识 A 依赖属性。
        ///// </summary>
        //public static readonly DependencyProperty AProperty =
        //    DependencyProperty.Register("A", typeof(int), typeof(ArgbColorBridge), new PropertyMetadata(0, OnAChanged));

        //private static void OnAChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        //{
        //    ArgbColorBridge target = obj as ArgbColorBridge;
        //    int oldValue = (int)args.OldValue;
        //    int newValue = (int)args.NewValue;
        //    if (oldValue != newValue)
        //        target.OnAChanged(oldValue, newValue);
        //}

        //protected virtual void OnAChanged(int oldValue, int newValue)
        //{
        //    OnArgbChanged();
        //}


        ///// <summary>
        ///// 获取或设置R的值
        ///// </summary>  
        //public int R
        //{
        //    get { return (int)GetValue(RProperty); }
        //    set { SetValue(RProperty, value); }
        //}

        ///// <summary>
        ///// 标识 R 依赖属性。
        ///// </summary>
        //public static readonly DependencyProperty RProperty =
        //    DependencyProperty.Register("R", typeof(int), typeof(ArgbColorBridge), new PropertyMetadata(0, OnRChanged));

        //private static void OnRChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        //{
        //    ArgbColorBridge target = obj as ArgbColorBridge;
        //    int oldValue = (int)args.OldValue;
        //    int newValue = (int)args.NewValue;
        //    if (oldValue != newValue)
        //        target.OnRChanged(oldValue, newValue);
        //}

        //protected virtual void OnRChanged(int oldValue, int newValue)
        //{
        //    OnArgbChanged();
        //}


        ///// <summary>
        ///// 获取或设置G的值
        ///// </summary>  
        //public int G
        //{
        //    get { return (int)GetValue(GProperty); }
        //    set { SetValue(GProperty, value); }
        //}

        ///// <summary>
        ///// 标识 G 依赖属性。
        ///// </summary>
        //public static readonly DependencyProperty GProperty =
        //    DependencyProperty.Register("G", typeof(int), typeof(ArgbColorBridge), new PropertyMetadata(0, OnGChanged));

        //private static void OnGChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        //{
        //    ArgbColorBridge target = obj as ArgbColorBridge;
        //    int oldValue = (int)args.OldValue;
        //    int newValue = (int)args.NewValue;
        //    if (oldValue != newValue)
        //        target.OnGChanged(oldValue, newValue);
        //}

        //protected virtual void OnGChanged(int oldValue, int newValue)
        //{
        //    OnArgbChanged();
        //}


        ///// <summary>
        ///// 获取或设置B的值
        ///// </summary>  
        //public int B
        //{
        //    get { return (int)GetValue(BProperty); }
        //    set { SetValue(BProperty, value); }
        //}

        ///// <summary>
        ///// 标识 B 依赖属性。
        ///// </summary>
        //public static readonly DependencyProperty BProperty =
        //    DependencyProperty.Register("B", typeof(int), typeof(ArgbColorBridge), new PropertyMetadata(0, OnBChanged));



        //private static void OnBChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        //{
        //    ArgbColorBridge target = obj as ArgbColorBridge;
        //    int oldValue = (int)args.OldValue;
        //    int newValue = (int)args.NewValue;
        //    if (oldValue != newValue)
        //        target.OnBChanged(oldValue, newValue);
        //}

        //protected virtual void OnBChanged(int oldValue, int newValue)
        //{
        //    OnArgbChanged();
        //}

        private void OnArgbChanged()
        {
            if (_isIgnoreColorChanged)
                return;

            Color = Color.FromArgb(Convert.ToByte(A), Convert.ToByte(R), Convert.ToByte(G), Convert.ToByte(B));
        }


    }
}
