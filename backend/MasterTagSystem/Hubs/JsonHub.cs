using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MasterTagSystem.Hubs
{
    public class JsonHub : Hub
    {
        // Méthode pour diffuser les mises à jour de JSON
        public async Task SendJsonUpdate(object jsonData)
        {
            await Clients.All.SendAsync("ReceiveJsonUpdate", jsonData);
        }
    }
}
