using System;

namespace Calculator.Core
{
    public class Numeric : Token
    {
        public Numeric(string symbol, int bracketPriority) : base(symbol, bracketPriority)
        {
        }
        
        public Numeric(string symbol,Guid id, int bracketPriority) : base(symbol, id, bracketPriority)
        {
        }
    }
}