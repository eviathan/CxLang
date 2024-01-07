using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cx.Compiler
{
    public class Token
    {
        public TokenType Type { get; set; }
        public string Lexeme { get; set; }
        public object? Literal { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }

        public Token(TokenType type, string lexeme, int line, int column, object? literal = null)
        {
            Type = type;
            Lexeme = lexeme;
            Literal = literal;
            Line = line;
            Column = column;
        }

        public override string ToString() =>
            $"{Type} {Lexeme} {Literal} (Ln {Line}, Col {Column})";
    }
}