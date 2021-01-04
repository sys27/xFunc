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

using xFunc.Maths.Results;
using Xunit;

namespace xFunc.Tests.Results
{
    public class BooleanResultTest
    {
        [Fact]
        public void ResultTest()
        {
            var result = new BooleanResult(true);

            Assert.True(result.Result);
        }

        [Fact]
        public void IResultTest()
        {
            var result = new BooleanResult(true) as IResult;

            Assert.True((bool) result.Result);
        }

        [Fact]
        public void ToStringTest()
        {
            var result = new BooleanResult(true);

            Assert.Equal("True", result.ToString());
        }
    }
}