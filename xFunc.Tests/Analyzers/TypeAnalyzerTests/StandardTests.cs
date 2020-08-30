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

using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class StandardTests : TypeAnalyzerBaseTests
    {
        [Fact]
        public void TestAbsNumber()
        {
            var exp = new Abs(new Number(-2));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestAbsAngleNumber()
        {
            var exp = new Abs(Angle.Degree(1).AsExpression());

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestAbsComplexNumber()
        {
            var exp = new Abs(new ComplexNumber(2, 2));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestAbsVariable()
        {
            var exp = new Abs(Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAbsVector()
        {
            var exp = new Abs(new Vector(new IExpression[] { Number.One }));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestAbsException()
        {
            var exp = new Abs(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestCeilNumber()
        {
            var exp = new Ceil(new Number(-2));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestCeilAngle()
        {
            var exp = new Ceil(Angle.Degree(5.5).AsExpression());

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestCeilVariable()
        {
            var exp = new Ceil(Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestCeilException()
        {
            var exp = new Ceil(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestDefineUndefined()
        {
            var exp = new Define(Variable.X, new Number(-2));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestDelVector()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Del(diff, simp, Number.Two);

            Test(exp, ResultTypes.Vector);
        }

        [Fact]
        public void TestDerivExpression()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, Variable.X);

            Test(exp, ResultTypes.Expression);
        }

        [Fact]
        public void TestDerivExpressionWithVar()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, Variable.X, Variable.X);

            Test(exp, ResultTypes.Expression);
        }

        [Fact]
        public void TestDerivNumber()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, Variable.X, Variable.X, Number.Two);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestExpUndefined()
        {
            var exp = new Exp(Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestExpNumber()
        {
            var exp = new Exp(new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestExpComplexNumber()
        {
            var exp = new Exp(new ComplexNumber(10, 10));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestExpException()
        {
            var exp = new Exp(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestFactUndefined()
        {
            var exp = new Fact(Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestFactNumber()
        {
            var exp = new Fact(new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestFactException()
        {
            var exp = new Fact(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestFloorUndefined()
        {
            var exp = new Floor(Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestFloorNumber()
        {
            var exp = new Floor(new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestFloorAngle()
        {
            var exp = new Floor(Angle.Degree(5.5).AsExpression());

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestFloorException()
        {
            var exp = new Floor(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestTruncUndefined()
        {
            var exp = new Trunc(Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestTruncNumber()
        {
            var exp = new Trunc(new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestTruncAngle()
        {
            var exp = new Trunc(Angle.Degree(5.5).AsExpression());

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestTruncException()
        {
            var exp = new Trunc(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestFracUndefined()
        {
            var exp = new Frac(Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestFracNumber()
        {
            var exp = new Frac(new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestFracAngle()
        {
            var exp = new Frac(Angle.Degree(5.5).AsExpression());

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestFracException()
        {
            var exp = new Frac(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestGCDUndefined()
        {
            var exp = new GCD(new Number(10), Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestGCDNumber()
        {
            var exp = new GCD(new IExpression[]
            {
                new Number(10), new Number(10), new Number(10)
            });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestGCDException()
        {
            var exp = new GCD(new ComplexNumber(10), new Number(10));

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestLbUndefined()
        {
            var exp = new Lb(Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestLbNumber()
        {
            var exp = new Lb(new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestLbException()
        {
            var exp = new Lb(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestLCMUndefined()
        {
            var exp = new LCM(new Number(10), Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestLCMNumber()
        {
            var exp = new LCM(new IExpression[]
            {
                new Number(10), new Number(10), new Number(10)
            });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestLCMException()
        {
            var exp = new LCM(new ComplexNumber(10), new Number(10));

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestLgUndefined()
        {
            var exp = new Lg(Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestLgNumber()
        {
            var exp = new Lg(new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestLgComplexNumber()
        {
            var exp = new Lg(new ComplexNumber(10, 10));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestLgException()
        {
            var exp = new Lg(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestLnUndefined()
        {
            var exp = new Ln(Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestLnNumber()
        {
            var exp = new Ln(new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestLnComplexNumber()
        {
            var exp = new Ln(new ComplexNumber(10, 10));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestLnException()
        {
            var exp = new Ln(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestLogUndefined()
        {
            var exp = new Log(Number.Two, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestLogNumber()
        {
            var exp = new Log(Number.Two, new Number(4));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestLogComplexNumber()
        {
            var exp = new Log(Number.Two, new ComplexNumber(8, 3));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestLogException()
        {
            var exp = new Log(Number.Two, Bool.False);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestLogBaseIsNotNumber()
        {
            var exp = new Log(Bool.False, Number.Two);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestModUndefined()
        {
            var exp = new Mod(Variable.X, Number.Two);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestModNumber()
        {
            var exp = new Mod(new Number(4), Number.Two);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestModNumberBoolException()
        {
            var exp = new Mod(new Number(4), Bool.True);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestModException()
        {
            var exp = new Mod(Bool.False, Number.Two);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestModNoNumbersException()
        {
            var exp = new Mod(Bool.False, Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestNumber()
        {
            Test(Number.One, ResultTypes.Number);
        }

        [Fact]
        public void TestPowUndefined()
        {
            var exp = new Pow(Variable.X, Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestPowNumber()
        {
            var exp = new Pow(new Number(4), Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestPowComplexNumber()
        {
            var exp = new Pow(new ComplexNumber(2, 4), new Number(4));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestPowNumberComplexNumber()
        {
            var exp = new Pow(new Number(4), new ComplexNumber(2, 4));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestPowException()
        {
            var exp = new Pow(Bool.False, Number.Two);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestPowRightIsInvalidException()
        {
            var exp = new Pow(new ComplexNumber(2, 2), Bool.False);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootUndefined()
        {
            var exp = new Root(Variable.X, Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestRootNumber()
        {
            var exp = new Root(new Number(4), Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestRootNumberBoolException()
        {
            var exp = new Root(Number.Two, Bool.False);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootException()
        {
            var exp = new Root(Bool.False, Number.Two);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootInvalidArgsException()
        {
            var exp = new Root(Bool.False, Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestRoundVariable()
        {
            var exp = new Round(new Number(10), Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestRoundNumber()
        {
            var exp = new Round(new Number(10), new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestRoundException()
        {
            var exp = new Round(new ComplexNumber(10), new Number(10));

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestSimplify()
        {
            var simp = new Simplifier();
            Test(new Simplify(simp, Variable.X), ResultTypes.Undefined);
        }

        [Fact]
        public void TestSqrt()
        {
            Test(new Sqrt(Variable.X), ResultTypes.Undefined);
        }

        [Fact]
        public void TestUnaryMinusUndefined()
        {
            var exp = new UnaryMinus(Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestUnaryMinusNumber()
        {
            var exp = new UnaryMinus(new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestUnaryMinusAngleNumber()
        {
            var exp = new UnaryMinus(Angle.Degree(10).AsExpression());

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestUnaryMinusComplexNumber()
        {
            var exp = new UnaryMinus(new ComplexNumber(10, 10));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestUnaryMinusException()
        {
            var exp = new UnaryMinus(Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestUndefine()
        {
            Test(new Undefine(Variable.X), ResultTypes.Undefined);
        }

        [Fact]
        public void TestUserFunction()
        {
            Test(new UserFunction("f", new IExpression[0]), ResultTypes.Undefined);
        }

        [Fact]
        public void TestVariable()
        {
            Test(new Sqrt(Variable.X), ResultTypes.Undefined);
        }

        [Fact]
        public void TestDeletageExpression()
        {
            Test(new DelegateExpression(x => null), ResultTypes.Undefined);
        }

        [Fact]
        public void TestSignUndefined()
        {
            Test(new Sign(Variable.X), ResultTypes.Undefined);
        }

        [Fact]
        public void TestSignNumber()
        {
            Test(new Sign(new Number(-5)), ResultTypes.Number);
        }

        [Fact]
        public void TestSignAngle()
        {
            Test(new Sign(Angle.Degree(10).AsExpression()), ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestSignException()
        {
            TestException(new Sign(Bool.False));
        }

        [Fact]
        public void TestAngleNumber()
        {
            Test(Angle.Degree(10).AsExpression(), ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestToDegreeNumber()
        {
            Test(new ToDegree(new Number(10)), ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestToDegreeAngle()
        {
            Test(new ToDegree(Angle.Radian(10).AsExpression()), ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestToDegreeException()
        {
            TestException(new ToDegree(Bool.True));
        }

        [Fact]
        public void TestToRadianNumber()
        {
            Test(new ToRadian(new Number(10)), ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestToRadianAngle()
        {
            Test(new ToRadian(Angle.Degree(10).AsExpression()), ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestToRadianException()
        {
            TestException(new ToRadian(Bool.True));
        }

        [Fact]
        public void TestToGradianNumber()
        {
            Test(new ToGradian(new Number(10)), ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestToGradianAngle()
        {
            Test(new ToGradian(Angle.Radian(10).AsExpression()), ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestToGradianException()
        {
            TestException(new ToGradian(Bool.True));
        }

        [Fact]
        public void TestToNumber()
        {
            Test(new ToNumber(Angle.Degree(10).AsExpression()), ResultTypes.Number);
        }

        [Fact]
        public void TestToNumberException()
        {
            TestException(new ToNumber(Bool.True));
        }
    }
}