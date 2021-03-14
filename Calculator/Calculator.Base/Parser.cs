using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Base.config;
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
        
        public void Parse(string equation)
        {
            var trimmedEquation = Trim(equation);
            SplitToTokens(trimmedEquation);
            MoveTokensToStack();
            foreach (var token in _tokenStack)
            {
                Console.WriteLine("-----------");
                Console.WriteLine($"Symbol: {token.Symbol}");
                Console.WriteLine($"Priority: {token.Priority}");
            }
        }
        
        private string Trim(string stringWithSpaces)
        {
            return stringWithSpaces.Replace(" ","");
        }
        
        private void SplitToTokens(string str)
        {
            for (var index = 0; index < str.Length; index++)
            {
                var item = str[index];
                var priority = EvalPriority();
                
                if (item == '(')
                {
                    _countOpenParenthesis++;
                }
                else if (item == ')')
                {
                    _countCloseParenthesis++;
                }
                else if (char.IsDigit(item))
                {
                    var completeNumber = string.Empty;
                    var counter = 0;
                    while (index+counter < str.Length && char.IsDigit(str[index+counter]))
                    {
                        completeNumber += str[index + counter];
                        counter++;
                    }
                    var token = new Token(completeNumber, priority);
                    _tokens.Add(token);
                    index += --counter;
                }
                else if (Config.Operators.Contains(item))
                {
                    var token = new Token(item.ToString(), priority);
                    _tokens.Add(token);
                }
            }
        }
        
        private int EvalPriority()
        {
            return _countOpenParenthesis - _countCloseParenthesis;
        }
        
        private void MoveTokensToStack()
        {
            var counter = 0;
            while (counter <= _tokens.Max(token => token.Priority))
            {
                var tokensGroup = _tokens.FindAll(token => token.Priority == counter);
                foreach (var token in tokensGroup)
                {
                    _tokenStack.Push(token);
                }

                counter++;
            }
        }

    }
}