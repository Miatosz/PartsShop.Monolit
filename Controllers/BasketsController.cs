namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BasketsController(AppDbContext ctx)
    {
        _context = ctx;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Basket>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Basket>>> GetBaskets()
    {
        var baskets = await _context.Baskets.Include(x => x.BasketItems).ToListAsync();
        return Ok(baskets);
    }


    [HttpGet("{id}", Name = "GetBasket")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Basket>> GetBasket(int id)
    {
        var basket = await _context.Baskets
                            .Include(x => x.BasketItems)
                            .FirstAsync(x => x.Id == id);

        if (basket is null)
            return NotFound();

        return Ok(basket);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Basket>> CreateBasket([FromBody] Basket basket)
    {
        await _context.Baskets.AddAsync(basket);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("GetBasket", new { id = basket.Id }, basket);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(int id)
    {
        var basket = await _context.Baskets.FirstAsync(x => x.Id == id);
        return Ok(_context.Baskets.Remove(basket));
    }
}
