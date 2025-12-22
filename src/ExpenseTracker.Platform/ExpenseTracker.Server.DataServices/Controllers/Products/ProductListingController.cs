using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Framework;
using ExpenseTracker.ServiceContracts.Features.Products;

namespace ExpenseTracker.Server.DataServices.Controllers.Products;

[ApiController, Route("api/[controller]/[action]")]
public class ProductListingController : ControllerBase, IProductListingDataService
{
    private readonly IProductListingDataService dataService;

    public ProductListingController(IProductListingDataService dataService)
    {
        this.dataService = dataService;
    }

    [HttpGet]
    public async Task<PagedDataList<ProductListingBusinessModel>> GetPaginatedItemsAsync([FromQuery] ProductFilterBusinessModel filterViewModel)
    {
        return await dataService.GetPaginatedItemsAsync(filterViewModel);
    }
}