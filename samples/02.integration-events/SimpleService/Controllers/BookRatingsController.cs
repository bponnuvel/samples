using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleService.Infrastructure;
using SimpleService.Model;

namespace SimpleService.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/BookRatings")]
    public class BookRatingsController : Controller
    {
        private readonly AppDataContext _context;

        public BookRatingsController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/BookRatings
        [HttpGet]
        public IEnumerable<BookRating> GetBookItems()
        {
            return _context.BookRatings;
        }

        // GET: api/BookRatings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookRating([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookRating = await _context.BookRatings.SingleOrDefaultAsync(m => m.Id == id);

            if (bookRating == null)
            {
                return NotFound();
            }

            return Ok(bookRating);
        }

        // PUT: api/BookRatings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookRating([FromRoute] int id, [FromBody] BookRating bookRating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookRating.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookRating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookRatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BookRatings
        [HttpPost]
        public async Task<IActionResult> PostBookRating([FromBody] BookRating bookRating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BookRatings.Add(bookRating);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookRating", new { id = bookRating.Id }, bookRating);
        }

        // DELETE: api/BookRatings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookRating([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookRating = await _context.BookRatings.SingleOrDefaultAsync(m => m.Id == id);
            if (bookRating == null)
            {
                return NotFound();
            }

            _context.BookRatings.Remove(bookRating);
            await _context.SaveChangesAsync();

            return Ok(bookRating);
        }

        private bool BookRatingExists(int id)
        {
            return _context.BookRatings.Any(e => e.Id == id);
        }
    }
}