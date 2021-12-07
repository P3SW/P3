using System;
using System.Collections.Generic;
using BlazorApp.Data;

namespace BlazorApp.DataStreaming.Events.CustomEventArgs
{
    public class LogDataEventArgs : EventArgs
    {
        public List<LogData> NewLogDataList { get; set; }
    }
}