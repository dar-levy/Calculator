using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Calculator.Core;

namespace Calculator.Base
{
    public class Calculator
    {
        public static void Run()
        {
            Console.Write("Calc: ");
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
            while (operatorStack.Count > 0 )
            {
                var subResult = EvalSubResult(operatorStack, numericList);
                numericList.Add(subResult);
            }

            return numericList.Last().Symbol;
        }
        
        private static Numeric EvalSubResult(Stack<IToken> opStack, List<IToken> numericList)
        {
            double leftOperand;
            double rightOperand;
            var op = opStack.Pop();
            var operatorFunctionality = GetOperatorFunctionality(op.Symbol);
            if (op.LeftOperand != null && op.RightOperand != null && numericList.Exists(numberToken => numberToken.Id == op.LeftOperand.Id)  && numericList.Exists(numberToken => numberToken.Id == op.RightOperand.Id))
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
                rightOperand = GetLastOperand(numericList);
                leftOperand = op.LeftOperand == null ? 0 : PopOperand(numericList, op.LeftOperand);
            }
            
            return Eval(leftOperand, rightOperand, operatorFunctionality);
        }

        private static double GetLastOperand(List<IToken> numericList)
        {
            var lastOperand = numericList.Last();
            numericList.Remove(numericList.Last());
            return Convert.ToDouble(lastOperand.Symbol);
        }
        
        private static double PopOperand(List<IToken> numericList, IToken operand)
        {
            if (!numericList.Contains(operand)) return GetLastOperand(numericList);
            numericList.Remove(operand);
            return Convert.ToDouble(operand.Symbol);
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
        
        public Func<double,double,double> Add()
        {
            return (a,b) => a+b;
        }

        public Func<double,double,double> Subtract()
        {
            return (a,b) => a-b;
        }

        public Func<double,double,double> Multiply()
        {
            return (a,b) => a*b;
        }
        
        public Func<double,double,double> Divide()
        {
            return (a,b) => a/b;
        }
        
        public Func<double,double,double> Power()
        {
            return Math.Pow;
        }
    }
}