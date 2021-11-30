using System;
using System.Collections.Generic;
using BlazorApp.Data;

namespace BlazorApp.DataStreaming.Events.CustomEventArgs
{
    public class EfficiencyDataEventArgs : EventArgs
    {
        public List<EfficiencyData> EfficiencyDataList { get; set; }
    }
}