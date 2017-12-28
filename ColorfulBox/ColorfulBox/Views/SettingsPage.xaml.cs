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
using ColorfulBox.Localization;
using Windows.UI.Core;
using Microsoft.Toolkit.Uwp.Helpers;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace ColorfulBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            ConfigureAnimations();
            VersionElement.Text =
          $"{SystemInformation.ApplicationVersion.Major}.{SystemInformation.ApplicationVersion.Minor}.{SystemInformation.ApplicationVersion.Build}.{SystemInformation.ApplicationVersion.Revision}";

            Loaded += OnSettingsPageLoaded;
        }

        private static bool _hasChangedLanguage;
        private bool _hasLoaded;

        private void OnSettingsPageLoaded(object sender, RoutedEventArgs e)
        {
            var currentTheme = App.RootTheme.ToString();
            (ThemePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == currentTheme)).IsChecked = true;

            var language = ApplicationResources.Current.Language;
            var radioButton = LanguagePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == language);
            if (radioButton == null)
                radioButton = LanguagePanel.Children.Cast<RadioButton>().FirstOrDefault();

            radioButton.IsChecked = true;

            if (_hasChangedLanguage)
                ShowNoteElement();

            _hasLoaded = true;
        }


        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var selectedTheme = ((RadioButton)sender)?.Tag?.ToString();

            if (selectedTheme != null)
            {
                App.RootTheme = (ElementTheme)Enum.Parse(typeof(ElementTheme), selectedTheme);
            }
        }

        private async void OnLanguageRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var language = ((RadioButton)sender)?.Tag?.ToString();
            ApplicationResources.Current.Language = language;

            if (_hasLoaded == false)
                return;

            _hasChangedLanguage = true;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, ShowNoteElement);
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


        private void ShowNoteElement()
        {
            NoteElement.Visibility = Visibility.Visible;
        }
    }
}
