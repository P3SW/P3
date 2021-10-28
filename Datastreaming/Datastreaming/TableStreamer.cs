using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public class TableStreamer<T> : IStreamer where T : IData, new()
    {
        private SqlDependency _dependency;
        private string _queryString;
        private SqlConnection _connection;
        private List<T> dataList;
        public TableStreamer(SqlConnection connection, string queryString)
        {
            _connection = connection;
            _queryString = queryString;
            dataList = new List<T>();
            
            //Queries the database for data currently in the database
            AddQueryToList(_queryString, dataList);
        }

        //Function responsible for listening for changes in the tables.
        public void StartListening()
        {   
            try
            {
                //Creates a command and passes it to the SqlDependency constructor
                using (SqlCommand command = new SqlCommand(_queryString, _connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = _queryString;
                
                    _dependency = new SqlDependency(command);
                    _dependency.OnChange += SqlDependencyChange;
                    
                    CloseReader(command);
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void SqlDependencyChange(object sender, SqlNotificationEventArgs eventArgs)
        {
            if (eventArgs.Info == SqlNotificationInfo.Invalid)
            {
                Console.WriteLine("Info: {0}, Source: {1}, Type: {2}", eventArgs.Info, eventArgs.Source, eventArgs.Type);
            }
            else
            {
                //To minimise network traffic, a separate list containing only the changes is made and send to the client
                List<T> changesList = new List<T>();
                AddQueryToList(new T().GetChangesQueryString(), changesList);
                //Implement SignalR interaction here.

                dataList.AddRange(changesList);
                
            }
            StartListening();
        }
        
        private void AddQueryToList(string queryString, List<T> list)
        {
            using (SqlCommand command = new SqlCommand(queryString, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Creates objects of the given type and stores the data from the reader in said objects.
                        T t = new T();
                        t.ConstructFromSqlReader(reader);
                        list.Add(t);
                        
                        //Prints the reader in the console
                        // for (int i = 0; i < reader.VisibleFieldCount; i++)
                        // {
                        //     Console.Write("{0} ", reader[i]);
                        // }
                        // Console.Write("\n");
                    }
                }
            }
        }

        //Closes the reader to ensure only the new data in the table is queried.
        private static void CloseReader(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }
    }
}