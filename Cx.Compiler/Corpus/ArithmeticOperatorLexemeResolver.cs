using Cx.Compiler.Attributes;
using Cx.Compiler.Interfaces;

namespace Cx.Compiler.Corpus
{
    [Lexeme('+', TokenType.Plus)]
    [Lexeme('-', TokenType.Minus)]
    [Lexeme('/', TokenType.ForwardSlash)]
    [Lexeme('*', TokenType.Asterisk)]
    public class ArithmeticOperatorLexemeResolver : ILexemeResolver
    {
        public string Lexeme { get; set; }
        public TokenType TokenType { get; set; }

        public Token ResolveToken(Lexer lexer, int line, int column)
        {
            var token = new Token(TokenType, Lexeme, line, column);
            lexer.Advance();
            return token;
        }
    }
}