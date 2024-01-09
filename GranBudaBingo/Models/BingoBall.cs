namespace GranBudaBingo.Models
{
    public class BingoBall
    {
        public int BingoBallId { get; set; }
        public int BingoGameId { get; set; }  // Clave foránea
        public BingoGame BingoGame { get; set; }  // Propiedad de navegación
        public int Number { get; set; }
        public string Column { get; set; }
    }
}
