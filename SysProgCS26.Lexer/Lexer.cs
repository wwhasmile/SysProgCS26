namespace SysProgCS26.Lexer;

public class Lexer
{
    private static readonly HashSet<char> IdentifierCharacters = "філюшкінФІЛЮШКІН0123456789".ToHashSet();

    private readonly string _source;
    private int start;
    private int pos;

    public Lexer(string source)
    {
        _source = source;
        start = 0;
        pos = 0;
    }

    public Token Next()
    {
        if (_source is null)
            throw new ArgumentNullException("Source is not set");

        SkipWhitespaces();
        start = pos;
        
        if (IsAtEnd())
            return MakeToken(TokenType.Eof);

        char c = Advance();
        if (char.IsDigit(c))
            return Number();
        if (IdentifierCharacters.Contains(c))
            return Identifier();
        if (char.IsAsciiLetter(c))
            return Keyword();
        return c switch
        {
            '+' => MakeToken(TokenType.Plus),
            '-' => MakeToken(TokenType.Minus),
            '/' => MakeToken(TokenType.Divide),
            '*' => MakeToken(TokenType.Multiply),
            '%' => MakeToken(TokenType.Modulo),
            _ => MakeError($"Invalid character {c}")
        };
    }

    private void SkipWhitespaces()
    {
        while (!IsAtEnd() && char.IsWhiteSpace(Peek()))
            Advance();
    }

    private Token Number()
    {
        while (char.IsDigit(Peek()))
            Advance();
        
        if (Peek() == '.')
        {
            Advance();
            while (char.IsDigit(Peek()))
                Advance();
        }

        return MakeToken(TokenType.Number);
    }

    private Token Identifier()
    {
        while (IdentifierCharacters.Contains(Peek()))
            Advance();

        return MakeToken(TokenType.Identifier);
    }

    private Token Keyword()
    {
        while (char.IsAsciiLetter(Peek()))
            Advance();
        
        return _source.AsSpan(start, pos - start) switch
        {
            "if" => MakeToken(TokenType.If),
            "else" => MakeToken(TokenType.Else),
            "while" => MakeToken(TokenType.While),
            "TRUE" => MakeToken(TokenType.True),
            "FALSE" => MakeToken(TokenType.False),
            "sin" => MakeToken(TokenType.Sin),
            "cos" => MakeToken(TokenType.Cos),
            _ => MakeError($"Invalid keyword {_source.AsSpan(start, pos - start)}")
        };
    }

    private Token MakeToken(TokenType type)
    {
        return new Token(type, _source, start, pos);
    }

    private Token MakeError(string message)
    {
        return new Token(message, start);
    }

    private bool IsAtEnd() => pos >= _source.Length;

    private char Peek() => IsAtEnd() ? '\0' : _source[pos];

    private bool Match(char c)
    {
        if (IsAtEnd()) return false;
        if (Peek() != c) return false;
        Advance();
        return true;
    }

    private char Advance() => _source[pos++];
}