using System;
using System.IO;

namespace DataStreamingSimulation
{
    public class QuerySetup
    {
        public string MakeOneTimeQueryString(string table)
        {
            string queryString = null;
            switch (table)
            {
                case "LOGGING_CONTEXT": // NULL
                    queryString = $@"SELECT * FROM LOGGING_CONTEXT;";
                    break;
                case "MANAGERS": // NULL
                    queryString = $@"SELECT * FROM MANAGERS;";
                    break;
                default: 
                    break;
            }

            return queryString;
        }
        public string MakeQueryString(string table, DateTime startTime, DateTime nextTime)
        {
            string queryString = null;
            switch (table)
            {
                case "AFSTEMNING": // 2021-10-28 15:09:05.390
                    queryString = $@"SELECT * FROM AFSTEMNING
                                     WHERE START_TIME BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                case "LOGGING": // 2021-10-28 15:04:42.693
                    queryString = $@"SELECT * FROM LOGGING
                                     WHERE CREATED BETWEEN '{startTime}' and '{nextTime}';"; 
                    break;
                case "ENGINE_PROPERTIES": //2021-10-28 15:07:23.347
                    queryString = $@"SELECT * FROM ENGINE_PROPERTIES
                                     WHERE TIMESTAMP BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                case "HEALTH_REPORT": // 2021-10-28 15:07:16.777
                    queryString = $@"SELECT * FROM HEALTH_REPORT
                                     WHERE LOG_TIME BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                case "MANAGER_TRACKING": // 2021-10-28 00:00:00.000
                    queryString = $@"SELECT * FROM MANAGER_TRACKING
                                     WHERE [MGR] = (SELECT [MANAGER] FROM ENGINE_PROPERTIES WHERE 
                                     [TIMESTAMP] = (SELECT [TIMESTAMP] FROM ENGINE_PROPERTIES WHERE [TIMESTAMP] 
                                     BETWEEN '{startTime}' and '{nextTime}' AND [KEY] = 'WRITE [TOTAL]'));";
                    break;
                default:
                    break;
            }
            return queryString;
        }
    }
}