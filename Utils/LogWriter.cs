using Microsoft.Extensions.Hosting.Internal;
using System.Net.NetworkInformation;
using System.Reflection;

namespace PizzaApi.Utils
{
    public class LogWriter
    {
        static string projectRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private string m_exePath = Path.Combine(projectRootPath, "logi");
        public LogWriter()
        {
        }
        public void LogWrite(string logMessage)
        {
            Console.WriteLine(m_exePath);
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  {0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
