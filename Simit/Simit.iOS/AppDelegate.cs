using Foundation;
//using Plugin.AzurePushNotification;
using Prism;
using Prism.Ioc;
using Rg.Plugins.Popup;
using UIKit;

namespace Simit.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
	        Xamarin.Forms.Forms.SetFlags(new string[] { "Brush_Experimental", "RadioButton_Experimental" });
            global::Xamarin.Forms.Forms.Init();

            Popup.Init();
            Xamarin.Forms.Nuke.FormsHandler.Init(debug: false);
            Sharpnado.Shades.iOS.iOSShadowsRenderer.Initialize();
            LoadApplication(new App(new iOSInitializer()));

            //AzurePushNotificationManager.CurrentNotificationPresentationOption = UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Sound;

            //AzurePushNotificationManager.Initialize("Endpoint=sb://congressi-nadirex-hub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=M7byCHSA6rvDshQ81Q0DvkVoZETs1DNH+OslC0/m2s8=","simit", options);

            return base.FinishedLaunching(app, options);
        }

        public override void WillEnterForeground(UIApplication uiApplication)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }

       /* public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            AzurePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            AzurePushNotificationManager.RemoteNotificationRegistrationFailed(error);

        }
        // To receive notifications in foreground on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            AzurePushNotificationManager.DidReceiveMessage(userInfo);
        }*/
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
