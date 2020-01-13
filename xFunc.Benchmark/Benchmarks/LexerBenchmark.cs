// Copyright 2012-2019 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using xFunc.Maths.Tokenization;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Benchmark.Benchmarks
{

    public class LexerBenchmark
    {

        private ILexer lexer;

        [Params(10, 100, 1000)]
        public int Iterations;

        [GlobalSetup]
        public void Setup()
        {
            lexer = new Lexer();
        }

        [Benchmark]
        public void TestLexer()
        {
            IEnumerable<IToken> tokens = null;
            for (var i = 0; i < Iterations; i++)
                tokens = lexer.Tokenize("(100.1 + 2(3sin(4cos(5tan(6ctg(10x)))) * 3) / (func(a, b, c) ^ 2)) - (cos(y) - 111.3) & (true | false -> true <-> false) + (det({{1, 2}, {3, 4}}) * 10log(2, 3))");
        }

    }

}