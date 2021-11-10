using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    //Class containing lists of reconciliation data. Implements IData which ensures the class contains necessary methods.
    public class ReconciliationLog : IData
    {
        public List<ReconciliationData> ReconciliationList { get; private set; }
        public List<ReconciliationData> newReconciliationData { get; private set; }
        public static DateTime LastRowTimeStamp { get; private set; }

        public ReconciliationLog()
        {
            ReconciliationList = new List<ReconciliationData>();
        }
        
        //Inserts data from the reader into temporary list and adds these to the full list of data.
        public async void AddDataFromSqlReader(SqlDataReader reader)
        {
            newReconciliationData = new List<ReconciliationData>();
            while (reader.Read())
            {
                newReconciliationData.Add(new ReconciliationData(reader));
                LastRowTimeStamp = (DateTime)reader[0];
            }

            LastRowTimeStamp = newReconciliationData[newReconciliationData.Count - 1].Timestamp;
            ReconciliationList.AddRange(newReconciliationData);
            PrintRecons(newReconciliationData);
        }

        //Returns a query string with the latest timestamp to ensure only new data is queried.
        public string GetChangesQueryString()
        {
            return string.Format($"SELECT [AFSTEMTDATO],[DESCRIPTION],[MANAGER],[AFSTEMRESULTAT]" +
                                 $"FROM [dbo].[AFSTEMNING] WHERE AFSTEMTDATO > '{LastRowTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")}' " +
                                 $"ORDER BY AFSTEMTDATO");
        }

        public void PrintRecons(List<ReconciliationData> recons)
        {
            foreach (var recon in recons)
            {
                Console.WriteLine(recon.Timestamp + " " + recon.Result + " " + recon.Manager + " " + recon.Description);
            }
        }
    }
}