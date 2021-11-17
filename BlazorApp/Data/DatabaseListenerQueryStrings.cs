using System;

namespace BlazorApp.Data
{
    //Class containing constant sql strings. These are used for sql dependencies.
    public static class DatabaseListenerQueryStrings
    {
        public const string HealthSelect = "SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT WHERE REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY' ORDER BY LOG_TIME";
        public const string ErrorSelect = "SELECT [CREATED], [LOG_MESSAGE], [LOG_LEVEL], [dbo].[LOGGING_CONTEXT].[CONTEXT]  FROM [dbo].[LOGGING] INNER JOIN [dbo].[LOGGING_CONTEXT] ON (LOGGING.CONTEXT_ID = LOGGING_CONTEXT.CONTEXT_ID) ORDER BY CREATED";
        public const string ReconciliationSelect = "SELECT [AFSTEMTDATO],[DESCRIPTION],[MANAGER],[AFSTEMRESULTAT] FROM [dbo].[AFSTEMNING] ORDER BY AFSTEMTDATO";
        public const string ManagersSelect = "SELECT MANAGER_NAME, ROW_ID FROM dbo.MANAGERS";

        public static string ManagerDependencyTimes(DateTime time)
        {
            if (time == DateTime.MinValue) //If time is not assigned
            {
                return "SELECT MANAGER, [TIMESTAMP] FROM dbo.ENGINE_PROPERTIES WHERE [KEY] = 'START_TIME'";
            }

            return "SELECT [MANAGER], [TIMESTAMP] FROM [dbo].[ENGINE_PROPERTIES] WHERE [KEY] = 'START_TIME'";
        }

        public static string ManagerStartTimes(DateTime time)
        {
            if (time == DateTime.MinValue) //If time is not assigned
            {
                return "SELECT MANAGER, [TIMESTAMP] FROM dbo.ENGINE_PROPERTIES WHERE [KEY] = 'START_TIME'";
            }

            Console.WriteLine("Er jeg her?" + time);
            return String.Format($"SELECT [MANAGER], [TIMESTAMP] FROM [dbo].[ENGINE_PROPERTIES] WHERE [TIMESTAMP] > '{time}' AND [KEY] = 'START_TIME'");  
        } 
            public static string ManagerTrackingSelect(string name)
        {
            if (name == null)
            {
                return "SELECT MGR FROM dbo.MANAGER_TRACKING";
            }
            return String.Format("SELECT [MGR], [STATUS], [RUNTIME], [PERFORMANCECOUNTROWSREAD], [PERFORMANCECOUNTROWSWRITTEN]" +
                                 $"FROM dbo.MANAGER_TRACKING WHERE '{name}' = [MGR]");
        }
    } 
}