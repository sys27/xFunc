// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Benchmark.Benchmarks;

public class VectorBenchmark
{
    private Vector vector1;
    private Vector vector2;

    [Params(2, 10, 100)]
    public int Size;

    [GlobalSetup]
    public void Setup()
    {
        vector1 = CreateVector();
        vector2 = CreateVector();
    }

    private Vector CreateVector()
    {
        var vector = ImmutableArray.CreateBuilder<IExpression>(Size);
        for (var j = 0; j < Size; j++)
            vector.Add(new Number(Random.Shared.Next()));

        return new Vector(vector.ToImmutableArray());
    }

    [Benchmark]
    public object AddVectors()
        => new Add(vector1, vector2).Execute();

    [Benchmark]
    public object SubVectors()
        => new Sub(vector1, vector2).Execute();

    [Benchmark]
    public object MulVectors()
        => new Mul(vector1, vector2).Execute();

    [Benchmark]
    public object MulVectorByNumber()
        => new Mul(vector1, Number.Two).Execute();
}