using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseTracker.MauiNativeApp.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    string title = string.Empty;
}