using System.Linq;
using Prism.AppModel;
using Prism.Navigation;
using Simit.Core.Models;

namespace Simit.ViewModels
{
	public class DettagliPopupPageViewModel : ViewModelBase, IAutoInitialize
	{
		public Programma Programma { get; set; }
		public Scheda    Scheda    { get; set; }

		public DettagliPopupPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			Title = "Dettagli";
		}

		public override void Initialize(INavigationParameters parameters)
		{
			base.Initialize(parameters);
			Scheda = Programma?.Scheda.FirstOrDefault();
		}
	}
}
