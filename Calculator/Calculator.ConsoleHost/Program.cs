using System;
using System.Collections.Generic;
using System.Globalization;
using Calculator.Base;

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
            var tokenStack = parser.Parse(input);
            var result = Solve(tokenStack);

            Console.WriteLine($"\nResult: {result}");
        }

        private static string Solve(Stack<string> tokenStack)
        {
            while (tokenStack.Count >= 3)
            {
                var subResult = EvalSubResult(tokenStack).ToString(CultureInfo.InvariantCulture);
                tokenStack.Push(subResult);
            }

            return tokenStack.Pop();            
        }
        
        private static double EvalSubResult(Stack<string> tokenStack)
        {
            var rightOperand = Convert.ToDouble(tokenStack.Pop());
            var op = GetOperatorFunctionality(tokenStack.Pop());
            var leftOperand = Convert.ToDouble(tokenStack.Pop());
            return Eval(leftOperand, rightOperand, op);
        }

        private static double Eval(double leftOperand, double rightOperand, Func<double, double, double> op)
        {
            var lo = Convert.ToDouble(leftOperand);
            var ro = Convert.ToDouble(rightOperand);
            return op(lo, ro);
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