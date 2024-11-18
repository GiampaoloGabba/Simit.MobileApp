using System;
using Prism;
using Prism.Navigation;

namespace Simit.ViewModels
{
	public class SegreteriaPageViewModel : ViewModelBase, IActiveAware
	{
        public bool IsActive    { get; set; }
        public bool ShowContent { get; set; }
		
        public event EventHandler IsActiveChanged;
		
		public SegreteriaPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			ShowClose = false;
			Title     = "Segreteria Organizzativa";
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
