using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Models;
using PizzaApi.Utils.Model;
using PizzaApi.Utils;
using System.Collections;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly Pizza_DBContext _context;

        public FavoritesController(Pizza_DBContext context)
        {
            _context = context;
        }

        // GET: api/Pizza
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GraphFavorite>>> GetFavorites()
        {
          if (_context.Favorites == null)
          {
              return NotFound();
          }
            List<GraphFavorite> list = new List<GraphFavorite>();
          var favorites = await _context.Favorites.ToListAsync();
            foreach (var item in favorites)
            {
                Product furniture = await _context.Products.FirstOrDefaultAsync(u => u.IdProduct == item.ProductId);
                if (list.Any(item => item.name == furniture.NameProduct))
                {
                    var obj = list.FirstOrDefault(x => x.name == furniture.NameProduct);
                    obj.count++;
                }
                else
                {
                    list.Add(new GraphFavorite(furniture.NameProduct, 1));
                }
            }

            return list;
        }

        // GET: api/Favorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetFavorite(int id)
        {
          if (_context.Favorites == null)
          {
              return NotFound();
          }
            var favorite = await _context.Favorites.FindAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
        }

        // PUT: api/Favorites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorite(int id, Favorite favorite)
        {
            if (id != favorite.IdFavorite)
            {
                return BadRequest();
            }

            _context.Entry(favorite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteExists(id))
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

        // POST: api/Favorites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Favorite>> PostFavorite(FavoriteItem favoriteItem)
        {
          if (_context.Favorites == null)
          {
              return Problem("Entity set 'Furniture_DBContext.Favorites'  is null.");
          }
            int idMebel = Int16.Parse(favoriteItem.idMebel);
            int idUser = favoriteItem.idUser;
            Favorite favorite1 = await _context.Favorites.FirstOrDefaultAsync(u => u.ProductId == idMebel && u.AppuserId == idUser);
            if(favorite1 != null)
            {
                return Ok();
            }
            Favorite favorite = new Favorite();
            favorite.AppuserId = idUser;
            favorite.ProductId = idMebel;
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavorite", new { id = favorite.IdFavorite}, favorite);
        }

        [HttpPost("add/{idMebel}/{idUser}")]
        public async Task<ActionResult<Favorite>> PostFavoriteAdd(int idMebel, int idUser)
        {
            if (_context.Favorites == null)
            {
                return Problem("Entity set 'Furniture_DBContext.Favorites'  is null.");
            }
            Favorite favorite1 = await _context.Favorites.FirstOrDefaultAsync(u => u.ProductId == idMebel && u.AppuserId == idUser);
            if (favorite1 != null)
            {
                return Ok();
            }
            Favorite favorite = new Favorite();
            favorite.AppuserId = idUser;
            favorite.ProductId = idMebel;
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavorite", new { id = favorite.IdFavorite }, favorite);
        }

        // DELETE: api/Favorites/5
        [HttpDelete("{id}/{idUser}")]
        public async Task<IActionResult> DeleteFavorite(int id, int idUser)
        {
            if (_context.Favorites == null)
            {
                return NotFound();
            }
            Favorite favorite = await _context.Favorites.FirstOrDefaultAsync(u => u.ProductId == id && u.AppuserId == idUser);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteExists(int id)
        {
            return (_context.Favorites?.Any(e => e.IdFavorite == id)).GetValueOrDefault();
        }
    }
}
