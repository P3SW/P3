using System;
using System.Collections.Generic;
using BlazorApp.DataStreaming;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class ErrorLog : IData
    {
        public List<ErrorData> ErrorList { get; private set; }
        public List<ErrorData> NewErrorList { get; private set; }
        public static DateTime LastRowTimeStamp { get; private set; }


        public ErrorLog()
        {
            ErrorList = new List<ErrorData>();
        }
        
        public void AddDataFromSqlReader(SqlDataReader reader)
        {
            NewErrorList = new List<ErrorData>();

            while (reader.Read())
            {
                NewErrorList.Add(new ErrorData(reader));
                LastRowTimeStamp = (DateTime) reader[0];
            }
            ErrorList.AddRange(NewErrorList);
        }

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
    }
}