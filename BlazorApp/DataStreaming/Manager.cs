using System;
using Datastreaming;

namespace BlazorApp.DataStreaming
{
    public class Manager
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        
        public DateTime StartTime { get; private set; }
        //TODO: find out whether or not TimeSpan is what we should use instead of DateTime for RunningTime property. Seems like a frontend problem to me so idk 
        public int RunningTime { get; private set; }

        public HealthData Health { get; private set; }
        public ErrorData Error { get; private set; }
        public ReconciliationData Reconciliation { get; private set; }

        public int RowsRead { get; private set; }
        public int RowsWritten { get; private set; }
        
        public int EfficiencyScore { get; private set; }

        public Manager()
        {
            Health = new HealthData();
            
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