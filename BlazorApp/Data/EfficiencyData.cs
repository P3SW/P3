using System;

namespace BlazorApp.Data
{
    public class EfficiencyData
    {
        public string ManagerName { get; set; }
        public int Score { get; set; }
        public DateTime TimeFinished { get; set; }
        public int RowsRead { get; set; }
        public int RowsWritten { get; set; }
        public int TimeRan { get; set; }
        public int AvgCpu { get; set; }
        public int AvgMemoryPercent { get; set; }

        public EfficiencyData(string managerName, int score, DateTime timeFinished, int rowsRead, int rowsWritten, int timeRan, int avgCpu, int avgMemoryPercent)
        {
            ManagerName = managerName;
            Score = score;
            TimeFinished = timeFinished;
            RowsRead = rowsRead;
            RowsWritten = rowsWritten;
            TimeRan = timeRan;
            AvgCpu = avgCpu;
            AvgMemoryPercent = avgMemoryPercent;
        }


    }
}