using System;
using System.Collections.Generic;
using System.Data;
using BlazorApp.DataStreaming.Events;
using Microsoft.Data.SqlClient;

namespace BlazorApp.DataStreaming
{
    public class Engine : EventBase
    {
        public List<Manager> FinishedManagers { get; private set; }
        private Manager _currentManager;
        private SqlConnection _connection;
        private Queue<Manager> _managerQueue;
        private string _connectionString;
        
        //Constructor initialising the some of the fields in the class
        public Engine()
        {
            _connectionString = ConfigReader.ReadSetupFile();
            _managerQueue = new Queue<Manager>();
            FinishedManagers = new List<Manager>();
        }

        //Method starting the tracking of the tables in the DB. This is done by querying rows from the Managers table. 
        //The program will wait for data if the table is empty
        public void Start()
        {
            Console.WriteLine("Engine started");
            //new EventBase().TriggerUpdate("UPDATED");
            //TriggerUpdate("UPDATED");
            
            SqlConnection conn = new SqlConnection(_connectionString);
            
            try
            {
                conn.Open();
                
                SqlDependency.Stop(_connectionString);
                SqlDependency.Start(_connectionString);
                Manager.Connection = conn;
                TableStreamer.Connection = conn;

                _connection = conn;
                
                using (SqlCommand command = new SqlCommand(SqlQueryStrings.ManagersSelect, _connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) //If the result of the query contains any rows, they will be added to the queue
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("READING MANAGER");
                                AddManagerToQueue((string) reader[0], (int) reader[1]);
                            }
                            reader.Close();
                            ManagerTrackingListener();
                            WatchNextManager();
                        }
                        else //The program will wait for rows to be inserted into the table if the table is empty
                        {
                            reader.Close();
                            WaitForStart();
                        }
                    
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Creates a SqlDependency with an eventhandler which is called when data is inserted in the table
        private void WaitForStart()
        {
            using (SqlCommand command = new SqlCommand(SqlQueryStrings.ManagersSelect, _connection))
            {
                command.CommandType = CommandType.Text;
                command.CommandText = SqlQueryStrings.ManagersSelect;
                    
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange += ContinueSetup;
                    
                TableStreamer.CloseReader(command);
            }            
        }
        
        //Continues the setup by adding managers to the queue when rows are inserted into the MANAGERS table
        //The MANAGER_TRACKING table will be watched and the first manager will be started after the rows in MANAGERS are read
        private void ContinueSetup(object sender, SqlNotificationEventArgs eventArgs)
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
                        AddManagerToQueue((string) reader[0], (int) reader[1]);
                    }
                }
            }
            ManagerTrackingListener();
            WatchNextManager();
        }

        //Creates a manager from a string and an int and enqueues it.
        public void AddManagerToQueue(string name, int id)
        {
            _managerQueue.Enqueue(new Manager(name, id));
        }
        
        //Listens for updates in the MANAGER_TRACKING table using a SqlDependency. The eventhandler stops the current manager from listening 
        private void ManagerTrackingListener()
        {
            using (SqlCommand command = new SqlCommand(SqlQueryStrings.ManagerTrackingSelect, _connection))
            {
                command.CommandType = CommandType.Text;
                command.CommandText = SqlQueryStrings.ManagerTrackingSelect;
                    
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange += ManagerTrackingChange;
                    
                TableStreamer.CloseReader(command);
            }
        }

        //Method handling the event. Calls the next manager and creates a new SqlDependency to track the table again
        private void ManagerTrackingChange(object sender, SqlNotificationEventArgs eventArgs)
        {
            if (eventArgs.Info == SqlNotificationInfo.Invalid)
            {
                Console.WriteLine("Info: {0}, Source: {1}, Type: {2}", eventArgs.Info, eventArgs.Source,
                    eventArgs.Type);
            }
            else
            {
                Console.WriteLine("Changing manager!");
                WatchNextManager();
            }
            ManagerTrackingListener();
        }

        //Stops the current manager and starts the next one. This does nothing if the queue is empty
        private void WatchNextManager()
        {
            if (_currentManager != null) //Checks if a manager is running
            {
                FinishedManagers.Add(_currentManager);
                _currentManager.FinishManager();
                PrintFinishedManager();
            }

            if (_managerQueue.Count == 0) //If the last manager has run the method will stop
            {
                _currentManager = null;
                return;
            }

            _currentManager = _managerQueue.Dequeue(); 
            _currentManager.WatchManager();

            //Send status of finished manager here
            
        }

        private void PrintFinishedManager()
        {
            Console.WriteLine($"Name: {_currentManager.Name}\nStatus: {_currentManager.Status}\n" +
                              $"Runtime: {_currentManager.RunTime}\nRows read: {_currentManager.RowsRead}\n" +
                              $"Rows written: {_currentManager.RowsWritten}\nAverage CPU: {_currentManager.Cpu}\n" +
                              $"Efficiency score: {_currentManager.EfficiencyScore}");
        }
        
    }
}