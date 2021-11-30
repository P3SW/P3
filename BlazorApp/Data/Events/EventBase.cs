using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BlazorApp.Data;
using BlazorApp.DataStreaming.Events.CustomEventArgs;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.DataStreaming.Events
{
    public class EventBase : ComponentBase
    {
        // Health *********************************************************************
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
        
        // Error *********************************************************************
        public static event EventHandler<LogDataEventArgs> ErrorUpdateTriggered;
        public static event EventHandler ErrorUpdateResseted;
        
        public void ErrorTriggerUpdate(List<LogData> logDatalist)
        {
            ErrorUpdateTriggered?.Invoke(this, new LogDataEventArgs
            {
                LogDataList = logDatalist,
            });
        }

        public void ResetLogUpdate()
        {
            ErrorUpdateResseted?.Invoke(this,null);
        }
        
        // Reconciliation ************************************************************
        public static event EventHandler<ReconDataEventArgs> ReconUpdateTriggered;
        public static event EventHandler ReconUpdateResseted;
        
        public void ReconTriggerUpdate(List<LogData> logDatalist)
        {
            ReconUpdateTriggered?.Invoke(this, new ReconDataEventArgs()
            {
                ReconDataList = logDatalist,
            });
        }
        
        // Efficiency ************************************************************
        public static event EventHandler<EfficiencyDataEventArgs> EfficiencyUpdateTriggered;
        public static event EventHandler EfficiencyUpdateResseted;

        public void EfficiencyTriggerUpdate(List<EfficiencyData> efficiencyDatalist)
        {
            EfficiencyUpdateTriggered?.Invoke(this, new EfficiencyDataEventArgs()
            {
                EfficiencyDataList = efficiencyDatalist,
            });
        }

        // Update Overview ********************************************************

        public static event EventHandler UpdateOverviewTriggered;

        public void OverviewTriggerUpdate()
        {
            UpdateOverviewTriggered?.Invoke(this,null);
        }
    }
}