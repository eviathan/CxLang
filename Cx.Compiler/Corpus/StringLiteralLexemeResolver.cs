using Cx.Compiler.Attributes;
using Cx.Compiler.Interfaces;

namespace Cx.Compiler.Corpus
{
    [Lexeme("\"", TokenType.StringLiteral)]
    public class StringLiteralLexemeResolver : ILexemeResolver
    {
        public string Lexeme { get; set; }
        public TokenType TokenType { get; set; }
        public string RegEx { get; set; }

        public Token ResolveToken(Lexer lexer, int line, int column)
        {
            lexer.Advance(); // Skip the opening quote

            var stringLiteral = "";

            while (lexer.Current != '"' && lexer.Current != '\0')
            {
                // Handle escape sequences if necessary
                if (lexer.Current == '\\' && lexer.Next == '"')
                {
                    stringLiteral += lexer.Current; // Append the escape character
                    lexer.Advance();
                }

                stringLiteral += lexer.Current;
                lexer.Advance();
            }
            if (lexer.Current == '"')
            {
                lexer.Advance(); // Skip the closing quote
            }
            else
            {
                // Handle error: String literal not closed
            }

            var token = new Token(TokenType, Lexeme, line, column, stringLiteral);
            return token;
        }
    }
}

