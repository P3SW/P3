using System.IO;

namespace BlazorApp.Data
{
    //Class responsible for reading the setup.txt file and returns the connection string.
    public static class ConfigReader
    {
        public static string ReadSetupFile(string fileName)
        {
            string connectionString;
            using (StreamReader sr = new StreamReader(fileName))
            {
                connectionString = sr.ReadLine();
            }
            return connectionString;
        }
    }
}