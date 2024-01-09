namespace GranBudaBingo.Services
{
    public interface IBingoCardGenerator
    {
        Task<int?[][]> GenerateBingoCardMatrixAsync();
    }

    public class BingoCardGenerator : IBingoCardGenerator
    {
        private readonly Random rand;

        public BingoCardGenerator()
        {
            rand = new Random();
        }

        public async Task<int?[][]> GenerateBingoCardMatrixAsync()
        {
            return await Task.Run(() =>
            {
                int?[][] card = new int?[5][];
                for (int i = 0; i < 5; i++)
                {
                    card[i] = new int?[5];
                    HashSet<int> numbers = new HashSet<int>();

                    while (numbers.Count < 5)
                    {
                        if (i == 2 && numbers.Count == 2)
                        {
                            card[i][numbers.Count] = null;
                            numbers.Add(-1);
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
            });
        }
    }
}
