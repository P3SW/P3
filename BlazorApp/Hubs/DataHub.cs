using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BlazorServerSignalRApp.Server.Hubs
{
    public class DataHub : Hub
    {
        public async Task SendError(string managerName, string severity, string timestamp, string message)
        {   
            // Send new error to all clients
            await Clients.All.SendAsync("ReceiveError", managerName, severity, timestamp, message);
        }
        public async Task SendReconciliation(string managerName, string result, string timestamp, string description)
        {   
            // Send new reconciliation to all clients
            await Clients.All.SendAsync("ReceiveReconciliation", managerName, result, timestamp, description);
        }
        public async Task SendHealth(string type, int value)
        {   
            // Send new health data to all clients
            await Clients.All.SendAsync("ReceiveHealth", type, value);
        }
    }
}