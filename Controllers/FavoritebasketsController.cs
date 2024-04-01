using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Models;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritebasketsController : ControllerBase
    {
        private readonly Pizza_DBContext _context;

        public FavoritebasketsController(Pizza_DBContext context)
        {
            _context = context;
        }

        // GET: api/Favoritebaskets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favoritebasket>>> GetFavoritebaskets()
        {
          if (_context.Favoritebaskets == null)
          {
              return NotFound();
          }
            return await _context.Favoritebaskets.ToListAsync();
        }

        // GET: api/Favoritebaskets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favoritebasket>> GetFavoritebasket(int id)
        {
          if (_context.Favoritebaskets == null)
          {
              return NotFound();
          }
            var favoritebasket = await _context.Favoritebaskets.FindAsync(id);

            if (favoritebasket == null)
            {
                return NotFound();
            }

            return favoritebasket;
        }

        // PUT: api/Favoritebaskets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoritebasket(int id, Favoritebasket favoritebasket)
        {
            if (id != favoritebasket.IdFavoritebasket)
            {
                return BadRequest();
            }

            _context.Entry(favoritebasket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoritebasketExists(id))
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

        // POST: api/Favoritebaskets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Favoritebasket>> PostFavoritebasket(Favoritebasket favoritebasket)
        {
          if (_context.Favoritebaskets == null)
          {
              return Problem("Entity set 'Pizza_DBContext.Favoritebaskets'  is null.");
          }
            _context.Favoritebaskets.Add(favoritebasket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavoritebasket", new { id = favoritebasket.IdFavoritebasket }, favoritebasket);
        }

        // DELETE: api/Favoritebaskets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoritebasket(int id)
        {
            if (_context.Favoritebaskets == null)
            {
                return NotFound();
            }
            var favoritebasket = await _context.Favoritebaskets.FindAsync(id);
            if (favoritebasket == null)
            {
                return NotFound();
            }

            _context.Favoritebaskets.Remove(favoritebasket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoritebasketExists(int id)
        {
            return (_context.Favoritebaskets?.Any(e => e.IdFavoritebasket == id)).GetValueOrDefault();
        }
    }
}
