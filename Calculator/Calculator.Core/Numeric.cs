using System;
using static System.Guid;

namespace Calculator.Core.Entities
{
    public class Numeric : Token
    {
        public string Symbol { get; }
        
        public Numeric(string symbol, int bracketPriority) : base(symbol, bracketPriority)
        {
        }
        
        public Numeric(string symbol,Guid id, int bracketPriority) : base(symbol, id, bracketPriority)
        {
        }
    }
}