using System;
using Prism;
using Prism.Navigation;

namespace Simit.ViewModels
{
	public class SediPageViewModel : ViewModelBase, IActiveAware
	{
        public bool     IsActive        { get; set; }
        public bool     ShowContent     { get; set; }
		
        public event EventHandler IsActiveChanged;
		
		public SediPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			ShowClose = false;
			Title     = "Sede Congressuale";
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
