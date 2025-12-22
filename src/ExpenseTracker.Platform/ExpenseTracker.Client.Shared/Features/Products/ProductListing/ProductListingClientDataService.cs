using ExpenseTracker.Framework;
using ExpenseTracker.Framework.Extensions;
using ExpenseTracker.ServiceContracts.Features.Products;

namespace ExpenseTracker.ClientShared.Features.Products;

internal class ProductListingClientDataService : IProductListingDataService
{
    private readonly BaseHttpClient httpClient;

    public ProductListingClientDataService(BaseHttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<PagedDataList<ProductListingBusinessModel>> GetPaginatedItemsAsync(ProductFilterBusinessModel filterViewModel)
    {
        return await httpClient.GetFromJsonAsync<PagedDataList<ProductListingBusinessModel>>("api/productListing/GetPaginatedItems" + filterViewModel.ToQueryString());
    }
}

