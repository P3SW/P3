using System;
using System.Globalization;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace DataStreamingSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DatabaseStreamer streamer = new DatabaseStreamer("../../../setup.txt");
                streamer.Stream();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
