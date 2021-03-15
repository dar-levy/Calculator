using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Calculator.Base;
using Calculator.Core.Entities;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            var validator = new Validator();
            var parser = new Parser();
            
            Console.WriteLine("Please enter your equation :");
            var input = Console.ReadLine();
            
            validator.Validate(input);
            var tokens = parser.Parse(input);
            var result = Solve(tokens);

            Console.WriteLine($"\nResult: {result}");
        }

        private static string Solve((Stack<IToken>, List<IToken>) tokens)
        {
            var tokenStack = tokens.Item1;
            var numericList = tokens.Item2;
            
            while (tokenStack.Count >= 1)
            {
                var subResult = EvalSubResult(tokenStack, numericList);
                numericList.Add(subResult);
            }

            return numericList.Last().Symbol;
        }
        
        private static IToken EvalSubResult(Stack<IToken> tokenStack, List<IToken> numericList)
        {
            var rightOperand = GetLastTokenInList(numericList);
            var leftOperand = GetLastTokenInList(numericList);
            var op = GetOperatorFunctionality(tokenStack.Pop().Symbol);
            return Eval(leftOperand, rightOperand, op);
        }

        private static double GetLastTokenInList(List<IToken> numericList)
        {
            var lastNumber = numericList.Last();
            numericList.Remove(numericList.Last());
            return Convert.ToDouble(lastNumber.Symbol);
        }

        private static IToken Eval(double leftOperand, double rightOperand, Func<double, double, double> op)
        {
            var lo = Convert.ToDouble(leftOperand);
            var ro = Convert.ToDouble(rightOperand);
            var subResult = op(lo, ro).ToString(CultureInfo.InvariantCulture);
            var resultToken = new Token(subResult, 2);
            return resultToken;
        }

        private static Func<double, double, double> GetOperatorFunctionality(string operatorString)
        {
            var calculator = new Base.Calculator();
            return operatorString switch
            {
                "+" => calculator.Add(),
                "-" => calculator.Subtract(),
                "*" => calculator.Multiply(),
                "/" => calculator.Divide(),
                "^" => calculator.Power(),
                _ => null
            };            
        }
    }
}