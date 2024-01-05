namespace GranBudaBingo.Models
{
    public class BingoGame
    {
        private List<string> balls;
        private Random random;

        public BingoGame()
        {
            balls = new List<string>();
            random = new Random();

            // Inicializar balotas
            InitializeBalls();
        }

        private void InitializeBalls()
        {
            string[] letters = { "B", "I", "N", "G", "O" };
            for (int i = 0; i < letters.Length; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    balls.Add(letters[i] + (i * 15 + j).ToString());
                }
            }
        }

        public string GetNextBall()
        {
            if (balls.Count == 0)
            {
                return null; // Todas las balotas han sido seleccionadas
            }

            int index = random.Next(balls.Count);
            string nextBall = balls[index];
            balls.RemoveAt(index); // Asegúrate de remover la balota seleccionada

            return nextBall;
        }
    }

}
