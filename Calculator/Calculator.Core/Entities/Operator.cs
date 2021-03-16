using System;

namespace Calculator.Core.Entities
{
    public class Operator : Token
    {
        protected IToken LeftOperand;
        protected IToken RightOperand;
        public string Symbol { get; }
        public int Priority { get; set; }

        public Operator(string symbol, int bracketPriority, IToken leftOperand, IToken rightOperand):base(symbol, bracketPriority)
        {
            Symbol = symbol;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        public virtual double Calculate()
        {
            return 0;
        }
    }
}