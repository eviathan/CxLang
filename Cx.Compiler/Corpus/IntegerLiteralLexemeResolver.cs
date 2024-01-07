using Cx.Compiler.Attributes;
using Cx.Compiler.Interfaces;

namespace Cx.Compiler.Corpus
{
    [Lexeme("0", TokenType.IntegerLiteral)]
    [Lexeme("1", TokenType.IntegerLiteral)]
    [Lexeme("2", TokenType.IntegerLiteral)]
    [Lexeme("3", TokenType.IntegerLiteral)]
    [Lexeme("4", TokenType.IntegerLiteral)]
    [Lexeme("5", TokenType.IntegerLiteral)]
    [Lexeme("6", TokenType.IntegerLiteral)]
    [Lexeme("7", TokenType.IntegerLiteral)]
    [Lexeme("8", TokenType.IntegerLiteral)]
    [Lexeme("9", TokenType.IntegerLiteral)]
    public class IntegerLiteralLexemeResolver : ILexemeResolver
    {
        public string Lexeme { get; set; }
        public string RegEx { get; set; }
        public TokenType TokenType { get; set; }

        public Token ResolveToken(Lexer lexer, int line, int column)
        {
            var numberLiteral = "";
            while (char.IsDigit(lexer.Current))
            {
                numberLiteral += lexer.Current;
                lexer.Advance();
            }

            return new Token(TokenType, Lexeme, line, column, numberLiteral);
        }
    }
}

