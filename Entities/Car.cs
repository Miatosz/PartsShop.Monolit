using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class Car
{
    [Required]
    public int Id { get; set; }

    [Required]
    public Manufacturer Manufacturer { get; set; }
    //public Model Model { get; set; }
}
