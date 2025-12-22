using ExpenseTracker.MauiNativeApp.ViewModels;

namespace ExpenseTracker.MauiNativeApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}