namespace PartsShop.Monolit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReviewsController(AppDbContext ctx)
    {
        _context = ctx;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Review>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
        var reviews = await _context.Reviews
                                .Include(x => x.User)
                                .ToListAsync();
        return Ok(reviews);
    }


    [HttpGet("{id}", Name = "GetReview")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Review), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Review>> GetReview(int id)
    {
        var review = await _context.Reviews
                            .Include(x => x.User)
                            .FirstAsync(x => x.Id == id);

        if (review is null)
            return NotFound();

        return Ok(review);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Review), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<Review>> CreateReview([FromBody] Review review)
    {
        await _context.Reviews.AddAsync(review);

        if (_context.SaveChanges() > 0)
            return CreatedAtRoute("GetReview", new { id = review.Id }, review);
        return StatusCode(500);
    }


    [HttpDelete("{id}", Name = "DeleteReview")]
    [ProducesResponseType(typeof(Review), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.Reviews.FirstAsync(x => x.Id == id);
        return Ok(_context.Reviews.Remove(review));
    }
}
