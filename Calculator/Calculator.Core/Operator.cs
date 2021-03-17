using System;

namespace Calculator.Core.Entities
{
    public class Operator : Token
    {
        public string Symbol { get; }
        public int OperatorPriority { get; }

        public Operator(string symbol, int bracketPriority, IToken leftOperand, IToken rightOperand, int operatorPriority):base(symbol, bracketPriority, operatorPriority, leftOperand, rightOperand)
        {
            Symbol = symbol;
            OperatorPriority = operatorPriority;
        }

        public virtual double Calculate()
        {
            return 0;
        }
    }
}