// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Benchmark.Benchmarks;

public class ProcessorBenchmark
{
    private Processor processor;

    [GlobalSetup]
    public void Setup()
    {
        processor = new Processor();
    }

    [Benchmark]
    public IExpression Parse()
        => processor.Parse("(100.1 + 2 * (3 * sin(4 * cos(5 * tan(6 * ctg(10 * x)))) * 3) / (func(a, b, c) ^ 2)) - (cos(y) - 111.3) & (true | false impl true eq false) + (det({{1, 2}, {3, 4}}) * 10 * log(2, 3)) + re(3 + 2 * i) - im(2 - 9 * i) + (9 + 2 * i)");

    [Benchmark]
    public NumberResult Solve()
        => processor.Solve<NumberResult>("count(1, 2, 3, 4, 5, 6, 7, 8, 9, 10) + (2 * sin(4 * cos(6 * tan(8 * cot(pi / 4) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi / 4) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi / 4) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi / 4) ^ 2) ^ 3) ^ 4) ^ 5) * 10 ^ 6 + 10!");
}