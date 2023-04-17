namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ModelsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ModelsController(AppDbContext ctx) 
    {
        _context = ctx;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Model>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Model>>> GetModels()
    {
        var models = await _context.Models.ToListAsync();
        return Ok(models);
    }


    [HttpGet("{id}", Name = "GetModel")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Model), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Model>> GetModel(int id)
    {
        var model = await _context.Models
                            .FirstAsync(x => x.Id == id);

        if (model is null)
            return NotFound();

        return Ok(model);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Model), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Model>> CreateModel([FromBody] Model model)
    {
        await _context.Models.AddAsync(model);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("Getmodel", new { id = model.Id }, model);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteModel")]
    [ProducesResponseType(typeof(Model), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteModel(int id)
    {
        var model = await _context.Models.FirstAsync(x => x.Id == id);
        return Ok(_context.Models.Remove(model));
    }
}
