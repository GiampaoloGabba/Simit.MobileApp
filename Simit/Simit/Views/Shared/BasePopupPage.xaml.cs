using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Simit.Views.Shared
{
    public partial class BasePopupPage
    {
        public ICommand GoBackCommand { get; set; }
        public string   Titolo        { get; set; }

        public BasePopupPage()
        {
            InitializeComponent();
            BindingContext = this;

            GoBackCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    await Navigation.PopPopupAsync();
                    IsBusy = false;
                }
            });
        }
    }
}
