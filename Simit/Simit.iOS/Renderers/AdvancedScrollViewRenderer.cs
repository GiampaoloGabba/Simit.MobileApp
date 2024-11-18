using Foundation;
using Simit.iOS.Renderers;
using Simit.Views.Shared;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ScrollView), typeof(AdvancedScrollViewRenderer))]

namespace Simit.iOS.Renderers
{
    public class AdvancedScrollViewRenderer : ScrollViewRenderer
    {
        //Attiva altre gesture se lo scroll è al top
        [Export("gestureRecognizer:shouldRecognizeSimultaneouslyWithGestureRecognizer:")]
        public virtual bool ShouldRecognizeSimultaneously(UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
        {
            if (Element is AdvancedScrollView element)
                //TODO solo controllo bindable property
                return ContentOffset.Y <= 0 && element.ShouldRecognizeSimultaneously;

            return false;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (Element is AdvancedScrollView element)
                if (element.DisableContentInsetAdjustment)
                    //Fix scrolling anche quando non serve
                    ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;
        }
    }
}