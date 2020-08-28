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

using Moq;
using System;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class DerivTest
    {
        [Fact]
        public void ExecutePointTest()
        {
            var differentiator = new Mock<IDifferentiator>();
            differentiator
                .Setup(d => d.Analyze(It.IsAny<Derivative>(), It.IsAny<DifferentiatorContext>()))
                .Returns<Derivative, DifferentiatorContext>((exp, context) => exp.Expression);

            var simplifier = new Mock<ISimplifier>();

            var deriv = new Derivative(
                differentiator.Object,
                simplifier.Object,
                Variable.X,
                Variable.X,
                Number.Two);

            Assert.Equal(2.0, deriv.Execute());
        }

        [Fact]
        public void ExecuteNullDerivTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Derivative(null, null, Variable.X));
        }

        [Fact]
        public void ExecuteNullSimpTest()
        {
            var differentiator = new Mock<IDifferentiator>();
            differentiator
                .Setup(d => d.Analyze(It.IsAny<Derivative>(), It.IsAny<DifferentiatorContext>()))
                .Returns<Derivative, DifferentiatorContext>((e, context) => e.Expression);

            var simplifier = new Mock<ISimplifier>();

            var exp = new Derivative(differentiator.Object, simplifier.Object, Variable.X);

            var result = exp.Execute();

            Assert.Equal(result, result);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Derivative(new Differentiator(), new Simplifier(), new Sin(Variable.X), Variable.X, Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}