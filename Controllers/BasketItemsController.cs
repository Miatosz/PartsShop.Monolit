namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BasketItemsController(AppDbContext ctx)
    {
        _context = ctx;
    }
}
