using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Fusillade;
using Prism.AppModel;
using Prism.Navigation;
using Simit.Core.Api;
using Simit.Core.Models;

namespace Simit.ViewModels
{
    public class RelatoriPopupPageViewModel : ViewModelBase
    {
        private readonly SimitApiManager _simitApiManager;

        public ObservableCollection<string> Faculty { get; set; }

        public RelatoriPopupPageViewModel(INavigationService navigationService, SimitApiManager simitApiManager)
            : base(navigationService)
        {
            _simitApiManager = simitApiManager;
            Title            = "Faculty";
        }
        
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await GetRelatori();
        }


        public async Task GetRelatori()
        {
            //prima dalla cache
            var facultyReq = await _simitApiManager.GetFaculty(Priority.UserInitiated, true);
            Faculty = new ObservableCollection<string>(facultyReq.Content ?? new List<string>());

            //poi leggo online
            facultyReq = await _simitApiManager.GetFaculty(Priority.UserInitiated);
            Faculty    = new ObservableCollection<string>(facultyReq.Content ?? new List<string>());
        }
    }
}