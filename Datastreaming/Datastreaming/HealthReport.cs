namespace Datastreaming
{
    public class HealthReport
    {
        public int RowNo { get; set; }
        
        public int MonitorNo { get; set; }
        
        public int ExecutionId { get; set; }
        
        public string ReportType { get; set; }
        
        public string ReportKey { get; set; }
        
        public string ReportStringValue { get; set; }
        
        public long ReportNumericValue { get; set; }
        
        public string ReportValueType { get; set; }
        
        public string ReportValueHuman { get; set; }
        
        public string LogTime { get; set; }
        
    }
}