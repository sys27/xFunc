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
using xFunc.Maths;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Tests.Expressions.LogicalAndBitwise
{
    
    public class NotTest
    {

        private Parser parser;
        
        public NotTest()
        {
            parser = new Parser();
        }

        [Fact]
        public void CalculateTest()
        {
            var exp = parser.Parse("~a");
            var parameters = new ParameterCollection();
            parameters.Add("a");

            parameters["a"] = true;
            Assert.False((bool)exp.Calculate(parameters));

            parameters["a"] = false;
            Assert.True((bool)exp.Calculate(parameters));
        }

    }

}
