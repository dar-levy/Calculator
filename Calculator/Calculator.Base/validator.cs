using System;
using System.Linq;
using Calculator.Base.config;

namespace Calculator.Base
{
    public class Validator
    {
        private int _openParensCounter;
        private int _closeParensCounter;
        private char _previous = '0';
        
        public void Validate(string equation)
        {
            if (equation == null) return;
            CheckStandaloneParens(equation);
            foreach (var item in equation)
            {
                if (char.IsWhiteSpace(item)) continue;
                CheckUnknownToken(item);
                CheckEdgesForOperators(equation);
                CheckOpenCloseParens(item, equation);
                CheckSuccessorOperators(_previous, item);
                _previous = item;
            }
        }

        private void CheckEdgesForOperators(string equation)
        {
            var firstToken = equation[0];
            var lastToken = equation.Last();
            if ((Config.Operators.Contains(firstToken)  || Config.Operators.Contains(lastToken)) && firstToken != '-' || lastToken != '-')
            {
                throw new Exception("Equation must not begin\\end with non-unary operator");
            }
        }
        
        private void CheckUnknownToken(char item)
        {
            if (!char.IsDigit(item) && !Config.Operators.Contains(item) && item !='(' && item != ')')
            {
                throw new Exception(
                    $"An unknown character '{item}' in your equation, Please delete it and try again");
            }
        }
        
        private void CheckOpenCloseParens(char item, string items)
        {
            switch (item)
            {
                case '(':
                    _openParensCounter++;
                    break;
                case ')':
                    _closeParensCounter++;
                    break;
            }

            if (item.Equals(items.Last()) && _openParensCounter != _closeParensCounter || _closeParensCounter > _openParensCounter)
            {
                throw new Exception("You're missing parenthesis");
            }
        }
        
        private void CheckStandaloneParens(string str)
        {
            if (str.Contains("()"))
            {
                throw new Exception("No stand-alone parenthesis allowed");
            }
        }
        
        private void CheckSuccessorOperators(char previous, char current)
        {
            if (Config.Operators.Contains(previous) && Config.Operators.Contains(current))
            {
                throw new Exception("Can not use more than one operator for each operand");
            }
        }
    }
}