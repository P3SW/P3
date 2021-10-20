using System;
using System.IO;

namespace Datastreaming
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