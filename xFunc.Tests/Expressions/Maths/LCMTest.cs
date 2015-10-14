// Copyright 2012-2015 Dmitry Kischenko
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

namespace xFunc.Tests.Expressions.Maths
{
    
    public class LCMTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var exp = new LCM(new Number(12), new Number(16));

            Assert.Equal(48.0, exp.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var exp = new LCM(new IExpression[] { new Number(4), new Number(16), new Number(8) }, 3);

            Assert.Equal(16.0, exp.Calculate());
        }

    }

}
