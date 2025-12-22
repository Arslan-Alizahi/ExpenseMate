using ExpenseTracker.Common.Enums;
using ExpenseTracker.Framework;

namespace ExpenseTracker.ClientShared.Features.Products;

public class ProductFilterViewModel : BaseFilterViewModel
{
    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public ProductStatus? ProductStatus { get; set; }
}
