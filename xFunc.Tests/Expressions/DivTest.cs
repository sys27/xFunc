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

using System;
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class DivTest
    {
        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Div(new Number(1), new Number(2));

            Assert.Equal(1.0 / 2.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Div(new ComplexNumber(3, 2), new ComplexNumber(2, 4));
            var expected = new Complex(0.7, -0.4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new Div(new Number(3), new ComplexNumber(2, 4));
            var expected = new Complex(0.3, -0.6);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = new Div(new ComplexNumber(3, 2), new Number(2));
            var expected = new Complex(1.5, 1);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest6()
        {
            var exp = new Div(new Sqrt(new Number(-16)), new Number(2));
            var expected = new Complex(0, 2);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void DivNumberAndDegree()
        {
            var exp = new Div(new Number(10), Angle.Degree(2).AsExpression());
            var actual = exp.Execute();
            var expected = Angle.Degree(5);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DivRadianAndNumber()
        {
            var exp = new Div(Angle.Radian(10).AsExpression(), new Number(2));
            var actual = exp.Execute();
            var expected = Angle.Radian(5);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DivDegreeAndRadian()
        {
            var exp = new Div(
                Angle.Radian(Math.PI).AsExpression(),
                Angle.Degree(10).AsExpression()
            );
            var actual = exp.Execute();
            var expected = Angle.Degree(18);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DivGradianAndGradian()
        {
            var exp = new Div(
                Angle.Gradian(20).AsExpression(),
                Angle.Gradian(10).AsExpression()
            );
            var actual = exp.Execute();
            var expected = Angle.Gradian(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteBoolTest()
        {
            var exp = new Div(Bool.False, Bool.True);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteComplexNumberBoolTest()
        {
            var exp = new Div(new ComplexNumber(2, 4), Bool.True);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteBoolComplexNumberTest()
        {
            var exp = new Div(Bool.True, new ComplexNumber(2, 4));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Div(Variable.X, new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}