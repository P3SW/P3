using System;

namespace BlazorApp.Data
{
    public class Error
    {
        public Error(int id, string severity, DateTime timestamp, string message)
        {
            _id = id;
            _severity = severity;
            _timestamp = timestamp;
            _message = message;
        }
        public int _id { get; protected set; }
        public DateTime _timestamp { get; protected set; }
        public string _severity { get; private set; }
        public string _message { get; private set; }
        
    }
}