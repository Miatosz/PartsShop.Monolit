using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class Category
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public ICollection<Product> Products { get; set; } = new List<Product>();   
}
