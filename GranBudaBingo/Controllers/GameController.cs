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

        [HttpGet("generate")]
        public async Task<ActionResult<BingoCard>> BingoCardGenerateAsync()
        {
            var bingoCard = await Task.Run(() => new BingoCard(bingoCardGenerator));
            return Ok(bingoCard);
        }

        [HttpGet("draw")]
        public IActionResult DrawBall()
        {
            try
            {
                var ball = bingoBallService.DrawBall();
                return Ok(ball);
            }
            catch (InvalidOperationException e)
            {
                return Ok(e.Message);
            }
        }

        [HttpPost("start")]
        public IActionResult StartGame()
        {
            bingoBallService.StartNewGame();
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
