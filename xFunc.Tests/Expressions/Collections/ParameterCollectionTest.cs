// Copyright 2012-2020 Dmytro Kyshchenko
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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Tests.Expressions.Collections
{
    public class ParameterCollectionTest
    {
        [Fact]
        public void InitializeWithConstantsTest()
        {
            var parameters = new ParameterCollection(true);

            Assert.True(parameters.Constants.Any());
        }

        [Fact]
        public void InitializeWithoutConstantsTest()
        {
            var parameters = new ParameterCollection(false);

            Assert.False(parameters.Constants.Any());
        }

        [Fact]
        public void ChangedEventTest()
        {
            var isExecuted = false;

            var parameters = new ParameterCollection(false);
            parameters.CollectionChanged += (sender, args) =>
            {
                isExecuted = true;

                Assert.Equal(NotifyCollectionChangedAction.Add, args.Action);
                Assert.Null(args.OldItems);
                Assert.Equal(1, args.NewItems.Count);
            };

            parameters.Add(new Parameter("xxx", 1.0));

            Assert.True(isExecuted);
        }

        [Fact]
        public void EnumeratorTest()
        {
            var parameters = new ParameterCollection(true);

            Assert.True(parameters.Any());
        }

        [Fact]
        public void AddNullParameter()
        {
            var parameters = new ParameterCollection(true);

            Assert.Throws<ArgumentNullException>(() => parameters.Add(null as Parameter));
        }

        [Fact]
        public void AddConstantParameter()
        {
            var parameters = new ParameterCollection(true);
            var parameter = new Parameter("xxx", 1.0, ParameterType.Constant);

            Assert.Throws<ArgumentException>(() => parameters.Add(parameter));
        }

        [Fact]
        public void AddParameter()
        {
            var parameters = new ParameterCollection(true);
            var parameter = new Parameter("xxx", 1.0);

            parameters.Add(parameter);

            var result = parameters[parameter.Key];

            Assert.Equal(parameter.Value, result);
        }

        [Fact]
        public void AddStringParameter()
        {
            var parameters = new ParameterCollection(true);
            var key = "xxx";

            parameters.Add(key);

            var result = parameters[key];

            Assert.Equal(0.0, result);
        }

        [Fact]
        public void RemoveNullParameter()
        {
            var parameters = new ParameterCollection(true);

            Assert.Throws<ArgumentNullException>(() => parameters.Remove(null as Parameter));
        }

        [Fact]
        public void RemoveConstantParameter()
        {
            var parameters = new ParameterCollection(true);
            var parameter = new Parameter("xxx", 1.0, ParameterType.Constant);

            Assert.Throws<ArgumentException>(() => parameters.Add(parameter));
        }

        [Fact]
        public void RemoveStringParameter()
        {
            var parameters = new ParameterCollection(true);

            Assert.Throws<ArgumentNullException>(() => parameters.Remove(string.Empty));
        }

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

            Assert.Equal(AngleValue.Radian(Math.PI), parameters["π"]);
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
            var parameters = new ParameterCollection
            {
                ["x"] = 2.3
            };

            Assert.Single(parameters.Collection);
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

            Assert.Single(parameters.Collection);
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

            Assert.Single(parameters.Collection);
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

            Assert.Empty(parameters.Collection);
        }

        [Fact]
        public void ClearTest()
        {
            var parameters = new ParameterCollection(false)
            {
                new Parameter("a", 1)
            };

            parameters.Clear();

            Assert.Empty(parameters);
        }

        [Fact]
        public void GetByIndexOutOfRangeLowerTest()
        {
            var parameters = new ParameterCollection(true);

            Assert.Throws<IndexOutOfRangeException>(() => parameters[-1]);
        }

        [Fact]
        public void GetByIndexOutOfRangeHigherTest()
        {
            var parameters = new ParameterCollection(true)
            {
                new Parameter("x", 1.0)
            };

            Assert.Throws<IndexOutOfRangeException>(() => parameters[parameters.Count()]);
        }

        [Fact]
        public void GetConstantByIndexTest()
        {
            var parameters = new ParameterCollection(true)
            {
                new Parameter("x", 1.0)
            };

            var parameter = parameters[0];

            Assert.Equal(AngleValue.Radian(Math.PI), parameter);
        }

        [Fact]
        public void GetParameterByIndexTest()
        {
            var parameters = new ParameterCollection(true)
            {
                new Parameter("x", 1.0)
            };

            var parameter = parameters[parameters.Constants.Count()];

            Assert.Equal(1.0, parameter);
        }

        [Fact]
        public void SetByIndexOutOfRangeLowerTest()
        {
            var parameters = new ParameterCollection(true);

            Assert.Throws<IndexOutOfRangeException>(() => parameters[-1] = new Parameter("x", 1.0));
        }

        [Fact]
        public void SetByIndexOutOfRangeHigherTest()
        {
            var parameters = new ParameterCollection(true)
            {
                new Parameter("x", 1.0)
            };

            Assert.Throws<IndexOutOfRangeException>(() => parameters[parameters.Count()] = new Parameter("x", 1.0));
        }

        [Fact]
        public void SetConstantByIndexTest()
        {
            var parameters = new ParameterCollection(true)
            {
                new Parameter("x", 1.0)
            };

            Assert.Throws<ParameterIsReadOnlyException>(() => parameters[0] = 1.0);
        }

        [Fact]
        public void SetParameterByIndexTest()
        {
            var parameters = new ParameterCollection(true)
            {
                new Parameter("x", 1.0)
            };

            parameters[parameters.Constants.Count()] = 2.0;
            var parameter = parameters[parameters.Constants.Count()];

            Assert.Equal(2.0, parameter);
        }
    }
}