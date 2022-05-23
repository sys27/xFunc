// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Benchmark.Benchmarks;

public class StatisticalBenchmark
{
    private IExpression stdev;
    private IExpression stdevp;

    private IExpression var;
    private IExpression varp;

    [GlobalSetup]
    public void Setup()
    {
        var processor = new Processor();

        stdev = processor.Parse("stdev(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
        stdevp = processor.Parse("stdevp(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");

        var = processor.Parse("var(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
        varp = processor.Parse("varp(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
    }

    [Benchmark]
    public object StdevExecute() => stdev.Execute();

    [Benchmark]
    public object StdevpExecute() => stdevp.Execute();

    [Benchmark]
    public object VarExecute() => var.Execute();

    [Benchmark]
    public object VarpExecute() => varp.Execute();
}