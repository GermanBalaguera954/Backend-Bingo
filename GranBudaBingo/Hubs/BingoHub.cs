using Microsoft.AspNetCore.SignalR;

namespace GranBudaBingo.Hubs
{
    public class BingoHub : Hub
    {
        public async Task SendBingo(string user)
        {
            await Clients.Others.SendAsync("ReceiveMessage", $"{user} han realizado BINGO! Redirigiendo al inicio...");
        }
    }
}
