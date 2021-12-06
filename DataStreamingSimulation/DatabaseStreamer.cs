using System;

namespace DataStreamingSimulation
{
    public class DatabaseStreamer
    {
        const int INCREASE_IN_SEC = 1;
        const bool CONSOLE_PRINT = false;
        string[] tablesToStream = {"AFSTEMNING", "LOGGING", "MANAGER_TRACKING", "HEALTH_REPORT", "ENGINE_PROPERTIES"};
        private string[] tablesToStreamOneTime = {"MANAGERS","LOGGING_CONTEXT"};
        private DateTime currentStreamTime;
        private DateTime stopTime;
        private DateTime nextTime;
        private DatabaseConnect sqlConnection;
        public string setupFile;
        private QuerySetup querySetup;
        public static string FilePath;
        public string _queryString;
        
        public DatabaseStreamer(string fileName, string streamStartDate, string streamStopDate)
        {
            currentStreamTime = DateTime.Parse(streamStartDate);
            stopTime = DateTime.Parse(streamStopDate);
            nextTime = currentStreamTime.AddSeconds(INCREASE_IN_SEC);
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

        public void StreamTable(string table, DateTime currentStreamTime, DateTime nextTime)
        {
            _queryString = new QuerySetup().MakeQueryString(table, currentStreamTime, nextTime);
            SqlConnect(_queryString);
        }

        private void SqlConnect(string queryString)
        {
            sqlConnection.SqlConnect(queryString, setupFile, CONSOLE_PRINT);
        }
        
        
        public void Stream(int sleepInMiliSecs)
        {
            foreach (string table in tablesToStreamOneTime)
                StreamTableOneTime(table);

            while (currentStreamTime < stopTime)
            {
                for (int i = 0; i < tablesToStream.Length; i++)
                    StreamTable(tablesToStream[i], currentStreamTime,nextTime);
                
                currentStreamTime = nextTime;
                nextTime = currentStreamTime.AddSeconds(INCREASE_IN_SEC);
                System.Threading.Thread.Sleep(INCREASE_IN_SEC * sleepInMiliSecs); // How frequently it inserts into ANS_DB_P3
            }
        }
    }
}