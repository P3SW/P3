using System;
using BlazorApp.DataStreaming.Events.CustomEventArgs;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.DataStreaming.Events
{
    public class EventBase : ComponentBase
    {
        // Every componend should be involved
        // Invoke event only in this class
        public static event EventHandler<UpdateEventArgs> UpdateTriggered;
        public static event EventHandler UpdateResseted;

        public void TriggerUpdate(string message)
        {
            // If NOT null do..
            UpdateTriggered?.Invoke(this, new UpdateEventArgs { Message = message});
        }
        
        public void ResetUpdate(string message)
        {
            // If NOT null do..
            UpdateResseted?.Invoke(this, new UpdateEventArgs { Message = message});
        }
    }
}