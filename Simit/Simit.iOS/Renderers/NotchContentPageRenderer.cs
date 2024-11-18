using System;
using Simit.iOS.Renderers;
using Simit.Views.Shared;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NotchContentPage), typeof(NotchContentPageRenderer))]

namespace Simit.iOS.Renderers
{
    public class NotchContentPageRenderer : PageRenderer, IUIGestureRecognizerDelegate
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var element = (NotchContentPage) Element;

            if (NavigationController != null && Element != null && element.ReActivateEdgeSwipeOniOS)
            {
                NavigationController.InteractivePopGestureRecognizer.Delegate = this;
                NavigationController.InteractivePopGestureRecognizer.Enabled  = true;
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            var element = (NotchContentPage) Element;
            if (element.SafeAreaTop == 0)
                ((NotchContentPage) Element).SafeAreaTop =
                    Math.Max(20, UIApplication.SharedApplication.Windows[0].SafeAreaInsets.Top);

            if (element.SafeAreaBottom == 0)
                ((NotchContentPage) Element).SafeAreaBottom = UIApplication.SharedApplication.Windows[0].SafeAreaInsets.Bottom;

            base.OnElementChanged(e);
        }
    }
}