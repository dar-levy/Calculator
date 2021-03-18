using System;
using static System.Guid;

namespace Calculator.Core
{
    public class Token : IToken
    {
        public Guid Id { get; }
        public string Symbol { get; }
        public int BracketPriority { get; }
        public int OperatorPriority { get; }
        public IToken RightOperand { get; set; }
        public IToken LeftOperand { get; set; }

        public Token(string symbol,int bracketPriority)
        {
            Id = NewGuid();
            Symbol = symbol;
            BracketPriority = bracketPriority;
        }

        protected Token(string symbol,int bracketPriority, int operatorPriority, IToken leftOperand, IToken rightOperand)
        {
            Id = NewGuid();
            Symbol = symbol;
            BracketPriority = bracketPriority;
            OperatorPriority = operatorPriority;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        protected Token(string symbol, Guid id, int bracketPriority)
        {
            Id = id;
            Symbol = symbol;
            BracketPriority = bracketPriority;
        }
    }
}