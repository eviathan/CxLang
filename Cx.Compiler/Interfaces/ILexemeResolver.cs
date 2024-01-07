using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cx.Compiler.Interfaces
{
    public interface ILexemeResolver
    {
        string Lexeme { get; set; }
        string RegEx { get; set; }
        TokenType TokenType { get; set; }

        Token ResolveToken(Lexer lexer, int line, int column);
    }
}