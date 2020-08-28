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
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class ReverseHyperbolicTests : BaseParserTests
    {
        [Fact]
        public void ArsinhTest()
        {
            var tokens = Builder()
                .Id("arsinh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arsinh(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArshTest()
        {
            var tokens = Builder()
                .Id("arsh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arsinh(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcoshTest()
        {
            var tokens = Builder()
                .Id("arcosh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcosh(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArchTest()
        {
            var tokens = Builder()
                .Id("arch")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcosh(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArtanhTest()
        {
            var tokens = Builder()
                .Id("artanh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Artanh(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArthTest()
        {
            var tokens = Builder()
                .Id("arth")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Artanh(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcothTest()
        {
            var tokens = Builder()
                .Id("arcoth")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcoth(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcthTest()
        {
            var tokens = Builder()
                .Id("arcth")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcoth(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArsechTest()
        {
            var tokens = Builder()
                .Id("arsech")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arsech(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArschTest()
        {
            var tokens = Builder()
                .Id("arsch")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arsech(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcschTest()
        {
            var tokens = Builder()
                .Id("arcsch")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcsch(Number.Two);

            Assert.Equal(expected, exp);
        }
    }
}