namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CarsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CarsController(AppDbContext ctx)
    {
        _context = ctx;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Car>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Car>>> GetCars()
    {
        var cars = await _context.Cars
                            .Include(x => x.Manufacturer)
                            .ThenInclude(x => x.Models)
                            .ToListAsync();
        return Ok(cars);
    }


    [HttpGet("{id}", Name = "GetCar")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Car), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Car>> GetCar(int id)
    {
        var car = await _context.Cars
                            .Include(x => x.Manufacturer)
                            .ThenInclude(x => x.Models)
                            .FirstAsync(x => x.Id == id);

        if (car is null)
            return NotFound();

        return Ok(car);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Car), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Car>> CreateCar([FromBody] Car car)
    {
        await _context.Cars.AddAsync(car);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("GetCar", new { id = car.Id }, car);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteCar")]
    [ProducesResponseType(typeof(Car), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var car = await _context.Cars.FirstAsync(x => x.Id == id);
        return Ok(_context.Cars.Remove(car));
    }
}
