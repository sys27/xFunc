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

using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Units.AngleUnits;
using Xunit;

namespace xFunc.Tests.Expressions.Hyperbolic
{
    public class HyperbolicTangentTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var exp = new Tanh(Number.One);
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(0.017451520543541533);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Tanh(AngleValue.Radian(1).AsExpression());
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(0.7615941559557649);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Tanh(AngleValue.Degree(1).AsExpression());
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(0.017451520543541533);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Tanh(AngleValue.Gradian(1).AsExpression());
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(0.015706671467249425);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Tanh(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(1.0032386273536098, result.Real, 15);
            Assert.Equal(-0.0037640256415041864, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Tanh(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Tanh(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}