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

using System;
using Moq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class DelTest
    {
        [Fact]
        public void DifferentiatorNull()
            => Assert.Throws<ArgumentNullException>(() => new Del(null, null, null));

        [Fact]
        public void SimplifierNull()
        {
            var differentiator = new Mock<IDifferentiator>().Object;

            Assert.Throws<ArgumentNullException>(() => new Del(differentiator, null, null));
        }

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Add(
                new Add(
                    new Mul(Number.Two, Variable.X),
                    new Pow(new Variable("y"), Number.Two)
                ),
                new Pow(new Variable("z"), new Number(3))
            );
            var del = new Del(new Differentiator(), new Simplifier(), exp);

            var expected = new Vector(new IExpression[]
            {
                Number.Two,
                new Mul(Number.Two, new Variable("y")),
                new Mul(new Number(3), new Pow(new Variable("z"), Number.Two))
            });

            Assert.Equal(expected, del.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Add(
                new Add(new Mul(Number.Two, new Variable("x1")), new Pow(new Variable("x2"), Number.Two)),
                new Pow(new Variable("x3"), new Number(3))
            );
            var del = new Del(new Differentiator(), new Simplifier(), exp);

            var expected = new Vector(new IExpression[]
            {
                Number.Two,
                new Mul(Number.Two, new Variable("x2")),
                new Mul(new Number(3), new Pow(new Variable("x3"), Number.Two))
            });

            Assert.Equal(expected, del.Execute());
        }

        [Fact]
        public void NullDiffTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Del(null, null, Variable.X));
        }

        [Fact]
        public void NullSimplifierTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Del(new Differentiator(), null, Variable.X));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Add(
                new Add(new Mul(Number.Two, new Variable("x1")), new Pow(new Variable("x2"), Number.Two)),
                new Pow(new Variable("x3"), new Number(3))
            );
            var del = new Del(new Differentiator(), new Simplifier(), exp);
            var clone = del.Clone();

            Assert.Equal(del, clone);
        }
    }
}