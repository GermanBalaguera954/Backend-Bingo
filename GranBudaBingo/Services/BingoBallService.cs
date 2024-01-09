using GranBudaBingo.Models;

namespace GranBudaBingo.Services
{
    public interface IBingoBallService
    {
        Task<BingoBall> DrawBallAsync(int gameId);
        Task<bool> IsGameFinishedAsync(int gameId);
        Task StartNewGameAsync(int gameId);
    }

    public class BingoBallService : IBingoBallService
    {
        private readonly Random random;
        private readonly Dictionary<int, List<BingoBall>> ballsByGame; // Bolas disponibles por juego
        private readonly Dictionary<int, List<BingoBall>> drawnBallsByGame; // Bolas sorteadas por juego

        public BingoBallService()
        {
            random = new Random();
            ballsByGame = new Dictionary<int, List<BingoBall>>();
            drawnBallsByGame = new Dictionary<int, List<BingoBall>>();
        }

        private void InitializeGame(int gameId)
        {
            string[] columns = { "B", "I", "N", "G", "O" };
            var balls = new List<BingoBall>();

            for (int i = 0; i < columns.Length; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    balls.Add(new BingoBall
                    {
                        Number = j + 15 * i,
                        Column = columns[i],
                        BingoGameId = gameId
                    });
                }
            }

            ballsByGame[gameId] = balls.OrderBy(x => random.Next()).ToList();
        }

        public async Task<BingoBall> DrawBallAsync(int gameId)
        {
            var balls = ballsByGame[gameId];
            int index = random.Next(balls.Count);
            BingoBall drawnBall = balls[index];
            balls.RemoveAt(index);

            if (!drawnBallsByGame.ContainsKey(gameId))
            {
                drawnBallsByGame[gameId] = new List<BingoBall>();
            }
            drawnBallsByGame[gameId].Add(drawnBall);

            return await Task.Run(() =>
            {
                if (!ballsByGame.ContainsKey(gameId) || ballsByGame[gameId].Count == 0)
                {
                    throw new InvalidOperationException("No hay más balotas para sortear.");
                }

                var balls = ballsByGame[gameId];
                int index = random.Next(balls.Count);
                BingoBall drawnBall = balls[index];
                balls.RemoveAt(index);
                return drawnBall;
            });
        }

        public async Task<bool> IsGameFinishedAsync(int gameId)
        {
            return await Task.FromResult(ballsByGame.ContainsKey(gameId) && ballsByGame[gameId].Count == 0);
        }

        public async Task StartNewGameAsync(int gameId)
        {

            await Task.Run(() =>
            {

                InitializeGame(gameId);
            });

        }
    }
}
