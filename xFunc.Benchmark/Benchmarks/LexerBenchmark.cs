// Copyright 2012-2020 Dmytro Kyshchenko
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

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using xFunc.Maths.Tokenization;

namespace xFunc.Benchmark.Benchmarks
{
    public class LexerBenchmark
    {
        private ILexer lexer;

        private readonly Consumer consumer = new Consumer();

        [GlobalSetup]
        public void Setup()
        {
            lexer = new Lexer();
        }

        [Benchmark]
        public void TestLexer()
        {
            lexer.Tokenize("(100.1 + 2(3Sin(4cos(5tan(6ctg(10x)))) * 3) / (func(a, b, c) ^ 2)) - (cos(y) - 111.3) & (true | false -> true <-> false eq true) + (det({{1, 2}, {3, 4}}) * 10log(2, 3)) + re(3 + 2i) - im(2 - 9i) + (9 + 2i)").Consume(consumer);
        }

        [Benchmark]
        public void TestBinNumber()
        {
            lexer.Tokenize("0b0011001111001100").Consume(consumer);
        }

        [Benchmark]
        public void TestOctNumber()
        {
            lexer.Tokenize("012345671234567").Consume(consumer);
        }

        [Benchmark]
        public void TestHexNumber()
        {
            lexer.Tokenize("0x1234567890ABCDEF").Consume(consumer);
        }

        [Benchmark]
        public void TestNumber()
        {
            lexer.Tokenize("12345678901234567890").Consume(consumer);
        }
    }
}