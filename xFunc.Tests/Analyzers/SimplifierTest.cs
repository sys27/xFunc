// Copyright 2012-2018 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;
using xFunc.Maths.Analyzers;

namespace xFunc.Tests.Analyzers
{

    public class SimplifierTest
    {

        private IAnalyzer<IExpression> simplifier;

        public SimplifierTest()
        {
            simplifier = new Simplifier();
        }

        private void SimpleTest(IExpression exp, IExpression expected)
        {
            var simple = exp.Analyze(simplifier);

            Assert.Equal(expected, simple);
        }

        [Fact]
        public void DoubleUnary()
        {
            var un = new UnaryMinus(new UnaryMinus(Variable.X));
            var expected = Variable.X;

            SimpleTest(un, expected);
        }

        [Fact]
        public void UnaryNumber()
        {
            var un = new UnaryMinus(new Number(1));
            var expected = new Number(-1);

            SimpleTest(un, expected);
        }

        #region Add

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

        #endregion

        #region Sub

        [Fact]
        public void SubFirstZero()
        {
            var sub = new Sub(new Number(0), Variable.X);
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSecondZero()
        {
            var sub = new Sub(Variable.X, new Number(0));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubTwoNumbers()
        {
            var sub = new Sub(new Number(3), new Number(2));
            var expected = new Number(1);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSecondUnaryMinus()
        {
            var sub = new Sub(new Number(2), new UnaryMinus(Variable.X));
            var expected = new Add(new Number(2), Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiff_NumAddVar_SubNum()
        {
            // (2 + x) - 2
            var sub = new Sub(new Add(new Number(2), Variable.X), new Number(2));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiff_VarAddNum_SubNum()
        {
            // (x + 2) - 2
            var sub = new Sub(new Add(Variable.X, new Number(2)), new Number(2));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiffNumSub_NumAddVar_()
        {
            // 2 - (2 + x)
            var sub = new Sub(new Number(2), new Add(new Number(2), Variable.X));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiffNumSub_VarAddNum_()
        {
            // 2 - (x + 2)
            var sub = new Sub(new Number(2), new Add(Variable.X, new Number(2)));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiff_NumSubVar_SubNum()
        {
            var sub = new Sub(new Sub(new Number(2), Variable.X), new Number(2));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiff_VarSubNum_SubNum()
        {
            var sub = new Sub(new Sub(Variable.X, new Number(2)), new Number(2));
            var expected = new Sub(Variable.X, new Number(4));

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiffNumSub_NumSubVar_()
        {
            var sub = new Sub(new Number(2), new Sub(new Number(2), Variable.X));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiffNumSub_VarSubNum_()
        {
            var sub = new Sub(new Number(2), new Sub(Variable.X, new Number(2)));
            var expected = new Sub(new Number(4), Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars1()
        {
            // x - x
            var sub = new Sub(Variable.X, Variable.X);
            var expected = new Number(0);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars2()
        {
            // (x - x) - x
            var x = Variable.X;
            var sub = new Sub(new Sub(x, x), x);
            var expected = new UnaryMinus(x);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars3()
        {
            // 2x - x
            var x = Variable.X;
            var sub = new Sub(new Mul(new Number(2), x), x);
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars4()
        {
            // x - 2x
            var x = Variable.X;
            var sub = new Sub(x, new Mul(new Number(2), x));
            var expected = new UnaryMinus(x);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars5()
        {
            // x - (x * 2)
            var x = Variable.X;
            var sub = new Sub(x, new Mul(x, new Number(2)));
            var expected = new UnaryMinus(x);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars6()
        {
            // 2x - x
            var x = Variable.X;
            var sub = new Sub(new Mul(new Number(2), x), x);
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars7()
        {
            // (x * 2) - x
            var x = Variable.X;
            var sub = new Sub(new Mul(x, new Number(2)), x);
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars8()
        {
            // 3x - 2x
            var x = Variable.X;
            var sub = new Sub(new Mul(new Number(3), x), new Mul(new Number(2), x));
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars9()
        {
            // (x * 3) - (x * 2)
            var x = Variable.X;
            var sub = new Sub(new Mul(x, new Number(3)), new Mul(x, new Number(2)));
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars10()
        {
            // (x * 4) - (x * 2)
            var x = Variable.X;
            var sub = new Sub(new Mul(x, new Number(4)), new Mul(x, new Number(2)));
            var expected = new Mul(new Number(2), x);

            SimpleTest(sub, expected);
        }

        #endregion

        #region Mul

        [Fact]
        public void MulByZero()
        {
            var mul = new Mul(Variable.X, new Number(0));
            var expected = new Number(0);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulFirstOne()
        {
            var mul = new Mul(new Number(1), Variable.X);
            var expected = Variable.X;

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSecondOne()
        {
            var mul = new Mul(Variable.X, new Number(1));
            var expected = Variable.X;

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulTwoNumbers()
        {
            var mul = new Mul(new Number(2), new Number(3));
            var expected = new Number(6);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiffNumMul_NumMulVar_()
        {
            var mul = new Mul(new Number(2), new Mul(new Number(2), Variable.X));
            var expected = new Mul(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiffNumMul_VarMulNum_()
        {
            var mul = new Mul(new Number(2), new Mul(Variable.X, new Number(2)));
            var expected = new Mul(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiff_NumMulVar_MulNum()
        {
            var mul = new Mul(new Mul(new Number(2), Variable.X), new Number(2));
            var expected = new Mul(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiff_VarMulNum_MulNum()
        {
            var mul = new Mul(new Mul(Variable.X, new Number(2)), new Number(2));
            var expected = new Mul(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiffNumMul_NumDivVar_()
        {
            // 2 * (2 / x)
            var mul = new Mul(new Number(2), new Div(new Number(2), Variable.X));
            var expected = new Div(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiffNumMul_VarDivNum_()
        {
            // 2 * (x / 2)
            var mul = new Mul(new Number(2), new Div(Variable.X, new Number(2)));
            var expected = Variable.X;

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiffl_NumDivVar_MulNum()
        {
            // (2 / x) * 2
            var mul = new Mul(new Div(new Number(2), Variable.X), new Number(2));
            var expected = new Div(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiff_VarDivNum_MulNum()
        {
            // (x / 2) * 2
            var mul = new Mul(new Div(Variable.X, new Number(2)), new Number(2));
            var expected = Variable.X;

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar1()
        {
            // x * x
            var var = Variable.X;
            var mul = new Mul(var, var);
            var expected = new Pow(var, new Number(2));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar2()
        {
            // 2x * x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), var);
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar3()
        {
            // 2x * 3x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), new Mul(new Number(3), var));
            var expected = new Mul(new Number(6), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar4()
        {
            // x * 2x
            var var = Variable.X;
            var mul = new Mul(var, new Mul(new Number(2), var));
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar5()
        {
            // x * (x * 2)
            var var = Variable.X;
            var mul = new Mul(var, new Mul(var, new Number(2)));
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar6()
        {
            // 2x * x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), var);
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar7()
        {
            // (x * 2) * x
            var var = Variable.X;
            var mul = new Mul(new Mul(var, new Number(2)), var);
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar8()
        {
            // 2x * 3x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), new Mul(new Number(3), var));
            var expected = new Mul(new Number(6), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar9()
        {
            // (x * 2) * (x * 3)
            var var = Variable.X;
            var mul = new Mul(new Mul(var, new Number(2)), new Mul(var, new Number(3)));
            var expected = new Mul(new Number(6), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar10()
        {
            // 2x * -2x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), new Mul(new Number(0.5), var));
            var expected = new Pow(var, new Number(2));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar11()
        {
            // 2x * -3x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), new Mul(new Number(-0.5), var));
            var expected = new UnaryMinus(new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulNegativeRightParamTest1()
        {
            // sin(cos(x))
            var x = Variable.X;
            var mul = new Mul(new Cos(new Cos(x)), new UnaryMinus(new Sin(x)));
            var expected = new UnaryMinus(new Mul(new Sin(x), new Cos(new Cos(x))));

            SimpleTest(mul, expected);
        }

        #endregion

        #region Div

        [Fact]
        public void DivZero()
        {
            var div = new Div(new Number(0), Variable.X);
            var expected = new Number(0);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivByZero()
        {
            var div = new Div(Variable.X, new Number(0));

            Assert.Throws<DivideByZeroException>(() => SimpleTest(div, null));
        }

        [Fact]
        public void ZeroDivByZero()
        {
            var div = new Div(new Number(0), new Number(0));
            var expected = new Number(double.NaN);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivByOne()
        {
            var div = new Div(Variable.X, new Number(1));
            var expected = Variable.X;

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivTwoNumbers()
        {
            var div = new Div(new Number(8), new Number(2));
            var expected = new Number(4);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiff_NumMulVar_DivNum()
        {
            // (2 * x) / 4
            var div = new Div(new Mul(new Number(2), Variable.X), new Number(4));
            var expected = new Div(Variable.X, new Number(2));

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiff_VarMulNum_DivNum()
        {
            // (x * 2) / 4
            var div = new Div(new Mul(Variable.X, new Number(2)), new Number(4));
            var expected = new Div(Variable.X, new Number(2));

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiffNumDiv_NumMulVar_()
        {
            // 2 / (2 * x)
            var div = new Div(new Number(2), new Mul(new Number(2), Variable.X));
            var expected = new Div(new Number(1), Variable.X);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiffNumDiv_VarMulNum_()
        {
            // 2 / (2 * x)
            var div = new Div(new Number(2), new Mul(Variable.X, new Number(2)));
            var expected = new Div(new Number(1), Variable.X);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiff_NumDivVar_DivNum()
        {
            // (2 / x) / 2
            var div = new Div(new Div(new Number(2), Variable.X), new Number(2));
            var expected = new Div(new Number(1), Variable.X);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiff_VarDivNum_DivNum()
        {
            // (x / 2) / 2
            var div = new Div(new Div(Variable.X, new Number(2)), new Number(2));
            var expected = new Div(Variable.X, new Number(4));

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiffNumDiv_NumDivVar_()
        {
            // 2 / (2 / x)
            var div = new Div(new Number(2), new Div(new Number(2), Variable.X));
            var expected = Variable.X;

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiffNumDiv_VarDivNum_()
        {
            // 2 / (x / 2)
            var div = new Div(new Number(2), new Div(Variable.X, new Number(2)));
            var expected = new Div(new Number(4), Variable.X);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivSameVars()
        {
            var x = Variable.X;
            var div = new Div(x, x);
            var expected = new Number(1);

            SimpleTest(div, expected);
        }

        #endregion

        [Fact]
        public void Define()
        {
            var define = new Define(Variable.X, new Add(new Number(2), new Number(2)));
            var expected = new Define(Variable.X, new Number(4));

            SimpleTest(define, expected);
        }

        [Fact]
        public void PowerZero()
        {
            var pow = new Pow(Variable.X, new Number(0));
            var expected = new Number(1);

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowerOne()
        {
            var pow = new Pow(Variable.X, new Number(1));
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void RootOne()
        {
            var root = new Root(Variable.X, new Number(1));
            var expected = Variable.X;

            SimpleTest(root, expected);
        }

        [Fact]
        public void Root()
        {
            var root = new Root(Variable.X, new Number(5));

            SimpleTest(root, root);
        }

        [Fact]
        public void Log()
        {
            var log = new Log(Variable.X, Variable.X);
            var expected = new Number(1);

            SimpleTest(log, expected);
        }

        [Fact]
        public void Log2()
        {
            var log = new Log(new Number(11), new Number(3));

            SimpleTest(log, log);
        }

        [Fact]
        public void Ln()
        {
            var ln = new Ln(new Variable("e"));
            var expected = new Number(1);

            SimpleTest(ln, expected);
        }

        [Fact]
        public void Ln2()
        {
            var ln = new Ln(new Variable("z"));

            SimpleTest(ln, ln);
        }

        [Fact]
        public void Lg()
        {
            var log = new Lg(new Number(10));
            var expected = new Number(1);

            SimpleTest(log, expected);
        }

        [Fact]
        public void Lg2()
        {
            var log = new Lg(new Number(101));

            SimpleTest(log, log);
        }

        [Fact]
        public void Lb()
        {
            var log = new Lb(new Number(2));
            var expected = new Number(1);

            SimpleTest(log, expected);
        }

        [Fact]
        public void Lb2()
        {
            var log = new Lb(new Number(3));

            SimpleTest(log, log);
        }

        [Fact]
        public void Simplify()
        {
            var simpl = new Simplifier();
            var simp = new Simplify(simpl, new Pow(Variable.X, new Number(0)));
            var expected = new Number(1);

            SimpleTest(simp, expected);
        }

        [Fact]
        public void Deriv()
        {
            var diff = new Differentiator();
            var simpl = new Simplifier();
            var simp = new Derivative(diff, simpl, new Add(new Number(2), new Number(3)));
            var expected = new Derivative(diff, simpl, new Number(5));

            SimpleTest(simp, expected);
        }

        #region Trigonometric

        [Fact]
        public void ArcsinSin()
        {
            var exp = new Arcsin(new Sin(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArccosCos()
        {
            var exp = new Arccos(new Cos(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArctanTan()
        {
            var exp = new Arctan(new Tan(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArccotCot()
        {
            var exp = new Arccot(new Cot(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArcsecSec()
        {
            var exp = new Arcsec(new Sec(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArccscCsc()
        {
            var exp = new Arccsc(new Csc(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void SinArcsin()
        {
            var exp = new Sin(new Arcsin(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void CosArccos()
        {
            var exp = new Cos(new Arccos(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void TanArctan()
        {
            var exp = new Tan(new Arctan(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void CotArccot()
        {
            var exp = new Cot(new Arccot(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void SecArcsec()
        {
            var exp = new Sec(new Arcsec(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void CscArccsc()
        {
            var exp = new Csc(new Arccsc(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        #endregion

        #region Hyperbolic

        [Fact]
        public void ArsinhSinh()
        {
            var exp = new Arsinh(new Sinh(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArcoshCosh()
        {
            var exp = new Arcosh(new Cosh(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArtanhTanh()
        {
            var exp = new Artanh(new Tanh(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArcothCoth()
        {
            var exp = new Arcoth(new Coth(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArsechSech()
        {
            var exp = new Arsech(new Sech(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ArcschCsch()
        {
            var exp = new Arcsch(new Csch(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void SinhArsinh()
        {
            var exp = new Sinh(new Arsinh(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void CoshArcosh()
        {
            var exp = new Cosh(new Arcosh(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void TanhArtanh()
        {
            var exp = new Tanh(new Artanh(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void CothArcoth()
        {
            var exp = new Coth(new Arcoth(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void SechArsech()
        {
            var exp = new Sech(new Arsech(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void CschArcsch()
        {
            var exp = new Csch(new Arcsch(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        #endregion

        [Fact]
        public void UserFunc()
        {
            var exp = new UserFunction("f", new IExpression[] { new Mul(new Number(2), new Number(2)) }, 1);
            var expected = new UserFunction("f", new IExpression[] { new Number(4) }, 1);

            SimpleTest(exp, expected);
        }

    }

}
