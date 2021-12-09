using System;
using System.Collections.Generic;
using BlazorApp.DataStreaming.Events.CustomEventArgs;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Data.Events
{
    public class EventBase : ComponentBase
    {
        // Health *********************************************************************
        public static event EventHandler<HealthDataEventsArgs> HealthUpdateTriggered;

        public void HealthTriggerUpdate(List<HealthData> cpu, List<HealthData> memory)
        {
            HealthUpdateTriggered?.Invoke(this, new HealthDataEventsArgs 
            { Cpu = cpu, 
              Memory = memory
            });
        }

        // Error *********************************************************************
        public static event EventHandler<LogDataEventArgs> ErrorUpdateTriggered;
        
        public void ErrorTriggerUpdate(List<LogData> logDatalist)
        {
            ErrorUpdateTriggered?.Invoke(this, new LogDataEventArgs
            {
                NewLogDataList = logDatalist,
            });
        }

        // Reconciliation ************************************************************
        public static event EventHandler<ReconDataEventArgs> ReconUpdateTriggered;

        public void ReconTriggerUpdate(List<LogData> logDatalist)
        {
            ReconUpdateTriggered?.Invoke(this, new ReconDataEventArgs()
            {
                ReconDataList = logDatalist,
            });
        }
        
        // Update Overview ********************************************************

        public static event EventHandler UpdateOverviewTriggered;

        public void OverviewTriggerUpdate()
        {
            UpdateOverviewTriggered?.Invoke(this,null!);
        }
    }
}