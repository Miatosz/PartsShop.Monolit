namespace PartsShop.Monolit.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext ctx)
    {
        _context = ctx;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _context.Categories
                                .Include(x => x.Products)
                                .ToListAsync();
        return Ok(categories);
    }


    [HttpGet("{id}", Name = "GetCategory")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Categories
                            .Include(x => x.Products)
                            .FirstAsync(x => x.Id == id);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
    {
        await _context.Categories.AddAsync(category);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteCategory")]
    [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FirstAsync(x => x.Id == id);
        return Ok(_context.Categories.Remove(category));
    }
}
