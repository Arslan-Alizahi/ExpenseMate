using ExpenseTracker.ClientShared.Features.__moduleNamespace__;
using ExpenseTracker.MauiShared;
using ExpenseTracker.ServiceContracts.Features.__moduleNamespace__;

namespace ExpenseTracker.MauiNativeApp.ViewModels.__moduleNamespace__;

public partial class __ComponentPrefix__ListPageViewModel : ListingBaseMaui<__ComponentPrefix__ListingViewModel, __ComponentPrefix__ListingBusinessModel,
                                __ComponentPrefix__FilterViewModel, __ComponentPrefix__FilterBusinessModel, I__ComponentPrefix__ListingDataService>
{
    public __ComponentPrefix__ListPageViewModel(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
        Title = "__ComponentPrefix__s";
    }
}