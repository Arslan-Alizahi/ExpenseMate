using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Framework;

namespace ExpenseTracker.ClientShared.Features.__moduleNamespace__;

public class __ComponentPrefix__FormViewModel : ObservableBase
{
    [MaxLength(50)]
    public __primaryKeyType__? Id { get; set; }

    [Required, MaxLength(450)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(4000)]
    public string? Description { get; set; }

    // Add other properties as needed based on your entity
}
