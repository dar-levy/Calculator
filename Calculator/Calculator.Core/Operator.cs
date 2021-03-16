using System;

namespace Calculator.Core.Entities
{
    public class Operator : Token
    {
        protected IToken LeftOperand;
        protected IToken RightOperand;
        public string Symbol { get; }
        public int OperatorPriority { get; }

        public Operator(string symbol, int bracketPriority, IToken leftOperand, IToken rightOperand, int operatorPriority):base(symbol, bracketPriority, operatorPriority)
        {
            Symbol = symbol;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
            OperatorPriority = operatorPriority;
        }

        public virtual double Calculate()
        {
            return 0;
        }
    }
}