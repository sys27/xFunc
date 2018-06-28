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
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class MulTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Mul(new Number(2), new Number(2));

            Assert.Equal(4.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));
            var expected = new Complex(-4, 19);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new Number(2));
            var expected = new Complex(4, 10);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = new Mul(new Number(2), new ComplexNumber(3, 2));
            var expected = new Complex(6, 4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest6()
        {
            var exp = new Mul(new Number(2), new Sqrt(new Number(-9)));
            var expected = new Complex(0, 6);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteCrossTest()
        {
            var vector1 = new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2), new Number(3) });
            var vector2 = new Maths.Expressions.Matrices.Vector(new[] { new Number(10), new Number(20), new Number(30) });
            var exp = new Mul(vector1, vector2);

            var expected = new Maths.Expressions.Matrices.Vector(new[] { new Number(0), new Number(0), new Number(0) });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteCrossFailTest()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var vector1 = new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(3) });
                var vector2 = new Maths.Expressions.Matrices.Vector(new[] { new Number(10), new Number(20) });
                var exp = new Mul(vector1, vector2);
                exp.Execute();
            });
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Mul(Variable.X, new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
