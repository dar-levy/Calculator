using System;
using static System.Guid;

namespace Calculator.Core.Entities
{
    public class Token : IToken
    {
        public Guid Id { get; }
        public string Symbol { get; }

        public int BracketPriority { get; }
        
        public Token(string symbol,int bracketPriority)
        {
            Id = NewGuid();
            Symbol = symbol;
            BracketPriority = bracketPriority;
        }
        
        public Token(string symbol, Guid id, int bracketPriority)
        {
            Id = id;
            Symbol = symbol;
            BracketPriority = bracketPriority;
        }
    }
}