using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Simit.Views
{
    public partial class SediPage
    {
        public SediPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var location = new Location(40.8385488, 14.2560814);
            var options  = new MapLaunchOptions {NavigationMode = NavigationMode.Driving, Name = "Centro Congressi Stazione Marittima"};

            await Map.OpenAsync(location, options);

            /*var opt = new BrowserLaunchOptions
            {
                LaunchMode            = BrowserLaunchMode.SystemPreferred,
                PreferredControlColor = Color.FromHex("#046093"),
                PreferredToolbarColor = Color.White,
                TitleMode             = BrowserTitleMode.Hide,
                Flags                 = BrowserLaunchFlags.PresentAsFormSheet
            };

            await Browser.OpenAsync("https://www.google.it/maps/place/Centro+Congressi+Stazione+Marittima/@40.8385488,14.2560814,662m/data=!3m2!1e3!5s0x133b08496b3bd9cd:0x304492f6f688716d!4m14!1m7!3m6!1s0x133b09d0267239b5:0xd22ea19fc7faa765!2sCentro+Congressi+Stazione+Marittima!8m2!3d40.8385488!4d14.2586617!16s%2Fg%2F11fnpzpj9t!3m5!1s0x133b09d0267239b5:0xd22ea19fc7faa765!8m2!3d40.8385488!4d14.2586617!16s%2Fg%2F11fnpzpj9t?entry=ttu&g_ep=EgoyMDI0MTExMy4xIKXMDSoASAFQAw%3D%3D", opt);*/
        }
    }
}
