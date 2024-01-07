
using Cx.Compiler.Enums;

namespace Cx.Compiler.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LexemeAttribute : Attribute
    {
        public string Pattern { get; private set; }
        public TokenType TokenType { get; private set; }
        public LexemeType Type { get; private set; }

        public LexemeAttribute(string pattern, LexemeType type = LexemeType.String)
        {
            Pattern = pattern;
            Type = type;
        }

        public LexemeAttribute(string pattern, TokenType tokenType, LexemeType type = LexemeType.String)
        {
            Pattern = pattern;
            TokenType = tokenType;
            Type = type;
        }
    }
}