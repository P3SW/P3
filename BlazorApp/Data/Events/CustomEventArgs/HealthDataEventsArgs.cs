using System;
using System.Collections.Generic;
using BlazorApp.Data;

namespace BlazorApp.DataStreaming.Events.CustomEventArgs
{
    public class HealthDataEventsArgs : EventArgs
    {
        public List<HealthData> Cpu { get; set; }
        public List<HealthData> Memory { get; set; }
        public string ReportType { get; set; }
    }
}