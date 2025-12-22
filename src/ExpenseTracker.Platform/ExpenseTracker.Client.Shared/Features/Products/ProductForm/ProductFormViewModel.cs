using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Common.Enums;
using ExpenseTracker.Framework;

namespace ExpenseTracker.ClientShared.Features.Products;

public class ProductFormViewModel : ObservableBase
{
    [MaxLength(50)]
    public string? Id { get; set; }

    [MaxLength(450), Required]
    public string Name { get; set; } = null!;

    [MaxLength(4000)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Enter a valid product price")]
    public decimal? Price { get; set; }

    [Required(ErrorMessage = "Select a Product Status")]
    public ProductStatus? ProductStatus { get; set; }
    
    public string? ProductStatusStr
    {
        get => ProductStatus.ToString();
        set
        {
            ProductStatus = value.ToEnum<ProductStatus>();
        }
    }
}

