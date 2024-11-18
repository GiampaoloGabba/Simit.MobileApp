using CoreAnimation;
using Simit.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace Simit.iOS.Renderers
{
	public class CustomTabbedPageRenderer : TabbedRenderer
	{

		private bool _tabBarModified = true;

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			TabBar.Translucent = false;
			TabBar.Opaque      = true;

			//if (!_tabBarModified)
			//{
				/*_tabBarModified = true;
				TabBar.Layer.MasksToBounds = true;
				TabBar.Layer.CornerRadius  = 20;
				TabBar.Layer.MaskedCorners = CACornerMask.MaxXMinYCorner|CACornerMask.MaxXMaxYCorner|CACornerMask.MinXMaxYCorner|CACornerMask.MinXMinYCorner;


				TabBar.BarStyle = UIBarStyle.BlackOpaque;*/
			//}
			//else
			//{
		//		_tabBarModified = false;
		//	}
		}

	}
}
