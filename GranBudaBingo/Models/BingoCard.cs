using GranBudaBingo.Models;
using GranBudaBingo.Services;
using System.ComponentModel.DataAnnotations.Schema;
public class BingoCard
{
    public int BingoCardId { get; set; }
    public int BingoGameId { get; set; }  // Clave foránea
    public BingoGame BingoGame { get; set; }  // Propiedad de navegación    

    [NotMapped]
    public int?[][] CardNumbers { get; private set; }

    public void SetCardNumbers(int?[][] cardNumbers)
    {
        CardNumbers = cardNumbers;
    }
}

