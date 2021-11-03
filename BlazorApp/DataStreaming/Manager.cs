using System;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class Manager
    {
        //https://i.imgflip.com/34lzjo.png <--- you after looking at this code
        public string Name { get; private set; }
        public int Id { get; private set; }
        public string Status { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int RunTime { get; private set; }
        public HealthData Health { get; set; }
        public ErrorLog Error { get; set; }
        public ReconciliationLog Reconciliation { get; set; }
        public int RowsRead { get; private set; }
        public int RowsWritten { get; private set; }
        public int EfficiencyScore { get; private set; }
        public static SqlConnection Connection { get; set; }

        public Manager(string name, int id)
        {
            Health = new HealthData();
            Reconciliation = new ReconciliationLog();
            Error = new ErrorLog();
            Name = name;
            Id = id;
        }

        public void WatchManager()
        {
            TableStreamer.Connection = Connection;
            TableStreamer healthStreamer = new TableStreamer(SqlQueryStrings.HealthSelect, Health);
            TableStreamer errorStreamer = new TableStreamer(SqlQueryStrings.ErrorSelect, Error);
            TableStreamer reconciliationStreamer =
                new TableStreamer(SqlQueryStrings.ReconciliationSelect, Reconciliation);
            healthStreamer.StartListening();
            errorStreamer.StartListening();
            reconciliationStreamer.StartListening();
        }

        public void FinishedManagerDataCollection()
        {
            AssignManagerTrackingData();
            AssignEndTime();
            
            CalculateEfficiencyScore();
        }

        //The efficiency score is in no way a recognised way of measuring performance and is based on the data made available to us.
        public void CalculateEfficiencyScore()
        {
            double averageCpu = CalculateAverageCpuUsage();
            double result = (double) (RowsRead + RowsWritten) / RunTime * averageCpu;
            EfficiencyScore = Convert.ToInt32(result);
        }

        private double CalculateAverageCpuUsage()
        {
            double result = 0.0;
            if (Health.Cpu.Count == 0)
            {
                return result;
            }
            foreach (Data value in Health.Cpu )
            {
                result += value.NumericValue / Health.Cpu.Count;
            }

            return result;
        }
        //TODO: Learn to parse dates from strings lmao
        private void AssignStartTime()
        {
            using (SqlCommand command = new SqlCommand(ObtainEnginePropertiesQueryStringByInteger(0), Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    StartTime = DateTime.Parse((string) reader["VALUE"]);   //(DateTime) reader["VALUE"];
                    reader.Close();
                }
            }
        }
        
        private void AssignManagerTrackingData()
        {
            using (SqlCommand command = new SqlCommand(GetManagerTrackingQueryString(), Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Status = (string) reader["STATUS"];
                    RunTime = (int) reader["RUNTIME"];
                    RowsRead = (int) reader["PERFORMANCECOUNTROWSREAD"];
                    RowsWritten = (int) reader["PERFORMANCECOUNTROWSWRITTEN"];
                    reader.Close();
                }
            }
        }

        private void AssignEndTime()
        {
            using (SqlCommand command = new SqlCommand(ObtainEnginePropertiesQueryStringByInteger(3), Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    EndTime = (DateTime) reader["TIMESTAMP"];
                    reader.Close();
                }
            }
        }
        
        private string ObtainEnginePropertiesQueryStringByInteger(int i)
        {
            switch (i)
            {
                case 0:
                    return string.Format($"SELECT VALUE FROM dbo.ENGINE_PROPERTIES WHERE MANAGER = '{Name}' AND [dbo].[KEY] = 'START_TIME'");
                // case 1:
                //     return string.Format($"SELECT VALUE FROM dbo.ENGINE_PROPERTIES WHERE MANAGER = '{Name}' AND [dbo].[KEY] = 'Læste rækker'");
                // case 2:
                //     return string.Format($"SELECT VALUE FROM dbo.ENGINE_PROPERTIES WHERE MANAGER = '{Name}' AND [dbo].[KEY] = 'Skrevne rækker'");
                case 3:
                    return string.Format($"SELECT TIMESTAMP FROM dbo.ENGINE_PROPERTIES WHERE MANAGER = '{Name}' AND [dbo].[KEY] = 'runtimeOverall'");
                default:
                    throw new ArgumentException($"{i} is an invalid argument");
            }
        }

        private string GetManagerTrackingQueryString()
        {
            return string.Format($"SELECT STATUS, RUNTIME, PERFORMANCECOUNTROWSREAD, PERFORMANCECOUNTROWSWRITTEN FROM dbo.MANAGER_TRACKING WHERE MGR = '{Name}'");
        }
    }
}