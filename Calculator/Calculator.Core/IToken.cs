using System;

namespace Calculator.Core.Entities
{
    public interface IToken
    {
        Guid Id
        {
            get;
        }
        string Symbol
        {
            get;
        }

        int BracketPriority
        {
            get;
        }

        int OperatorPriority
        {
            get;
        }

        IToken RightOperand
        {
            get;
        }
        
        IToken LeftOperand
        {
            get;
        }
    }
}