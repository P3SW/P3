namespace BlazorApp.Data
{
    public class Error : Log
    {
        public Error(int id, string severity, string timestamp, string message)
        {
            _id = id;
            _severity = severity;
            _timestamp = timestamp;
            _message = message;
        }
        
        private string _severity;
        private string _message;
    }
}