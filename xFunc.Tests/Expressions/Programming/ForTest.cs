// Copyright 2012-2021 Dmytro Kyshchenko
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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{
    public class ForTest
    {
        [Fact]
        public void CalculateForTest()
        {
            var parameters = new ExpressionParameters();

            var init = new Define(new Variable("i"), Number.Zero);
            var cond = new LessThan(new Variable("i"), new Number(10));
            var iter = new Define(new Variable("i"), new Add(new Variable("i"), Number.One));

            var @for = new For(new Variable("i"), init, cond, iter);
            @for.Execute(parameters);

            Assert.Equal(new NumberValue(10.0), parameters.Variables["i"]);
        }

        [Fact]
        public void CloneTest()
        {
            var init = new Define(new Variable("i"), Number.Zero);
            var cond = new LessThan(new Variable("i"), new Number(10));
            var iter = new Define(new Variable("i"), new Add(new Variable("i"), Number.One));

            var exp = new For(new Variable("i"), init, cond, iter);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}