using System.IO;

namespace BlazorApp.DataStreaming
{
    public class ConfigReader
    {
        public string ReadSetupFile()
        {
            const string fileName = "setup.txt";
            string connectionString;
            using (StreamReader sr = new StreamReader(fileName))
            {
                connectionString = sr.ReadLine();
            }
            return connectionString;
        }
    }
}