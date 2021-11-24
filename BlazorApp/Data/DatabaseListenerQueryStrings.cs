
namespace BlazorApp.Data
{
    //Class containing constant sql strings. These are used for sql dependencies.
    public static class DatabaseListenerQueryStrings
    {
        public const string HealthSelect = "SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT WHERE REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY' ORDER BY LOG_TIME";

        public const string ErrorSelect = "SELECT [CREATED], [LOG_MESSAGE], [LOG_LEVEL], [dbo].[LOGGING_CONTEXT].[CONTEXT]  FROM [dbo].[LOGGING] INNER JOIN [dbo].[LOGGING_CONTEXT] ON (LOGGING.CONTEXT_ID = LOGGING_CONTEXT.CONTEXT_ID) ORDER BY CREATED";
       
        public const string ReconciliationSelect = "SELECT [AFSTEMTDATO],[DESCRIPTION],[MANAGER],[AFSTEMRESULTAT] FROM [dbo].[AFSTEMNING] ORDER BY AFSTEMTDATO";

        public const string ManagersSelect = "SELECT MANAGER_NAME, ROW_ID FROM dbo.MANAGERS";

        public const string ManagerStartTimesSelect = "SELECT MANAGER, [TIMESTAMP] FROM dbo.ENGINE_PROPERTIES WHERE [KEY] = 'START_TIME' ORDER BY [TIMESTAMP] DESC";

        public const string ExecutionIDSelect = "SELECT DISTINCT [EXECUTION_ID] FROM LOGGING ORDER BY EXECUTION_ID DESC";
    }
}