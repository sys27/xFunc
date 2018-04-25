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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
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

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestAbsComplexNumber()
        {
            var exp = new Abs(new ComplexNumber(2, 2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestAbsVariable()
        {
            var exp = new Abs(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAbsVector()
        {
            var exp = new Abs(new Vector(new[] { new Number(1) }));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestAbsException()
        {
            var exp = new Abs(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestAddTwoNumberTest()
        {
            var add = new Add(new Number(1), new Number(2));

            Test(add, ResultType.Number);
        }

        [Fact]
        public void TestAddNumberVarTest()
        {
            var add = new Add(new Number(1), Variable.X);

            Test(add, ResultType.Undefined);
        }

        [Fact]
        public void TestAddComplicatedTest()
        {
            var add = new Add(new Mul(new Number(1), new Number(2)), Variable.X);

            Test(add, ResultType.Undefined);
        }

        [Fact]
        public void TestAddTwoVectorTest()
        {
            var add = new Add(new Vector(new[] { new Number(1) }),
                              new Vector(new[] { new Number(2) }));

            Test(add, ResultType.Vector);
        }

        [Fact]
        public void TestAddTwoMatrixTest()
        {
            var add = new Add(new Matrix(new[] { new Vector(new[] { new Number(1) }) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Test(add, ResultType.Matrix);
        }

        [Fact]
        public void TestAddNumberVectorTest()
        {
            var exp = new Add(new Number(1), new Vector(new[] { new Number(1) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddBoolVectorException()
        {
            var exp = new Add(new Bool(true), new Vector(new[] { new Number(1) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddVectorNumberTest()
        {
            var exp = new Add(new Vector(new[] { new Number(1) }), new Bool(true));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddNumberMatrixTest()
        {
            var exp = new Add(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddMatrixNumberTest()
        {
            var exp = new Add(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Number(1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddVectorMatrixTest()
        {
            var exp = new Add(new Vector(new[] { new Number(1) }), new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddMatrixVectorTest()
        {
            var exp = new Add(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Vector(new[] { new Number(1) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddNumberComplexNumberTest()
        {
            var add = new Add(new Number(1), new ComplexNumber(2, 1));

            Test(add, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestAddBoolComplexNumberException()
        {
            var add = new Add(new Bool(true), new ComplexNumber(2, 1));

            TestBinaryException(add);
        }

        [Fact]
        public void TestAddComplexNumberNumberTest()
        {
            var add = new Add(new ComplexNumber(1, 3), new Number(2));

            Test(add, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestAddComplexNumberBoolException()
        {
            var add = new Add(new ComplexNumber(1, 3), new Bool(true));

            TestBinaryException(add);
        }

        [Fact]
        public void TestAddComplexNumberComplexNumberTest()
        {
            var add = new Add(new ComplexNumber(1, 3), new ComplexNumber(2, 5));

            Test(add, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestAddNumberAllTest()
        {
            var exp = new Add(new Number(1), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddComplexNumberAllTest()
        {
            var exp = new Add(new ComplexNumber(3, 2), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddVectorAllTest()
        {
            var exp = new Add(new Vector(1), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddMatrixAllTest()
        {
            var exp = new Add(new Matrix(1, 1), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddNumberSqrtComplexTest()
        {
            var exp = new Add(new Number(2), new Sqrt(new Number(-9)));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddTwoVarTest()
        {
            var exp = new Add(Variable.X, Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddThreeVarTest()
        {
            var exp = new Add(new Add(Variable.X, Variable.X), Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddException()
        {
            var exp = new Add(new Bool(false), new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCeilNumber()
        {
            var exp = new Ceil(new Number(-2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCeilVariable()
        {
            var exp = new Ceil(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCeilException()
        {
            var exp = new Ceil(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestdDefineUndefine()
        {
            var exp = new Define(Variable.X, new Number(-2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestDelVector()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Del(diff, simp, new Number(2));

            Test(exp, ResultType.Vector);
        }

        [Fact]
        public void TestDerivExpression()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, Variable.X);

            Test(exp, ResultType.Expression);
        }

        [Fact]
        public void TestDerivExpressionWithVar()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, Variable.X, Variable.X);

            Test(exp, ResultType.Expression);
        }

        [Fact]
        public void TestDerivNumber()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, Variable.X, Variable.X, new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestDerivException()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, new IExpression[] { Variable.X, new Number(1) }, 2);

            TestException(exp);
        }

        [Fact]
        public void TestDivNumberNumberTest()
        {
            var exp = new Div(new Number(1), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestDivComplexNumberComplexNumberTest()
        {
            var exp = new Div(new ComplexNumber(3, 2), new ComplexNumber(2, 4));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestDivNumberComplexNumberTest()
        {
            var exp = new Div(new Number(3), new ComplexNumber(2, 4));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestDivComplexNumberNumberTest()
        {
            var exp = new Div(new ComplexNumber(3, 2), new Number(2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestDivComplexNumberBoolException()
        {
            var exp = new Div(new ComplexNumber(3, 2), new Bool(true));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestDivBoolComplexNumberException()
        {
            var exp = new Div(new Bool(true), new ComplexNumber(3, 2));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestDivNumberBoolException()
        {
            var exp = new Div(new Number(3), new Bool(true));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestDivBoolNumberException()
        {
            var exp = new Div(new Bool(true), new Number(3));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestDivNumberSqrtComplexTest()
        {
            var exp = new Div(new Sqrt(new Number(-16)), new Number(2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestDivTwoVarTest()
        {
            var exp = new Div(Variable.X, Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestDivThreeVarTest()
        {
            var exp = new Div(new Add(Variable.X, Variable.X), Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestDivException()
        {
            var exp = new Div(new Bool(false), new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestExpUndefined()
        {
            var exp = new Exp(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestExpNumber()
        {
            var exp = new Exp(new Number(10));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestExpComplexNumber()
        {
            var exp = new Exp(new ComplexNumber(10, 10));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestExpException()
        {
            var exp = new Exp(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestFactUndefined()
        {
            var exp = new Fact(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestFactNumber()
        {
            var exp = new Fact(new Number(10));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestFactException()
        {
            var exp = new Fact(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestFloorUndefined()
        {
            var exp = new Floor(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestFloorNumber()
        {
            var exp = new Floor(new Number(10));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestFloorException()
        {
            var exp = new Floor(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestGCDUndefined()
        {
            var exp = new GCD(new Number(10), Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestGCDnumber()
        {
            var exp = new GCD(new[] { new Number(10), new Number(10), new Number(10) }, 3);

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestGCDException()
        {
            var exp = new GCD(new ComplexNumber(10), new Number(10));

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestGCDEmpty()
        {
            var exp = new GCD(new IExpression[0], 0);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestLbUndefined()
        {
            var exp = new Lb(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestLbNumber()
        {
            var exp = new Lb(new Number(10));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestLbException()
        {
            var exp = new Lb(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestLCMUndefined()
        {
            var exp = new LCM(new Number(10), Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestLCMnumber()
        {
            var exp = new LCM(new[] { new Number(10), new Number(10), new Number(10) }, 3);

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestLCMEmpty()
        {
            var exp = new LCM(new IExpression[0], 0);

            Test(exp, ResultType.Undefined);
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

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestLgNumber()
        {
            var exp = new Lg(new Number(10));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestLgComplexNumber()
        {
            var exp = new Lg(new ComplexNumber(10, 10));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestLgException()
        {
            var exp = new Lg(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestLnUndefined()
        {
            var exp = new Ln(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestLnNumber()
        {
            var exp = new Ln(new Number(10));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestLnComplexNumber()
        {
            var exp = new Ln(new ComplexNumber(10, 10));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestLnException()
        {
            var exp = new Ln(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestLogUndefined()
        {
            var exp = new Log(Variable.X, new Number(2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestLogNumber()
        {
            var exp = new Log(new Number(4), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestLogComplexNumber()
        {
            var exp = new Log(new ComplexNumber(8, 3), new Number(2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestLogException()
        {
            var exp = new Log(new Bool(false), new Number(2));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestLogBaseIsNotNumber()
        {
            var exp = new Log(new Number(2), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestModUndefined()
        {
            var exp = new Mod(Variable.X, new Number(2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestModNumber()
        {
            var exp = new Mod(new Number(4), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestModNumberBoolException()
        {
            var exp = new Mod(new Number(4), new Bool(true));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestModException()
        {
            var exp = new Mod(new Bool(false), new Number(2));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestModNoNumbersException()
        {
            var exp = new Mod(new Bool(false), new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestMulTwoNumberTest()
        {
            var mul = new Mul(new Number(1), new Number(2));

            Test(mul, ResultType.Number);
        }

        [Fact]
        public void TestMulNumberVarTest()
        {
            var mul = new Mul(new Number(1), Variable.X);

            Test(mul, ResultType.Undefined);
        }

        [Fact]
        public void TestMulNumberBoolTest()
        {
            var mul = new Mul(new Number(1), new Bool(true));

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulBoolNumberTest()
        {
            var mul = new Mul(new Bool(true), new Number(1));

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulVarNumberTest()
        {
            var mul = new Mul(Variable.X, new Number(1));

            Test(mul, ResultType.Undefined);
        }

        [Fact]
        public void TestMulTwoMatrixTest()
        {
            var mul = new Mul(new Matrix(new[] { new Vector(new[] { new Number(1) }) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Test(mul, ResultType.Matrix);
        }

        [Fact]
        public void TestMulLeftMatrixRightException()
        {
            var mul = new Mul(new Matrix(new[] { new Vector(new[] { new Number(1) }) }),
                              new Bool(false));

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulRightMatrixLeftException()
        {
            var mul = new Mul(new Bool(false),
                              new Matrix(new[] { new Vector(new[] { new Number(1) }) }));

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulNumberVectorTest()
        {
            var mul = new Mul(new Number(1), new Vector(new[] { new Number(1) }));

            Test(mul, ResultType.Vector);
        }

        [Fact]
        public void TestMulNumberMatrixTest()
        {
            var mul = new Mul(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Test(mul, ResultType.Matrix);
        }

        [Fact]
        public void TestMulVectorMatrixTest()
        {
            var mul = new Mul(new Vector(new[] { new Number(1) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Test(mul, ResultType.Matrix);
        }

        [Fact]
        public void TestMulVectorNumber()
        {
            var mul = new Mul(new Vector(new[] { new Number(1) }),
                              new Number(2));

            Test(mul, ResultType.Vector);
        }

        [Fact]
        public void TestMulVectorBoolException()
        {
            var mul = new Mul(new Vector(new[] { new Number(1) }), new Bool(false));

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulBoolVectorException()
        {
            var mul = new Mul(new Bool(false), new Vector(new[] { new Number(1) }));

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulComplexNumberComplexNumberTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestMulComplexNumberNumberTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new Number(2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestMulComplexNumberBoolTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new Bool(true));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestMulNumberComplexNumberTest()
        {
            var exp = new Mul(new Number(2), new ComplexNumber(3, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestMulBoolComplexNumberTest()
        {
            var exp = new Mul(new Bool(true), new ComplexNumber(2, 5));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestMulException()
        {
            var exp = new Mul(new Bool(false), new Bool(true));

            TestException(exp);
        }

        [Fact]
        public void TestNumber()
        {
            Test(new Number(1), ResultType.Number);
        }

        [Fact]
        public void TestPowUndefined()
        {
            var exp = new Pow(Variable.X, new Number(2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestPowNumber()
        {
            var exp = new Pow(new Number(4), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestPowComplexNumber()
        {
            var exp = new Pow(new ComplexNumber(2, 4), new Number(4));

            Test(exp, ResultType.ComplexNumber);
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
            var exp = new Pow(new Bool(false), new Number(2));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestPowRightIsInvalidException()
        {
            var exp = new Pow(new ComplexNumber(2, 2), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootUndefined()
        {
            var exp = new Root(Variable.X, new Number(2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestRootNumber()
        {
            var exp = new Root(new Number(4), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestRootNumberBoolException()
        {
            var exp = new Root(new Number(2), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootException()
        {
            var exp = new Root(new Bool(false), new Number(2));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootInvalidArgsException()
        {
            var exp = new Root(new Bool(false), new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestRoundUndefined()
        {
            var exp = new Round(new Number(10), Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestRoundNumber()
        {
            var exp = new Round(new Number(10), new Number(10));

            Test(exp, ResultType.Number);
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
            Test(new Simplify(simp, Variable.X), ResultType.Undefined);
        }

        [Fact]
        public void TestSqrt()
        {
            Test(new Sqrt(Variable.X), ResultType.Undefined);
        }

        [Fact]
        public void SubTwoNumberTest()
        {
            var sub = new Sub(new Number(1), new Number(2));

            Test(sub, ResultType.Number);
        }

        [Fact]
        public void SubNumberVarTest()
        {
            var sub = new Sub(new Number(1), Variable.X);

            Test(sub, ResultType.Undefined);
        }

        [Fact]
        public void SubComplicatedTest()
        {
            var sub = new Sub(new Mul(new Number(1), new Number(2)), Variable.X);

            Test(sub, ResultType.Undefined);
        }

        [Fact]
        public void SubTwoVectorTest()
        {
            var sub = new Sub(new Vector(new[] { new Number(1) }),
                              new Vector(new[] { new Number(2) }));

            Test(sub, ResultType.Vector);
        }

        [Fact]
        public void SubTwoMatrixTest()
        {
            var sub = new Sub(new Matrix(new[] { new Vector(new[] { new Number(1) }) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Test(sub, ResultType.Matrix);
        }

        [Fact]
        public void SubNumberVectorTest()
        {
            var exp = new Sub(new Number(1), new Vector(new[] { new Number(1) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void SubVectorNumberTest()
        {
            var exp = new Sub(new Vector(new[] { new Number(1) }), new Number(1));

            TestBinaryException(exp);
        }

        [Fact]
        public void SubNumberMatrixTest()
        {
            var exp = new Sub(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void SubMatrixNumberTest()
        {
            var exp = new Sub(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Number(1));

            TestBinaryException(exp);
        }

        [Fact]
        public void SubVectorMatrixTest()
        {
            var exp = new Sub(new Vector(new[] { new Number(1) }), new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void SubMatrixVectorTest()
        {
            var exp = new Sub(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Vector(new[] { new Number(1) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void SubNumberComplexNumberTest()
        {
            var sub = new Sub(new Number(1), new ComplexNumber(2, 1));

            Test(sub, ResultType.ComplexNumber);
        }

        [Fact]
        public void SubComplexNumberNumberTest()
        {
            var sub = new Sub(new ComplexNumber(1, 3), new Number(2));

            Test(sub, ResultType.ComplexNumber);
        }

        [Fact]
        public void SubComplexNumberComplexNumberTest()
        {
            var sub = new Sub(new ComplexNumber(1, 3), new ComplexNumber(3, 5));

            Test(sub, ResultType.ComplexNumber);
        }

        [Fact]
        public void SubComplexNumberBoolException()
        {
            var sub = new Sub(new ComplexNumber(1, 3), new Bool(true));

            TestBinaryException(sub);
        }

        [Fact]
        public void SubBoolComplexNumberException()
        {
            var sub = new Sub(new Bool(true), new ComplexNumber(1, 3));

            TestBinaryException(sub);
        }

        [Fact]
        public void SubNumberAllTest()
        {
            var exp = new Sub(new Number(1), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void SubComplexNumberAllTest()
        {
            var exp = new Sub(new ComplexNumber(3, 2), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void SubVectorVectorTest()
        {
            var exp = new Sub(new Vector(1), new Vector(1));

            Test(exp, ResultType.Vector);
        }

        [Fact]
        public void SubVectorBoolTest()
        {
            var exp = new Sub(new Vector(1), new Bool(true));

            TestBinaryException(exp);
        }

        [Fact]
        public void SubBoolVectorTest()
        {
            var exp = new Sub(new Bool(true), new Vector(1));

            TestBinaryException(exp);
        }

        [Fact]
        public void SubVectorAllTest()
        {
            var exp = new Sub(new Vector(1), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void SubMatrixAllTest()
        {
            var exp = new Sub(new Matrix(1, 1), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void SubNumberComplexTest()
        {
            var exp = new Sub(new Number(2), new Sqrt(new Number(-9)));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void SubTwoVarTest()
        {
            var exp = new Sub(Variable.X, Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void SubThreeVarTest()
        {
            var exp = new Sub(new Add(Variable.X, Variable.X), Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestSubBoolsException()
        {
            var exp = new Sub(new Bool(false), new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestUnaryMinusUndefined()
        {
            var exp = new UnaryMinus(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestUnaryMinusNumber()
        {
            var exp = new UnaryMinus(new Number(10));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestUnaryMinusComplexNumber()
        {
            var exp = new UnaryMinus(new ComplexNumber(10, 10));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestUnaryMinusException()
        {
            var exp = new UnaryMinus(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestUndefine()
        {
            Test(new Undefine(Variable.X), ResultType.Undefined);
        }

        [Fact]
        public void TestUserFunction()
        {
            Test(new UserFunction("f", 0), ResultType.Undefined);
        }

        [Fact]
        public void TestVariable()
        {
            Test(new Sqrt(Variable.X), ResultType.Undefined);
        }

        [Fact]
        public void TestDeletageExpression()
        {
            Test(new DelegateExpression(x => null), ResultType.Undefined);
        }

    }
}
