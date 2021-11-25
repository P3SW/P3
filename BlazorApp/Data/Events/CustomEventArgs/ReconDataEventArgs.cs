using System;
using System.Collections.Generic;
using BlazorApp.Data;

namespace BlazorApp.DataStreaming.Events.CustomEventArgs
{
    public class ReconDataEventArgs : EventArgs
    {
        public List<LogData> ReconDataList { get; set; }
    }
}