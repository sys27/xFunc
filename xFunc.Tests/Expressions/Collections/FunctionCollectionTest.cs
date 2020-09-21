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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Tests.Expressions.Collections
{
    public class FunctionCollectionTest
    {
        [Fact]
        public void EmptyCollectionRemoveItem()
        {
            var collection = new FunctionCollection();
            var uf = new UserFunction("func", new IExpression[0]);

            Assert.Throws<KeyNotFoundException>(() => collection.Remove(uf));
        }

        [Fact]
        public void ChangedEventTest()
        {
            var isExecuted = false;

            var parameters = new FunctionCollection();
            parameters.CollectionChanged += (sender, args) =>
            {
                isExecuted = true;

                Assert.Equal(NotifyCollectionChangedAction.Add, args.Action);
            };

            var uf = new UserFunction("func", new IExpression[0]);
            var value = new Sin(new Number(90));
            parameters.Add(uf, value);

            Assert.True(isExecuted);
        }
    }
}