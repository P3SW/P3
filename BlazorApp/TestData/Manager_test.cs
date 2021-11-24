using System;
using System.Collections.Generic;

namespace BlazorApp.Data
{
    public class Manager_test
    {
        public Manager_test(int id, string name, int runtime, string status, DateTime timestamp, List<Error> errors, List<Reconciliation> recons)
        {
            _id = id;
            _name = name;
            _status = status;
            _timestamp = timestamp;
            _errors = errors;
            _recons = recons;
            _runtime = runtime;
        }
        public int _id { get; protected set; }
        public DateTime _timestamp { get; protected set; }
        public string _name { get; private set; }
        public int _runtime { get; private set; }
        public string _status { get; private set; }
        public List<Error> _errors { get; private set; }
        public List<Reconciliation> _recons { get; private set; }
    }
}