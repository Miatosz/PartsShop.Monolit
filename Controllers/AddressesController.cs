namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly AppDbContext _context;

    public AddressesController(AppDbContext ctx)
    {
        _context = ctx ?? throw new ArgumentNullException(nameof(ctx));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Address>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
    {
        var addresses = await _context.Addresses.ToListAsync();
        return Ok(addresses);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Address), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Address>> CreateAddress([FromBody] Address address)
    {
        await _context.Addresses.AddAsync(address);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("GetAddress", new { id = address.Id }, address);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteAddress")]
    [ProducesResponseType(typeof(Address), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var address = await _context.Addresses.FirstAsync(x => x.Id == id);
        return Ok(_context.Addresses.Remove(address));
    }
}

