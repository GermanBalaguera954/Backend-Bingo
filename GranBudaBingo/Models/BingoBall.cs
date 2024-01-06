namespace GranBudaBingo.Models
{
    public class BingoBall
    {
        public int Number { get; set; }
        public string Column { get; set; }

        public BingoBall(int number, string column)
        {
            Number = number;
            Column = column;
        }
    }
}
