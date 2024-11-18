using System;
using System.Linq;
using Simit.Core.Effects;
using Simit.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Simit")]
[assembly: ExportEffect(typeof(SafeAreaPadding), nameof(SafeAreaPadding))]

namespace Simit.iOS.Effects
{
    public class SafeAreaPadding : PlatformEffect
    {
        Thickness _padding;

        protected override void OnAttached()
        {
            if (Element is Layout element)
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    _padding = element.Padding;

                    var effect = (SafeAreaPaddingEffect) Element.Effects.FirstOrDefault(e => e is SafeAreaPaddingEffect);
                    var insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets; // Can't use KeyWindow this early

                    element.Padding = new Thickness(_padding.Left + insets.Left,
                        _padding.Top + Math.Max(insets.Top, effect?.MinPaddingTop ?? 0),
                        _padding.Right + insets.Right, _padding.Bottom);
                }
            }
            if (Element is Label label)
            {
	            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
	            {
		            _padding = label.Margin;

		            var effect = (SafeAreaPaddingEffect) Element.Effects.FirstOrDefault(e => e is SafeAreaPaddingEffect);
		            var insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets; // Can't use KeyWindow this early
		            
		            label.Margin = new Thickness(_padding.Left + insets.Left,
			            _padding.Top + Math.Max(insets.Top, effect?.MinPaddingTop ?? 0),
			            _padding.Right + insets.Right, _padding.Bottom);
	            }
            }
        }

        protected override void OnDetached()
        {
            if (Element is Layout element)
            {
                element.Padding = _padding;
            }
        }
    }
}