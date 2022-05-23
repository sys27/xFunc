// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Benchmark.Benchmarks;

public class TypeAnalyzerBenchmark
{
    private TypeAnalyzer analyzer;

    private IExpression gcd;
    private IExpression gcdUndefined;
    private IExpression matrix;
    private IExpression count;
    private IExpression exp;

    [GlobalSetup]
    public void Setup()
    {
        var processor = new Processor();
        analyzer = new TypeAnalyzer();

        gcd = processor.Parse("gcd(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
        gcdUndefined = processor.Parse("gcd(2, 4, 6, 8, 2, 4, 6, 8, x, 4, 6, 8, 2, 4, 6, 8)");
        matrix = processor.Parse("{{2, 2, 2}, {2, 2, 2}, {2, 2, 2}}");
        count = processor.Parse("count(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2)");
        exp = processor.Parse("count(1, 2, 3, 4, 5, 6, 7, 8, 9, 10) + (2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5) * 10 ^ 6");
    }

    [Benchmark]
    public ResultTypes GCD()
        => gcd.Analyze(analyzer);

    [Benchmark]
    public ResultTypes GCDUndefined()
        => gcdUndefined.Analyze(analyzer);

    [Benchmark]
    public ResultTypes Matrix()
        => matrix.Analyze(analyzer);

    [Benchmark]
    public ResultTypes Count()
        => count.Analyze(analyzer);

    [Benchmark]
    public ResultTypes Exp()
        => exp.Analyze(analyzer);
}