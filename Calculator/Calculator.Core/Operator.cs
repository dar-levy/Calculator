namespace Calculator.Core
{
    public class Operator : Token
    {
        public Operator(string symbol, int bracketPriority, IToken leftOperand, IToken rightOperand, int operatorPriority):base(symbol, bracketPriority, operatorPriority, leftOperand, rightOperand)
        {
        }

        public virtual double Calculate()
        {
            return 0;
        }
    }
}