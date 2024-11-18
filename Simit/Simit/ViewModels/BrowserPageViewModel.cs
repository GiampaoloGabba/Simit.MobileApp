using System.Threading.Tasks;
using Prism.Navigation;
using Simit.Core;

namespace Simit.ViewModels
{
	public class BrowserPageViewModel : ViewModelBase
	{
		public string Url { get; set; }

		public BrowserPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			ShowClose = true;
		}

		public override async void Initialize(INavigationParameters parameters)
		{
			base.Initialize(parameters);

			if (IsOffline)
			{
				await Chiudi("Per continuare è richiesta la connessione ad internet");
				return;
			}

			if (parameters.ContainsKey("azione"))
			{
				var azione = parameters["azione"].ToString();

				switch (azione)
				{
					case "poster":
						Title = "Vota Poster";
						Url   = $"https://www.simit2024.it/api/simit/redirect?e={Settings.Current.Email}&g={Settings.Current.Token}&d={azione}";
						break;

					case "area-riservata":
						Title = "Area Riservata";
						Url   = $"https://www.simit2024.it/api/simit/redirect?e={Settings.Current.Email}&g={Settings.Current.Token}&d={azione}";
						break;

					default:
						await Chiudi("La pagina richiesta non è valida");
						break;
				}
			}
		}

		async Task Chiudi(string errore)
		{
			App.DisplayAlert("Attenzione", errore);
			await NavigationService.GoBackAsync();
		}
	}
}
