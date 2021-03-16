using System;
using System.Linq;
using Calculator.Base.config;

namespace Calculator.Base
{
    public class Validator
    {
        private int _openParensCounter;
        private int _closeParensCounter;
        private string _previous;
        
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
                CheckSuccessorOperators(_previous, item.ToString());
                _previous = item.ToString();
            }
        }

        private void CheckEdgesForOperators(string equation)
        {
            var firstToken = equation[0].ToString();
            var lastToken = equation.Last().ToString();
            
            if ((Config.Operators.Contains(firstToken)  || Config.Operators.Contains(lastToken)) && (firstToken != "-" || lastToken != "-"))
            {
                throw new Exception("Equation must not begin\\end with non-unary operator");
            }
        }
        
        private void CheckUnknownToken(char item)
        {
            if (!char.IsDigit(item) && !Config.Operators.Contains(item.ToString()) && item !='(' && item != ')')
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
                    return;
                case ')':
                    _closeParensCounter++;
                    return;
            }

            if ((item.Equals(items.Last()) && _openParensCounter != _closeParensCounter) || _closeParensCounter > _openParensCounter)
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
        
        private void CheckSuccessorOperators(string previous, string current)
        {
            if (Config.Operators.Contains(previous) && Config.Operators.Contains(current))
            {
                throw new Exception("Can not use more than one operator for each operand");
            }
        }
    }
}