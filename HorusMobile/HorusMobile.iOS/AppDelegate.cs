using Com.OneSignal;
using Foundation;
using UIKit;

namespace HorusMobile.iOS
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
            global::Xamarin.Forms.Forms.Init();
            //FFImageLoading
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            LoadApplication(new App());
            OneSignal.Current.StartInit("5bd931bc-a426-44ec-85a5-bfd47a771213")
                  .EndInit();

            return base.FinishedLaunching(app, options);
        }
        [Export("oneSignalApplicationDidBecomeActive:")]
        public void OneSignalApplicationDidBecomeActive(UIApplication application)
        {
            System.Console.WriteLine("oneSignalApplicationDidBecomeActive:");
        }
    }
}
