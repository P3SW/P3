using System;
using System.Collections.Generic;
using BlazorApp.Data;
using BlazorApp.DataStreaming.Events.CustomEventArgs;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.DataStreaming.Events
{
    public class EventBase : ComponentBase
    {
        public static event EventHandler<HealthDataEventsArgs> HealthUpdateTriggered;
        public static event EventHandler HealthUpdateResseted;
        
        public void HealthTriggerUpdate(List<HealthData> cpu, List<HealthData> memory)
        {
            HealthUpdateTriggered?.Invoke(this, new HealthDataEventsArgs 
            { Cpu = cpu, 
              Memory = memory
            });
        }
        public void HealthResetUpdate(string message)
        {
            HealthUpdateResseted?.Invoke(this, null);
        }
        
        public static event EventHandler<LogDataEventArgs> LogUpdateTriggered;
        public static event EventHandler LogUpdateResseted;

        public void LogTriggerUpdate(List<LogData> logDatalist)
        {
            LogUpdateTriggered?.Invoke(this, new LogDataEventArgs
            {
                LogDataList = logDatalist,
            });
        }

        public void ResetLogUpdate()
        {
            LogUpdateResseted?.Invoke(this,null);
        }
    }
}