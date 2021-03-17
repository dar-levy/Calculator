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
            Console.WriteLine("Please enter your equation :");
            var input = Console.ReadLine();
            
            var validator = new Validator();
            var parser = new Parser();
            
            validator.Validate(input);
            parser.Parse(input);
            var result = Solve(parser.OperatorStack, parser.NumericList);

            Console.WriteLine($"\nResult: {result}");
        }

        private static string Solve(Stack<IToken> operatorStack, List<IToken> numericList)
        {
            while (operatorStack.Count >= 1)
            {
                var subResult = EvalSubResult(operatorStack, numericList);
                numericList.Add(subResult);
            }

            return numericList.Last().Symbol;
        }
        
        private static Numeric EvalSubResult(Stack<IToken> tokenStack, List<IToken> numericList)
        {
            double leftOperand;
            double rightOperand;
            var op = tokenStack.Pop();
            var operatorFunctionality = GetOperatorFunctionality(op.Symbol);
            if (numericList.Exists(numberToken => numberToken.Id == op.LeftOperand.Id)  && numericList.Exists(numberToken => numberToken.Id == op.RightOperand.Id))
            {
                leftOperand = Convert.ToDouble(op.LeftOperand.Symbol);
                rightOperand = Convert.ToDouble(op.RightOperand.Symbol);
                
                //Clean list and stack
                var leftOperandToRemove = numericList.Single(numericToken => numericToken.Id == op.LeftOperand.Id);
                numericList.Remove(leftOperandToRemove);
                var rightOperandToRemove = numericList.Single(numericToken => numericToken.Id == op.RightOperand.Id);
                numericList.Remove(rightOperandToRemove);
            }
            else
            {
                rightOperand = GetLastTokenInList(numericList);
                leftOperand = GetLastTokenInList(numericList);
            }
            
            return Eval(leftOperand, rightOperand, operatorFunctionality);
        }

        private static double GetLastTokenInList(List<IToken> numericList)
        {
            var lastNumber = numericList.Last();
            numericList.Remove(numericList.Last());
            return Convert.ToDouble(lastNumber.Symbol);
        }

        private static Numeric Eval(double leftOperand, double rightOperand, Func<double, double, double> op)
        {
            var subResult = op(leftOperand, rightOperand).ToString(CultureInfo.InvariantCulture);
            var resultToken = new Numeric(subResult, 2);
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