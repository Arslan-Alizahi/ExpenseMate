using ExpenseTracker.MauiNativeApp.ViewModels.__moduleNamespace__;

namespace ExpenseTracker.MauiNativeApp.Views.__moduleNamespace__;

public partial class __ComponentPrefix__FormPage : ContentPage
{
    public __ComponentPrefix__FormPage(__ComponentPrefix__FormPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}