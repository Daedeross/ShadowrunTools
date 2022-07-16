// Template generated code from Antlr4BuildTasks.Template v 8.17
namespace ShadowrunTools.Dsl
{
    using Antlr4.Runtime;
    using System.Text;

    public class Program
    {
        static void Main(string[] args)
        {
            Try("1 + 2 + 3");
            // Try("1 2 + 3");
            // Try("1 + +");
            Try("TRUE");
            Try("\"foo\"+'bar'");
            Try("[foo]bar");
            Try("[foo]bar:rating");
            Try("me:rating");
            Try("foo(1)");
            Try("foo('bar', 2)");
            foreach (var arg in args)
            {
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    Try(arg); 
                }
            }
        }

        static void Try(string input)
        {
            var visitor = new DslExpressionVisitor<int>();

            var str = new AntlrInputStream(input);
            System.Console.WriteLine(input);
            var lexer = new CharacterBuilderLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CharacterBuilderParser(tokens);
            var listener_lexer = new ErrorListener<int>();
            var listener_parser = new ErrorListener<IToken>();
            lexer.AddErrorListener(listener_lexer);
            parser.AddErrorListener(listener_parser);
            var tree = parser.script();
            if (listener_lexer.had_error || listener_parser.had_error)
                System.Console.WriteLine("error in parse.");
            else
            {
                var expr = tree.Accept(visitor);
                System.Console.WriteLine("parse completed.");
            }
        }

        static string ReadAllInput(string fn)
        {
            var input = System.IO.File.ReadAllText(fn);
            return input;
        }
    }
}
