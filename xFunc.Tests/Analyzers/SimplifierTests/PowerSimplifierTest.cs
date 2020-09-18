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
    public class PowerSimplifierTest : BaseSimplifierTest
    {
        [Fact]
        public void PowerXZero()
        {
            var pow = new Pow(Variable.X, Number.Zero);
            var expected = Number.One;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowerZeroX()
        {
            var pow = new Pow(Number.Zero, Variable.X);
            var expected = Number.Zero;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowerOne()
        {
            var pow = new Pow(Variable.X, Number.One);
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowLog()
        {
            var pow = new Pow(
                new Number(30),
                new Log(new Number(30), Variable.X));
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowLg()
        {
            var pow = new Pow(
                new Number(10),
                new Lg(Variable.X));
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowLn()
        {
            var pow = new Pow(
                new Variable("e"),
                new Ln(Variable.X));
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowLb()
        {
            var pow = new Pow(
                Number.Two,
                new Lb(Variable.X));
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void RootOne()
        {
            var root = new Root(Variable.X, Number.One);
            var expected = Variable.X;

            SimpleTest(root, expected);
        }

        [Fact]
        public void Root()
        {
            var root = new Root(Variable.X, new Number(5));

            SimpleTest(root, root);
        }

        [Fact]
        public void Exp()
        {
            var exp = new Exp(new Number(30));

            SimpleTest(exp, exp);
        }

        [Fact]
        public void ExpLn()
        {
            var exp = new Exp(new Ln(new Number(30)));
            var expected = new Number(30);

            SimpleTest(exp, expected);
        }
    }
}