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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{
    public class DecTest
    {
        [Fact]
        public void DecCalcTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var dec = new Dec(new Variable("x"));
            var result = (double)dec.Execute(parameters);

            Assert.Equal(9.0, result);
            Assert.Equal(9.0, parameters["x"]);
        }

        [Fact]
        public void DecNullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => new Dec(new Variable("x")).Execute());
        }

        [Fact]
        public void DecBoolTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var dec = new Dec(new Variable("x"));

            Assert.Throws<ResultIsNotSupportedException>(() => dec.Execute(parameters));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Dec(new Variable("x"));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}