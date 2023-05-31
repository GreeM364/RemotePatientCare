using Microsoft.AspNetCore.SignalR;

namespace RemotePatientCare.IoT.Hubs
{
    public class PhysicalConditionHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserDate", Context.ConnectionId);
        }

        public async Task Enter( string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
