using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using SQLDatabaseRead;

namespace BlazorApp.Data
{
    public class ManagerStatusHandler
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public string Status { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int RunTime { get; private set; }
        public HealthDataHandler Health { get; set; }
        public LogDataHandler ErrorHandler { get; set; }
        public LogDataHandler ReconciliationHandler { get; set; }
        public int RowsRead { get; private set; }
        public int RowsWritten { get; private set; }
        public int Cpu { get; set; }
        public int EfficiencyScore { get; private set; }
        public static SqlConnection Connection { get; set; }
        private TableStreamer _healthStreamer;
        private TableStreamer _errorStreamer;
        private TableStreamer _reconciliationStreamer;
        private SqlCommand command;
        private int run_number = 0;

        public ManagerStatusHandler(string name, int id, DateTime startTime)
        {
            Health = new HealthDataHandler(startTime);
            ReconciliationHandler = new LogDataHandler(startTime);
            ErrorHandler = new LogDataHandler(startTime);
            Name = name;
            Id = id;
            StartTime = startTime;
        }

        //Starts the tablestreamers and assigns the start time of the manager
        public void WatchManager()
        {
            Console.WriteLine($"Manager {Name} started");
            Console.WriteLine("MANAGER START TIME IS: " + StartTime);
            
            _healthStreamer = new TableStreamer(DatabaseListenerQueryStrings.HealthSelect,
                GetSelectStringsForTableStreamer("health"), Health, "HEALTH");
            
            _errorStreamer = new TableStreamer(DatabaseListenerQueryStrings.ErrorSelect,
                GetSelectStringsForTableStreamer("logging"), ErrorHandler, "ERROR");
            
            _reconciliationStreamer = new TableStreamer(
                DatabaseListenerQueryStrings.ReconciliationSelect,
                GetSelectStringsForTableStreamer("reconciliation"), 
                ReconciliationHandler, "RECONCILIATION");
            
            _healthStreamer.StartListening();
            _errorStreamer.StartListening();
            _reconciliationStreamer.StartListening();
        }

        //Stops the tablestreamers, queries relevant data and calculates the EffiencyScore(TM)
        public void FinishManager()
        {
            Console.WriteLine("Stopping the data from listening");
            _healthStreamer.StopListening();
            _errorStreamer.StopListening();
            _reconciliationStreamer.StopListening();
            Console.WriteLine("Listening stopped");
            
            AssignEndTime();
            AssignManagerTrackingData();
            CalculateEfficiencyScore();
        }

        //The EfficiencyScore(TM) algorithm is a proprietary intellectual property owned by Arthur Osnes Gottlieb.
        //Do NOT change, share or reproduce in any form.
        public void CalculateEfficiencyScore()
        {
            double averageCpu = Health.Cpu.Count > 0 ?  Health.Cpu.Average(data => data.NumericValue) : 0.0;
            Cpu = Convert.ToInt32(averageCpu);
            double result = (double) (RowsRead + RowsWritten) / RunTime * averageCpu;
            EfficiencyScore = Convert.ToInt32(result);
        }

        //Queries status, runtime, rows read and rows written from the MANAGER_TRACKING table.
        private void AssignManagerTrackingData()
        {
            using (SqlCommand command = new SqlCommand(GetManagerTrackingQueryString(), Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Status = (string) reader["STATUS"];
                        RunTime = (int) reader["RUNTIME"];
                        RowsRead = (int) reader["PERFORMANCECOUNTROWSREAD"];
                        RowsWritten = (int) reader["PERFORMANCECOUNTROWSWRITTEN"];
                    }
                    reader.Close();
                }
            }
        }

        //Queries the end time from the ENGINE_PROPERTIES table
        private void AssignEndTime()
        {
            using (SqlCommand command = new SqlCommand(ObtainManagerEndTime(), Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        EndTime = (DateTime) reader[0];
                    }
                    reader.Close();
                }
            }
        }
        
        //Returns a sql string which queries the relevant data from ENGINE_PROPERTIES, if name has a random number, the runtimeOverall is found using the name without randomnumber
        private string ObtainManagerEndTime()
        {
            return string.Format($"SELECT [ENDTIME] FROM dbo.MANAGER_TRACKING WHERE [MGR] = '{Name}'");
        }

        //Queries data from the MANAGER_TRACKING table
        private string GetManagerTrackingQueryString()
        {
            return string.Format($"SELECT [STATUS], RUNTIME, PERFORMANCECOUNTROWSREAD, PERFORMANCECOUNTROWSWRITTEN FROM dbo.MANAGER_TRACKING WHERE MGR = '{Name}'");
        }

        //Returns the select string for the table streamers
        private string GetSelectStringsForTableStreamer(string s)
        {
            switch (s)
            {
                case "health":
                    return string.Format($"SELECT REPORT_TYPE, REPORT_NUMERIC_VALUE, LOG_TIME FROM dbo.HEALTH_REPORT " +
                                         $"WHERE (REPORT_TYPE = 'CPU' OR REPORT_TYPE = 'MEMORY')" +
                                         $"AND LOG_TIME > '{StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}'" +
                                         "ORDER BY LOG_TIME");
                case "logging":
                    return string.Format($"SELECT DISTINCT [CREATED], [LOG_MESSAGE], [LOG_LEVEL]," +
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