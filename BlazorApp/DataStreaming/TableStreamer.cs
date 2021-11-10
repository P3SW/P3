using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class TableStreamer
    {
        private SqlDependency _dependency;
        private string _queryString;
        public static SqlConnection Connection { get; set; }
        private IData _dataObject;
        private bool run;
        public TableStreamer(string queryString, string currentDataQueryString, IData dataObject)
        {
            _queryString = queryString;
            _dataObject = dataObject;
            run = true;
            
            //Queries the database for data currently in the database
            AddQueryToObject(currentDataQueryString);
        }

        //Removes the OnChange event handler and stops the StartListening method
        public void StopListening()
        {
            run = false;
            _dependency.OnChange -= SqlDependencyChange;
        }

        //Method responsible for listening for changes in the tables.
        public void StartListening()
        {
            if (!run)
            {
                return;
            }
            
            try
            {
                //Creates a command and passes it to the SqlDependency constructor
                using (SqlCommand command = new SqlCommand(_queryString, Connection))
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
            using (SqlCommand command = new SqlCommand(queryString, Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("Start");
                    _dataObject.AddDataFromSqlReader(reader);
                    Console.WriteLine("Stop");
                }
            }
        }

        //Closes the reader to ensure only the new data in the table is queried.
        public static void CloseReader(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }
    }
}