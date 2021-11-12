using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.DataStreaming.Events;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class Manager : EventBase
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
        public int Cpu { get; set; }
        public int EfficiencyScore { get; private set; }
        public static SqlConnection Connection { get; set; }
        private TableStreamer _healthStreamer;
        private TableStreamer _errorStreamer;
        private TableStreamer _reconciliationStreamer;
        private SqlCommand command;

        public Manager(string name, int id)
        {
            Health = new HealthData();
            Reconciliation = new ReconciliationLog();
            Error = new ErrorLog();
            Name = name;
            Id = id;
        }

        //Starts the tablestreamers and assigns the start time of the manager
        public void WatchManager()
        {
            Console.WriteLine($"Manager {Name} started");
            AssignStartTime();
        }

        public void SetupDataPoints()
        {
            Console.WriteLine("MANAGER START TIME IS: " + StartTime);
            _healthStreamer = new TableStreamer(SqlQueryStrings.HealthSelect,GetSelectStringsForTableStreamer("health"), Health);
            _errorStreamer = new TableStreamer(SqlQueryStrings.ErrorSelect,GetSelectStringsForTableStreamer("logging"), Error);
            _reconciliationStreamer = new TableStreamer(SqlQueryStrings.ReconciliationSelect,GetSelectStringsForTableStreamer("reconciliation"), Reconciliation);
            _healthStreamer.StartListening();
            _errorStreamer.StartListening();
            _reconciliationStreamer.StartListening();
        }

        //Stops the tablestreamers, queries relevant data and calculates the EffiencyScore(TM)
        public void FinishManager()
        {
            _healthStreamer.StopListening();
            _errorStreamer.StopListening();
            _reconciliationStreamer.StopListening();

            AssignManagerTrackingData();
            AssignEndTime();
            
            CalculateEfficiencyScore();
        }

        //The EfficiencyScore(TM) algorithm is a proprietary intellectual property owned by Arthur Osnes Gottlieb.
        //Do NOT change, share or reproduce in any form.
        public void CalculateEfficiencyScore()
        {
            double averageCpu = CalculateAverageCpuUsage();
            Cpu = Convert.ToInt32(averageCpu);
            double result = (double) (RowsRead + RowsWritten) / RunTime * averageCpu;
            EfficiencyScore = Convert.ToInt32(result);
        }

        //Calculates the average CPU usage logged in the HEALTH_REPORT table
        private double CalculateAverageCpuUsage()
        {
            double result = 0.0;
            if (Health.Cpu.Count == 0)
            {
                return result;
            }
            foreach (Data value in Health.Cpu )
            {
                result += (double) value.NumericValue / Health.Cpu.Count;
            }

            return result;
        }
        
        private async void AssignStartTime()
        {
            Console.WriteLine("Finder Manager Start Time....");
            // ****************************
            TriggerUpdate("UPDATED");
            // ****************************
            using (command = new SqlCommand(ObtainEnginePropertiesQueryStringByInteger("startTime"), Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        Console.WriteLine("reader closed...");
                        await Task.Delay(1000);
                        AssignStartTime();
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("found starttime!");
                            StartTime = DateTime.Parse((string)reader[0]);
                            //Console.WriteLine("reader almost closed...");
                        } 
                        reader.Close();
                        Console.WriteLine("reader closed...");
                        SetupDataPoints();
                    }
                    
                }
            }
        }

        //Queries status, runtime, rows read and rows written from the MANAGER_TRACKING table.
        private void AssignManagerTrackingData()
        {
            using (SqlCommand command = new SqlCommand(GetManagerTrackingQueryString(), Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Status = (string) reader["STATUS"];
                    RunTime = (int) reader["RUNTIME"];
                    RowsRead = (int) reader["PERFORMANCECOUNTROWSREAD"];
                    RowsWritten = (int) reader["PERFORMANCECOUNTROWSWRITTEN"];
                    reader.Close();
                }
            }
        }

        //Queries the end time from the ENGINE_PROPERTIES table
        private void AssignEndTime()
        {
            using (SqlCommand command = new SqlCommand(ObtainEnginePropertiesQueryStringByInteger("runtime"), Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    EndTime = (DateTime) reader[0];
                    reader.Close();
                }
            }
        }
        
        //Returns a sql string which queries the relevant data from ENGINE_PROPERTIES
        private string ObtainEnginePropertiesQueryStringByInteger(string s)
        {
            switch (s)
            {
                case "startTime":
                    return string.Format($"SELECT [VALUE] FROM dbo.ENGINE_PROPERTIES WHERE MANAGER LIKE '{Name}%' AND [KEY] = 'START_TIME'");
                case "runtime":
                    return string.Format($"SELECT [TIMESTAMP] FROM dbo.ENGINE_PROPERTIES WHERE MANAGER LIKE '{Name}%' AND [KEY] = 'runtimeOverall'");
                default:
                    throw new ArgumentException($"{s} is an invalid argument");
            }
        }

        //Queries data from the MANAGER_TRACKING table
        private string GetManagerTrackingQueryString()
        {
            return string.Format($"SELECT STATUS, RUNTIME, PERFORMANCECOUNTROWSREAD, PERFORMANCECOUNTROWSWRITTEN FROM dbo.MANAGER_TRACKING WHERE MGR = '{Name}'");
        }

        //Returns the select string for the table streamers
        private string GetSelectStringsForTableStreamer(string s)
        {
            switch (s)
            {
                case "health":
                    return string.Format($"SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT " +
                                         $"WHERE REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY'" +
                                         $"AND LOG_TIME > '{StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                         "ORDER BY LOG_TIME");
                case "logging":
                    return string.Format($"SELECT [CREATED], [LOG_MESSAGE], [LOG_LEVEL]," +
                                         $"[dbo].[LOGGING].[CONTEXT_ID]," +
                                         $"[dbo].[LOGGING_CONTEXT].[CONTEXT] " +
                                         $"FROM [dbo].[LOGGING] " +
                                         $"INNER JOIN [dbo].[LOGGING_CONTEXT] " +
                                         $"ON (LOGGING.CONTEXT_ID = LOGGING_CONTEXT.CONTEXT_ID) " +
                                         $"WHERE CREATED > '{StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                         $"ORDER BY CREATED");
                case "reconciliation":
                    return string.Format($"SELECT [AFSTEMTDATO],[DESCRIPTION],[MANAGER],[AFSTEMRESULTAT]" +
                                         $"FROM dbo.AFSTEMNING WHERE AFSTEMTDATO > '{StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}' " +
                                         $"ORDER BY AFSTEMTDATO");
                default:
                    throw new ArgumentException();
            }
        }
    }
}