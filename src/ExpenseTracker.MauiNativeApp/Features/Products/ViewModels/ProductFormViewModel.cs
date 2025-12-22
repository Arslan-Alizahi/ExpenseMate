using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Common.Enums;
using ExpenseTracker.Framework;

namespace ExpenseTracker.MauiNativeApp.Features.Products.ViewModels;

public class ProductFormViewModel : ObservableBase
{
    private string _id = string.Empty;
    public string Id
    {
        get => _id;
        set { _id = value; NotifyPropertyChanged(); }
    }

    private string _name = string.Empty;
    [Required(ErrorMessage = "Product name is required")]
    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }

    private string? _description;
    public string? Description
    {
        get => _description;
        set { _description = value; NotifyPropertyChanged(); }
    }

    private decimal _price;
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price
    {
        get => _price;
        set { _price = value; NotifyPropertyChanged(); }
    }

    private ProductStatus _productStatus;
    public ProductStatus ProductStatus
    {
        get => _productStatus;
        set { _productStatus = value; NotifyPropertyChanged(); }
    }
}
