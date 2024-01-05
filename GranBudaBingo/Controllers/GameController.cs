using GranBudaBingo.Services;
using Microsoft.AspNetCore.Mvc;

namespace GranBudaBingo.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IBingoCardGenerator bingoCardGenerator;

        public GameController(IBingoCardGenerator bingoCardGenerator)
        {
            this.bingoCardGenerator = bingoCardGenerator;
        }

        [HttpGet("generate")]
        public ActionResult<BingoCard> BingoCardGenerate()
        {
            var bingoCard = new BingoCard(bingoCardGenerator);
            return Ok(bingoCard);
        }
    }
}
