// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Benchmark.Benchmarks;

public class MulVectorBenchmark
{
    private readonly Random random = new Random();

    private Mul mul;

    [Params(2, 10, 100)]
    public int Size;

    [GlobalSetup]
    public void Setup()
    {
        var left = CreateVector();
        var right = CreateVector();

        mul = new Mul(left, right);
    }

    private Vector CreateVector()
    {
        var vector = ImmutableArray.CreateBuilder<IExpression>(Size);
        for (var j = 0; j < Size; j++)
            vector.Add(new Number(random.Next()));

        return new Vector(vector.ToImmutableArray());
    }

    [Benchmark]
    public object MulVector() => mul.Execute();
}