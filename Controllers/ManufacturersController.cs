namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ManufacturersController : ControllerBase
{
    private readonly AppDbContext _context;

    public ManufacturersController(AppDbContext ctx) 
    {
        _context = ctx;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Manufacturer>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
    {
        var manufacturers = await _context.Manufacturers
                                    .Include(x => x.Models)
                                    .ToListAsync();
        return Ok(manufacturers);
    }


    [HttpGet("{id}", Name = "GetManufacturer")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Manufacturer), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Manufacturer>> GetManufacturer(int id)
    {
        var manufacturer = await _context.Manufacturers
                            .Include(x => x.Models)
                            .FirstAsync(x => x.Id == id);

        if (manufacturer is null)
            return NotFound();

        return Ok(manufacturer);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Manufacturer), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Manufacturer>> CreateManufacturer([FromBody] Manufacturer manufacturer)
    {
        await _context.Manufacturers.AddAsync(manufacturer);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("GetManufacturer", new { id = manufacturer.Id }, manufacturer);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteManufacturer")]
    [ProducesResponseType(typeof(Manufacturer), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteManufacturer(int id)
    {
        var manufacturer = await _context.Manufacturers.FirstAsync(x => x.Id == id);
        return Ok(_context.Manufacturers.Remove(manufacturer));
    }
}
