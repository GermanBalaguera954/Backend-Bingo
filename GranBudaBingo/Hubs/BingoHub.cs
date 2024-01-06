using Microsoft.AspNetCore.SignalR;

namespace GranBudaBingo.Hubs
{
    public class BingoHub : Hub
    {
        public async Task NotifyGameWon()
        {
            await Clients.Others.SendAsync("GameWon");
        }
    }
}
