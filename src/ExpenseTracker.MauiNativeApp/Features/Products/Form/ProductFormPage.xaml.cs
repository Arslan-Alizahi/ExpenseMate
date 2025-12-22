using ExpenseTracker.MauiNativeApp.Features.Products;

namespace ExpenseTracker.MauiNativeApp.Views.Products;

public partial class ProductFormPage : ContentPage
{
    public ProductFormPage(IServiceScopeFactory scopeFactory, string id)
    {
        InitializeComponent();
        BindingContext = new ProductFormPageViewModel(scopeFactory, id);
    }
}