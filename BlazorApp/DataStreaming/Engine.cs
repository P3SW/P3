using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public static class Engine
    {
        public static List<Manager> Managers { get; private set; }
        private static Manager _currentManager;
        private static SqlConnection _connection;
        private static Queue<Manager> _managerQueue;
        

        public static void Start()
        {
            string connectionString = ConfigReader.ReadSetupFile();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDependency.Stop(connectionString);
                SqlDependency.Start(connectionString);
                Manager.Connection = conn;
                _connection = conn;
                using (SqlCommand command = new SqlCommand(SqlQueryStrings.ManagersSelect, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = SqlQueryStrings.ManagersSelect;
                    
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += ContinueSetup;
                    
                    TableStreamer.CloseReader(command);
                }
                
            }

        }

        private static void ContinueSetup(object sender, SqlNotificationEventArgs eventArgs)
        {
            if (eventArgs.Info == SqlNotificationInfo.Invalid)
            {
                Console.WriteLine("Info: {0}, Source: {1}, Type: {2}", eventArgs.Info, eventArgs.Source, eventArgs.Type);
            }
            
            using (SqlCommand command = new SqlCommand(SqlQueryStrings.ManagersSelect, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AddManagerToQueue((string) reader["MANAGER_NAME"], (int) reader["ROW_ID"]);
                    }
                }
            }
            
            
        }

        public static void AddManagerToQueue(string name, int id)
        {
            _managerQueue.Enqueue(new Manager(name, id));
        }

        public static void WatchNextManager()
        {
            if (_currentManager != null)
            {
                Managers.Add(_currentManager);
            }   
            _currentManager = _managerQueue.Dequeue();
            _currentManager.WatchManager();

        }

    }
}