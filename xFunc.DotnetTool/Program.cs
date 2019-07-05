using CommandLine;
using System;
using xFunc.DotnetTool.Options;

namespace xFunc.DotnetTool
{
    public class Program
    {
        private static readonly Maths.Processor processor = new Maths.Processor();

        private static void Tokenize(string stringExpression)
        {
            var tokens = processor.Lexer.Tokenize(stringExpression);
            foreach (var token in tokens)
                Console.WriteLine(token);
        }

        private static void Parse(string stringExpression)
        {
            var exp = processor.Parse(stringExpression);
            Console.WriteLine(exp);
        }

        private static void Solve(string stringExpression)
        {
            var result = processor.Solve(stringExpression);
            Console.WriteLine(result);
        }

        private static void Interactive()
        {
            while (true)
            {
                Console.Write("> ");

                var stringExpression = Console.ReadLine();
                if (stringExpression.ToLower() == "quit")
                    break;

                var result = processor.Solve(stringExpression);
                Console.WriteLine($"> {result}");
            }
        }

        public static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<TokenizeOptions, ParseOptions, SolveOptions, InteractiveOptions>(args)
                .WithParsed<TokenizeOptions>(action => Tokenize(action.StringExpression))
                .WithParsed<ParseOptions>(action => Parse(action.StringExpression))
                .WithParsed<SolveOptions>(action => Solve(action.StringExpression))
                .WithParsed<InteractiveOptions>(active => Interactive());
        }
    }
}
