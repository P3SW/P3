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
                DatabaseStreamer streamer = new DatabaseStreamer("../../../setup.txt", "2021-10-28 15:07:10.347", "2021-10-28 16:58:52.720");
                streamer.Stream(1000);
            }   
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
