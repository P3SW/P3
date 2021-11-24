using System;

namespace BlazorApp.DataStreaming.Events.CustomEventArgs
{
    public class UpdateEventArgs : EventArgs
    {
        public string Message { get; set; }
        public DateTime Date { get; } = DateTime.Now;
    }
}