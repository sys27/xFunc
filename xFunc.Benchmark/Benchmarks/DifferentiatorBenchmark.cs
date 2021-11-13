// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Benchmark.Benchmarks;

public class DifferentiatorBenchmark
{
    private Differentiator differentiator;
    private DifferentiatorContext context;

    private IExpression complexExp;

    [GlobalSetup]
    public void Setup()
    {
        differentiator = new Differentiator();
        context = DifferentiatorContext.Default();

        var processor = new Processor();

        complexExp = processor.Parse("(2 * abs(3 * sin(4 * cos(5 * tan(6 * ctg(x ^ 2))))) - ln(x ^ 2)) + arcsin(arccos(arctan(arcctg(x ^ 10))))");
    }

    [Benchmark]
    public IExpression ComplexExpression()
        => complexExp.Analyze(differentiator, context);
}