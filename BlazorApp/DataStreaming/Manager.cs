using System;
using Datastreaming;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class Manager
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        
        public DateTime StartTime { get; private set; }
        public int RunningTime { get; private set; }

        public HealthData Health { get; set; }
        public ErrorLog Error { get; set; }
        public ReconciliationLog Reconciliation { get; set; }

        public int RowsRead { get; private set; }
        public int RowsWritten { get; private set; }
        public int EfficiencyScore { get; private set; }
        
        public static SqlConnection Connection { get; set; }

        public Manager(string name)
        {
            Health = new HealthData();
            Reconciliation = new ReconciliationLog();
            Error = new ErrorLog();
            Name = name;
        }

        public void WatchManager()
        {
            TableStreamer healthStreamer = new TableStreamer(Connection, SqlQueryStrings.HealthSelect, Health);
            TableStreamer errorStreamer = new TableStreamer(Connection, SqlQueryStrings.ErrorSelect, Error);
            TableStreamer reconciliationStreamer =
                new TableStreamer(Connection, SqlQueryStrings.ReconciliationSelect, Reconciliation);
            healthStreamer.StartListening();
            errorStreamer.StartListening();
            reconciliationStreamer.StartListening();
        }

        public void CalculateEfficiencyScore()
        {
            double CPU = CalculateAverageCPU();
            double result = (RowsRead + RowsWritten) / RunningTime * CPU;
            EfficiencyScore = Convert.ToInt32(result);
        }

        private double CalculateAverageCPU()
        {
            double result = 0.0;
            foreach (Data value in Health.CPU )
            {
                result += value.NumericValue / Health.CPU.Count;
            }
            return result;
        }
    }
}