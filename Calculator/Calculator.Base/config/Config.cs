using System.Collections.Generic;

namespace Calculator.Base.config
{
    public class Config
    {
        public static readonly List<string> BinaryOperators = new List<string>{"+", "*", "/", "^"};
        public static readonly List<string> CharsToDelete = new List<string>{" ", "(", ")"};
        public static readonly List<string> UnaryOperators = new List<string>{"-"};
    }
}