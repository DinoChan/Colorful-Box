using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using  Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ColorfulBox
{
    public  partial class ExportView : UserControl
    {
        public ExportView()
        {
            this.InitializeComponent();
            
        }

        private string _colorText;

        /// <summary>
        /// 获取或设置ColorPoints的值
        /// </summary>  
        public ColorPointCollection ColorPoints
        {
            get { return (ColorPointCollection)GetValue(ColorPointsProperty); }
            set { SetValue(ColorPointsProperty, value); }
        }

        /// <summary>
        /// 标识 ColorPoints 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ColorPointsProperty =
            DependencyProperty.Register("ColorPoints", typeof(ColorPointCollection), typeof(ExportView), new PropertyMetadata(null, OnColorPointsChanged));

        private static void OnColorPointsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ExportView target = obj as ExportView;
           var oldValue = (ColorPointCollection)args.OldValue;
           var newValue = (ColorPointCollection)args.NewValue;
            if (oldValue != newValue)
                target.OnColorPointsChanged(oldValue, newValue);
        }

        protected virtual void OnColorPointsChanged(ColorPointCollection oldValue, ColorPointCollection newValue)
        {
            if (oldValue != null)
                oldValue.CollectionChanged -= OnColorPointsCollectionChanged;

            if (newValue!=null)
                newValue.CollectionChanged += OnColorPointsCollectionChanged;

            UpdateText();
        }

        private void OnColorPointsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            string text = string.Empty;
            if (ColorPoints != null)
            {
                int index = 1;
                foreach (var item in ColorPoints)
                {
                    text += $"<SolidColorBrush x:Key=\"ThemeColor{index}\" Color=\"{item.Color.ToHex()}\"/>";
                    text += Environment.NewLine;
                    index++;
                }
            }
            _colorText = text;
            ExportTextBox.Text = _colorText;

        }

        private void OnCopy(object sender, RoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(_colorText);
            Clipboard.SetContent(dataPackage);
            InAppNotification.Show(2000);
        }
    }
}
