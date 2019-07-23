using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using xFunc.Maths.Tokenization;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Benchmark.Benchmarks
{

    public class LexerBenchmark
    {

        private ILexer lexer;

        [Params(10, 100, 1000)]
        public int Iterations;

        [GlobalSetup]
        public void Setup()
        {
            lexer = new Lexer();
        }

        [Benchmark]
        public void TestLexer()
        {
            IEnumerable<IToken> tokens = null;
            for (var i = 0; i < Iterations; i++)
                tokens = lexer.Tokenize("(100.1 + (sin(cos(tan(ctg(x)))) * 3) / (func(a, b, c) ^ 2)) - (cos(y) - 111.3) & (true | false -> true <-> false) + (det({{1, 2}, {3, 4}}) * log(2, 3))");
        }

    }

}