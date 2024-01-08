using GranBudaBingo.Models;

namespace GranBudaBingo.Services
{
    public interface IBingoBallService
    {
        BingoBall DrawBall();
        bool IsGameFinished();
        void StartNewGame();
    }

    public class BingoBallService : IBingoBallService
    {
        private List<BingoBall> balls;
        private Random random;
        private List<BingoBall> drawnBalls;

        public BingoBallService()
        {
            random = new Random();
            balls = new List<BingoBall>();
            drawnBalls = new List<BingoBall>();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Limpia listas para un nuevo juego
            balls.Clear();
            drawnBalls.Clear();

            // Inicializar balotas
            string[] columns = { "B", "I", "N", "G", "O" };
            for (int i = 0; i < columns.Length; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    balls.Add(new BingoBall(j + 15 * i, columns[i]));
                }
            }

            // Opcional: Mezclar las balotas
            balls = balls.OrderBy(x => random.Next()).ToList();
        }

        public BingoBall DrawBall()
        {
            if (IsGameFinished())
            {
                throw new InvalidOperationException("No hay más balotas para sortear.");
            }

            int index = random.Next(balls.Count);
            BingoBall drawnBall = balls[index];
            balls.RemoveAt(index);
            drawnBalls.Add(drawnBall); // Agregar a las balotas sorteadas
            return drawnBall;
        }

        public bool IsGameFinished()
        {
            return balls.Count == 0;
        }

        // Método para iniciar un nuevo juego
        public void StartNewGame()
        {
            InitializeGame();
        }
    }
}
