using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using PizzaApi.Models;
using PizzaApi.Utils;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppusersController : ControllerBase
    {
        private readonly Pizza_DBContext _context;

        public AppusersController(Pizza_DBContext context)
        {
            _context = context;
        }

        // GET: api/Appusers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appuser>>> GetAppusers()
        {

            if (_context.Appusers == null)
          {
                return NotFound();
          }
           /* _logger.LogCritical("LogCritical");
            _logger.LogDebug("LogDebug ");
            _logger.LogError("LogError ");
            _logger.LogInformation("LogInformation ");
            _logger.LogWarning("LogWarning");
            //EmailService.SendEmailAsync("syyynoksyyynok@gmail.com", "Критическая ошибка!!!", "Мировая угроза бегом в приложение, чини получение пользователей.");
            var cs = "Host=localhost;Username=postgres;Password=1234;Database=Pizza_DB";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "insert into LogMyApp(Level_Log, Data_Log) values ('"+ "INFO" +"', '"+ "Получение пользователей" +"');";

            using var cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteScalar();*/
            return await _context.Appusers.ToListAsync();
        }

        // GET: api/Appusers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appuser>> GetAppuser(int id)
        {
          if (_context.Appusers == null)
          {
              return NotFound();
          }
            var appuser = await _context.Appusers.FindAsync(id);

            if (appuser == null)
            {
                return NotFound();
            }

            return appuser;
        }

        // PUT: api/Appusers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppuser(int id, Appuser appuser)
        {
            if (id != appuser.IdAppuser)
            {
                return BadRequest();
            }

            _context.Entry(appuser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppuserExists(id))
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

        // POST: api/Appusers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appuser>> PostAppuser(Appuser appuser)
        {
          if (_context.Appusers == null)
          {
              return Problem("Entity set 'Pizza_DBContext.Appusers'  is null.");
          }
            _context.Appusers.Add(appuser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppuser", new { id = appuser.IdAppuser }, appuser);
        }

        [HttpPost("balance/{money}/{id}")]
        public async Task<ActionResult<Appuser>> PostAppuserBalanca(int money, int id)
        {
            if (_context.Appusers == null)
            {
                return Problem("Entity set 'Pizza_DBContext.Appusers'  is null.");
            }
            var appuser = await _context.Appusers.FindAsync(id);

            if (appuser == null)
            {
                return NotFound();
            }
            appuser.Balance = appuser.Balance + money;
            _context.Update(appuser);
            await _context.SaveChangesAsync();
            return Ok(appuser);
        }

        [HttpPost("{mail}/{pass}/{passNew}")]
        public async Task<ActionResult<Appuser>> PostPass(string mail, string pass, string passNew)
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
            user2.Apppassword = CryptoApp.GenerateHash(passNew, user2.Salt);

            _context.Entry(user2).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppuserExists(user2.IdAppuser))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user2);
        }

        // DELETE: api/Appusers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppuser(int id)
        {
            if (_context.Appusers == null)
            {
                return NotFound();
            }
            var appuser = await _context.Appusers.FindAsync(id);
            if (appuser == null)
            {
                return NotFound();
            }

            _context.Appusers.Remove(appuser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppuserExists(int id)
        {
            return (_context.Appusers?.Any(e => e.IdAppuser == id)).GetValueOrDefault();
        }
    }
}
