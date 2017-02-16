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
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions.Maths
{

    public class CachedResultTypeExpressionTest
    {

        [Fact]
        public void IsChangedTest()
        {
            var sin1 = new Sin(new Number(1));
            var sin2 = new Sin(new Number(2));
            var sin3 = new Add(sin1, sin2);
            var sin4 = new Sin(sin3);
            var exp = new Sin(sin3);

            Assert.Equal(ExpressionResultType.Number, exp.ResultType);

            sin3.Left = new Number(3);
            Assert.False(sin2.IsChanged);
            Assert.True(exp.IsChanged);

            Assert.Equal(ExpressionResultType.Number, exp.ResultType);
        }

    }

}
