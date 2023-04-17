namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext ctx)
    {
        _context = ctx;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _context.Products
                                .Include(x => x.Category)
                                .ToListAsync();
        return Ok(products);
    }


    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products
                            .Include(x => x.Category)
                            .FirstAsync(x => x.Id == id);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        await _context.Products.AddAsync(product);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FirstAsync(x => x.Id == id);
        return Ok(_context.Products.Remove(product));
    }
}
