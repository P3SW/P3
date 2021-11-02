using System;
using System.Data;
using Datastreaming;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class TableStreamer
    {
        private SqlDependency _dependency;
        private string _queryString;
        private SqlConnection _connection;
        private IData _dataObject;
        public TableStreamer(SqlConnection connection, string queryString, IData dataObject)
        {
            _connection = connection;
            _queryString = queryString;
            _dataObject = dataObject;
            
            //Queries the database for data currently in the database
            AddQueryToObject(_queryString);
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
                AddQueryToObject(_dataObject.GetChangesQueryString());
                //Implement SignalR interaction here.

            }
            StartListening();
        }
        
        private void AddQueryToObject(string queryString)
        {
            using (SqlCommand command = new SqlCommand(queryString, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    _dataObject.AddDataFromSqlReader(reader);
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