using System.Collections.Generic;

namespace BlazorApp.Data
{
    public class Engine
    {
        
        private Queue<Manager> ManagerQueue = new Queue<Manager>();

        public void AddManagerToQueue(string ManagerName)
        {
            ManagerQueue.Enqueue(new Manager(ManagerName, false));
        }

        public void StartNewManager(string ManagerName)
        {
            Manager StartedManager = ManagerQueue.Dequeue();
            StartedManager.Start();
        }

    }
}