using System;

namespace DataStreamingSimulation
{
    public class DatabaseStreamer
    {
        const int INCREASE_IN_SEC = 1;
        const bool CONSOLE_PRINT = false;
        private readonly string[] _tablesToStream = {"AFSTEMNING", "LOGGING", "MANAGER_TRACKING", "HEALTH_REPORT", "ENGINE_PROPERTIES"};
        private readonly string[] _tablesToStreamOneTime = {"MANAGERS","LOGGING_CONTEXT"};
        
        public DatabaseStreamer(string fileName, string streamStartDate, string streamStopDate)
        {
            _currentStreamTime = DateTime.Parse(streamStartDate);
            stopTime = DateTime.Parse(streamStopDate);
            _nextTime = _currentStreamTime.AddSeconds(INCREASE_IN_SEC);
            _sqlConnection = new DatabaseConnect();
            FilePath = fileName;
            _setupFile = _sqlConnection.ReadSetupFile(fileName);
            _querySetup = new QuerySetup();
            QueryString = "";
        }
        
        private DateTime _currentStreamTime;
        private DateTime stopTime;
        private DateTime _nextTime;
        private readonly DatabaseConnect _sqlConnection;
        public static string FilePath;
        private readonly string _setupFile;
        private readonly QuerySetup _querySetup;
        public string QueryString;

        public void StreamTableOneTime(string table)
        {
            QueryString = _querySetup.MakeOneTimeQueryString(table);
            SqlConnect(QueryString);
        }

        public void StreamTable(string table, DateTime currentStreamTime, DateTime nextTime)
        {
            QueryString = new QuerySetup().MakeQueryString(table, currentStreamTime, nextTime);
            SqlConnect(QueryString);
        }

        private void SqlConnect(string queryString)
        {
            _sqlConnection.SqlConnect(queryString, _setupFile, CONSOLE_PRINT);
        }
        
        public void Stream(int sleepInMiliSecs)
        {
            foreach (string table in _tablesToStreamOneTime)
                StreamTableOneTime(table);

            while (_currentStreamTime < stopTime)
            {
                for (int i = 0; i < _tablesToStream.Length; i++)
                    StreamTable(_tablesToStream[i], _currentStreamTime,_nextTime);
                
                _currentStreamTime = _nextTime;
                _nextTime = _currentStreamTime.AddSeconds(INCREASE_IN_SEC);
                System.Threading.Thread.Sleep(INCREASE_IN_SEC * sleepInMiliSecs); // How frequently it inserts into ANS_DB_P3
            }
        }
    }
}