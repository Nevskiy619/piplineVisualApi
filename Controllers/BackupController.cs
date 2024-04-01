
using PizzaApi.Models;
using PizzaApi.Utils;
using PizzaApi.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PizzaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : Controller
    {
        private readonly Pizza_DBContext _context;
        public BackupController(Pizza_DBContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> DownloadDatabaseBackup()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "C:\\ycheba\\scriptsBat\\BackupFile.bat";
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();
                proc.WaitForExit();
                var net = new System.Net.WebClient();
                var data = net.DownloadData("C:\\ycheba\\scriptsBat\\database0001.sql");
                var content = new System.IO.MemoryStream(data);
                var contentType = "APPLICATION/octet-stream";
                var fileName = "database0002.sql";
                return File(content, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("restore")]
        public async Task<IActionResult> restoreDatabase()
        {
            if (!FilesOnly.isFilesBackup()) return BadRequest("Выбранного файла не существует");
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "C:\\ycheba\\scriptsBat\\RestoreFile.bat";
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();

                return Ok("Импорт базы данных успешно произведён");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        public class FileDetails
        {
            public string Name { get; set; } = null!;
            public string Extension { get; set; } = null!;
            public DateTime CreationTime { get; set; }
            public DateTime LastWriteTime { get; set; }
        }
    }
}