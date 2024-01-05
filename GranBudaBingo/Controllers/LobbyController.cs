using GranBudaBingo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GranBudaBingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        public static class LobbyData
        {
            public static List<string> PlayersInLobby = new List<string>();
        }
        [HttpGet("players")]
        public ActionResult<IEnumerable<string>> GetPlayersInLobby()
        {
            return Ok(LobbyData.PlayersInLobby);
        }

        [HttpGet("generate")]
        public ActionResult<BingoCard> GenerateBingoCard()
        {
            var bingoCard = new BingoCard();
            return Ok(bingoCard);
        }

        [HttpPost("reset")]
        public IActionResult ResetLobby()
        {
            LobbyData.PlayersInLobby.Clear();
            return Ok();
        }

    }
}
