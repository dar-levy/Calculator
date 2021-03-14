using System.Collections.Generic;
using Calculator.Core.Entities;

namespace Calculator.Base
{
    public class Parser
    {
        private int _countOpenParenthesis;
        private int _countCloseParenthesis;
        private List<Token> _tokens;
        private Stack<Token> _tokenStack;
    }
}