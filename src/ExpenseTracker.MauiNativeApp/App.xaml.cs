using ExpenseTracker.MauiNativeApp.Views;

namespace ExpenseTracker.MauiNativeApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        
        MainPage = new AppShellTabs();
        

        
    }
}