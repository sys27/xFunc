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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class CeilTest
    {

        [Fact]
        public void ExecuteTestNumber()
        {
            var ceil = new Ceil(new Number(5.55555555));
            var result = ceil.Execute();
            var expected = 6.0;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteTestExecption()
        {
            var exp = new Ceil(new Bool(false));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Ceil(new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
