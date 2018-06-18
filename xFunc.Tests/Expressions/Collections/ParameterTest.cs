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
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Tests.Expressionss.Collections
{

    public class ParameterTest
    {

        [Fact]
        public void NullEqual()
        {
            var parameter = new Parameter("x", 1);
            var isEqual = parameter.Equals(null);

            Assert.False(isEqual);
        }

        [Fact]
        public void OtherType()
        {
            var parameter = new Parameter("x", 1);
            var obj = new object();
            var isEqual = parameter.Equals(obj);

            Assert.False(isEqual);
        }

        [Fact]
        public void EqualParameters()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("x", 1);
            var isEqual = parameter1.Equals(parameter2);

            Assert.True(isEqual);
        }

        [Fact]
        public void NotEqualKey()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("y", 1);
            var isEqual = parameter1.Equals(parameter2);

            Assert.False(isEqual);
        }

        [Fact]
        public void NotEqualValue()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("x", 2);
            var isEqual = parameter1.Equals(parameter2);

            Assert.False(isEqual);
        }

        [Fact]
        public void ToStringTest()
        {
            var parameter = new Parameter("x", 1, ParameterType.Constant);
            var str = parameter.ToString();
            var expected = "x: 1 (Constant)";

            Assert.Equal(expected, str);
        }

    }

}