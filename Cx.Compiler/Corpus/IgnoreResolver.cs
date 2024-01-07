using Cx.Compiler.Attributes;
using Cx.Compiler.Interfaces;

namespace Cx.Compiler.Corpus
{
    [Lexeme(" ")]
    [Lexeme("\t")]
    [Lexeme("\r")]
    public class IgnoreResolver : ILexemeResolver
    {
        public string Lexeme { get; set; }
        public TokenType TokenType { get; set; }
        public string RegEx { get; set; }

        public Token ResolveToken(Lexer lexer, int line, int column)
        {
            lexer.Advance();
            return null!;
        }
    }
}

