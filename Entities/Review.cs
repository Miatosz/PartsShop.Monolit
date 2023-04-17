using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class Review
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime DateAdded { get; set; }

    [Required]
    public User User { get; set; }

    [RegularExpression("[A-Za-z+łóęąśłńćźż]{1,200}", ErrorMessage = "A comment can only have 200 characters")]
    public string Content { get; set; }

    [Required]
    public int NumberOfStars { get; set; }
}
