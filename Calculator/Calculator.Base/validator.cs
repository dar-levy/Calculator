using System;
using Calculator.Base.config;

namespace Calculator.Base
{
    public class Validator
    {
        private int _openParensCounter;
        private int _closeParensCounter;
        private char _previous = '0';
        
        private void CheckUnknownToken(char item)
        {
            if (!char.IsDigit(item) && !Config.Operators.Contains(item) && item !='(' && item != ')')
            {
                throw new Exception(
                    $"An unknown character '{item}' in your equation, Please delete it and try again");
            }
        }
    }
}