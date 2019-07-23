using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Tokenization;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Benchmark.Benchmarks
{
    public class ParserBenchmark
    {
        private Parser processor;

        private IList<IToken> tokens;

        [Params(10, 100, 1000)]
        public int Iterations;

        [GlobalSetup]
        public void Setup()
        {
            processor = new Parser();

            // (100.1 + (sin(cos(tan(ctg(x)))) * 3) / (func(a, b, c) ^ 2)) - (cos(y) - 111.3) & (true | false -> true <-> false) + (det({{1, 2}, {3, 4}}) * log(2, 3))
            tokens = new TokensBuilder()
                .OpenBracket()
                .Number(100.1)
                .Operation(Operations.Addition)
                .OpenBracket()
                .Function(Functions.Sine, 1)
                .OpenBracket()
                .Function(Functions.Cosine, 1)
                .OpenBracket()
                .Function(Functions.Tangent, 1)
                .OpenBracket()
                .Function(Functions.Cotangent, 1)
                .OpenBracket()
                .VariableX()
                .CloseBracket()
                .CloseBracket()
                .CloseBracket()
                .CloseBracket()
                .Operation(Operations.Multiplication)
                .Number(3)
                .CloseBracket()
                .Operation(Operations.Division)
                .OpenBracket()
                .UserFunction("func", 3)
                .OpenBracket()
                .Variable("a")
                .Comma()
                .Variable("b")
                .Comma()
                .Variable("c")
                .CloseBracket()
                .Operation(Operations.Exponentiation)
                .Number(2)
                .CloseBracket()
                .CloseBracket()
                .Operation(Operations.Subtraction)
                .OpenBracket()
                .Function(Functions.Cosine, 1)
                .OpenBracket()
                .VariableY()
                .CloseBracket()
                .Operation(Operations.Subtraction)
                .Number(111.3)
                .CloseBracket()
                .Operation(Operations.And)
                .OpenBracket()
                .True()
                .Operation(Operations.Or)
                .False()
                .Operation(Operations.Implication)
                .True()
                .Operation(Operations.Equality)
                .False()
                .CloseBracket()
                .Operation(Operations.Addition)
                .OpenBracket()
                .Function(Functions.Determinant, 1)
                .OpenBracket()
                .Function(Functions.Matrix, 2)
                .OpenBrace()
                .Function(Functions.Vector, 2)
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBrace()
                .Comma()
                .Function(Functions.Vector, 2)
                .OpenBrace()
                .Number(3)
                .Comma()
                .Number(4)
                .CloseBrace()
                .CloseBrace()
                .CloseBracket()
                .Operation(Operations.Multiplication)
                .Function(Functions.Log, 2)
                .OpenBracket()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBracket()
                .CloseBracket()
                .Tokens;
        }

        [Benchmark]
        public void Parse()
        {
            IExpression exp = null;
            for (var i = 0; i < Iterations; i++)
                exp = processor.Parse(tokens);
        }
    }
}
