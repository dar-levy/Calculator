using System;
using System.Collections.Generic;
using System.Linq;
using static Calculator.Base.Config.Config;

namespace Calculator.Base
{
    public class Validator
    {
        private int _openParensCounter;
        private int _closeParensCounter;
        
        public void Validate(string expression)
        {
            if (expression == null) return;
            CheckStandaloneParens(expression);
            CheckSuccessorOperators(expression);
            foreach (var item in expression)
            {
                if (char.IsWhiteSpace(item)) continue;
                CheckUnknownToken(item);
                CheckEdgesForOperators(expression);
                CheckOpenCloseParens(item, expression);
            }
        }

        private string MinorParseExpression(string expression)
        {
            return CharsToDelete.Aggregate(expression, (current, charToDelete) =>
                current.Replace(charToDelete, ""));
        }
        
        private void CheckEdgesForOperators(string equation)
        {
            var firstToken = equation[0].ToString();
            var lastToken = equation.Last().ToString();

            if (!BinaryOperators.Contains(firstToken) 
                && !BinaryOperators.Contains(lastToken) 
                || firstToken == "-" && lastToken == "-") return;
            throw new Exception("Equation must not begin\\end with non-unary operator");
        }
        
        private void CheckUnknownToken(char item)
        {
            if (!char.IsDigit(item) && !BinaryOperators.Contains(item.ToString()) && !UnaryOperators.Contains(item.ToString()) && item !='(' && item != ')')
            {
                throw new Exception(
                    $"An unknown character '{item}' in your equation, Please delete it and try again");
            }
        }
        
        private void CheckOpenCloseParens(char item, string items)
        {
            _openParensCounter = item == '(' ? _openParensCounter++ : _openParensCounter;
            _closeParensCounter = item == '(' ? _closeParensCounter++ : _closeParensCounter;
            if (_closeParensCounter <= _openParensCounter) return;
            throw new Exception("You're missing parenthesis");
        }
        
        private void CheckStandaloneParens(string str)
        {
            if (!str.Contains("()")) return;
            throw new Exception("No stand-alone parenthesis allowed");
        }
        
        private void CheckSuccessorOperators(string expression)
        {
            var parseExpression = MinorParseExpression(expression);
            var previous = "-1";
            foreach (var token in parseExpression)
            {
                var current = token.ToString();
                if (!BinaryOperators.Contains(previous)
                    || !BinaryOperators.Contains(current)
                    || UnaryOperators.Contains(previous)
                    || UnaryOperators.Contains(current))
                {
                    previous = current;
                }
                else
                {
                    throw new Exception("Can not use more than one operator for each operand");
                }
            }
        }
    }
}