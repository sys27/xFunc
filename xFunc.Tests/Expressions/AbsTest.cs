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

using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class AbsTest
    {
        [Fact]
        public void ExecuteTestNumber()
        {
            var exp = new Abs(new Number(-1));

            Assert.Equal(1.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTestAngleNumber()
        {
            var exp = new Abs(AngleValue.Degree(-10).AsExpression());
            var expected = AngleValue.Degree(10);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTestComplexNumber()
        {
            var exp = new Abs(new ComplexNumber(4, 2));

            Assert.Equal(Complex.Abs(new Complex(4, 2)), exp.Execute());
        }

        [Fact]
        public void ExecuteTestVector()
        {
            var exp = new Abs(new Maths.Expressions.Matrices.Vector(new IExpression[] { new Number(5), new Number(4), new Number(6), new Number(7) }));

            Assert.Equal(11.2249721603218241567, (double)exp.Execute(), 15);
        }

        [Fact]
        public void ExecuteTEstException()
        {
            var exp = new Abs(Bool.False);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void EqualsTest1()
        {
            Variable x1 = "x";
            Number num1 = 2;
            var mul1 = new Mul(num1, x1);
            var abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 2;
            var mul2 = new Mul(num2, x2);
            var abs2 = new Abs(mul2);

            Assert.True(abs1.Equals(abs2));
            Assert.True(abs1.Equals(abs1));
        }

        [Fact]
        public void EqualsTest2()
        {
            Variable x1 = "x";
            Number num1 = 2;
            var mul1 = new Mul(num1, x1);
            var abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 3;
            var mul2 = new Mul(num2, x2);
            var abs2 = new Abs(mul2);

            Assert.False(abs1.Equals(abs2));
            Assert.False(abs1.Equals(mul2));
            Assert.False(abs1.Equals(null));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Abs(Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}