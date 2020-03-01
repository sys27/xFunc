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
    public class IncTest
    {
        [Fact]
        public void IncCalcTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var inc = new Inc(Variable.X);
            var result = (double)inc.Execute(parameters);

            Assert.Equal(11.0, result);
            Assert.Equal(11.0, parameters["x"]);
        }

        [Fact]
        public void IncNullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => new Inc(Variable.X).Execute());
        }

        [Fact]
        public void IncBoolTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var inc = new Inc(Variable.X);

            Assert.Throws<ResultIsNotSupportedException>(() => inc.Execute(parameters));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Inc(Variable.X);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}