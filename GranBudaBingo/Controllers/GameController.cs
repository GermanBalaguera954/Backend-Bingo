using GranBudaBingo.Hubs;
using GranBudaBingo.Models;
using GranBudaBingo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GranBudaBingo.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IBingoCardGenerator bingoCardGenerator;
        private readonly IBingoBallService bingoBallService;
        private readonly IHubContext<BingoHub> bingoHub;
        private readonly IBingoCheckService bingoCheckService;

        public GameController(IBingoCardGenerator bingoCardGenerator,
            IBingoBallService bingoBallService, IHubContext<BingoHub> bingoHub,
            IBingoCheckService bingoCheckService)
        {
            this.bingoCardGenerator = bingoCardGenerator;
            this.bingoBallService = bingoBallService;
            this.bingoHub = bingoHub;
            this.bingoCheckService = bingoCheckService;
        }

        [HttpGet("generate/{gameId}")]
        public async Task<ActionResult<BingoCard>> BingoCardGenerateAsync(int gameId)
        {
            var cardNumbers = await bingoCardGenerator.GenerateBingoCardMatrixAsync();
            var bingoCard = new BingoCard
            {
                BingoGameId = gameId
            };
            bingoCard.SetCardNumbers(cardNumbers);

            return Ok(bingoCard);
        }

        [HttpGet("draw/{gameId}")]
        public async Task<ActionResult<BingoBall>> DrawBallAsync(int gameId)
        {
            try
            {
                var ball = await bingoBallService.DrawBallAsync(gameId);
                return Ok(ball);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("start/{gameId}")]
public async Task<IActionResult> StartGame(int gameId)
{
    await bingoBallService.StartNewGameAsync(gameId);
    return Ok("Nuevo juego iniciado");
}

        [HttpPost("checkBingo")]
        public ActionResult<bool> CheckBingo([FromBody] BingoCheckRequest request)
        {
            var isBingo = bingoCheckService.IsBingo(request.MarkedCellNumbers, request.GameType, request.DrawnNumbers);
            return Ok(isBingo);
        }

        [HttpPost("winner")]
        public async Task<ActionResult> Winner()
        {
            await bingoHub.Clients.All.SendAsync("GameWon");
            return Ok();
        }
    }
}
