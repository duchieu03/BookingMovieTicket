using Microsoft.AspNetCore.SignalR;

namespace MovieTicketBooking.Hubs
{
    public class DocumentHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
