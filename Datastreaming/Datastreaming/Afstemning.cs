using System.Globalization;

namespace Datastreaming
{
    public class Afstemning
    {
        public string Id { get; set; }
        
        public string AfstemtDato { get; set; }
        
        public string Description { get; set; }
        
        public string Manager { get; set; }
        
        public string Context { get; set; }
        
        public int SrcAntal { get; set; }
        
        public int DstAntal { get; set; }
        
        public int CustomAntal { get; set; }
        
        public string AfstemResultat { get; set; }
        
        public string RunJob { get; set; }
        
        public int ToolKitId { get; set; }
        
        public int SrcSqlCost { get; set; }
        
        public int DstSqlCost { get; set; }
        
        public int CustomSqlCost { get; set; }
        
        public string SrcSql { get; set; }
        
        public string DstSql { get; set; }
        
        public string CustomSql { get; set; }
        
        public int SrcSqlTime { get; set; }
        
        public int DestSqlTime { get; set; }
        
        public int CustomSqlTime { get; set; }
        
        public string StartTime { get; set; }
        
        public string EndTime { get; set; }
        
        public string AfstemningsData { get; set; }
        
        
        
    }
}

