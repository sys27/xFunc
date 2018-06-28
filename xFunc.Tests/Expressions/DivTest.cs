// Copyright 2012-2018 Dmitry Kischenko
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
        public void ExecuteBoolTest()
        {
            var exp = new Div(new Bool(false), new Bool(true));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteComplexNumberBoolTest()
        {
            var exp = new Div(new ComplexNumber(2, 4), new Bool(true));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteBoolComplexNumberTest()
        {
            var exp = new Div(new Bool(true), new ComplexNumber(2, 4));

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
