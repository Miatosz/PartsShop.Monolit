using System.ComponentModel.DataAnnotations;

namespace PartsShop.Monolit.Entities;

public class Basket
{
    [Required]
    public int Id { get; set; }

    [Required]
    public User User { get; set; }

    [Required]
    public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

    public Basket()
    {

    }

    public Basket(User user)
    {
        User = user;
    }

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            foreach (var item in BasketItems)
            {
                totalPrice += item.Price * item.Quantity;
            };
            return totalPrice;
        }
    }
}
