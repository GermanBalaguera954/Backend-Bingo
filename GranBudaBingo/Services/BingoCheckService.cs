using System.Collections;

namespace GranBudaBingo.Services
{
    public interface IBingoCheckService
    {
        bool IsBingo(Dictionary<string, int> markedCells, string gameType, List<int> drawnNumbers);
    }

    public class BingoCheckService: IBingoCheckService
    {
        public bool IsBingo(Dictionary<string, int> markedCells, string gameType, List<int> drawnNumbers)
        {
            // Verificar si el número en cada celda marcada está entre los números sorteados
            if (!markedCells.All(cell => drawnNumbers.Contains(cell.Value)))
            {
                return false;
            }

            switch (gameType)
            {
                case "fullHouse":
                    return markedCells.Count == 24; // Centro vacío no se cuenta
                case "horizontalLine":
                    return CheckHorizontalLines(markedCells);
                case "verticalLine":
                    return CheckVerticalLines(markedCells);
                case "diagonal":
                    return CheckDiagonals(markedCells);
                case "corners":
                    return CheckCorners(markedCells);
                default:
                    return false;
            }
        }

        private bool CheckHorizontalLines(Dictionary<string, int> markedCells)
        {
            for (int i = 0; i < 5; i++)
            {
                // Se crea una lista de claves que representan cada celda en la fila actual
                var row = Enumerable.Range(0, 5).Select(col => $"{i}-{col}").ToList();

                // Se verifica si todas las celdas de la fila actual están marcadas
                if (row.All(id => markedCells.ContainsKey(id)))
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckVerticalLines(Dictionary<string, int> markedCells)
        {
            for (int i = 0; i < 5; i++)
            {
                var column = Enumerable.Range(0, 5).Select(row => $"{row}-{i}");
                if (column.All(id => markedCells.ContainsKey(id)))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDiagonals(Dictionary<string, int> markedCells)
        {
            var diagonal1 = new List<string> { "0-0", "1-1", "3-3", "4-4" };
            var diagonal2 = new List<string> { "0-4", "1-3", "3-1", "4-0" };
            return diagonal1.All(id => markedCells.ContainsKey(id)) || diagonal2.All(id => markedCells.ContainsKey(id));
        }

        private bool CheckCorners(Dictionary<string, int> markedCells)
        {
            var corners = new List<string> { "0-0", "0-4", "4-0", "4-4" };
            return corners.All(id => markedCells.ContainsKey(id));
        }
    }
}
