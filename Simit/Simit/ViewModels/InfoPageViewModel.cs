using System;
using System.Threading.Tasks;
using Fusillade;
using Prism;
using Prism.Navigation;
using Simit.Core.Api;

namespace Simit.ViewModels
{
	public class InfoPageViewModel : ViewModelBase, IActiveAware
	{
		private readonly SimitApiManager _simitApiManager;
		public bool IsActive { get; set; }
		public event EventHandler IsActiveChanged;

		public InfoPageViewModel(INavigationService navigationService, SimitApiManager simitApiManager)
			: base(navigationService)
		{
			_simitApiManager = simitApiManager;
			Title                 = "Benvenuto";
		}

		public override void Initialize(INavigationParameters parameters)
		{
			base.Initialize(parameters);
			_simitApiManager.GetProgramma(Priority.Background).ConfigureAwait(false);
		}

		private async void OnIsActiveChanged()
		{
			if (IsActive)
			{
				await Task.Delay(400);
				ShowGilead = true;
			}
		}
	}
}
