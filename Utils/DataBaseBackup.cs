using System.Diagnostics;

namespace PizzaApi.Utils
{
    public class DataBaseBackup
    {
        public static string BackupDatabase(
            string server,
            string port,
            string user,
            string password,
            string dbname,
            string backupdir,
            string backupFileName,
            string backupCommandDir)
        {
            try
            {

                Environment.SetEnvironmentVariable("PGPASSWORD", password);

                string backupFile = backupdir + backupFileName + DateTime.Now.ToString("yyyy") + "_" + DateTime.Now.ToString("MM") + "_" + DateTime.Now.ToString("dd") + ".backup";
                string BackupString = "-ibv -Z3 -f \"" + backupFile + "\" " +
                 "-Fc -h " + server + " -U " + user + " -p " + port + " " + dbname;

                Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = backupCommandDir + "\\pg_dump.exe";
                proc.StartInfo.Arguments = BackupString;

                proc.Start();
                proc.WaitForExit();
                proc.Close();

                return backupFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
