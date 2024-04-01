using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApi.Models;
using System.Globalization;
using System.Text.Json.Nodes;
using PizzaApi.Utils.Model;
using PizzaApi.Utils;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Cors;
using System.Reflection;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly Pizza_DBContext _context;

        public PizzaController(Pizza_DBContext context)
        {
            _context = context;
        }

        [HttpPost("addd/{name}/{description}/{link}/{price}")]
        public async Task<ActionResult> PostFurnitureAAA(string name, string description, string link, int price)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            Image image = new Image();
            image.Linkimage = link;
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            var furniture = new Product();
            furniture.Quantity = 0;
            furniture.NameProduct = name;
            furniture.DescriptionProduct = description;
            furniture.MiniDescriptionProduct = description;
            furniture.Price = price;
            furniture.ImageId = image.IdImage;
            furniture.CategoryProductId = 1;
            _context.Products.Add(furniture);
            await _context.SaveChangesAsync();
            return Ok(furniture);
        }



        // GET: api/Pizzas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetFurnitures()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            return await _context.Products.ToListAsync();
        }
        // GET: api/Pizzas/sort
        [HttpGet("/sort")]
        public async Task<ActionResult<IEnumerable<Mebel>>> GetSort(string? search, string sortBy, string order, string? category, [FromQuery] PaginationFilter filter)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            List<Mebel> result = new List<Mebel>();
            IQueryable<Product> data;
            if (category != null)
            {
                CategoryProduct categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(x => x.NameCategory == category);
                data = _context.Products.Where(p => p.CategoryProductId == categoryProduct.IdCategoryProduct)
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize);
            }
            else
            {
                data = _context.Products
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize);
            }
            Console.WriteLine(data.ToArray().Length + " SIZEEEE");
            /*var data = _context.Pizzas
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize);*/
            if (sortBy == "price")
            {
                if (order == "asc")
                {
                    data = data.OrderByDescending(p => p.Price);
                }
                else
                {
                    data = data.OrderBy(p => p.Price); //Возрастанию
                }
            }
            if(sortBy == "title")
            {
                if (order == "asc")
                {
                    data = data.OrderByDescending(p => p.NameProduct);
                }
                else
                {
                    data = data.OrderBy(p => p.NameProduct); //Возрастанию
                }
            }
            if(search != "" && search != null)
            {
                data = data.Where(p => p.NameProduct.ToLower().Contains(search.ToLower()));
            }
            /*if (category != null)
            {
                var category_data = _context.Products.Where(p => p.CategoryProductId == category)
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize);

                //var category_id = category_data.Select(t => t.CategoryFurnitureId).Where(p => p == category);
                data = data.Where(p => p.CategoryProductId == category);
            }*/
            foreach (var item in data.ToArray())
            {
                Image image = await _context.Images.FindAsync(item.ImageId);
                /*Manufacturer manufacturer = await _context.Manufacturers.FindAsync(item.ManufacturerId);
                RatingManufacturer rating = await _context.RatingManufacturers.FindAsync(manufacturer.RatingManufacturerId);*/
                CategoryProduct categoryProduct = await _context.CategoryProducts.FindAsync(item.CategoryProductId);
                result.Add(ToMebel.ToMebelGo(item.IdProduct.ToString(), image.Linkimage, item.NameProduct, item.Price, 5, item.Typef, item.DescriptionProduct, categoryProduct.NameCategory));
            }

            return result;
        }

        // GET: api/Pizzas/5
        [HttpGet("{id}")]
        public async Task<Mebel> GetFurniture(int id)
        {
            var item = await _context.Products.FindAsync(id);

            Image image = await _context.Images.FindAsync(item.ImageId);
            CategoryProduct categoryProduct = await _context.CategoryProducts.FindAsync(item.CategoryProductId);

            return ToMebel.ToMebelGo(item.IdProduct.ToString(), image.Linkimage, item.NameProduct, item.Price, 5, item.Typef, item.DescriptionProduct, categoryProduct.NameCategory);

        }

        // PUT: api/Pizzas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFurniture(int id, Product furniture)
        {
            if (id != furniture.IdProduct)
            {
                return BadRequest();
            }

            _context.Entry(furniture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FurnitureExists(id))
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
        [HttpPut("fur/{id}")]
        public async Task<IActionResult> PutNewFurniture(int id, FurTwo furTwo)
        {
            Product furniture = await _context.Products.FirstOrDefaultAsync(u => u.IdProduct == id);
            if (furniture == null)
            {
                return BadRequest();
            }
            furniture.DescriptionProduct = furTwo.Description;
            furniture.NameProduct = furTwo.Name;
            furniture.Price = furTwo.Price;

            Image image = await _context.Images.FirstOrDefaultAsync(u => u.IdImage == furniture.ImageId);
            image.Linkimage = furTwo.Image;

            _context.Update(image);
            _context.Update(furniture);

            await _context.SaveChangesAsync();


            return NoContent();
        }

        // PUT: api/Pizzas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("sait")]
        public async Task<IActionResult> PutFurnitureSait(Mebel mebel)
        {
            Furniture furniture = await _context.Pizzas.FirstOrDefaultAsync(u => u.IdFurniture == int.Parse(mebel.id));
            if (null == furniture)
            {
                return BadRequest();
            }

            Furniture furnew = ToFur.ToFurGo(mebel, furniture);

            try
            {
                await _context.Pizzas.AddAsync(furnew);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FurnitureExists(furniture.IdFurniture))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        [HttpPut("sait")]
        public async Task<ActionResult<Product>> PutFurnitureSait(int id, string title, string description, int price)
        {
            Product furniture = await _context.Products.FirstOrDefaultAsync(u => u.IdProduct == id);
            if(furniture == null)
            {
                return BadRequest();
            }
            furniture.NameProduct = title;
            furniture.DescriptionProduct = title;
            furniture.Price = price;

            _context.Update(furniture);
            await _context.SaveChangesAsync();
            return Ok(furniture);
        }

        // POST: api/Pizzas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostFurniture(Product furniture)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'Furniture_DBContext.Pizzas'  is null.");
          }
            _context.Products.Add(furniture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFurniture", new { id = furniture.IdProduct }, furniture);
        }

        [HttpPost("buy/{login}")]
        public async Task<ActionResult<Product>> PostFurnitureBuy(Buy[] buys, string login)
        {
            Console.WriteLine(buys);
            int fullPrice = 0;
            Appuser appuser = await _context.Appusers.FirstOrDefaultAsync(u => u.Login == login);
            if (appuser == null)
            {
                return BadRequest();
            }
            foreach (Buy buy in buys)
            {
                fullPrice += buy.price;
            }
            appuser.Balance -= fullPrice;
            _context.Appusers.Update(appuser);
            History history = new History();
            history.PriceHistory = fullPrice;
            history.AppuserId = appuser.IdAppuser;
            history.BasketId = 1;
            history.DataBuy = "Покупка на сумму товара - "+fullPrice+ " купил - "+ appuser.Login;
            _context.Histories.Add(history);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("buyApp/{id}/{cost}")]
        public async Task<ActionResult<Product>> PostFurnitureBuyApp(int id, int cost)
        {
            int fullPrice = 0;
            Appuser appuser = await _context.Appusers.FindAsync(id);
            if (appuser == null)
            {
                return BadRequest();
            }
            appuser.Balance -= cost;
            _context.Appusers.Update(appuser);
            History history = new History();
            history.PriceHistory = fullPrice;
            history.AppuserId = appuser.IdAppuser;
            history.BasketId = 1;
            history.DataBuy = "Покупка на сумму товара - " + fullPrice + " купил - " + appuser.Login;
            _context.Histories.Add(history);
            await _context.SaveChangesAsync();
            return Ok(appuser);
        }
        //[EnableCors("AllowAnyOrigin")]
        // POST: api/Pizzas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("add/{category}/{type}")]
        public async Task<ActionResult> PostFurnitureTwo(FurTwo furTwo, string category, int type)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            CategoryProduct categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(u => u.NameCategory == category);
            if (categoryProduct == null)
            {
                return NotFound();
            }
            Image image = new Image();
            image.Linkimage = furTwo.Image;
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            var furniture = new Product();
            furniture.Quantity = 0;
            furniture.NameProduct = furTwo.Name;
            furniture.DescriptionProduct = furTwo.Description;
            furniture.MiniDescriptionProduct = furTwo.Description;
            furniture.Price = furTwo.Price;
            furniture.ImageId = image.IdImage;
            furniture.Typef = type;
            furniture.CategoryProductId = categoryProduct.IdCategoryProduct;
            _context.Products.Add(furniture);
            await _context.SaveChangesAsync();
            return Ok(furTwo);
        }

        [HttpPost("bla")]
        public async Task<ActionResult> PostBla(FurTwo furTwo)
        {
           
            return Ok(furTwo);
        }

        // DELETE: api/Pizzas/5
        [HttpGet("del/{id}")]
        public async Task<IActionResult> DeleteFurniture(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            Product furniture = await _context.Products.FirstOrDefaultAsync(u => u.IdProduct == id);
            if (furniture == null)
            {
                return NotFound();
            }
            Image image = await _context.Images.FirstOrDefaultAsync(u => u.IdImage == furniture.ImageId);
            if (image == null)
            {
                return NotFound();
            }
            _context.Products.Remove(furniture);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FurnitureExists(int id)
        {
            return (_context.Products?.Any(e => e.IdProduct == id)).GetValueOrDefault();
        }
    }
}
