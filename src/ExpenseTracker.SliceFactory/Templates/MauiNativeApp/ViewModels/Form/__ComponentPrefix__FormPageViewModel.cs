using ExpenseTracker.ClientShared.Features.__moduleNamespace__;
using ExpenseTracker.MauiShared;
using ExpenseTracker.ServiceContracts.Features.__moduleNamespace__;

namespace ExpenseTracker.MauiNativeApp.ViewModels.__moduleNamespace__;

public partial class __ComponentPrefix__FormPageViewModel : FormBaseMaui<__ComponentPrefix__FormViewModel, __ComponentPrefix__FormBusinessModel, I__ComponentPrefix__FormDataService>
{
    public __ComponentPrefix__FormPageViewModel(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
        Title = "__ComponentPrefix__ Details";
    }
}