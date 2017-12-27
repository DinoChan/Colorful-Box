using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ColorfulBox.Services;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Controls;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace ColorfulBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page, INavigationRoot
    {
        private INavigationService _navigationService;


        public MainPage()
        {
            this.InitializeComponent();

            HamburgerMenu.RegisterPropertyChangedCallback(HamburgerMenu.IsPaneOpenProperty, OnNavigationViewIsPaneOpenPropertyChanged);
            SystemNavigationManager.GetForCurrentView().BackRequested += OnAppBackRequested;
            WindowTitle.EnableLayoutImplicitAnimations(TimeSpan.FromMilliseconds(100));
            this.Loaded += OnLoaded;
        }

        public TitleBarHelper TitleHelper => TitleBarHelper.Instance;

        public event EventHandler IsPaneOpenChanged;

        public bool IsPaneOpen => HamburgerMenu.IsPaneOpen;

        public double CompactPaneLength => HamburgerMenu.CompactPaneLength;

        public void InitializeNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateToHomeAsync();
        }

      
        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            switch (e.SourcePageType)
            {
                case Type c when e.SourcePageType == typeof(HomePage):
                    HamburgerMenu.SelectedIndex = 0;
                    break;
                case Type c when e.SourcePageType == typeof(SettingsPage):
                    HamburgerMenu.SelectedIndex = 1;
                    break;
            }
            DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = RootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            });
        }


        private void OnNavigationViewIsPaneOpenPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            IsPaneOpenChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnAppBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (RootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                RootFrame.GoBack();
            }
        }

        private void HamburgerMenu_ItemInvoked(object sender, HamburgetMenuItemInvokedEventArgs e)
        {
            switch ((e.InvokedItem as HamburgerMenuGlyphItem).Tag as string)
            {
                case "Home":
                    _navigationService.NavigateToHomeAsync();
                    break;
                case "Settings":
                    _navigationService.NavigateToSettingsAsync();
                    break;
                  
            }
        }
    }
}
