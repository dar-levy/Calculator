using System;
using System.Collections.Generic;
using Calculator.Core.Entities;
using System.Linq;
using System.Security.Permissions;
using System.Xml;
using static Calculator.Base.config.Config;

namespace Calculator.Base
{
    public class Parser
    {
        private int _totalBrackets;
        private readonly List<Token> _tokens;
        private readonly List<IToken> _specifiedTokens;
        public readonly List<IToken> NumericList;
        public readonly Stack<IToken> OperatorStack;
        
        public Parser()
        {
            _tokens = new List<Token>();
            _specifiedTokens = new List<IToken>();
            NumericList = new List<IToken>();
            OperatorStack = new Stack<IToken>();
        }
        
        public void Parse(string equation)
        {
            var trimmedEquation = Trim(equation);
            ConvertStringToListOfTokens(trimmedEquation);
            SplitToTokens();
            DetermineOperandsOfOperators();
            MoveOperatorsToStack();
            MoveNumbersToList();
        }

        private void DetermineOperandsOfOperators()
        {
            for (var i = 1; i < _specifiedTokens.Count - 1; i++)
            {
                var predecessor = _specifiedTokens[i - 1];
                var successor = _specifiedTokens[i + 1];
                _specifiedTokens[i].LeftOperand = char.IsDigit(Convert.ToChar(predecessor.Symbol[0])) ? predecessor : null;
                _specifiedTokens[i].RightOperand = char.IsDigit(Convert.ToChar(successor.Symbol[0])) ? successor : null;
            }
        }
        
        private string Trim(string stringWithSpaces)
        {
            return stringWithSpaces.Replace(" ","");
        }

        private void ConvertStringToListOfTokens(string equation)
        {
            foreach (var item in equation)
            {
                var token = new Token(item.ToString(), -1);
                _tokens.Add(token);
            }
        }
        
        private void SplitToTokens()
        {
            for (var tokenIndex = 0; tokenIndex < _tokens.Count; tokenIndex++)
            {
                var tokenSymbol = _tokens[tokenIndex].Symbol;
                var priority = EvalPriorityByParenthesis();
                
                IsBracket(tokenSymbol);
                IsDigit(Convert.ToChar(tokenSymbol), priority, ref tokenIndex);
                IsOperator(tokenIndex, tokenSymbol, priority, _tokens);
            }
        }

        private void IsOperator(int tokenIndex, string symbol, int bracketsPriority, List<Token> tokens)
        {
            if (!BinaryOperators.Contains(symbol) && !UnaryOperators.Contains(symbol)) return;
            Token predecessor = null;
            Token successor = null;
            if (tokenIndex > 0)
                predecessor = char.IsDigit(Convert.ToChar(_tokens[tokenIndex - 1].Symbol)) ? _tokens[tokenIndex - 1] : null;

            if (tokenIndex < tokens.Count - 1)
                successor = char.IsDigit(Convert.ToChar(_tokens[tokenIndex + 1].Symbol)) ? _tokens[tokenIndex + 1] : null;

            var operatorPriority = EvalOperatorPriority(symbol);
            var opToken = new Operator(symbol, bracketsPriority, predecessor, successor, operatorPriority);
            _specifiedTokens.Add(opToken);
        }

        private int EvalOperatorPriority(string symbol)
        {
            switch (symbol)
            {
                case "+":
                    return 0;
                case "-":
                    return 0;
                case "*":
                    return 1;
                case "/":
                    return 1;
                case "^":
                    return 2;
                default:
                    return -1;
            }
        }
        
        private void IsBracket(string symbol)
        {
            if (symbol == "(")
                _totalBrackets++;
            
            else if (symbol == ")")
                _totalBrackets--;
        }

        private void IsDigit(char symbol, int priority,ref int tokenIndex)
        {
            if (!char.IsDigit(symbol)) return;
            var numericToken = ConvertTokenToNumeric(priority, ref tokenIndex);
            _specifiedTokens.Add(numericToken);
        }

        private Numeric ConvertTokenToNumeric(int priority, ref int tokenIndex)
        {
            var completeNumber = string.Empty;
            var counter = 0;
            while (tokenIndex+counter < _tokens.Count && char.IsDigit(Convert.ToChar(_tokens[tokenIndex+counter].Symbol)))
            {
                completeNumber += _tokens[tokenIndex + counter].Symbol;
                counter++;
            }
            
            tokenIndex += --counter;
            return new Numeric(completeNumber, _tokens[tokenIndex].Id, priority);
        }
        
        private int EvalPriorityByParenthesis()
        {
            return _totalBrackets;
        }
        
        private void MoveOperatorsToStack()
        {
            var priorityValue = 0;
            while (priorityValue <= _specifiedTokens.Max(token => token.BracketPriority))
            {
                var tokensGroup = _specifiedTokens.FindAll(opToken => opToken.BracketPriority == priorityValue && ( UnaryOperators.Contains(opToken.Symbol) || BinaryOperators.Contains(opToken.Symbol)));
                if (tokensGroup.Count == 0)
                {
                    priorityValue++;
                    continue;
                };
                _specifiedTokens.RemoveAll(opToken => opToken.BracketPriority == priorityValue && (UnaryOperators.Contains(opToken.Symbol) || BinaryOperators.Contains(opToken.Symbol)));
                var orderedByOperatorLevelGroup = OrderOperatorsByPriority(tokensGroup);
                foreach (var operatorToken in orderedByOperatorLevelGroup)
                {
                    OperatorStack.Push(operatorToken);
                }

                priorityValue++;
            }
        }

        private List<IToken> OrderOperatorsByPriority(List<IToken> operators)
        {
            var orderedOperators = new List<IToken>();
            var priorityValue = 0;
            while (priorityValue <= operators.Max(op => op.OperatorPriority))
            {
                var operatorsGroup = operators.FindAll(opToken => opToken.OperatorPriority == priorityValue);
                foreach (var operatorToken in operatorsGroup)
                {
                    orderedOperators.Add(operatorToken);
                }

                priorityValue++;
            }

            return orderedOperators;
        }
        
        private void MoveNumbersToList()
        {
            var priorityValue = 0;
            while (priorityValue <= _specifiedTokens.Max(token => token.BracketPriority))
            {
                var tokensGroup = _specifiedTokens.FindAll(numToken => numToken.BracketPriority == priorityValue);
                foreach (var token in tokensGroup)
                {
                    NumericList.Add(token);
                }

                priorityValue++;
            }
        }
    }
}