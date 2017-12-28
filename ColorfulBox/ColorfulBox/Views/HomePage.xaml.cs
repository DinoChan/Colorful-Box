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
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace ColorfulBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            ConfigureAnimations();
        }


        private void ConfigureAnimations()
        {
            ElementCompositionPreview.SetIsTranslationEnabled(TitleElement, true);
            ElementCompositionPreview.SetImplicitShowAnimation(TitleElement,
                VisualHelpers.CreateAnimationGroup(
                VisualHelpers.CreateVerticalOffsetAnimationFrom(0.45, -50f),
                VisualHelpers.CreateOpacityAnimation(0.5)));

            Canvas.SetZIndex(this, 1);
            ElementCompositionPreview.SetImplicitHideAnimation(this, VisualHelpers.CreateOpacityAnimation(0.4, 0));

            var contentShowAnimations = VisualHelpers.CreateVerticalOffsetAnimation(0.45, 50, 0.2);
            var contentOpacityAnimations = VisualHelpers.CreateOpacityAnimation(.8);

            ElementCompositionPreview.SetIsTranslationEnabled(ContentElement, true);
            ElementCompositionPreview.SetImplicitShowAnimation(
                ContentElement,
                VisualHelpers.CreateAnimationGroup(contentShowAnimations, contentOpacityAnimations));

            ElementCompositionPreview.SetImplicitHideAnimation(ContentElement, VisualHelpers.CreateVerticalOffsetAnimationTo(0.4, 50));
        }

        private async void OnLickButtonClick(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=24329C-Soft.ColorfulBox_nzvxjwp4batka"));
        }
    }
}
