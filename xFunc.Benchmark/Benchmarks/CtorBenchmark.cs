// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BenchmarkDotNet.Attributes;
using xFunc.Maths;

namespace xFunc.Benchmark.Benchmarks
{
    public class CtorBenchmark
    {
        [Benchmark]
        public IParser ParserCtor()
        {
            return new Parser();
        }
    }
}