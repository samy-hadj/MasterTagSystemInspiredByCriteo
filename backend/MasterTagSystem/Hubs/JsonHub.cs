using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MasterTagSystem.Hubs
{
    /// <summary>
    /// Hub class for handling JSON updates.
    /// </summary>
    public class JsonHub : Hub
    {
        /// <summary>
        /// Method to broadcast JSON updates to all connected clients.
        /// </summary>
        /// <param name="jsonData">The JSON data to be sent to clients.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task SendJsonUpdate(object jsonData)
        {
            await Clients.All.SendAsync("ReceiveJsonUpdate", jsonData);
        }
    }
}
