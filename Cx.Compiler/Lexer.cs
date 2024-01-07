using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cx.Compiler.Extensions;
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
        private int _currentColumn = 0;

        private char _current => _position < _input.Length ? _input[_position] : '\0';
        private char _next => _position + 1 < _input.Length ? _input[_position + 1] : '\0';

        public Lexer(string input)
        {
            _input = input;
        }

        public void Advance()
        {
            if (_current == '\n')
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

                var resolver = LexemeResolverFactory.Instance.GetResolver(_current);
                var token = resolver.ResolveToken(this, _currentLine, _currentColumn);
                tokens.Add(token);

                switch (_current)
                {
                    // case '+':
                    //     tokens.Add(new Token(TokenType.Plus, "+", _currentLine, _currentColumn));
                    //     Advance();
                    //     break;
                    // case '-':
                    //     // ... handle other single-character tokens similarly ...
                    //     break;
                    case ' ':
                    case '\t':
                    case '\r':
                        Advance();
                        break;
                    case '\n':
                        tokens.Add(new Token(TokenType.EndOfLine, "\\n", _currentLine, _currentColumn));
                        Advance();
                        break;
                    case '"': // Assuming " is used for string literals
                        Advance(); // Skip the opening quote
                        var stringLiteral = "";
                        while (_current != '"' && _current != '\0')
                        {
                            // Handle escape sequences if necessary
                            if (_current == '\\' && _next == '"')
                            {
                                stringLiteral += _current; // Append the escape character
                                Advance();
                            }

                            stringLiteral += _current;
                            Advance();
                        }
                        if (_current == '"')
                        {
                            Advance(); // Skip the closing quote
                        }
                        else
                        {
                            // Handle error: String literal not closed
                        }
                        tokens.Add(new Token(TokenType.StringLiteral, stringLiteral, startLine, startColumn));
                        break;
                    case char c when char.IsDigit(c):
                        var number = "";
                        while (char.IsDigit(_current))
                        {
                            number += _current;
                            Advance();
                        }
                        tokens.Add(new Token(TokenType.IntegerLiteral, number, startLine, startColumn));
                        break;
                    // ... add more cases for other types of tokens ...
                    default:
                        tokens.Add(new Token(TokenType.Unknown, _current.ToString(), _currentLine, _currentColumn));
                        Advance();
                        break;
                }
            }

            tokens.Add(new Token(TokenType.EndOfFile, "<EOF>", _currentLine, _currentColumn));
            return tokens;
        }
    }
}