using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Autofac;
using ColorfulBox.Services;
using System.Threading.Tasks;

namespace ColorfulBox
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        private const string SelectedAppThemeKey = "SelectedAppTheme";
        public static IContainer Container { get; private set; }

        /// <summary>
        /// Gets or sets (with LocalSettings persistence) the RequestedTheme of the root element.
        /// </summary>
        public static ElementTheme RootTheme
        {
            get
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    return rootElement.RequestedTheme;
                }

                return ElementTheme.Default;
            }
            set
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = value;
                }
                SetupTitlebar();
                ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] = value.ToString();
            }
        }

        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            
             Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数

                    rootFrame.Navigate(typeof(MainPage), e.Arguments);

                    var mainPage = rootFrame.Content as MainPage;
                    var builder = new ContainerBuilder();
                    FrameAdapter adapter = new FrameAdapter(mainPage.RootFrame);

                    builder.RegisterInstance(adapter)
                        .AsImplementedInterfaces();

                    builder.RegisterType<NavigationService>()
                        .AsImplementedInterfaces()
                        .SingleInstance();

                    builder.RegisterInstance(mainPage).As<INavigationRoot>();

                    Container = builder.Build();
                    mainPage.InitializeNavigationService(Container.Resolve<INavigationService>());
                    adapter.NavigationFailed += OnNavigationFailed;

                    RootTheme = LoadThemeFromSettingsAsync();
                    SetupTitlebar();
                    //ApplicationView.PreferredLaunchViewSize = new Size(1257, 893);
                    //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
                    //rootFrame.Height = 893;
                    //rootFrame.Width = 1257;
                }
                // 确保当前窗口处于活动状态
                Window.Current.Activate();
            }

        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        private static void SetupTitlebar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = Colors.Transparent;
                    if (TrueTheme() == ElementTheme.Dark)
                    {
                        titleBar.ButtonForegroundColor = Colors.White;
                        titleBar.ForegroundColor = Colors.White;
                    }
                    else
                    {
                        titleBar.ButtonForegroundColor = Colors.Black;
                        titleBar.ForegroundColor = Colors.Black;
                    }

                    titleBar.BackgroundColor = Colors.Black;

                    titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                    titleBar.ButtonInactiveForegroundColor = Colors.LightGray;

                    CoreApplicationViewTitleBar coreTitleBar = TitleBarHelper.Instance.TitleBar;

                    coreTitleBar.ExtendViewIntoTitleBar = true;
                }
            }
        }

        public static ElementTheme TrueTheme()
        {
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract",5))
            {
                var frameworkElement = Window.Current.Content as FrameworkElement;
                return frameworkElement.ActualTheme;
            }
            else
            {
                if (Application.Current.RequestedTheme == ApplicationTheme.Light)
                    return ElementTheme.Light;
                else
                    return ElementTheme.Dark;
            }
        }

        private static  ElementTheme LoadThemeFromSettingsAsync()
        {
            ElementTheme cacheTheme = ElementTheme.Default;
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(SelectedAppThemeKey))
            {
                string themeName = ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] as string;
                if (string.IsNullOrWhiteSpace(themeName) == false)
                    Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }
    }
}
