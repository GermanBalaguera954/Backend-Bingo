using GranBudaBingo.Models;

namespace GranBudaBingo.Services
{
    public interface IBingoBallService
    {
        Task<BingoBall> GetNextBallAsync();
        void ResetBalls();
    }

    public class BingoBallService : IBingoBallService
    {
        private Queue<BingoBall> balls;

        public BingoBallService()
        {
            balls = new Queue<BingoBall>(GenerateAndShuffleBalls());
        }

        private List<BingoBall> GenerateAndShuffleBalls()
        {
            var balls = new List<BingoBall>();

            // Generar balotas
            for (int i = 1; i <= 75; i++)
            {
                string column = i switch
                {
                    <= 15 => "B",
                    <= 30 => "I",
                    <= 45 => "N",
                    <= 60 => "G",
                    _=> "O"
                };

                balls.Add(new BingoBall(i, column));
            }

            // Barajar las balotas usando el algoritmo de Fisher-Yates
            Random rand = new Random();
            for (int i = balls.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                var temp = balls[i];
                balls[i] = balls[j];
                balls[j] = temp;
            }

            return balls;
        }
        public Task<BingoBall> GetNextBallAsync()
        {
            if (balls.Count == 0)
            {
                // No hay más balotas, el juego ha terminado
                return Task.FromResult<BingoBall>(null);
            }

            return Task.FromResult(balls.Dequeue());
        }

        public void ResetBalls()
        {
            balls = new Queue<BingoBall>(GenerateAndShuffleBalls());
        }
    }
}