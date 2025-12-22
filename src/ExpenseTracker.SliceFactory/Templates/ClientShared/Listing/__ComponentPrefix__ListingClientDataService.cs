using ExpenseTracker.Framework;
using ExpenseTracker.Framework.Extensions;
using ExpenseTracker.ClientShared;
using ExpenseTracker.ServiceContracts.Features.__moduleNamespace__;

namespace ExpenseTracker.ClientShared.Features.__moduleNamespace__;

internal class __ComponentPrefix__ListingClientDataService : I__ComponentPrefix__ListingDataService
{
    private readonly BaseHttpClient httpClient;

    public __ComponentPrefix__ListingClientDataService(BaseHttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<PagedDataList<__ComponentPrefix__ListingBusinessModel>> GetPaginatedItemsAsync(__ComponentPrefix__FilterBusinessModel filter)
    {
        return await httpClient.GetFromJsonAsync<PagedDataList<__ComponentPrefix__ListingBusinessModel>>(
            $"api/__ComponentPrefix__Listing/GetPaginatedItems{filter.ToQueryString()}");
    }
}
