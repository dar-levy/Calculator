using System;

namespace Calculator.Base
{
    public class Calculator
    {
        public double Add(double rightOperand, double leftOperand)
        {
            return leftOperand + rightOperand;
        }

        public double Subtract(double rightOperand, double leftOperand)
        {
            return leftOperand - rightOperand;
        }

        public double Multiply(double rightOperand, double leftOperand)
        {
            return leftOperand * rightOperand;
        }
        
        public double Divide(double rightOperand, double leftOperand)
        {
            return leftOperand / rightOperand;
        }
        
        public double Power(double rightOperand, double leftOperand)
        {
            return Math.Pow(leftOperand, rightOperand);
        }
    }
}