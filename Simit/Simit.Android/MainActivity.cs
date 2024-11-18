using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
//using Plugin.AzurePushNotification;
using Prism;
using Prism.Ioc;
using Simit.Core;
using Simit.Droid.Renderers;
using Simit.Droid.Services;
using Xamarin.Forms;

namespace Simit.Droid
{
    //Theme = "@style/MainTheme",
    //ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
    //                       ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    
    [Activity(
        Label = "SIMIT2024",Exported = true, Icon = "@mipmap/ic_launcher", RoundIcon = "@mipmap/ic_launcher_round", Theme = "@style/LaunchTheme",
        LaunchMode = LaunchMode.SingleTop, MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource    = Resource.Layout.Tabbar;
            ToolbarResource      = Resource.Layout.Toolbar;
            RequestedOrientation = ScreenOrientation.Portrait;

            
            base.SetTheme(Resource.Style.MainTheme);
            
            base.OnCreate(savedInstanceState);

            Forms.SetFlags(new string[] { "Brush_Experimental", "RadioButton_Experimental" });

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);
            Android.Glide.Forms.Init(this);

            LoadApplication(new App(new AndroidInitializer()));
            //AzurePushNotificationManager.ProcessIntent(this,Intent);

            RequestedOrientation = ScreenOrientation.Portrait;

            //allowing the device to change the screen orientation based on the rotation
            MessagingCenter.Subscribe<FullScreenEnabledWebViewRenderer>(this, "allowLandScapePortrait",
                sender => { RequestedOrientation = ScreenOrientation.Unspecified; });

            //during page close setting back to portrait
            MessagingCenter.Subscribe<FullScreenEnabledWebViewRenderer>(this, "preventLandScape",
                sender => { RequestedOrientation = ScreenOrientation.Portrait; });
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
                                                        Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            //AzurePushNotificationManager.ProcessIntent(this, intent);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IToastService, ToastService>();
        }
    }
}