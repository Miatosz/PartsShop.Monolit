using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class User
{
    [Required]
    public int Id { get; set; }

    [Required]
    [RegularExpression("[A-Za-z]+", ErrorMessage = "Characters are not allowed.")]
    public string Username { get; set; }

    [Required]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", 
                        ErrorMessage = "Password must be at least 8 characters long, " +
                                       "have 1 uppercase and 1 lowercase letter and a number")]
    public string Password { get; set; }

    [Required]
    public Address Address { get; set; }

    [Required]
    [RegularExpression("[-A-Za-z0-9!#$%&'*+/=?^_`{|}~]+(?:\\.[-A-Za-z0-9!#$%&'*+/=" +
                       "?^_`{|}~]+)*@(?:[A-Za-z0-9](?:[-A-Za-z0-9]*[A-Za-z0-9])?\\.)+" +
                       "[A-Za-z0-9](?:[-A-Za-z0-9]*[A-Za-z0-9])?", 
                        ErrorMessage = "Characters are not allowed. Example email: thomas@gmail.com")]
    public string Email { get; set; }


    public ICollection<Review> Reviews { get; set; } = new List<Review>();


    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
