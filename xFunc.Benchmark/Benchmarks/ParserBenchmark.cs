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
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Tokenization;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Benchmark.Benchmarks
{
    public class ParserBenchmark
    {
        private Parser processor;

        private IList<IToken> tokens;

        [GlobalSetup]
        public void Setup()
        {
            processor = new Parser();

            // (100.1 + 2(3sin(4cos(5tan(6ctg(10x)))) * 3) / (func(a, b, c) ^ 2)) - (cos(y) - 111.3) & (true | false -> true <-> false eq true) + (det({{1, 2}, {3, 4}}) * 10log(2, 3)) + re(3 + 2i) - im(2 - 9i) + (9 + 2i)
            tokens = new TokensBuilder()
                .OpenParenthesis()
                .Number(100.1)
                .Operation(Operators.Plus)
                .Number(2)
                .Operation(Operators.Multiplication)
                .OpenParenthesis()
                .Number(3)
                .Id("sin")
                .OpenParenthesis()
                .Number(4)
                .Id("cos")
                .OpenParenthesis()
                .Number(5)
                .Id("tan")
                .OpenParenthesis()
                .Number(6)
                .Id("cot")
                .OpenParenthesis()
                .Number(10)
                .Operation(Operators.Multiplication)
                .VariableX()
                .CloseParenthesis()
                .CloseParenthesis()
                .CloseParenthesis()
                .CloseParenthesis()
                .Operation(Operators.Multiplication)
                .Number(3)
                .CloseParenthesis()
                .Operation(Operators.Division)
                .OpenParenthesis()
                .Id("func")
                .OpenParenthesis()
                .Id("a")
                .Comma()
                .Id("b")
                .Comma()
                .Id("c")
                .CloseParenthesis()
                .Operation(Operators.Exponentiation)
                .Number(2)
                .CloseParenthesis()
                .CloseParenthesis()
                .Operation(Operators.Minus)
                .OpenParenthesis()
                .Id("cos")
                .OpenParenthesis()
                .VariableY()
                .CloseParenthesis()
                .Operation(Operators.Minus)
                .Number(111.3)
                .CloseParenthesis()
                .Operation(Operators.And)
                .OpenParenthesis()
                .True()
                .Operation(Operators.Or)
                .False()
                .Operation(Operators.Implication)
                .True()
                .Operation(Operators.Equality)
                .False()
                .Keyword(Keywords.Eq)
                .True()
                .CloseParenthesis()
                .Operation(Operators.Plus)
                .OpenParenthesis()
                .Id("det")
                .OpenParenthesis()
                .OpenBrace()
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(3)
                .Comma()
                .Number(4)
                .CloseBrace()
                .CloseBrace()
                .CloseParenthesis()
                .Operation(Operators.Multiplication)
                .Number(10)
                .Id("log")
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseParenthesis()
                .CloseParenthesis()
                .Operation(Operators.Plus)
                .Id("re")
                .OpenParenthesis()
                .Number(3)
                .Operation(Operators.Plus)
                .Number(2)
                .Id("i")
                .CloseParenthesis()
                .Operation(Operators.Minus)
                .Id("im")
                .OpenParenthesis()
                .Number(2)
                .Operation(Operators.Minus)
                .Number(9)
                .Id("i")
                .CloseParenthesis()
                .Operation(Operators.Plus)
                .OpenParenthesis()
                .Number(9)
                .Operation(Operators.Plus)
                .Number(2)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
        }

        [Benchmark]
        public IExpression Parse()
        {
            return processor.Parse(tokens);
        }
    }
}