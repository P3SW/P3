using System;

namespace BlazorApp.Data
{
    public class Error : Log
    {
        public Error(int id, string severity, DateTime timestamp, string message)
        {
            _id = id;
            _severity = severity;
            _timestamp = timestamp;
            _message = message;
        }
        
        public string _severity { get; private set; }
        public string _message { get; private set; }
        
    }
}