using System;
using Prism;
using Xamarin.Forms;

namespace Simit.Views
{
	public partial class InfoPage : IActiveAware
	{
		public bool IsActive { get; set; }
		public event EventHandler IsActiveChanged;

		public InfoPage()
		{
			InitializeComponent();
		}

		private void OnIsActiveChanged()
		{
			if (IsActive && Device.RuntimePlatform != Device.Android)
			{
				MyScrollView.ScrollToAsync(0, 0, true);
			}
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			if (Device.RuntimePlatform == Device.Android)
				MessagingCenter.Send((ContentPage)this, "stopvideo");
		}
	}
}
