using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Data
{
    public class Manager
    {
        public Manager(string name, bool running)
        {
            _name = name;
            _running = running;
        }

        private string _startTime;
        private string _name;
        private bool _running;
        private int _reconciliationid = 1;
        private int _errorlogid = 1;

        public void Start()
        {
            _running = true;
        }

        public void Stop()
        {
            _running = false;
        }
        
        private List<Error> _errorLogs = new List<Error>();
        private List<Reconciliation> _reconciliations = new List<Reconciliation>();

        public void AddError(string severity, string timestamp, string message)
        {
            _errorLogs.Add(new Error(_errorlogid, severity, timestamp, message));
            _errorlogid++;
        }
        
        public void AddReconciliation(string result, string timestamp, string description)
        {
            _reconciliations.Add(new Reconciliation(_reconciliationid, result, timestamp, description));
            _errorlogid++;
        }

        public Error FindErrorLog(int id)
        {
            return _errorLogs.Find(ErrorLog => ErrorLog.Id == id);
        }

        public Reconciliation FindRecociliation(int id)
        {
            return _reconciliations.Find(Reconciliation => Reconciliation.Id == id);
        }

        public void ManagerFinished()
        {
            _running = false;
        }

    }
}