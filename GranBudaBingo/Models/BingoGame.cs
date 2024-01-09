namespace GranBudaBingo.Models
{
    public class BingoGame
    {
        public int BingoGameId { get; set; }
        public List<BingoCard> BingoCards { get; set; }
        public List<BingoBall> DrawnBalls { get; set; }
    }
}
