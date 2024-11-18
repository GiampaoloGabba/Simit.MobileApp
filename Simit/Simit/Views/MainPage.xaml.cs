
using Simit.ViewModels;
using Xamarin.Forms;

namespace Simit.Views
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		protected override bool OnBackButtonPressed()
		{

			if (CurrentPage is NavigationPage navigationPage)
			{
				if (navigationPage.Navigation?.NavigationStack?.Count > 1)
				{
					var bindingContext = (ViewModelBase)navigationPage.CurrentPage.BindingContext;
					bindingContext.NavigationService.GoBackAsync();
				}
			}

			return true;
		}
	}
}
