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
    public class CategoryPizzasController : ControllerBase
    {
        private readonly Pizza_DBContext _context;

        public CategoryPizzasController(Pizza_DBContext context)
        {
            _context = context;
        }

        // GET: api/CategoryProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryProduct>>> GetCategoryProducts()
        {
          if (_context.CategoryProducts == null)
          {
              return NotFound();
          }
            return await _context.CategoryProducts.ToListAsync();
        }

        // GET: api/CategoryProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryProduct>> GetCategoryProduct(int id)
        {
          if (_context.CategoryProducts == null)
          {
              return NotFound();
          }
            var categoryProduct = await _context.CategoryProducts.FindAsync(id);

            if (categoryProduct == null)
            {
                return NotFound();
            }

            return categoryProduct;
        }

        [HttpGet("cat")]
        public async Task<ActionResult<IEnumerable<String>>> GetCatCategoryProduct(int id)
        {
            if (_context.CategoryProducts == null)
            {
                return NotFound();
            }
            var categoryProduct = await _context.CategoryProducts.ToListAsync();
            List<string> result = new List<string>();

            foreach(var data in categoryProduct.ToArray())
            {
                result.Add(data.NameCategory);
            }

            return result;
        }

        // PUT: api/CategoryProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}/{oldName}")]
        public async Task<ActionResult<CategoryProduct>> PutFurnitureSait(string name, string oldName)
        {
            CategoryProduct categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(u => u.NameCategory == oldName);
            if (categoryProduct == null)
            {
                return BadRequest();
            }
            categoryProduct.NameCategory = name;

            _context.Update(categoryProduct);
            await _context.SaveChangesAsync();
            return Ok(categoryProduct);
        }

        // POST: api/CategoryProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{name}")]
        public async Task<ActionResult<CategoryProduct>> PostCategoryProduct(string name)
        {
          if (_context.CategoryProducts == null)
          {
              return Problem("Entity set 'Product_DBContext.CategoryProducts'  is null.");
          }
            CategoryProduct categoryProduct = new CategoryProduct();
            categoryProduct.NameCategory = name;
            _context.CategoryProducts.Add(categoryProduct);
            await _context.SaveChangesAsync();

            return Ok(categoryProduct);
        }

        [HttpDelete("del/{id}")]
        public async Task<IActionResult> DeleteCat(string id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            CategoryProduct categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(u => u.NameCategory == id);
            if (categoryProduct == null)
            {
                return NotFound();
            }

            _context.CategoryProducts.Remove(categoryProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/CategoryProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryProduct(int id)
        {
            if (_context.CategoryProducts == null)
            {
                return NotFound();
            }
            var categoryProduct = await _context.CategoryProducts.FindAsync(id);
            if (categoryProduct == null)
            {
                return NotFound();
            }

            _context.CategoryProducts.Remove(categoryProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryProductExists(int id)
        {
            return (_context.CategoryProducts?.Any(e => e.IdCategoryProduct == id)).GetValueOrDefault();
        }
    }
}
