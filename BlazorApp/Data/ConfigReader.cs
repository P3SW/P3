using System.IO;

namespace BlazorApp.Data
{
    //Class responsible for reading the setup file and returns the connection string.
    public static class ConfigReader
    {
        //Static method used to read a file using a path as parameter
        public static string ReadSetupFile(string path)
        {
            string connectionString;
            using (StreamReader sr = new StreamReader(path))
            {
                connectionString = sr.ReadLine();
            }
            return connectionString;
        }
    }
}