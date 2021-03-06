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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class PowTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Pow(Number.Two, new Number(10));
            var expected = new NumberValue(1024.0);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void NegativeExecuteTest1()
        {
            var exp = new Pow(new Number(-8), new Number(1 / 3.0));
            var expected = new NumberValue(-2.0);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void NegativeNumberExecuteTest1()
        {
            var exp = new Pow(new Number(-25), new Number(1 / 2.0));
            var result = (Complex)exp.Execute();
            var expected = new Complex(0, 5);

            Assert.Equal(expected.Real, result.Real, 14);
            Assert.Equal(expected.Imaginary, result.Imaginary, 14);
        }

        [Fact]
        public void NegativeNumberExecuteTest2()
        {
            var exp = new Pow(new Number(-25), new Number(-1 / 2.0));

            Assert.Equal(new Complex(0, -0.2), exp.Execute());
        }

        [Fact]
        public void NegativeNumberExecuteTest3()
        {
            var exp = new Pow(new Number(-5), Number.Two);

            Assert.Equal(new NumberValue(25.0), exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var complex1 = new Complex(3, 2);
            var complex2 = new Complex(4, 5);
            var exp = new Pow(new ComplexNumber(complex1), new ComplexNumber(complex2));

            Assert.Equal(Complex.Pow(complex1, complex2), exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var complex = new Complex(3, 2);
            var exp = new Pow(new ComplexNumber(complex), new Number(10));

            Assert.Equal(Complex.Pow(complex, 10), exp.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var complex1 = new Complex(-3, 2);
            var complex2 = new Complex(-4, 5);
            var exp = new Pow(new ComplexNumber(complex1), new ComplexNumber(complex2));

            Assert.Equal(Complex.Pow(complex1, complex2), exp.Execute());
        }

        [Fact]
        public void ExecuteTest5()
        {
            var complex = new Complex(-3, 2);
            var exp = new Pow(new ComplexNumber(complex), new Number(10));

            Assert.Equal(Complex.Pow(complex, 10), exp.Execute());
        }

        [Fact]
        public void ExecuteWrongArgumentTypeTest()
            => TestNotSupported(new Pow(Bool.True, Bool.True));

        [Fact]
        public void CloneTest()
        {
            var exp = new Pow(Variable.X, Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}