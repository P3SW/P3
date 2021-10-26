using System;

namespace Datastreaming
{
    public abstract class Data
    {
        public static DateTime LastRow;

        public abstract string GetChangesQueryString();
    }
}