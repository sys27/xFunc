// Copyright 2012-2019 Dmitry Kischenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions.Matrices
{

    public class DotProductTests
    {

        [Fact]
        public void ExecuteTest()
        {
            var exp = new DotProduct(
                new Vector(new[] { new Number(1), new Number(2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(5), new Number(6) })
            );
            var result = exp.Execute();

            Assert.Equal(32.0, result);
        }

        [Fact]
        public void ExecuteTypeExceptionTest()
        {
            var exp = new DotProduct(new Number(1), new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteLeftTypeExceptionTest()
        {
            var exp = new DotProduct(
                new Number(1),
                new Vector(new[] { new Number(1), new Number(2), new Number(3) }));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteRightTypeExceptionTest()
        {
            var exp = new DotProduct(
                new Vector(new[] { new Number(1), new Number(2), new Number(3) }),
                new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new DotProduct(
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) })
            );
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
