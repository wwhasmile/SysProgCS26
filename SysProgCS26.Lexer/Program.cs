namespace SysProgCS26.Lexer;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        Console.WriteLine("An input text should be passed in");
        Lexer lexer = new Lexer(args[0]);
        while (true)
        {
            Token token = lexer.Next();
            Console.WriteLine(token.ToString());
            if (token.Type == TokenType.Eof)
                break;
        }
    }
}