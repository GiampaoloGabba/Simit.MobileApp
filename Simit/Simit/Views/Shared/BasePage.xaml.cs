using System;
using System.Collections.Generic;
using System.Linq;
using Simit.Views.Shared.Loader;
using Xamarin.Forms;

namespace Simit.Views.Shared
{
    public partial class BasePage : NotchContentPage
    {
        public BasePage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, " ");
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetPrefersHomeIndicatorAutoHidden(this, true);
        }

        protected override void OnAppearing()
        {
	        if (!(this is LoadingPage))
	        {
		        RemovePage(this, Navigation.NavigationStack.ToList());
	        }
	        base.OnAppearing();
        }

        public bool RemovePage(Page pagina, IEnumerable<Page> stack)
        {
	        foreach (var page in stack)
	        {
		        if (page.GetType() != typeof(LoadingPage)) continue;

		        try
		        {
			        pagina.Navigation.RemovePage(page);
			        return true;
		        }
		        catch (Exception e)
		        {
			        Console.WriteLine(e);
		        }
	        }

	        return false;
        }
    }
}
