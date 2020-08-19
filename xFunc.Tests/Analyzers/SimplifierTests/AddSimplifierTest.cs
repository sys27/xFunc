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
        [Fact]
        public void AddFirstZero()
        {
            var add = new Add(new Number(0), Variable.X);
            var expected = Variable.X;

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddSecondZero()
        {
            var add = new Add(Variable.X, new Number(0));
            var expected = Variable.X;

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddTwoNumbers()
        {
            var add = new Add(new Number(3), new Number(2));
            var expected = new Number(5);

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddFirstUnaryMinus()
        {
            var add = new Add(new UnaryMinus(Variable.X), new Number(2));
            var expected = new Sub(new Number(2), Variable.X);

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddSecondUnaryMinus()
        {
            var add = new Add(new Number(2), new UnaryMinus(Variable.X));
            var expected = new Sub(new Number(2), Variable.X);

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddDiffNumAdd_NumAddVar_()
        {
            // 2 + (2 + x)
            var add = new Add(new Number(2), new Add(new Number(2), Variable.X));
            var expected = new Add(Variable.X, new Number(4));

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddDiffNumAdd_VarAddNum_()
        {
            // 2 + (x + 2)
            var add = new Add(new Number(2), new Add(Variable.X, new Number(2)));
            var expected = new Add(Variable.X, new Number(4));

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddDiff_NumAddVar_AddNum()
        {
            // (2 + x) + 2
            var add = new Add(new Add(new Number(2), Variable.X), new Number(2));
            var expected = new Add(Variable.X, new Number(4));

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddDiff_VarAddNum_AddNum()
        {
            // (x + 2) + 2
            var add = new Add(new Add(Variable.X, new Number(2)), new Number(2));
            var expected = new Add(Variable.X, new Number(4));

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddDiffNum_NumSubVar_()
        {
            // 2 + (2 - x)
            var add = new Add(new Number(2), new Sub(new Number(2), Variable.X));
            var expected = new Sub(new Number(4), Variable.X);

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddDiffNum_VarSubNum_()
        {
            // 2 + (x - 2)
            var add = new Add(new Number(2), new Sub(Variable.X, new Number(2)));
            var expected = Variable.X;

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddDiff_NumSubVar_AddNum()
        {
            // (2 - x) + 2
            var add = new Add(new Sub(new Number(2), Variable.X), new Number(2));
            var expected = new Sub(new Number(4), Variable.X);

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddDiff_VarSubNum_AddNum()
        {
            // (x - 2) + 2
            var add = new Add(new Sub(Variable.X, new Number(2)), new Number(2));
            var expected = Variable.X;

            SimpleTest(add, expected);
        }

        [Fact]
        public void AddSaveVars1()
        {
            // x + x
            var var = Variable.X;
            var exp = new Add(var, var);
            var expected = new Mul(new Number(2), var);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars2()
        {
            // 2x + x
            var var = Variable.X;
            var exp = new Add(new Mul(new Number(2), var), var);
            var expected = new Mul(new Number(3), var);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars3()
        {
            // x + 2x
            var var = Variable.X;
            var exp = new Add(var, new Mul(new Number(2), var));
            var expected = new Mul(new Number(3), var);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars4()
        {
            // x + x * 2
            var var = Variable.X;
            var exp = new Add(var, new Mul(var, new Number(2)));
            var expected = new Mul(new Number(3), var);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars5()
        {
            // 2x + 3x
            var var = Variable.X;
            var exp = new Add(new Mul(new Number(2), var), new Mul(new Number(3), var));
            var expected = new Mul(new Number(5), var);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars6()
        {
            // -x + x
            var var = Variable.X;
            var exp = new Add(new UnaryMinus(var), var);
            var expected = new Number(0);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars7()
        {
            // -x + 2x
            var var = Variable.X;
            var exp = new Add(new UnaryMinus(var), new Mul(new Number(2), var));
            var expected = var;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars8()
        {
            // x * 2 + x
            var var = Variable.X;
            var exp = new Add(new Mul(var, new Number(2)), var);
            var expected = new Mul(new Number(3), var);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars9()
        {
            // x * 2 + x * 3
            var var = Variable.X;
            var exp = new Add(new Mul(var, new Number(2)), new Mul(var, new Number(3)));
            var expected = new Mul(new Number(5), var);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars10()
        {
            // 3x + -2x
            var var = Variable.X;
            var exp = new Add(new Mul(new Number(3), var), new Mul(new Number(-2), var));
            var expected = var;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSaveVars11()
        {
            // 3x + -4x
            var var = Variable.X;
            var exp = new Add(new Mul(new Number(3), var), new Mul(new Number(-4), var));
            var expected = new UnaryMinus(var);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSameVars12()
        {
            // -2x + x*3
            var var = Variable.X;
            var exp = new Add(
                new Mul(new Number(-2), var),
                new Mul(var, new Number(3)));
            var expected = var;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSameVars13()
        {
            // x*3 + -2x
            var var = Variable.X;
            var exp = new Add(
                new Mul(var, new Number(3)),
                new Mul(new Number(-2), var));
            var expected = var;

            SimpleTest(exp, expected);
        }
    }
}