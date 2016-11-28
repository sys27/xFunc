// Copyright 2012-2016 Dmitry Kischenko
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
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions.Maths
{

    public class DerivTest
    {

        [Fact]
        public void ExecutePointTest()
        {
            var differentiator = new Mock<IDifferentiator>();
            differentiator.Setup(d => d.Differentiate(It.IsAny<IExpression>(),
                                                      It.IsAny<Variable>(),
                                                      It.IsAny<ExpressionParameters>()))
                          .Returns<IExpression, Variable, ExpressionParameters>((exp, v, p) => v);

            var deriv = new Derivative(new Variable("x"), new Variable("x"), new Number(2));
            deriv.Differentiator = differentiator.Object;

            Assert.Equal(2.0, deriv.Execute());
        }

        [Fact]
        public void ToStringExpTest()
        {
            var deriv = new Derivative(new Sin(new Variable("x")));

            Assert.Equal("deriv(sin(x))", deriv.ToString());
        }

        [Fact]
        public void ToStringVarTest()
        {
            var deriv = new Derivative(new Sin(new Variable("x")), new Variable("x"));

            Assert.Equal("deriv(sin(x), x)", deriv.ToString());
        }

        [Fact]
        public void ToStringPointTest()
        {
            var deriv = new Derivative(new Sin(new Variable("x")), new Variable("x"), new Number(1));

            Assert.Equal("deriv(sin(x), x, 1)", deriv.ToString());
        }

    }

}
