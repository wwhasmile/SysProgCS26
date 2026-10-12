namespace SysProgCS26.Lexer;

public enum TokenType
{
    Identifier,
    Number,
    Plus,
    Minus,
    Multiply,
    Divide,
    Modulo,
    Sin,
    Cos,
    If,
    Else,
    While,
    True,
    False,
    Eof,
    Error
}

public ref struct Token
{
    public readonly TokenType Type;
    public readonly ReadOnlySpan<char> Lexeme;
    public readonly int Position;

    public Token(TokenType type, string source, int position, int end)
    {
        Type = type;
        Lexeme = source.AsSpan(position, end - position);
        Position = position;
    }

    public Token(string message, int position)
    {
        Type = TokenType.Error;
        Lexeme = message.AsSpan();
        Position = position;
    }

    public override string ToString()
    {
        if (Type == TokenType.Eof)
            return $"EOF at {Position}";
        
        return $"{Type}: \"{Lexeme}\" at {Position}";
    }
}