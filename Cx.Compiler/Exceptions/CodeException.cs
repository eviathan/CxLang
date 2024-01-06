using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cx.Compiler.Exceptions
{
    public class CodeException : Exception
    {
        public CodeException(int line, string where, string message) : 
            base (FormatMessage(line, where, message)) { }

        private static string FormatMessage(int line, string where, string message)
        {
            var header = $"Error: line({line}) {where} {message}";
            return $"{header}{Environment.NewLine}{message}";
        }
    }
}