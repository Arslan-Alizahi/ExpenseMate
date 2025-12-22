using ExpenseTracker.ClientShared.Features.Products;
using ExpenseTracker.Common.Enums;
using ExpenseTracker.ServiceContracts.Features.Products;
using ExpenseTracker.MauiShared; 

namespace ExpenseTracker.MauiNativeApp.Features.Products;
public partial class ProductFormPageViewModel : FormBaseMaui<ProductFormBusinessModel, ProductFormViewModel, string, IProductFormDataService>
{
    public ProductFormPageViewModel(IServiceScopeFactory scopeFactory, string id) : base(scopeFactory, id)
    {
    }
}