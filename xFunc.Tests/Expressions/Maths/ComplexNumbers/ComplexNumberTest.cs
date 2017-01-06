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
using System;
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using Xunit;

namespace xFunc.Tests.Expressions.Maths.ComplexNumbers
{

    public class ComplexNumberTest
    {

        [Fact]
        public void ExecuteTest()
        {
            var complex = new Complex(5, 2);
            var exp = new ComplexNumber(complex);

            Assert.Equal(complex, exp.Execute());
        }

        [Fact]
        public void ExecuteWithParamsTest()
        {
            var complex = new Complex(5, 2);
            var exp = new ComplexNumber(complex);

            Assert.Equal(complex, exp.Execute(null));
        }

        [Fact]
        public void ValueTest()
        {
            var complex = new Complex(5, 2);
            var exp = new ComplexNumber(complex);

            Assert.Equal(complex, exp.Value);
        }

        [Fact]
        public void CastToComplexTest()
        {
            var complex = new Complex(5, 2);
            var exp = new ComplexNumber(complex);
            var result = (Complex)exp;

            Assert.Equal(complex, result);
        }

        [Fact]
        public void CastToComplexNumberTest()
        {
            var complex = new Complex(5, 2);
            var exp = (ComplexNumber)complex;
            var result = new ComplexNumber(complex);

            Assert.Equal(exp, result);
        }

        [Fact]
        public void EqualsTest()
        {
            var exp1 = new ComplexNumber(new Complex(5, 2));
            var exp2 = new ComplexNumber(new Complex(5, 2));

            Assert.True(exp1.Equals(exp2));
        }

        [Fact]
        public void NotEqualsTest()
        {
            var exp1 = new ComplexNumber(new Complex(5, 2));
            var exp2 = new ComplexNumber(new Complex(3, 2));

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void NotEqualsDiffTypesTest()
        {
            var exp1 = new ComplexNumber(new Complex(5, 2));
            var exp2 = new Number(2);

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void PositiveNegativeToStringTest()
        {
            var complex = new ComplexNumber(3, -2);

            Assert.Equal("3-2i", complex.ToString());
        }

        [Fact]
        public void NegativePositiveToStringTest()
        {
            var complex = new ComplexNumber(-3, 2);

            Assert.Equal("-3+2i", complex.ToString());
        }

        [Fact]
        public void TwoPositiveToStringTest()
        {
            var complex = new ComplexNumber(3, 2);

            Assert.Equal("3+2i", complex.ToString());
        }

        [Fact]
        public void TwoNegativeToStringTest()
        {
            var complex = new ComplexNumber(-3, -2);

            Assert.Equal("-3-2i", complex.ToString());
        }

        [Fact]
        public void OnlyRealPartToStringTest()
        {
            var complex = new ComplexNumber(-3, 0);

            Assert.Equal("-3", complex.ToString());
        }

        [Fact]
        public void OnlyImaginaryPartToStringTest()
        {
            var complex = new ComplexNumber(0, -2);

            Assert.Equal("-2i", complex.ToString());
        }

        [Fact]
        public void BinaryToStringTest()
        {
            var exp = new Add(new ComplexNumber(3, 2), new ComplexNumber(3, 2));

            Assert.Equal("3+2i + 3+2i", exp.ToString());
        }

        [Fact]
        public void AbsToStringTest()
        {
            var exp = new Abs(new ComplexNumber(3, 2));

            Assert.Equal("abs(3+2i)", exp.ToString());
        }

        [Fact]
        public void IToStringTest()
        {
            var exp = new ComplexNumber(0, 1);

            Assert.Equal("i", exp.ToString());
        }

        [Fact]
        public void NegativeIToStringTest()
        {
            var exp = new ComplexNumber(0, -1);

            Assert.Equal("-i", exp.ToString());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new ComplexNumber(new Complex(2, 2));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
