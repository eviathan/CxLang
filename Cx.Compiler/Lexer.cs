using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cx.Compiler
{
    /// <summary>
    /// This takes in a stream of characters and converts them into tokens
    /// </summary>
    public static class Lexer
    {
        public static List<Token> Parse(string source)
        {
            List<Token> tokens = [];

            // var scanner = new Scanner(source);
            // var tokens = scanner.GetTokens();

            // For now, just print the tokens.
            // foreach (var token in tokens) 
            // {
            //     Console.WriteLine(token);
            // }

            tokens.ForEach(Console.WriteLine);

            return tokens;
        }
    }
}