using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public class DatabaseRead
    {
        public void SqlReader(SqlCommand command, Action<SqlDataReader> readerHandler)
        {
            using(SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
            {
                while (reader.Read())
                {
                    readerHandler(reader);
                }
            }
        }
    }
}