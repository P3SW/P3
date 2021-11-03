using System.IO;

namespace BlazorApp.DataStreaming
{
    public static class ConfigReader
    {
        public static string ReadSetupFile()
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