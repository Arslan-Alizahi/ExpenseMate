using ExpenseTracker.MauiNativeApp.ViewModels.__moduleNamespace__;

namespace ExpenseTracker.MauiNativeApp.Views.__moduleNamespace__;

public partial class __ComponentPrefix__ListPage : ContentPage
{
    public __ComponentPrefix__ListPage(__ComponentPrefix__ListPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}