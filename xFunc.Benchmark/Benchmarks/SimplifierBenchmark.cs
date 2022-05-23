// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Benchmark.Benchmarks;

public class SimplifierBenchmark
{
    private Simplifier simplifier;

    private IExpression exp;

    [GlobalSetup]
    public void Setup()
    {
        simplifier = new Simplifier();

        var processor = new Processor();

        exp = processor.Parse("0 + x + x + 0 + 1 + 2 + 3 + x + (2 * x) + (3 * x) + (x * 4) - 0 - x - 0 - 1 - 2 - 3 - (2 * x) - (x * 3) + (x * 0) - (0 * x) + (1 * x) - (x * 1) * (x * x) * (2 * x) * (x * 3) + (x ^ 0) + (x ^ 0) + (e ^ ln(1)) + cos(arccos(0)) + (x * 0) + tan(arctan(0)) + sin(arcsin(x)) - (0 * x)");
    }

    [Benchmark]
    public IExpression Simplify()
        => exp.Analyze(simplifier);
}