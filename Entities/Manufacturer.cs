using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class Manufacturer
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public ICollection<Model> Models { get; set; } = new List<Model>();
}
