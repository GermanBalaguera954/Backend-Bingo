public class BingoCard
{
    public int[][] CardNumbers { get; private set; }

    public BingoCard()
    {
        CardNumbers = GenerateBingoCardMatrix();
    }

    private int[][] GenerateBingoCardMatrix()
    {
        int[][] card = new int[5][];
        Random rand = new Random();
        for (int i = 0; i < 5; i++)
        {
            card[i] = new int[5];
            HashSet<int> numbers = new HashSet<int>();
            while (numbers.Count < 5)
            {
                int num = rand.Next(i * 15 + 1, i * 15 + 16);
                if (numbers.Add(num))
                {
                    card[i][numbers.Count - 1] = num;
                }
            }
        }
        return card;
    }
}
