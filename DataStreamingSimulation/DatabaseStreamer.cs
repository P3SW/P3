using System;

namespace DataStreamingSimulation
{
    public class DatabaseStreamer
    {
        const int INCREASE_IN_SEC = 1;
        const bool CONSOLE_PRINT = false;
        const string STREAM_START_DATE = "2021-10-28 15:07:10.347"; 
        string[] tablesToStream = {"AFSTEMNING", "LOGGING", "MANAGER_TRACKING", "HEALTH_REPORT", "ENGINE_PROPERTIES"};
        private string[] tablesToStreamOneTime = {"MANAGERS","LOGGING_CONTEXT"};
        private DateTime startTime;
        private DateTime nextTime;
        private DatabaseConnect sqlConnection;
        public string setupFile;
        private QuerySetup querySetup;
        public static string FilePath;
        public string _queryString;
        
        public DatabaseStreamer(string fileName)
        {
            startTime = DateTime.Parse(STREAM_START_DATE);
            nextTime = startTime.AddSeconds(INCREASE_IN_SEC);
            sqlConnection = new DatabaseConnect();
            FilePath = fileName;
            setupFile = sqlConnection.ReadSetupFile(fileName);
            querySetup = new QuerySetup();
            _queryString = "";
        }

        public void StreamTableOneTime(string table)
        {
            _queryString = querySetup.MakeOneTimeQueryString(table);
            SqlConnect(_queryString);
        }

        public void StreamTable(string table, DateTime startTime, DateTime nextTime)
        {
            _queryString = new QuerySetup().MakeQueryString(table, startTime, nextTime);
            SqlConnect(_queryString);
        }

        private void SqlConnect(string queryString)
        {
            sqlConnection.SqlConnect(queryString, setupFile, CONSOLE_PRINT);
        }
        
        
        public void Stream()
        {
            foreach (string table in tablesToStreamOneTime)
                StreamTableOneTime(table);

            while (true)
            {
                for (int i = 0; i < tablesToStream.Length; i++)
                    StreamTable(tablesToStream[i], startTime,nextTime);
                
                startTime = nextTime;
                nextTime = startTime.AddSeconds(INCREASE_IN_SEC);
                System.Threading.Thread.Sleep(INCREASE_IN_SEC * 1000); // How frequently it inserts into ANS_DB_P3
            }
        }
    }
}