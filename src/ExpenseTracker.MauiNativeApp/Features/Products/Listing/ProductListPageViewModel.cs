using ExpenseTracker.MauiNativeApp.Features.Products.ViewModels;
using ExpenseTracker.Common.Enums;
using ExpenseTracker.MauiShared;
using ExpenseTracker.ServiceContracts.Features.Products;
using System.Windows.Input;

namespace ExpenseTracker.MauiNativeApp.Features.Products;

public partial class ProductListPageViewModel : ListingBaseMaui<ProductListingViewModel, ProductListingBusinessModel,
                                ProductFilterViewModel, ProductFilterBusinessModel, IProductListingDataService>
{
    public ProductListPageViewModel(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
        //Title = "Products";
    }

    private ICommand? _addProductCommand;
    public ICommand AddProductCommand
    {
        get
        {
            return _addProductCommand ??= new Command(async () =>
            {
                await Shell.Current.GoToAsync("//products/form/new");
            });
        }
    }

    private ICommand? _editProductCommand;
    public ICommand EditProductCommand
    {
        get
        {
            return _editProductCommand ??= new Command<ProductListingViewModel>(async (product) =>
            {
                if (product?.Id != null)
                {
                    await Shell.Current.GoToAsync($"//products/form/{product.Id}");
                }
            });
        }
    }
}