using System;
using System.Collections.Generic;

namespace BlazorApp.Data
{
    public class Manager
    {
        public Manager(int id, string name, DateTime timestamp, List<Error> errors, List<Reconciliation> recons)
        {
            _id = id;
            _name = name;
            _timestamp = timestamp;
            _errors = errors;
            _recons = recons;
        }
        public int _id { get; protected set; }
        public DateTime _timestamp { get; protected set; }
        public string _name { get; private set; }
        public List<Error> _errors { get; private set; }
        public List<Reconciliation> _recons { get; private set; }
    }
}