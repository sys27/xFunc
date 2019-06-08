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
using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class VariableTest
    {

        [Fact]
        public void ExecuteNotSupportedTest()
        {
            var exp = Variable.X;

            Assert.Throws<NotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteNullTest()
        {
            var exp = Variable.X;

            Assert.Throws<ArgumentNullException>(() => exp.Execute(null));
        }

        [Fact]
        public void ExecuteTest()
        {
            var exp = Variable.X;
            var parameters = new ExpressionParameters();
            parameters.Variables.Add("x", 1.0);

            var result = exp.Execute(parameters);

            Assert.Equal(1.0, result);
        }

        [Fact]
        public void ConvertToString()
        {
            var exp = Variable.X;

            Assert.Equal("x", exp);
        }

        [Fact]
        public void StringToConvert()
        {
            var exp = "x";
            var result = Variable.X;

            Assert.Equal<Variable>(result, exp);
        }

    }

}
