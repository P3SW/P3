using System;
using System.Collections.Generic;

namespace BlazorApp.Data
{
    public class HealthDataTest_cpu
    {

        public HealthDataTest_cpu(string ReportType, int NumericValue, DateTime Date)
        {
            _ReportType = ReportType;
            _NumericValue = NumericValue;
            _Date = Date;
        }
        
        public string _ReportType { get; private set; }
        public int _NumericValue { get; private set; }
        public DateTime _Date { get; private set; }
        
    }
}