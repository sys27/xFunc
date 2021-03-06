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
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class UnaryMinusTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var exp = new UnaryMinus(new Number(10));

            Assert.Equal(new NumberValue(-10.0), exp.Execute());
        }

        [Fact]
        public void ExecuteAngleNumberTest()
        {
            var exp = new UnaryMinus(AngleValue.Degree(10).AsExpression());
            var expected = AngleValue.Degree(-10);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteComplexTest()
        {
            var complex = new Complex(2, 3);
            var exp = new UnaryMinus(new ComplexNumber(complex));

            Assert.Equal(Complex.Negate(complex), exp.Execute());
        }

        [Fact]
        public void NotSupportedException()
            => TestNotSupported(new UnaryMinus(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new UnaryMinus(Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = new UnaryMinus(Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = new UnaryMinus(Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }
    }
}