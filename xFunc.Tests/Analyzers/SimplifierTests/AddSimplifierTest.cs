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
using Xunit;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class AddSimplifierTest : BaseSimplifierTest
    {
        [Fact(DisplayName = "0 + x")]
        public void AddFirstZero()
        {
            var add = new Add(new Number(0), new Variable("x"));
            var expected = new Variable("x");

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "x + 0")]
        public void AddSecondZero()
        {
            var add = new Add(new Variable("x"), new Number(0));
            var expected = new Variable("x");

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "3 + 2")]
        public void AddTwoNumbers()
        {
            var add = new Add(new Number(3), new Number(2));
            var expected = new Number(5);

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "-x + 2")]
        public void AddFirstUnaryMinus()
        {
            var add = new Add(new UnaryMinus(new Variable("x")), new Number(2));
            var expected = new Sub(new Number(2), new Variable("x"));

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "2 + (-x)")]
        public void AddSecondUnaryMinus()
        {
            var add = new Add(new Number(2), new UnaryMinus(new Variable("x")));
            var expected = new Sub(new Number(2), new Variable("x"));

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "2 + (2 + x)")]
        public void AddDiffNumAdd_NumAddVar_()
        {
            var add = new Add(new Number(2), new Add(new Number(2), new Variable("x")));
            var expected = new Add(new Variable("x"), new Number(4));

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "2 + (x + 2)")]
        public void AddDiffNumAdd_VarAddNum_()
        {
            var add = new Add(new Number(2), new Add(new Variable("x"), new Number(2)));
            var expected = new Add(new Variable("x"), new Number(4));

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "(2 + x) + 2")]
        public void AddDiff_NumAddVar_AddNum()
        {
            var add = new Add(new Add(new Number(2), new Variable("x")), new Number(2));
            var expected = new Add(new Variable("x"), new Number(4));

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "(x + 2) + 2")]
        public void AddDiff_VarAddNum_AddNum()
        {
            var add = new Add(new Add(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Add(new Variable("x"), new Number(4));

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "2 + (2 - x)")]
        public void AddDiffNum_NumSubVar_()
        {
            var add = new Add(new Number(2), new Sub(new Number(2), new Variable("x")));
            var expected = new Sub(new Number(4), new Variable("x"));

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "2 + (x - 2)")]
        public void AddDiffNum_VarSubNum_()
        {
            var add = new Add(new Number(2), new Sub(new Variable("x"), new Number(2)));
            var expected = new Variable("x");

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "(2 - x) + 2")]
        public void AddDiff_NumSubVar_AddNum()
        {
            var add = new Add(new Sub(new Number(2), new Variable("x")), new Number(2));
            var expected = new Sub(new Number(4), new Variable("x"));

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "(x - 2) + 2")]
        public void AddDiff_VarSubNum_AddNum()
        {
            var add = new Add(new Sub(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Variable("x");

            SimpleTest(add, expected);
        }

        [Fact(DisplayName = "x + x")]
        public void AddSaveVars1()
        {
            var exp = new Add(new Variable("x"), new Variable("x"));
            var expected = new Mul(new Number(2), new Variable("x"));

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "2x + x")]
        public void AddSaveVars2()
        {
            var exp = new Add(new Mul(new Number(2), new Variable("x")), new Variable("x"));
            var expected = new Mul(new Number(3), new Variable("x"));

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "x + 2x")]
        public void AddSaveVars3()
        {
            var exp = new Add(new Variable("x"), new Mul(new Number(2), new Variable("x")));
            var expected = new Mul(new Number(3), new Variable("x"));

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "x + x * 2")]
        public void AddSaveVars4()
        {
            var exp = new Add(new Variable("x"), new Mul(new Variable("x"), new Number(2)));
            var expected = new Mul(new Number(3), new Variable("x"));

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "2x + 3x")]
        public void AddSaveVars5()
        {
            var exp = new Add(
                new Mul(new Number(2), new Variable("x")),
                new Mul(new Number(3), new Variable("x"))
            );
            var expected = new Mul(new Number(5), new Variable("x"));

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "-x + x")]
        public void AddSaveVars6()
        {
            var exp = new Add(new UnaryMinus(new Variable("x")), new Variable("x"));
            var expected = new Number(0);

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "-x + 2x")]
        public void AddSaveVars7()
        {
            var exp = new Add(
                new UnaryMinus(new Variable("x")),
                new Mul(new Number(2), new Variable("x"))
            );
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "x * 2 + x")]
        public void AddSaveVars8()
        {
            var exp = new Add(new Mul(new Variable("x"), new Number(2)), new Variable("x"));
            var expected = new Mul(new Number(3), new Variable("x"));

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "x * 2 + x * 3")]
        public void AddSaveVars9()
        {
            var exp = new Add(
                new Mul(new Variable("x"), new Number(2)),
                new Mul(new Variable("x"), new Number(3))
            );
            var expected = new Mul(new Number(5), new Variable("x"));

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "3x + -2x")]
        public void AddSaveVars10()
        {
            var exp = new Add(
                new Mul(new Number(3), new Variable("x")),
                new Mul(new Number(-2), new Variable("x"))
            );
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "3x + -4x")]
        public void AddSaveVars11()
        {
            var exp = new Add(
                new Mul(new Number(3), new Variable("x")),
                new Mul(new Number(-4), new Variable("x"))
            );
            var expected = new UnaryMinus(new Variable("x"));

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "-2x + x * 3")]
        public void AddSameVars12()
        {
            var exp = new Add(
                new Mul(new Number(-2), new Variable("x")),
                new Mul(new Variable("x"), new Number(3))
            );
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "x * 3 + -2x")]
        public void AddSameVars13()
        {
            var exp = new Add(
                new Mul(new Variable("x"), new Number(3)),
                new Mul(new Number(-2), new Variable("x"))
            );
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }
    }
}