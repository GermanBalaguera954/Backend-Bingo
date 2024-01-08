namespace GranBudaBingo.Models
{
    public class BingoCheckRequest
    {
        public Dictionary<string, int> MarkedCellNumbers { get; set; }

        public string GameType { get; set; }
        public List<int> DrawnNumbers { get; set; }
    }
}
