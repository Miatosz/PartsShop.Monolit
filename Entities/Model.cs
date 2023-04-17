using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class Model
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [RegularExpression("(?:19|20)\\d\\d$", ErrorMessage = "Characters are not allowed.")]
    public int StartProductionDate { get; set; }
    
    [Required]
    [RegularExpression("(?:19|20)\\d\\d$", ErrorMessage = "Characters are not allowed.")]
    public int EndProductionDate { get; set; }

    [Required]
    [RegularExpression("[0-9]{3,4}", ErrorMessage = "Characters are not allowed.")]
    public int EngineCapacity { get; set; }

    [Required]
    [RegularExpression("\b[(A-H|J-N|P|R-Z|0-9)]{17}\b", ErrorMessage = "Vin number is not valid")]
    public int Vin { get; set; }
}
