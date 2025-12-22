using ExpenseTracker.Framework;
using ExpenseTracker.ServiceContracts.Features.Products;

namespace ExpenseTracker.ClientShared.Features.Products;

public class ProductListingOfflineDataService : IProductListingDataService
{
    public Task<PagedDataList<ProductListingBusinessModel>> GetPaginatedItemsAsync(ProductFilterBusinessModel filterViewModel)
    {
        throw new NotImplementedException();
    }
}