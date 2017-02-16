// Copyright 2012-2017 Dmitry Kischenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Statistical;
using Xunit;

namespace xFunc.Tests.Expressionss.Statistical
{

    public class VarTest
    {

        [Fact]
        public void OneNumberTest()
        {
            var exp = new Var(new[] { new Number(4) }, 1);
            var result = exp.Execute();

            Assert.Equal(double.NaN, result);
        }

        [Fact]
        public void TwoNumberTest()
        {
            var exp = new Var(new[] { new Number(4), new Number(9) }, 2);
            var result = exp.Execute();

            Assert.Equal(12.5, result);
        }

        [Fact]
        public void ThreeNumberTest()
        {
            var exp = new Var(new[] { new Number(9), new Number(2), new Number(4) }, 3);
            var result = exp.Execute();

            Assert.Equal(13.0, result);
        }

        [Fact]
        public void VectorTest()
        {
            var exp = new Var(new[] { new Vector(new[] { new Number(2), new Number(4), new Number(9) }) }, 1);
            var result = exp.Execute();

            Assert.Equal(13.0, result);
        }

        [Fact]
        public void ArgTypeTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() =>
                new Var(new[] {
                    new Vector(new[] { new Number(1), new Number(2), new Number(3), }),
                    new Vector(new[] { new Number(1), new Number(2), new Number(3), })
                }, 2));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Var(new[] { new Number(1), new Number(2) }, 2);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
