
namespace Cx.Compiler.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LexemeAttribute : Attribute
    {
        public char Character { get; private set; }
        public TokenType TokenType { get; private set; }

        public LexemeAttribute(char character, TokenType tokenType)
        {
            Character = character;
            TokenType = tokenType;
        }
    }
}