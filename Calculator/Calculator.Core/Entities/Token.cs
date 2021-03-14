namespace Calculator.Core.Entities
{
    public class Token
    {
        public string Symbol { get; }
        public int Priority { get; }
        
        public Token(string symbol, int priority)
        {
            Symbol = symbol;
            Priority = priority;
        }
    }
}