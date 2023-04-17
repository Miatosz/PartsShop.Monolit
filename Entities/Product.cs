using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class Product
{
    [Required]
    public int Id { get; set; }

    [Required]
    [RegularExpression("[A-Za-z]+", ErrorMessage = "Characters are not allowed.")]
    public string Name { get; set; }

    [Required]
    [RegularExpression("[A-Za-z0-9łóęąśłńćźż]+", ErrorMessage = "Characters are not allowed.")]
    public string? Description { get; set; }

    [Required]
    [RegularExpression("[0-9]*\\.[0-9]+", ErrorMessage = "Characters are not allowed.")]
    public decimal Price { get; set; }

    [Required]
    public bool IsAvailable { get; set; }

    [Required]
    [RegularExpression("/\r\n^0*?[1-9]\\d*$\r\n/", ErrorMessage = "Quantity cannot be less than 0")]
    public int QuantityInStock { get; set; }

    [Required]
    public Category Category { get; set; }

    [Required]
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    [Required]
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

}
