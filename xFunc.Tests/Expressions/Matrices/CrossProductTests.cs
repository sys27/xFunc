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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions.Matrices
{

    public class CrossProductTests
    {

        [Fact]
        public void ExecuteTest()
        {
            var exp = new CrossProduct(
                new Vector(new[] { new Number(1), new Number(2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(5), new Number(6) })
            );
            var result = exp.Execute();
            var expected = new Vector(new [] { new Number(-3), new Number(6), new Number(-3) });

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteTypeExceptionTest()
        {
            var exp = new CrossProduct(new Number(1), new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteLeftTypeExceptionTest()
        {
            var exp = new CrossProduct(
                new Number(1), 
                new Vector(new[] { new Number(1), new Number(2), new Number(3) }));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteRightTypeExceptionTest()
        {
            var exp = new CrossProduct(
                new Vector(new[] { new Number(1), new Number(2), new Number(3) }),
                new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new CrossProduct(
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) })
            );
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
