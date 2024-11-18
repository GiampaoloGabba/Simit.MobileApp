using System;
using Android.App;
using Android.OS;
using Android.Runtime;
//using Plugin.AzurePushNotification;

namespace Simit.Droid
{
    [Application(
        Theme = "@style/MainTheme"
        )]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                //AzurePushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

                //Change for your default notification channel name here
                //AzurePushNotificationManager.DefaultNotificationChannelName = "General";
            }

            var restToken = false;

#if DEBUG
            restToken = true;
#endif

           /* AzurePushNotificationManager.Initialize(this,
                "Endpoint=sb://congressi-nadirex-hub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=M7byCHSA6rvDshQ81Q0DvkVoZETs1DNH+OslC0/m2s8=",
                "simit",
                restToken);*/

            Xamarin.Essentials.Platform.Init(this);

            //Handle notification when app is closed here
            /*CrossAzurePushNotification.Current.OnNotificationReceived += (s, p) =>
            {


            }; */
        }
    }
}
