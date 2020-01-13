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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Statistical;
using Xunit;

namespace xFunc.Tests.Expressions.Statistical
{

    public class StdevTest
    {

        [Fact]
        public void OneNumberTest()
        {
            var exp = new Stdev(new[] { new Number(4) });
            var result = (double)exp.Execute();

            Assert.Equal(double.NaN, result);
        }

        [Fact]
        public void TwoNumberTest()
        {
            var exp = new Stdev(new[] { new Number(4), new Number(9) });
            var result = (double)exp.Execute();

            Assert.Equal(3.53553390593274, result, 14);
        }

        [Fact]
        public void ThreeNumberTest()
        {
            var exp = new Stdev(new[] { new Number(9), new Number(2), new Number(4) });
            var result = (double)exp.Execute();

            Assert.Equal(3.60555127546399, result, 14);
        }

        [Fact]
        public void VectorTest()
        {
            var exp = new Stdev(new[] { new Vector(new[] { new Number(2), new Number(4), new Number(9) }) });
            var result = (double)exp.Execute();

            Assert.Equal(3.60555127546399, result, 14);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Stdev(new[] { new Number(1), new Number(2) });
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}