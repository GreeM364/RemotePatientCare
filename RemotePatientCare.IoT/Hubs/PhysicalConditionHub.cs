using Microsoft.AspNetCore.SignalR;

namespace RemotePatientCare.IoT.Hubs
{
    public class PhysicalConditionHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserDate", Context.ConnectionId);
        }
    }
}
