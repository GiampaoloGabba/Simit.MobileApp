using Android.App;
using Android.Widget;
using Simit.Core;

namespace Simit.Droid.Services
{
	public class ToastService : IToastService
	{
		public void Show(string text)
		{
			Toast.MakeText(Application.Context, text, ToastLength.Long)?.Show();
		}
	}
}