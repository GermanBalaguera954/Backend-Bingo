using GranBudaBingo.Services;
public class BingoCard
{
    public int[][] CardNumbers { get; private set; }

    public BingoCard(IBingoCardGenerator generator)
    {
        CardNumbers = generator.GenerateBingoCardMatrix();
    }
}

