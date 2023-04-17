using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class Order
{
    [Required]
    public int Id { get; set; }

    [Required]
    public User User { get; set; }

    [Required]
    public bool IsCompleted { get; set; }

    [Required]
    public Basket Basket { get; set; }
}
