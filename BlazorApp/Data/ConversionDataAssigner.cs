using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BlazorApp.Data;
using BlazorApp.Pages;
using Microsoft.Data.SqlClient;
using SQLDatabaseRead;


namespace BlazorApp.Data
{
    public static class ConversionDataAssigner
    {

        public static List<ManagerStatusHandler> FinishedManagers { get; private set; } = new List<ManagerStatusHandler>();
        public static ManagerStatusHandler CurrentManager;

        private static SqlConnection _connection;
        private static string _connectionString;
        public static int _managerQueue = 0;
        private static int _managerId;
        private static int _executionId;
        
        //Method starting the tracking of the tables in the DB. This is done by querying rows from the Managers table. 
        //The program will wait for data if the table is empty
        public static void Start(string setupFile)
        {
            _connectionString = ConfigReader.ReadSetupFile(setupFile);
            _managerQueue = 0;
            _managerId = 1;
            CurrentManager = null;
            _executionId = 1;
            
            Console.WriteLine("Engine started");

            SqlConnection conn = new SqlConnection(_connectionString);
            
            try
            {
                conn.Open();
                
                SqlDependency.Stop(_connectionString);
                SqlDependency.Start(_connectionString);
                ManagerStatusHandler.Connection = new SqlConnection(_connectionString);
                SQLDependencyListener.Connection = new SqlConnection(_connectionString);
                
                SQLDependencyListener.Connection.Open();
                ManagerStatusHandler.Connection.Open();
                
                _connection = conn;
                using (SqlCommand command = new SqlCommand(DatabaseListenerQueryStrings.ManagersSelect, _connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) //If the result of the query contains any rows, they will be added to the queue
                        {
                            Summary.Runtime.StartTimer(); //Starts the timer for conversion runtime on summary
                            while (reader.Read())
                            {
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
        private static void WaitForStart()
        {
            using (SqlCommand command = new SqlCommand(DatabaseListenerQueryStrings.ManagersSelect, _connection))
            {
                command.CommandType = CommandType.Text;
                command.CommandText = DatabaseListenerQueryStrings.ManagersSelect;
                    
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange +=  ContinueSetup;
                    
                SQLDependencyListener.CloseReader(command);

            }            
        }
        
        //Continues the setup by adding managers to the queue when rows are inserted into the MANAGERS table
        //The MANAGER_TRACKING table will be watched and the first manager will be started after the rows in MANAGERS are read
        private static async void ContinueSetup(object sender, SqlNotificationEventArgs eventArgs)
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
        }

        private static void GetExecutionID(object sender, SqlNotificationEventArgs eventArgs)
        {
            if (eventArgs.Info == SqlNotificationInfo.Invalid)
            {
                Console.WriteLine("Info: {0}, Source: {1}, Type: {2}", eventArgs.Info, eventArgs.Source,
                    eventArgs.Type);
            }

            using (SqlCommand command = new SqlCommand(DatabaseListenerQueryStrings.ExecutionIDSelect, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _executionId = Convert.ToInt32(reader[0]);
                    }
                    reader.Close();
                }
            }
        }

        //Listens for updates in the MANAGER_TRACKING table using a SqlDependency. The eventhandler stops the current manager from listening 
        private static void ManagerTrackingListener()
        {
            using (SqlCommand command = new SqlCommand(DatabaseListenerQueryStrings.ManagerStartTimesSelect, _connection))
            {
                
                command.CommandType = CommandType.Text;
                command.CommandText = DatabaseListenerQueryStrings.ManagerStartTimesSelect;
                    
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange += GetExecutionID;
                dependency.OnChange += ManagerStartTracking;
                Console.WriteLine("Manager start dependency created");
                
                SQLDependencyListener.CloseReader(command);
            }
        }

        //Method handling the event. Calls the next manager and creates a new SqlDependency to track the table again
        private static void ManagerStartTracking(object sender, SqlNotificationEventArgs eventArgs)
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
                        if (CurrentManager != null) //Checks if a manager is running
                        {
                            Console.WriteLine("Finishing manager");
                            CurrentManager.FinishManager();
                            FinishedManagers.Add(CurrentManager);
                            PrintFinishedManager();
                        }

                        if (_managerQueue == 0) //If the last manager has run the method will stop
                        {
                            CurrentManager = null;
                            return;
                        }
                    }
                    
                    if (reader.Read())
                    {
                        Console.WriteLine("Starting new manager");
                        CurrentManager = new ManagerStatusHandler((string)reader[0], _managerId, (DateTime)reader[1], _executionId);
                        Console.WriteLine("New manager name is " + reader[0]);
                        _managerId++;
                        _managerQueue--;
                        Summary.Runtime.CurrentManagerResetTimer();
                    }
                    reader.Close();
                }
            }
            CurrentManager.WatchManager();
            ManagerTrackingListener();
        }

        private static void PrintFinishedManager()
        {
            Console.WriteLine($"Name: {CurrentManager.Name}\n" +
                              $"Status: {CurrentManager.Status}\n" +
                              $"Runtime: {CurrentManager.RunTime}\n" +
                              $"Reconciliations: {CurrentManager.ReconciliationHandler.LogDataList.Count}\n" +
                              $"Errors: {CurrentManager.ErrorHandler.LogDataList.Count}\n" +
                              $"Rows read: {CurrentManager.RowsRead}\n" +
                              $"Rows written: {CurrentManager.RowsWritten}\n" +
                              $"Average CPU: {CurrentManager.Cpu}\n" +
                              $"Memory logs: {CurrentManager.Health.Memory.Count}\n" +
                              $"CPU logs: {CurrentManager.Health.Cpu.Count}\n" +
                              $"Efficiency score: {CurrentManager.EfficiencyScore}");
        }
        
        public static async Task<List<LogData>> GetErrorLogList(string type)
        {
            if (CurrentManager == null)
            {
                Console.WriteLine(CurrentManager);
                Console.WriteLine("Sending new list");
                return new List<LogData>();
            }

            List<LogData> list = new List<LogData>();

            if (FinishedManagers.Count > 0)
            {
                foreach (var finishedManager in FinishedManagers)
                {
                    if (type == "error") 
                        list.AddRange(finishedManager.ErrorHandler.LogDataList);
                    else 
                        list.AddRange(finishedManager.ReconciliationHandler.LogDataList);
                }
                
            }
            
            if (type == "error") 
                list.AddRange(CurrentManager.ErrorHandler.LogDataList);
            else 
                list.AddRange(CurrentManager.ReconciliationHandler.LogDataList);
            
            return await Task.FromResult(list);
        }
        
    }
}