using System;
using System.IO;

namespace DataStreamingSimulation
{
    public class QuerySetup
    {
        public string makeQueryString(string table, DateTime startTime, DateTime nextTime)
        {
            string queryString = null;
            switch (table)
            {
                case "AFSTEMNING": // 2021-10-05 15:08:05.277
                    queryString = $@"SELECT * FROM AFSTEMNING
                                     WHERE START_TIME BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                case "AUDIT_LOGERROR": // 2021-10-05 15:41:08.663
                    queryString = $@"SELECT * FROM AUDIT_LOGERROR
                                     WHERE CREATED BETWEEN '{startTime}' and '{nextTime}';"; 
                    break;
                case "ENGINE_PROPERTIES": // 2021-10-05 15:06:20.823
                    queryString = $@"SELECT * FROM ENGINE_PROPERTIES
                                     WHERE TIMESTAMP BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                case "HEALTH_REPORT": // NULL
                    queryString = $@"SELECT * FROM HEALTH_REPORT
                                     WHERE LOG_TIME BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                case "MANAGER_TRACKING": // 2021-10-05 00:00:00.000
                    queryString = $@"SELECT * FROM MANAGER_TRACKING
                                     WHERE STARTTIME BETWEEN '{startTime}' and '{nextTime}';";
                    break;
                default:
                    break;
            }
            return queryString;
        }
    }
}