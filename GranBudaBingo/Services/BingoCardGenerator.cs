namespace GranBudaBingo.Services
{
    public interface IBingoCardGenerator
    {
        int[][] GenerateBingoCardMatrix();
    }

    public class BingoCardGenerator : IBingoCardGenerator
    {
        public int[][] GenerateBingoCardMatrix()
        {
            int[][] card = new int[5][];
            Random rand = new Random();

            for (int i = 0; i < 5; i++)
            {
                card[i] = new int[5];
                HashSet<int> numbers = new HashSet<int>();

                while (numbers.Count < 5)
                {
                    if (i == 2 && numbers.Count == 2) // Deja el centro vacío
                    {
                        card[i][numbers.Count] = 0;
                        numbers.Add(0);
                        continue;
                    }

                    int num = rand.Next(1 + i * 15, 16 + i * 15);
                    if (numbers.Add(num))
                    {
                        card[i][numbers.Count - 1] = num;
                    }
                }
            }
            return card;
        }
    }
}
