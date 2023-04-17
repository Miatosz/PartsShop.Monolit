using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class BasketItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public Product Product { get; set; }
}
