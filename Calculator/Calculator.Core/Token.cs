using System;
using static System.Guid;

namespace Calculator.Core.Entities
{
    public class Token : IToken
    {
        public Guid Id { get; }
        public string Symbol { get; }

        public int BracketPriority { get; }
        
        public int OperatorPriority { get; }
        public IToken RightOperand { get; }
        public IToken LeftOperand { get; }

        public Token(string symbol,int bracketPriority)
        {
            Id = NewGuid();
            Symbol = symbol;
            BracketPriority = bracketPriority;
        }
        
        public Token(string symbol,int bracketPriority, int operatorPriority, IToken rightOperand, IToken leftOperand)
        {
            Id = NewGuid();
            Symbol = symbol;
            BracketPriority = bracketPriority;
            OperatorPriority = operatorPriority;
            RightOperand = rightOperand;
            LeftOperand = leftOperand;
        }
        
        public Token(string symbol, Guid id, int bracketPriority)
        {
            Id = id;
            Symbol = symbol;
            BracketPriority = bracketPriority;
        }
    }
}