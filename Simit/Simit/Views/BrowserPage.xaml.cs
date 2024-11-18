
using System.Linq;

namespace Simit.Views
{
    public partial class BrowserPage
    {
        public BrowserPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (Navigation?.NavigationStack != null)
            {
                foreach (var page in Navigation.NavigationStack.ToList().Where(page => !(page is BrowserPage)))
                {
                    Navigation.RemovePage(page);
                }
            }
            base.OnAppearing();
        }
    }
}
