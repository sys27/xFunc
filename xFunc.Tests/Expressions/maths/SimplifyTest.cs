// Copyright 2012-2017 Dmitry Kischenko
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
using Moq;
using System;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions.Maths
{

    public class SimplifyTest
    {

        [Fact]
        public void ExecuteTest()
        {
            var mock = new Mock<ISimplifier>();
            mock.Setup(x => x.Analyze(It.IsAny<Simplify>())).Returns<IExpression>(x => x);

            var exp = new Simplify(new Sin(new Variable("x")));
            exp.Simplifier = mock.Object;

            Assert.Equal(exp, exp.Execute());
        }

        [Fact]
        public void ExecuteNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Simplify(new Sin(new Variable("x"))).Execute());
        }

        [Fact]
        public void ToStringTest()
        {
            var exp = new Simplify(new Sin(new Variable("x")));

            Assert.Equal("simplify(sin(x))", exp.ToString());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Simplify(new Sin(new Variable("x")));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
