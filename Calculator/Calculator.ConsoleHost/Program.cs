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
            var calculator = new Base.Calculator();
            var validator = new Validator();
            var parser = new Parser();

            validator.Validate(input);
            var tokenStack = parser.Parse(input);
            foreach (var token in tokenStack)
            {
                Console.WriteLine("-----------");
                Console.WriteLine($"Symbol: {token.Symbol}");
                Console.WriteLine($"Priority: {token.Priority}");
            }
        }
    }
}