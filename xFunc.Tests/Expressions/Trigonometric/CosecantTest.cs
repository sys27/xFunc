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
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions.Trigonometric
{
    public class CosecantTest
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var exp = new Csc(new Number(1));
            var result = (double)exp.Execute();

            Assert.Equal(57.298688498550185, result, 15);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Csc(Angle.Degree(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(57.298688498550185, result, 15);
        }

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Csc(Angle.Radian(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(1.1883951057781212, result, 15);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Csc(Angle.Gradian(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(63.664595306000564, result, 15);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Csc(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(0.040300578856891527, result.Real, 15);
            Assert.Equal(0.27254866146294021, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
        {
            var exp = new Csc(Bool.False);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Csc(new Number(1));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}