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
        private List<IToken> _numericList;
        private Stack<IToken> _operatorStack;
        private List<Token> _tokens;
        
        public Parser()
        {
            _numericList = new List<IToken>();
            _operatorStack = new Stack<IToken>();
            _tokens = new List<Token>();
        }
        
        public (Stack<IToken>, List<IToken>) Parse(string equation)
        {
            var trimmedEquation = Trim(equation);
            SplitToTokens(trimmedEquation);
            MoveOperatorsToStack();
            MoveNumbersToList();
            return (_operatorStack, _numericList);
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
                var priority = EvalPriorityByParenthesis();
                
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
                else if (Config.Operators.Contains(item.ToString()))
                {
                    var token = new Token(item.ToString(), priority);
                    _tokens.Add(token);
                }
            }
        }
        
        private int EvalPriorityByParenthesis()
        {
            return _countOpenParenthesis - _countCloseParenthesis;
        }
        
        /*private int EvalPriorityByOperator(Token token)
        {
            return token.Priority + 1;
        }*/
        
        private void MoveOperatorsToStack()
        {
            var counter = 0;
            while (counter <= _tokens.Max(token => token.Priority))
            {
                var tokensGroup = _tokens.FindAll(token => token.Priority == counter && Config.Operators.Contains(token.Symbol));
                _tokens.RemoveAll(token => token.Priority == counter && Config.Operators.Contains(token.Symbol));
                foreach (var token in tokensGroup)
                {
                    _operatorStack.Push(token);
                }

                counter++;
            }
        }

        private void MoveNumbersToList()
        {
            var counter = 0;
            while (counter <= _tokens.Max(token => token.Priority))
            {
                var tokensGroup = _tokens.FindAll(token => token.Priority == counter);
                foreach (var token in tokensGroup)
                {
                    _numericList.Add(token);
                }

                counter++;
            }
        }
    }
}