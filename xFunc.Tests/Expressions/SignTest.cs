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
using Xunit;

namespace xFunc.Tests.Expressionss
{

    public class SignTest
    {

        [Fact]
        public void PositiveSignTest()
        {
            var exp = new Sign(new Number(5));
            var result = exp.Execute();

            Assert.Equal(1, result);
        }

        [Fact]
        public void NegativeSignTest()
        {
            var exp = new Sign(new Number(-5));
            var result = exp.Execute();

            Assert.Equal(-1, result);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Sign(new Number(-5));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
