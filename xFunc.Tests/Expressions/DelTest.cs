// Copyright 2012-2018 Dmitry Kischenko
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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class DelTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Add(
                new Add(new Mul(new Number(2), Variable.X), new Pow(new Variable("y"), new Number(2))),
                new Pow(new Variable("z"), new Number(3))
            );
            var del = new Del(new Differentiator(), new Simplifier(), exp);

            var expected = new Vector(new IExpression[] {
                                        new Number(2),
                                        new Mul(new Number(2), new Variable("y")),
                                        new Mul(new Number(3), new Pow(new Variable("z"), new Number(2)))
                                    });

            Assert.Equal(expected, del.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Add(
                new Add(new Mul(new Number(2), new Variable("x1")), new Pow(new Variable("x2"), new Number(2))),
                new Pow(new Variable("x3"), new Number(3))
            );
            var del = new Del(new Differentiator(), new Simplifier(), exp);

            var expected = new Vector(new IExpression[] {
                                        new Number(2),
                                        new Mul(new Number(2), new Variable("x2")),
                                        new Mul(new Number(3), new Pow(new Variable("x3"), new Number(2)))
                                    });

            Assert.Equal(expected, del.Execute());
        }

        [Fact]
        public void NullDiffTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Del(null, null, Variable.X).Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Add(
                new Add(new Mul(new Number(2), new Variable("x1")), new Pow(new Variable("x2"), new Number(2))), 
                new Pow(new Variable("x3"), new Number(3))
            );
            var del = new Del(new Differentiator(), new Simplifier(), exp);
            var clone = del.Clone();

            Assert.Equal(del, clone);
        }

    }

}
