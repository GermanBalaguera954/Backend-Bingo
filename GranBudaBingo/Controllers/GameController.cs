﻿using GranBudaBingo.Models;
using GranBudaBingo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GranBudaBingo.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IBingoCardGenerator bingoCardGenerator;
        private readonly IBingoBallService bingoBallService;

        public GameController(IBingoCardGenerator bingoCardGenerator, IBingoBallService bingoBallService)
        {
            this.bingoCardGenerator = bingoCardGenerator;
            this.bingoBallService = bingoBallService;
        }

        [HttpGet("generate")]
        public async Task<ActionResult<BingoCard>> BingoCardGenerateAsync()
        {
            var bingoCard = await Task.Run(() => new BingoCard(bingoCardGenerator)); // Asumiendo que esto podría ser una operación costosa
            return Ok(bingoCard);
        }

        [HttpGet("nextball")]
        public async Task<ActionResult<BingoBall>> GetNextBingoBallAsync()
        {
            var ball = await bingoBallService.GetNextBallAsync(); // Asumiendo que GetNextBall ahora es GetNextBallAsync
            if (ball != null)
            {
                return Ok(ball);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("resetballs")]
        public ActionResult ResetBalls()
        {
            bingoBallService.ResetBalls();
            return Ok();
        }
    }
}
