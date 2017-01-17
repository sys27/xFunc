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
using System.Collections.Generic;
using System.Linq;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Tests
{

    public class MathParameterCollectionTest
    {

        [Fact]
        public void GetItemFromCollectionTest()
        {
            var parameters = new ParameterCollection
            {
                new Parameter("x", 2.3)
            };

            Assert.Equal(2.3, parameters["x"]);
        }

        [Fact]
        public void GetItemFromConstsTest()
        {
            var parameters = new ParameterCollection();

            Assert.Equal(Math.PI, parameters["π"]);
        }

        [Fact]
        public void GetItemKeyNotFoundTest()
        {
            var parameters = new ParameterCollection();

            Assert.Throws<KeyNotFoundException>(() => parameters["hello"]);
        }

        [Fact]
        public void SetItemFromCollectionTest()
        {
            var parameters = new ParameterCollection();
            parameters["x"] = 2.3;

            Assert.Equal(1, parameters.Collection.Count());
            Assert.True(parameters.ContainsKey("x"));
            Assert.Equal(2.3, parameters["x"]);
        }

        [Fact]
        public void SetExistItemFromCollectionTest()
        {
            var parameters = new ParameterCollection
            {
                new Parameter("x", 2.3)
            };
            parameters["x"] = 3.3;

            Assert.Equal(1, parameters.Collection.Count());
            Assert.True(parameters.ContainsKey("x"));
            Assert.Equal(3.3, parameters["x"]);
        }

        [Fact]
        public void SetReadOnlyItemTest()
        {
            var parameters = new ParameterCollection
            {
                new Parameter("hello", 2.5, ParameterType.ReadOnly)
            };

            Assert.Throws<ParameterIsReadOnlyException>(() => parameters["hello"] = 5);
        }

        [Fact]
        public void OverrideConstsTest()
        {
            var parameters = new ParameterCollection
            {
                ["π"] = 4
            };

            Assert.Equal(1, parameters.Collection.Count());
            Assert.True(parameters.ContainsKey("π"));
            Assert.Equal(4.0, parameters["π"]);
        }

        [Fact]
        public void OverrideRemoveTest()
        {
            var parameters = new ParameterCollection
            {
                new Parameter("a", 1)
            };
            parameters["a"] = 2;
            parameters.Remove("a");

            Assert.Equal(0, parameters.Collection.Count());
        }

    }

}
