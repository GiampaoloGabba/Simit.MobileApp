using System;
using System.Windows.Input;
using Prism;
using Prism.Navigation;
using Simit.Views;
using Xamarin.Forms;

namespace Simit.ViewModels
{
	public class PosterPageViewModel : ViewModelBase, IActiveAware
	{
		public bool     IsActive        { get; set; }
		public bool     ShowContent     { get; set; }
		public ICommand PosterCommand   { get; set; }

		public event EventHandler IsActiveChanged;

		public PosterPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			Title = "Poster";

			PosterCommand = new Command(async () =>
			{
				if (!IsBusy)
				{
					IsBusy = true;

					var navPar = new NavigationParameters {{"azione", "poster"}};
					await NavigationService.NavigateAsync(nameof(BrowserPage), navPar);

					IsBusy = false;
				}
			});
		}

		private void OnIsActiveChanged()
		{
			if (IsActive)
			{
				ShowGilead  = true;
				ShowContent = true;
			}
		}
	}
}
