using Cx.Compiler.Factories;

namespace Cx.Compiler
{
    /// <summary>
    /// This takes in a stream of characters and converts them into tokens
    /// </summary>
    public class Lexer
    {
        private readonly string _input;
        private int _position = 0;
        private int _currentLine = 1;
        private int _currentColumn = 1;

        public char Current => _position < _input.Length 
            ? _input[_position] 
            : '\0';

        public char Next => _position + 1 < _input.Length
            ? _input[_position + 1]
            : '\0';

        public Lexer(string input)
        {
            _input = input;
        }

        public void Advance()
        {
            if (Current == '\n')
            {
                _currentLine++;
                _currentColumn = 0;
            }
            else
            {
                _currentColumn++;
            }

            _position++;
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (_position < _input.Length)
            {
                var startColumn = _currentColumn;
                var startLine = _currentLine;

                try
                {
                    var resolver = LexemeResolverFactory.Instance.GetResolver(Current.ToString());
                    var token = resolver.ResolveToken(this, _currentLine, _currentColumn);

                    if(token != null)
                        tokens.Add(token);
                }
                catch(Exception ex)
                {
                    // TODO: Aggregate these exceptions and then throw the aggregate
                    tokens.Add(new Token(TokenType.Unknown, Current.ToString(), _currentLine, _currentColumn));
                    Advance();
                    break;
                }

                switch (Current)
                {
                    // case ' ':
                    // case '\t':
                    // case '\r':
                    //     Advance();
                    //     break;
                    case '\n':
                        tokens.Add(new Token(TokenType.EndOfLine, "\\n", _currentLine, _currentColumn));
                        Advance();
                        break;
                }
            }

            tokens.Add(new Token(TokenType.EndOfFile, "<EOF>", _currentLine, _currentColumn));
            return tokens;
        }
    }
}