namespace Cx.Compiler
{
    public enum TokenType
    {
        // Single-character tokens.
        LeftParenthesis,
        RightParenthesis,
        LeftCurlyBrace, 
        RightCurlyBrace,
        Comma,
        Dot,
        Minus,
        Plus,
        ForwardSlash, 
        Asterisk,

        // One or two character tokens.
        Bang,
        BangEqual,
        Equal,
        DoubleEqual,
        Greater,
        GreaterEqual,
        Less,
        LessEqual,
        And,
        Or,

        // Literals.
        Identifier,
        StringLiteral, 
        IntegerLiteral,
        FloatLiteral,
        ObjectLiteral,
        ArrayLiteral,
        DictionaryLiteral,
        TupleLiteral,
        Unknown,

        // Keywords 
        Class,
        Interface,
        If,
        Else,
        True,
        False,
        Null,
        For,
        In,
        While,
        Do,
        Return,
        Base,
        This,
        Var,
        Const,
        
        EndOfLine,
        EndOfFile,

        // Access Modifiers
        Public, 
        Private,
        Protected,
        Internal 
    }
}