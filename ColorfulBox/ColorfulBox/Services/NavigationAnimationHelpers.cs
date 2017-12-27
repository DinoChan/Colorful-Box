using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Animation;

namespace ColorfulBox.Services
{
    public static class NavigationAnimationHelpers
    {
        public static bool ConnectedNavigate(this Frame frame, object parameter, string connectedKey, UIElement element, Type destination)
        {
            ImplicitHideFrameContent(frame);

            var cas = ConnectedAnimationService.GetForCurrentView();
            cas.DefaultDuration = TimeSpan.FromSeconds(0.5);
            cas.PrepareToAnimate(connectedKey, element);

            return frame.Navigate(destination, parameter);
        }

        public static bool NavigateWithFadeOutgoing(this Frame frame, object parameter, Type destination)
        {
            ImplicitHideFrameContent(frame);

            return frame.Navigate(destination, parameter);
        }

        private static void ImplicitHideFrameContent(Frame frame)
        {
            if (frame.Content != null)
            {
                SetImplicitHide(frame.Content as UIElement);
            }
        }

        private static void SetImplicitHide(UIElement thisPtr)
        {
            ElementCompositionPreview.SetImplicitHideAnimation(thisPtr, VisualHelpers.CreateOpacityAnimation(0.4, 0));
            Canvas.SetTop(thisPtr, 1);
        }
    }
}
