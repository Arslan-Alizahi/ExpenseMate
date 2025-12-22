using ExpenseTracker.Common.Enums;

namespace ExpenseTracker.MauiNativeApp.Features.Products.ViewModels;

public class ProductListingViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public ProductStatus ProductStatus { get; set; }
}
