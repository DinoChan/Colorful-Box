using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.Helpers;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace ColorfulBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var temp = "";
            for (int i = 0; i < 36; i++)
            {
               var color= ColorExtensions.FromHsvEx(i * 10, 1, 1);
                temp+= string.Format("< GradientStop Color = \"{0}\" Offset = \"{1}\" />", color.ToString(), i/36d);
                temp += Environment.NewLine;
            }
          var  newcolor = ColorExtensions.FromHsvEx(359, 1, 1);
            temp += string.Format("< GradientStop Color = \"{0}\" Offset = \"{1}\" />", newcolor.ToString(), 1);
            temp += Environment.NewLine;
        }

        private async void OnLickButtonClick(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=24329C-Soft.ColorfulBox_nzvxjwp4batka"));
        }
    }
}
