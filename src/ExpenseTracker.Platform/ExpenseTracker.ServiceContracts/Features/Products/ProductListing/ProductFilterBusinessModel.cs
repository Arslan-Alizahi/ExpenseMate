using ExpenseTracker.Common.Enums;
using ExpenseTracker.Framework;

namespace ExpenseTracker.ServiceContracts.Features.Products
{
    public class ProductFilterBusinessModel : BaseFilterBusinessObject
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public ProductStatus? ProductStatus { get; set; }
    }
}
