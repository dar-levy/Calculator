using System.Collections.Generic;
using Calculator.Core.Entities;

namespace Calculator.Base
{
    public class Parser
    {
        private int _countOpenParenthesis;
        private int _countCloseParenthesis;
        private Stack<Token> _tokenStack;
        private List<Token> _tokens;
        
        public Parser()
        {
            _tokenStack = new Stack<Token>();
            _tokens = new List<Token>();
        }
        
        private string Trim(string stringWithSpaces)
        {
            return stringWithSpaces.Replace(" ","");
        }
    }
}