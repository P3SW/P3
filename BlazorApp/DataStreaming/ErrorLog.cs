using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    //Class contains lists of error data and methods for querying and storing new data.
    public class ErrorLog : IData
    {
        public List<ErrorData> ErrorList { get; private set; }
        public List<ErrorData> NewErrorList { get; private set; }
        public static DateTime LastRowTimeStamp { get; private set; }

        public ErrorLog()
        {
            ErrorList = new List<ErrorData>();
        }

        //Inserts data from the reader into temporary list and adds these to the full list of data.
        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            NewErrorList = new List<ErrorData>();

            while (reader.Read())
            {
                Console.WriteLine("test");
                NewErrorList.Add(new ErrorData(
                    (DateTime) reader["CREATED"], 
                    (string) reader["LOG_MESSAGE"], 
                    (string) reader["LOG_LEVEL"], 
                    (string) reader["CONTEXT"] ));
                LastRowTimeStamp = NewErrorList[NewErrorList.Count - 1].Created;
            }
            ErrorList.AddRange(NewErrorList);
            Console.WriteLine(ErrorList.Count);
            PrintLogs(ErrorList);
        }

        //Returns a query string with the latest timestamp to ensure only new data is queried.
        public string GetChangesQueryString()
        {
            return string.Format($"SELECT [CREATED], [LOG_MESSAGE], [LOG_LEVEL]," +
                                 $"[dbo].[LOGGING].[CONTEXT_ID]," +
                                 $"[dbo].[LOGGING_CONTEXT].[CONTEXT] " +
                                 $"FROM [dbo].[LOGGING] " +
                                 $"INNER JOIN [dbo].[LOGGING_CONTEXT] " +
                                 $"ON (LOGGING.CONTEXT_ID = LOGGING_CONTEXT.CONTEXT_ID) " +
                                 $"WHERE CREATED > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                 $"ORDER BY CREATED");
        }

        public void PrintLogs(List<ErrorData> errorlist)
        {
            foreach (var error in errorlist)
            {
                Console.WriteLine(error.LogLevel + " " + error.Created + " " + error.LogMessage);
            }
        }
    }
}