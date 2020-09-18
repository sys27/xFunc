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

            SimpleTest(log, expected);
        }

        [Fact]
        public void Log2()
        {
            var log = new Log(new Number(3), new Number(11));

            SimpleTest(log, log);
        }

        [Fact]
        public void Ln()
        {
            var ln = new Ln(new Variable("e"));
            var expected = Number.One;

            SimpleTest(ln, expected);
        }

        [Fact]
        public void Ln2()
        {
            var ln = new Ln(new Variable("z"));

            SimpleTest(ln, ln);
        }

        [Fact]
        public void Lg()
        {
            var log = new Lg(new Number(10));
            var expected = Number.One;

            SimpleTest(log, expected);
        }

        [Fact]
        public void Lg2()
        {
            var log = new Lg(new Number(101));

            SimpleTest(log, log);
        }

        [Fact]
        public void Lb()
        {
            var log = new Lb(Number.Two);
            var expected = Number.One;

            SimpleTest(log, expected);
        }

        [Fact]
        public void Lb2()
        {
            var log = new Lb(new Number(3));

            SimpleTest(log, log);
        }
    }
}