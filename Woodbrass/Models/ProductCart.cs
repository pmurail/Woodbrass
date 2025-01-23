using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Woodbrass.Models;

public class ProductCart
{
    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [Required]
    public int CartId { get; set; }
    public Cart Cart { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; } = 1;

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
}