using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudService.Infrastructure;
using CrudService.Infrastructure.IntegrationEvents.Events;
using CrudService.Model;
using HooliServices.BuildingBlocks.EventBus.Abstractions;

namespace CrudService.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Books")]
    public class BooksController : Controller
    {
        private readonly AppDataContext _context;
        private readonly IEventBus _eventBus;
        public BooksController(AppDataContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        // GET: api/Books
        [HttpGet]
        public IEnumerable<BookItem> GetBookItems()
        {
            return _context.BookItems;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookItem = await _context.BookItems.SingleOrDefaultAsync(m => m.Id == id);

            if (bookItem == null)
            {
                return NotFound();
            }

            return Ok(bookItem);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookItem([FromRoute] int id, [FromBody] BookItem bookItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookItemExists(id))
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

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> PostBookItem([FromBody] BookItem bookItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BookItems.Add(bookItem);
            await _context.SaveChangesAsync();

            // Publishing events
            var eventMessage = new BookAddedIntegrationEvent(bookItem.Id, bookItem.Author, bookItem.Title, bookItem.YearofPublish);
            _eventBus.Publish(eventMessage);

            return CreatedAtAction("GetBookItem", new { id = bookItem.Id }, bookItem);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookItem = await _context.BookItems.SingleOrDefaultAsync(m => m.Id == id);
            if (bookItem == null)
            {
                return NotFound();
            }

            _context.BookItems.Remove(bookItem);
            await _context.SaveChangesAsync();

            return Ok(bookItem);
        }

        private bool BookItemExists(int id)
        {
            return _context.BookItems.Any(e => e.Id == id);
        }
    }
}