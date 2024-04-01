using System.IO;

namespace PizzaApi.Utils
{
    public class FilesOnly
    {
        public static bool isFilesBackup()
        {
            return File.Exists("C:\\ycheba\\scriptsBat\\database0001.sql");
        }
    }
}
