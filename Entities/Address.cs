using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class Address
{
    [Required]
    public int Id { get; set; }

    [Required]
    [RegularExpression("[A-Za-z+łóęąśłńćźż]{5,30}", ErrorMessage = "Characters are not allowed.")]
    public string Street { get; set; }

    [Required]
    [RegularExpression("[0-9]{1,4}", ErrorMessage = "Letters are not allowed.")]
    public int HouseNumber { get; set; }

    [RegularExpression("[A-Za-z+łóęąśłńćźż]{3,30}", ErrorMessage = "Characters are not allowed.")]
    [Required]
    public string City { get; set; }

    [Required]
    [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Example: 10-221")]
    public string PostalCode { get; set; }

    [RegularExpression("[A-Za-z+łóęąśłńćźż]{3,30}", ErrorMessage = "Characters are not allowed.")]
    [Required]
    public string Country { get; set; }

}
