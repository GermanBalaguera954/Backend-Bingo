using GranBudaBingo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("[controller]")]
public class BingoController : ControllerBase
{
    private BingoGame bingoGame;

    public BingoController()
    {
        bingoGame = new BingoGame();
    }

    [HttpGet("stream")]
    public async Task GetBingoStream()
    {

        Response.ContentType = "text/event-stream";

        while (!HttpContext.RequestAborted.IsCancellationRequested)
        {
            var nextBall = bingoGame.GetNextBall();
            if (nextBall == null)
            {
                break; // Termina el stream si todas las balotas han sido seleccionadas
            }

            await Response.WriteAsync($"data: {JsonConvert.SerializeObject(nextBall)}\n\n");
            await Response.Body.FlushAsync();

            await Task.Delay(5000); // Esperar 5 segundos
        }
    }
}