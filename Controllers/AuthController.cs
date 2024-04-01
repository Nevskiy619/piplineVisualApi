
using PizzaApi.Models;
using PizzaApi.Utils;
using PizzaApi.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace PizzaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        LogWriter logWriter = new LogWriter();
        private readonly Pizza_DBContext _context;

        public AuthController(Pizza_DBContext context)
        {
            _context = context;
        }

        // POST api/users
        [HttpPost("{mail}/{pass}")]
        public async Task<ActionResult<Appuser>> Post(string mail, string pass)
        {
            if (mail == null)
            {
                return NotFound();
            }
            Appuser user2 = _context.Appusers.FirstOrDefault(x => x.Mail == mail);
            if (user2 == null)
            {
                return NotFound();
            }
            if (!CryptoApp.AreEqual(pass, user2.Apppassword, user2.Salt))
            {
                return BadRequest();
            }
            logWriter.LogWrite("{INFO} Пользователь: " + mail + " авторизовался");
            return Ok(user2);
        }

        [HttpGet]
        public async Task<ActionResult<String>> ExportToCsv()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var data = await _context.Products.ToListAsync();

            // Создаем CSV строку
            StringBuilder csv = new StringBuilder();
            foreach (var property in typeof(Mebel).GetProperties())
            {
                csv.Append(property.Name + ",");
            }
            csv.Append("\n");
            foreach (var item in data)
            {
                foreach (var property in item.GetType().GetProperties())
                {
                    csv.Append(property.GetValue(item, null) + ",");
                }
                csv.Append("\n");
            }

            Byte[] encodedBytes = UnicodeEncoding.UTF8.GetBytes(csv.ToString());
            return File(encodedBytes, "text/csv", "db_export.csv");
        }
    }
}