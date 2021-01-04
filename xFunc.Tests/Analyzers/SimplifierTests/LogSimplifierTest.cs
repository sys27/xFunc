// Copyright 2012-2021 Dmytro Kyshchenko
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

using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class LogSimplifierTest : BaseSimplifierTest
    {
        [Fact]
        public void Log()
        {
            var log = new Log(Variable.X, Variable.X);
            var expected = Number.One;

            SimplifyTest(log, expected);
        }

        [Fact]
        public void LogNotSimplified()
        {
            var log = new Log(new Number(3), new Number(11));

            SimplifyTest(log, log);
        }

        [Fact]
        public void LogArgumentSimplified()
        {
            var log = new Log(Variable.X, new Add(Number.One, Number.One));
            var expected = new Log(Variable.X, Number.Two);

            SimplifyTest(log, expected);
        }

        [Fact]
        public void Ln()
        {
            var ln = new Ln(new Variable("e"));
            var expected = Number.One;

            SimplifyTest(ln, expected);
        }

        [Fact]
        public void LnArgumentSimplified()
        {
            var log = new Ln(new Add(Number.Two, Number.Two));
            var expected = new Ln(new Number(4));

            SimplifyTest(log, expected);
        }

        [Fact]
        public void LnNotSimplified()
        {
            var ln = new Ln(new Variable("z"));

            SimplifyTest(ln, ln);
        }

        [Fact]
        public void Lg()
        {
            var log = new Lg(new Number(10));
            var expected = Number.One;

            SimplifyTest(log, expected);
        }

        [Fact]
        public void LgArgumentSimplified()
        {
            var log = new Lg(new Add(Number.Two, Number.Two));
            var expected = new Lg(new Number(4));

            SimplifyTest(log, expected);
        }

        [Fact]
        public void LgNotSimplified()
        {
            var log = new Lg(new Number(101));

            SimplifyTest(log, log);
        }

        [Fact]
        public void Lb()
        {
            var log = new Lb(Number.Two);
            var expected = Number.One;

            SimplifyTest(log, expected);
        }

        [Fact]
        public void LbArgumentSimplified()
        {
            var log = new Lb(new Add(Number.Two, Number.Two));
            var expected = new Lb(new Number(4));

            SimplifyTest(log, expected);
        }

        [Fact]
        public void LbNotSimplified()
        {
            var log = new Lb(new Number(3));

            SimplifyTest(log, log);
        }
    }
}