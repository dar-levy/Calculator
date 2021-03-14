using System;
using Calculator.Base;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Please enter your equation :");
            var input = Console.ReadLine();
            var validator = new Validator();

            validator.Validate(input);
        }
    }
}