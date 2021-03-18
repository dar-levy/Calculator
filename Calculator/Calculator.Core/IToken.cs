using System;

namespace Calculator.Core
{
    public interface IToken
    {
        Guid Id
        {
            get;
        }
        string Symbol { get; set; }

        int BracketPriority
        {
            get;
        }

        int OperatorPriority
        {
            get;
        }

        IToken RightOperand { get; set; }

        IToken LeftOperand { get; set; }
    }
}