// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Benchmark.Benchmarks;

public class GcdBenchmark
{
    private IExpression gcd;
    private IExpression lcm;

    [GlobalSetup]
    public void Setup()
    {
        var processor = new Processor();

        gcd = processor.Parse("gcd(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
        lcm = processor.Parse("gcd(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
    }

    [Benchmark]
    public object GcdExecute() => gcd.Execute();

    [Benchmark]
    public object LcmExecute() => lcm.Execute();
}