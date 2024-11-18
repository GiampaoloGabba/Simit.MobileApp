using System;
using System.Collections.Generic;
using Prism.Navigation;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Simit.Core;
using Simit.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Simit.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged, INavigationAware, IInitialize
	{
		public INavigationService NavigationService { get; private set; }

		public string   Title           { get; set; }
		public bool     IsBusy          { get; set; }
		public bool     ShowGilead      { get; set; }
		public bool     IsOffline       { get; private set; }
		public ICommand InfoLinkCommand { get; set; }
		public ICommand GoBackCommand   { get; set; }
		public bool     ShowClose       { get; set; }

		public ViewModelBase(INavigationService navigationService)
		{
			NavigationService = navigationService;

			InfoLinkCommand = new Command<string>(async (tipo) =>
			{
				if (!IsBusy)
				{
					IsBusy = true;

					if (tipo.StartsWith("https://"))
					{
						await ApriLink(tipo);
					}
					else if (tipo.Contains("@"))
					{
						try
						{
							var message = new EmailMessage
							{
								To = new List<string>{tipo},
							};
							await Email.ComposeAsync(message);
						}
						catch (FeatureNotSupportedException fbsEx)
						{
							App.DisplayAlert("Attenzione", "Questo dispositivo non è abilitato all'invio di Email");
						}
					}
					else
					{
						switch (tipo)
						{
							case "facebook":
								await ApriLink("https://www.facebook.com/SIMIT2024");
								break;
							case "instagram":
								await ApriLink("https://www.instagram.com/simitmilano2024/");
								break;
							case "linkedin":
								await ApriLink("https://www.linkedin.com/company/simit-2024/");
								break;
							case "website":
								await ApriLink("https://www.simit2024.it/");
								break;
							case "iscriviti":
								await ApriLink("https://www.simit2024.it/iscrizioni/residenziale");
								break;
							case "chiudi":
								await NavigationService.GoBackAsync();
								break;
							case "area-riservata":
								await AreaRiservata();
								break;
							case "nadirex":

								try
								{
									var message = new EmailMessage
									{
										To = new List<string>{"segreteria@simit2024.it"},
									};
									await Email.ComposeAsync(message);
								}
								catch (FeatureNotSupportedException fbsEx)
								{
									App.DisplayAlert("Attenzione", "Questo dispositivo non è abilitato all'invio di Email");
								}

								break;
							default:
								await NavigationService.NavigateAsync(tipo);
								break;
						}
					}

					IsBusy = false;
				}
			});

			GoBackCommand = new Command(async () =>
			{
				if (!IsBusy)
				{
					IsBusy = true;
					await NavigationService.GoBackAsync();
					IsBusy = false;
				}
			});
		}

		async Task AreaRiservata()
		{
			if (Settings.Current.Email != String.Empty && Settings.Current.Token != String.Empty)
			{
				var navPar = new NavigationParameters {{"azione", "area-riservata"}};
				await NavigationService.NavigateAsync(nameof(BrowserPage), navPar);
			}
			else
			{
				//await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LoginPage)}");
			}
		}

		protected async Task ApriLink(string link)
		{
			var opt = new BrowserLaunchOptions
			{
				LaunchMode            = BrowserLaunchMode.SystemPreferred,
				PreferredControlColor = Color.FromHex("#046093"),
				PreferredToolbarColor = Color.White,
				TitleMode             = BrowserTitleMode.Hide,
			};

			await Browser.OpenAsync(
				link, opt);
		}

		public virtual void Initialize(INavigationParameters parameters)
		{
			IsOffline = Connectivity.NetworkAccess != NetworkAccess.Internet;
		}

		public virtual void OnNavigatedFrom(INavigationParameters parameters)
		{
		}

		public virtual void OnNavigatedTo(INavigationParameters parameters)
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
