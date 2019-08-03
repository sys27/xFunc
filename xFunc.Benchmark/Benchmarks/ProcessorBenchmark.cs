using BenchmarkDotNet.Attributes;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Benchmark.Benchmarks
{
    public class ProcessorBenchmark
    {
        private Processor processor;

        [Params(10, 100, 1000)]
        public int Iterations;

        [GlobalSetup]
        public void Setup()
        {
            processor = new Processor();
        }

        [Benchmark]
        public void Parse()
        {
            IExpression exp = null;
            for (var i = 0; i < Iterations; i++)
                exp = processor.Parse("(100.1 + 2(3sin(4cos(5tan(6ctg(10x)))) * 3) / (func(a, b, c) ^ 2)) - (cos(y) - 111.3) & (true | false -> true <-> false) + (det({{1, 2}, {3, 4}}) * 10log(2, 3))");
        }
    }
}
