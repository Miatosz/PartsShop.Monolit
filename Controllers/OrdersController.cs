namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext ctx) 
    {
        _context = ctx;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _context.Orders
                            .Include(x => x.User)
                            .ToListAsync();
        return Ok(orders);
    }


    [HttpGet("{id}", Name = "GetOrder")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Orders
                            .Include(x => x.User)
                            .FirstAsync(x => x.Id == id);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
    {
        await _context.Orders.AddAsync(order);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteOrder")]
    [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FirstAsync(x => x.Id == id);
        return Ok(_context.Orders.Remove(order));
    }
}
