using System;
using System.Diagnostics;
using MonkeyCache;
using MonkeyCache.FileStore;
//using Plugin.AzurePushNotification;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
//using Simit.Core;
using Simit.Core.Api;
using Simit.Core.Api.Infrastructure;
using Simit.ViewModels;
using Simit.Views;
using Simit.Views.Popups;
using Xamarin.Forms;
using SediPage = Simit.Views.SediPage;

namespace Simit
{
	public partial class App
	{
		static readonly IApiService<ISimitApi> SimitApi = new ApiService<ISimitApi>("https://www.simit2024.it/api");

		public App(IPlatformInitializer initializer)
			: base(initializer)
		{
		}

		protected override async void OnInitialized()
		{
			InitializeComponent();

			Barrel.ApplicationId = "it.Simit2024";

			/*Settings.Current.Email = string.Empty;
			Settings.Current.Token = string.Empty;*/

			try
			{
				BarrelUtils.SetBaseCachePath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}

			Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);

			await NavigationService.NavigateAsync(nameof(MainPage));
		}

		/*protected override async void OnStart()
        {

            // Handle when your app starts
            CrossAzurePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                Debug.WriteLine($"TOKEN REC: {p.Token}");
            };
            Debug.WriteLine($"TOKEN: {CrossAzurePushNotification.Current.Token}");

            CrossAzurePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    Debug.WriteLine("Received");
                    MessagingCenter.Send<object>(this, "updateData");

                    if (Device.RuntimePlatform == Device.Android)
                    {
	                    Device.BeginInvokeOnMainThread(() =>
	                    {
		                    Container.Resolve<IToastService>().Show(p.Data["body"].ToString());
	                    });
                    }
                }
                catch (Exception ex)
                {

                }

            };

			await CrossAzurePushNotification.Current.RegisterAsync(new[] { "simit" });
        }*/

		public static void DisplayAlert(string title, string message)
		{
			Device.BeginInvokeOnMainThread(() => { Current.MainPage.DisplayAlert(title, message, "Ok"); });
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterPopupNavigationService();
			containerRegistry.RegisterForNavigation<NavigationPage>();
			containerRegistry.RegisterForNavigation<MainPage>();
			containerRegistry.RegisterForNavigation<InfoPage, InfoPageViewModel>();
			containerRegistry.RegisterForNavigation<ProgrammaPage, ProgrammaPageViewModel>();
			containerRegistry.RegisterForNavigation<PosterPage, PosterPageViewModel>();
			//containerRegistry.RegisterForNavigation<MediaPage, MediaPageViewModel>();
			containerRegistry.RegisterForNavigation<BrowserPage, BrowserPageViewModel>();
			//containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
			//containerRegistry.RegisterForNavigation<RecuperaPwdPage, RecuperaPwdPageViewModel>();

			//containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
			//containerRegistry.RegisterForNavigation<ComunicazioniPage, ComunicazioniPageViewModel>();
			containerRegistry.RegisterForNavigation<SegreteriaPage, SegreteriaPageViewModel>();
			containerRegistry.RegisterForNavigation<SediPage, SediPageViewModel>();

			containerRegistry.RegisterForNavigation<DettagliPopupPage, DettagliPopupPageViewModel>();
            containerRegistry.RegisterForNavigation<RelatoriPopupPage, RelatoriPopupPageViewModel>();
			containerRegistry.RegisterForNavigation<LegendaPopupPage>();

			containerRegistry.RegisterInstance(SimitApi);
			containerRegistry.RegisterSingleton<ISimitApiManager, SimitApiManager>();
		}
	}
}
