using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Cx.Compiler.Corpus
{
    public class LexemeDictionary
    {
        public TokenType this[string key]
        {
            get 
            {
                return _data[key];
            }
        }

        private Dictionary<string, TokenType> _data = new Dictionary<string, TokenType>
        {
            { "(" , TokenType.LeftParenthesis }
        };
    }
}