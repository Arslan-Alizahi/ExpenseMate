using ExpenseTracker.Client.Shared;
using ExpenseTracker.ServiceContracts.Features.__moduleNamespace__;
using ExpenseTracker.ServiceContracts.Models;
using ExpenseTracker.Framework.Extensions;

namespace ExpenseTracker.Client.Shared.Features.__moduleNamespace__;

internal class __ComponentPrefix__SelectListClientDataService : I__ComponentPrefix__SelectListDataService
{
    private readonly BaseHttpClient httpClient;

    public __ComponentPrefix__SelectListClientDataService(BaseHttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<SelectOption<__selectListDataType__>>> GetSelectListAsync(__ComponentPrefix__SelectListFilterBusinessModel filter)
    {
        return await httpClient.GetFromJsonAsync<List<SelectOption<__selectListDataType__>>>(
            $"api/__ComponentPrefix__SelectList/GetSelectList{filter.ToQueryString()}");
    }
}
