using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Models;
using PizzaApi.Utils.Model;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaExtrasController : ControllerBase
    {
        private readonly Pizza_DBContext _context;

        public PizzaExtrasController(Pizza_DBContext context)
        {
            _context = context;
        }

        // GET: api/PizzaExtras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaExtrasDto>>> GetPizzaExtras()
        {
          if (_context.PizzaExtras == null)
          {
              return NotFound();
          }
          List<PizzaExtrasDto> pizzaExtrasDto = new List<PizzaExtrasDto>();
            foreach (var item in _context.PizzaExtras)
            {
                PizzaExtrasDto dto = new PizzaExtrasDto();
                dto.ImageLink = item.ImageLink;
                dto.NameExtras = item.NameExtras;
                dto.Price = item.Price;
                pizzaExtrasDto.Add(dto);
            }
            return pizzaExtrasDto;
        }

        // GET: api/PizzaExtras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaExtra>> GetPizzaExtra(int id)
        {
          if (_context.PizzaExtras == null)
          {
              return NotFound();
          }
            var pizzaExtra = await _context.PizzaExtras.FindAsync(id);

            if (pizzaExtra == null)
            {
                return NotFound();
            }

            return pizzaExtra;
        }

        // PUT: api/PizzaExtras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaExtra(int id, PizzaExtra pizzaExtra)
        {
            if (id != pizzaExtra.IdPizzaExtras)
            {
                return BadRequest();
            }

            _context.Entry(pizzaExtra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExtraExists(id))
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

        // POST: api/PizzaExtras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PizzaExtra>> PostPizzaExtra(PizzaExtra pizzaExtra)
        {
          if (_context.PizzaExtras == null)
          {
              return Problem("Entity set 'Pizza_DBContext.PizzaExtras'  is null.");
          }
            _context.PizzaExtras.Add(pizzaExtra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizzaExtra", new { id = pizzaExtra.IdPizzaExtras }, pizzaExtra);
        }

        // DELETE: api/PizzaExtras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizzaExtra(int id)
        {
            if (_context.PizzaExtras == null)
            {
                return NotFound();
            }
            var pizzaExtra = await _context.PizzaExtras.FindAsync(id);
            if (pizzaExtra == null)
            {
                return NotFound();
            }

            _context.PizzaExtras.Remove(pizzaExtra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExtraExists(int id)
        {
            return (_context.PizzaExtras?.Any(e => e.IdPizzaExtras == id)).GetValueOrDefault();
        }

        [HttpPut("put/{oldName}")]
        public async Task<ActionResult<PizzaExtra>> PutPizzaExtra(string oldName, PizzaExtrasDto pizzaExtrasDto)
        {
            PizzaExtra pizzaExtra = await _context.PizzaExtras.FirstOrDefaultAsync(u => u.NameExtras == oldName);
            if (pizzaExtra == null)
            {
                return BadRequest();
            }
            pizzaExtra.NameExtras = pizzaExtrasDto.NameExtras;
            pizzaExtra.Price = pizzaExtrasDto.Price;
            pizzaExtra.ImageLink = pizzaExtrasDto.ImageLink;

            _context.Update(pizzaExtra);
            await _context.SaveChangesAsync();
            return Ok(pizzaExtra);
        }

        // POST: api/CategoryProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("add")]
        public async Task<ActionResult<PizzaExtra>> PostCategoryProduct(PizzaExtrasDto pizzaExtrasDto)
        {
            if (_context.PizzaExtras == null)
            {
                return Problem("Entity set 'Product_DBContext.CategoryProducts'  is null.");
            }
            PizzaExtra pizzaExtra = new PizzaExtra();
            pizzaExtra.NameExtras = pizzaExtrasDto.NameExtras;
            pizzaExtra.ImageLink = pizzaExtrasDto.ImageLink;
            pizzaExtra.Price = pizzaExtrasDto.Price;
            pizzaExtra.Quantity = 5;
            _context.PizzaExtras.Add(pizzaExtra);
            await _context.SaveChangesAsync();

            return Ok(pizzaExtra);
        }

        [HttpGet("del/{name}")]
        public async Task<IActionResult> DeleteCat(string name)
        {
            if (_context.PizzaExtras == null)
            {
                return NotFound();
            }
            PizzaExtra pizzaExtra = await _context.PizzaExtras.FirstOrDefaultAsync(u => u.NameExtras == name);
            if (pizzaExtra == null)
            {
                return NotFound();
            }

            _context.PizzaExtras.Remove(pizzaExtra);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
