using System;

namespace Calculator.Base
{
    public class Calculator
    {
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