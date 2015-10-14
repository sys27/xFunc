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
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions.Maths.Trigonometric
{
    
    public class ArctanTest
    {

        [Fact]
        public void CalculateRadianTest()
        {
            IExpression exp = new Arctan(new Number(1));

            Assert.Equal(Math.Atan(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [Fact]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Arctan(new Number(1));

            Assert.Equal(Math.Atan(1) / Math.PI * 180, exp.Calculate(AngleMeasurement.Degree));
        }

        [Fact]
        public void CalculateGradianTest()
        {
            IExpression exp = new Arctan(new Number(1));

            Assert.Equal(Math.Atan(1) / Math.PI * 200, exp.Calculate(AngleMeasurement.Gradian));
        }
        
    }

}
