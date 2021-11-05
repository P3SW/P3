using System;

namespace BlazorApp.Data
{
    public abstract class Log
    {
        public int _id { get; protected set; }

        public int Id
        {
            get
            {
                return _id;
            }
        }
        public DateTime _timestamp { get; protected set; }


    }
}