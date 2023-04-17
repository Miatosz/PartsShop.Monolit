namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext ctx)
    {
        _context = ctx;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _context.Users
                            .Include(x => x.Address)
                            .Include(x => x.Reviews)
                            .Include(x => x.Orders)
                            .ToListAsync();
        return Ok(users);
    }


    [HttpGet("{id}", Name = "GetUser")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users
                            .Include(x => x.Address)
                            .Include(x => x.Reviews)
                            .Include(x => x.Orders)
                            .FirstAsync(x => x.Id == id);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user)
    {
        await _context.Users.AddAsync(user);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteUser")]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FirstAsync(x => x.Id == id);
        return Ok(_context.Users.Remove(user));
    }
}
