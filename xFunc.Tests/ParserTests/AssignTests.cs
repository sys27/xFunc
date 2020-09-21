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
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class AssignTests : BaseParserTests
    {
        [Fact]
        public void ParseDefine()
        {
            var expected = new Define(Variable.X, new Number(3));

            ParseTest("x := 3", expected);
        }

        [Fact]
        public void ParseDefineFirstParamIsNotVar()
            => ParseErrorTest("5 := 3");

        [Fact]
        public void DefineComplexParserTest()
        {
            var expected = new Define(
                new Variable("aaa"),
                new Add(
                    new Number(3),
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                ));

            ParseTest("aaa := 3+2*i", expected);
        }

        [Fact]
        public void DefineUserFuncTest()
        {
            var expected = new Define(
                new UserFunction("func", new IExpression[] { Variable.X, new Variable("y") }),
                new Sin(Variable.X));

            ParseTest("func(x, y) := sin(x)", expected);
        }

        [Fact]
        public void UnaryMinusAssignTest()
        {
            var expected = new Define(
                Variable.X,
                new UnaryMinus(new Sin(Number.Two))
            );

            ParseTest("x := -sin(2)", expected);
        }

        [Fact]
        public void AddAssign()
        {
            var expected = new AddAssign(Variable.X, Number.Two);

            ParseTest("x += 2", expected);
        }

        [Fact]
        public void MulAssign()
        {
            var expected = new MulAssign(Variable.X, Number.Two);

            ParseTest("x *= 2", expected);
        }

        [Fact]
        public void SubAssign()
        {
            var expected = new SubAssign(Variable.X, Number.Two);

            ParseTest("x -= 2", expected);
        }

        [Fact]
        public void DivAssign()
        {
            var expected = new DivAssign(Variable.X, Number.Two);

            ParseTest("x /= 2", expected);
        }

        [Fact]
        public void UnaryMinusAddAssignTest()
        {
            var expected = new AddAssign(
                Variable.X,
                new UnaryMinus(new Sin(Number.Two))
            );

            ParseTest("x += -sin(2)", expected);
        }

        [Fact]
        public void UnaryMinusSubAssignTest()
        {
            var expected = new SubAssign(
                Variable.X,
                new UnaryMinus(new Sin(Number.Two))
            );

            ParseTest("x -= -sin(2)", expected);
        }

        [Fact]
        public void UnaryMinusMulAssignTest()
        {
            var expected = new MulAssign(
                Variable.X,
                new UnaryMinus(new Sin(Number.Two))
            );

            ParseTest("x *= -sin(2)", expected);
        }

        [Fact]
        public void UnaryMinusDivAssignTest()
        {
            var expected = new DivAssign(
                Variable.X,
                new UnaryMinus(new Sin(Number.Two))
            );

            ParseTest("x /= -sin(2)", expected);
        }

        [Theory]
        [InlineData("x :=")]
        [InlineData("x +=")]
        [InlineData("x -=")]
        [InlineData("x −=")]
        [InlineData("x *=")]
        [InlineData("x ×=")]
        [InlineData("x /=")]
        public void AssignMissingValue(string function)
            => ParseErrorTest(function);

        [Fact]
        public void LeftShiftAssignTest()
        {
            var expected = new LeftShiftAssign(Variable.X, new Number(10));

            ParseTest("x <<= 10", expected);
        }

        [Fact]
        public void RightShiftAssignTest()
        {
            var expected = new RightShiftAssign(Variable.X, new Number(10));

            ParseTest("x >>= 10", expected);
        }

        [Fact]
        public void AssignAsExpTest()
        {
            var expected = new Add(Number.One, new Define(Variable.X, Number.Two));

            ParseTest("1 + (x := 2)", expected);
        }

        [Fact]
        public void AddAssignAsExpTest()
        {
            var expected = new Add(Number.One, new AddAssign(Variable.X, Number.Two));

            ParseTest("1 + (x += 2)", expected);
        }

        [Fact]
        public void SubAssignAsExpTest()
        {
            var expected = new Add(Number.One, new SubAssign(Variable.X, Number.Two));

            ParseTest("1 + (x -= 2)", expected);
        }

        [Fact]
        public void MulAssignAsExpTest()
        {
            var expected = new Add(Number.One, new MulAssign(Variable.X, Number.Two));

            ParseTest("1 + (x *= 2)", expected);
        }

        [Fact]
        public void DivAssignAsExpTest()
        {
            var expected = new Add(Number.One, new DivAssign(Variable.X, Number.Two));

            ParseTest("1 + (x /= 2)", expected);
        }

        [Fact]
        public void LeftShiftAssignAsExpTest()
        {
            var expected = new Add(Number.One, new LeftShiftAssign(Variable.X, Number.Two));

            ParseTest("1 + (x <<= 2)", expected);
        }

        [Fact]
        public void RightShiftAssignAsExpTest()
        {
            var expected = new Add(Number.One, new RightShiftAssign(Variable.X, Number.Two));

            ParseTest("1 + (x >>= 2)", expected);
        }
    }
}