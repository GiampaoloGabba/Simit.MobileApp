using System.Threading.Tasks;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using Simit.ViewModels;

namespace Simit.Views.Shared.Loader
{
    public class LoadingPageViewModel : ViewModelBase, IAutoInitialize
    {
        [DoNotNotify]
        public string Pagina { get; set; }
        public LoadingPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.Back)
                await NavigationService.GoBackAsync();

            base.OnNavigatedTo(parameters);
            //un minimo di delay...
            await Task.Delay(150);
            await NavigationService.NavigateAsync(Pagina, null, false, false);
        }

    }
}
