using Foundation;
using ExpenseTracker.MauiNativeApp;
namespace ExpenseTracker.HybridApp
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
