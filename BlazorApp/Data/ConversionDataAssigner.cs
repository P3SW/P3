using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SQLDatabaseRead;

namespace BlazorApp.Data
{
    public class ConversionDataAssigner
    {
        public List<ManagerStatusHandler> FinishedManagers { get; private set; }
        private ManagerStatusHandler _currentManager;
        private SqlConnection _connection;
        private string _connectionString;
        private int _managerQueue;
        private int _managerId;


        //Constructor initialising the some of the fields in the class
        public ConversionDataAssigner()
        {
            _connectionString = ConfigReader.ReadSetupFile();
            _managerQueue = 0;
            FinishedManagers = new List<ManagerStatusHandler>();
            _managerId = 1;
            _currentManager = null;
        }

        //Method starting the tracking of the tables in the DB. This is done by querying rows from the Managers table. 
        //The program will wait for data if the table is empty
        public void Start()
        {
            Console.WriteLine("Engine started");

            SqlConnection conn = new SqlConnection(_connectionString);
            
            try
            {
                conn.Open();
                
                SqlDependency.Stop(_connectionString);
                SqlDependency.Start(_connectionString);
                ManagerStatusHandler.Connection = new SqlConnection(_connectionString);
                TableStreamer.Connection = new SqlConnection(_connectionString);

                TableStreamer.Connection.Open();
                ManagerStatusHandler.Connection.Open();
                
                _connection = conn;

                using (SqlCommand command = new SqlCommand(DatabaseListenerQueryStrings.ManagersSelect, _connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) //If the result of the query contains any rows, they will be added to the queue
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("READING MANAGER");
                                _managerQueue++;
                            }
                            reader.Close();
                            ManagerTrackingListener();
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
            using (SqlCommand command = new SqlCommand(DatabaseListenerQueryStrings.ManagersSelect, _connection))
            {
                command.CommandType = CommandType.Text;
                command.CommandText = DatabaseListenerQueryStrings.ManagersSelect;
                    
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange +=  ContinueSetup;
                    
                TableStreamer.CloseReader(command);

            }            
        }
        
        //Continues the setup by adding managers to the queue when rows are inserted into the MANAGERS table
        //The MANAGER_TRACKING table will be watched and the first manager will be started after the rows in MANAGERS are read
        private async void ContinueSetup(object sender, SqlNotificationEventArgs eventArgs)
        {
            if (eventArgs.Info == SqlNotificationInfo.Invalid)
            {
                Console.WriteLine("Info: {0}, Source: {1}, Type: {2}", eventArgs.Info, eventArgs.Source, eventArgs.Type);
            }
            
            await Task.Delay(1000);
            
            using (SqlCommand command = new SqlCommand(DatabaseListenerQueryStrings.ManagersSelect, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("READING MANAGER");
                        _managerQueue++;
                    }
                    reader.Close();
                }
            }
            ManagerTrackingListener();
            //WatchNextManager();
        }

        //Listens for updates in the MANAGER_TRACKING table using a SqlDependency. The eventhandler stops the current manager from listening 
        private void ManagerTrackingListener()
        {
            using (SqlCommand command = new SqlCommand(DatabaseListenerQueryStrings.ManagerStartTimesSelect, _connection))
            {
                
                command.CommandType = CommandType.Text;
                command.CommandText = DatabaseListenerQueryStrings.ManagerStartTimesSelect;
                    
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange += ManagerStartTracking;
                
                Console.WriteLine("Dependency created");
                TableStreamer.CloseReader(command);
            }
        }

        //Method handling the event. Calls the next manager and creates a new SqlDependency to track the table again
        private void ManagerStartTracking(object sender, SqlNotificationEventArgs eventArgs)
        {
            if (eventArgs.Info == SqlNotificationInfo.Invalid)
            {
                Console.WriteLine("Info: {0}, Source: {1}, Type: {2}", eventArgs.Info, eventArgs.Source,
                    eventArgs.Type);
            }

            using (SqlCommand command = new SqlCommand(DatabaseListenerQueryStrings.ManagerStartTimesSelect, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (_currentManager != null) //Checks if a manager is running
                        {
                            Console.WriteLine("Finishing manager");
                            _currentManager.FinishManager();
                            FinishedManagers.Add(_currentManager);
                            PrintFinishedManager();
                        }

                        if (_managerQueue == 0) //If the last manager has run the method will stop
                        {
                            _currentManager = null;
                            return;
                        }
                    }
                    
                    if (reader.Read())
                    {
                        Console.WriteLine("Starting new manager");
                        _currentManager = new ManagerStatusHandler((string)reader[0], _managerId, (DateTime)reader[1]);
                        Console.WriteLine("New manager name is " + reader[0]);
                        _managerId++;
                        _managerQueue--;
                    }
                    reader.Close();
                }
            }
            _currentManager.WatchManager();
            ManagerTrackingListener();
        }

        private void PrintFinishedManager()
        {
            Console.WriteLine($"Name: {_currentManager.Name}\n" +
                              $"Status: {_currentManager.Status}\n" +
                              $"Runtime: {_currentManager.RunTime}\n" +
                              $"Reconciliations: {_currentManager.ReconciliationHandler.LogDataList.Count}\n"+
                              $"Errors: {_currentManager.ErrorHandler.LogDataList.Count}\n"+
                              $"Rows read: {_currentManager.RowsRead}\n" +
                              $"Rows written: {_currentManager.RowsWritten}\n" +
                              $"Average CPU: {_currentManager.Cpu}\n" +
                              $"Memory logs: {_currentManager.Health.Memory.Count}\n"+
                              $"CPU logs: {_currentManager.Health.Cpu.Count}\n"+
                              $"Efficiency score: {_currentManager.EfficiencyScore}");
        }
        
    }
}