// Copyright 2012-2017 Dmitry Kischenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Analyzers
{

    public class TypeAnalyzerTest
    {

        private IAnalyzer<ResultType> analyzer;

        public TypeAnalyzerTest()
        {
            analyzer = new TypeAnalyzer();
        }

        private void Test(IExpression exp, ResultType expected)
        {
            var simple = exp.Analyze(analyzer);

            Assert.Equal(expected, simple);
        }

        private void TestException(IExpression exp)
        {
            Assert.Throws<ParameterTypeMismatchException>(() => exp.Analyze(analyzer));
        }

        #region Standard

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
            var exp = new Abs(new Variable("x"));

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
            var add = new Add(new Number(1), new Variable("x"));

            Test(add, ResultType.Undefined);
        }

        [Fact]
        public void TestAddComplicatedTest()
        {
            var add = new Add(new Mul(new Number(1), new Number(2)), new Variable("x"));

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
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Number(1), new Vector(new[] { new Number(1) })));
        }

        [Fact]
        public void TestAddVectorNumberTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Vector(new[] { new Number(1) }), new Number(1)));
        }

        [Fact]
        public void TestAddNumberMatrixTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) })));
        }

        [Fact]
        public void TestAddMatrixNumberTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Number(1)));
        }

        [Fact]
        public void TestAddVectorMatrixTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Vector(new[] { new Number(1) }),
                                                                        new Matrix(new[] { new Vector(new[] { new Number(2) }) })));
        }

        [Fact]
        public void TestAddMatrixVectorTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Matrix(new[] { new Vector(new[] { new Number(2) }) }),
                                                                        new Vector(new[] { new Number(1) })));
        }

        [Fact]
        public void TestAddNumberComplexNumberTest()
        {
            var add = new Add(new Number(1), new ComplexNumber(2, 1));

            Test(add, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestAddComplexNumberNumberTest()
        {
            var add = new Add(new ComplexNumber(1, 3), new Number(2));

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
        public void TestAddNumberComplexTest()
        {
            var exp = new Add(new Number(2), new Sqrt(new Number(-9)));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddTwoVarTest()
        {
            var exp = new Add(new Variable("x"), new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddThreeVarTest()
        {
            var exp = new Add(new Add(new Variable("x"), new Variable("x")), new Variable("x"));

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
            var exp = new Ceil(new Variable("x"));

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
            var exp = new Define(new Variable("x"), new Number(-2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestDelVector()
        {
            var exp = new Del(new Number(2));

            Test(exp, ResultType.Vector);
        }

        [Fact]
        public void TestDerivExpression()
        {
            var exp = new Derivative(new Variable("x"));

            Test(exp, ResultType.Expression);
        }

        [Fact]
        public void TestDerivExpressionWithVar()
        {
            var exp = new Derivative(new Variable("x"), new Variable("x"));

            Test(exp, ResultType.Expression);
        }

        [Fact]
        public void TestDerivNumber()
        {
            var exp = new Derivative(new Variable("x"), new Variable("x"), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestDerivException()
        {
            var exp = new Derivative(new IExpression[] { new Variable("x"), new Number(1) }, 2);

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
        public void TestDivNumberComplexTest()
        {
            var exp = new Div(new Sqrt(new Number(-16)), new Number(2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestDivTwoVarTest()
        {
            var exp = new Div(new Variable("x"), new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestDivThreeVarTest()
        {
            var exp = new Div(new Add(new Variable("x"), new Variable("x")), new Variable("x"));

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
            var exp = new Exp(new Variable("x"));

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
            var exp = new Fact(new Variable("x"));

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
            var exp = new Floor(new Variable("x"));

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
            var exp = new GCD(new Number(10), new Variable("x"));

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

            TestException(exp);
        }

        [Fact]
        public void TestLbUndefined()
        {
            var exp = new Lb(new Variable("x"));

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
            var exp = new LCM(new Number(10), new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestLCMnumber()
        {
            var exp = new LCM(new[] { new Number(10), new Number(10), new Number(10) }, 3);

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestLCMException()
        {
            var exp = new LCM(new ComplexNumber(10), new Number(10));

            TestException(exp);
        }

        [Fact]
        public void TestLgUndefined()
        {
            var exp = new Lg(new Variable("x"));

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
            var exp = new Ln(new Variable("x"));

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
            var exp = new Log(new Variable("x"), new Number(2));

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
            var exp = new Log(new ComplexNumber(8, 4), new ComplexNumber(2, 4));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestLogException()
        {
            var exp = new Log(new Bool(false), new Number(2));

            TestException(exp);
        }

        [Fact]
        public void TestModUndefined()
        {
            var exp = new Mod(new Variable("x"), new Number(2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestModNumber()
        {
            var exp = new Mod(new Number(4), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestModException()
        {
            var exp = new Mod(new Bool(false), new Number(2));

            TestException(exp);
        }

        // todo: mul?

        [Fact]
        public void TestNumber()
        {
            Test(new Number(1), ResultType.Number);
        }

        [Fact]
        public void TestPowUndefined()
        {
            var exp = new Pow(new Variable("x"), new Number(2));

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
            var exp = new Pow(new Number(4), new ComplexNumber(2, 4));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestPowException()
        {
            var exp = new Pow(new Bool(false), new Number(2));

            TestException(exp);
        }

        [Fact]
        public void TestRootUndefined()
        {
            var exp = new Root(new Variable("x"), new Number(2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestRootNumber()
        {
            var exp = new Root(new Number(4), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestRootException()
        {
            var exp = new Root(new Bool(false), new Number(2));

            TestException(exp);
        }

        [Fact]
        public void TestRoundUndefined()
        {
            var exp = new Round(new Number(10), new Variable("x"));

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

            TestException(exp);
        }

        [Fact]
        public void TestSimplify()
        {
            Test(new Simplify(new Variable("x")), ResultType.Undefined);
        }

        [Fact]
        public void TestSqrt()
        {
            Test(new Sqrt(new Variable("x")), ResultType.Undefined);
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
            var sub = new Sub(new Number(1), new Variable("x"));

            Test(sub, ResultType.Undefined);
        }

        [Fact]
        public void SubComplicatedTest()
        {
            var sub = new Sub(new Mul(new Number(1), new Number(2)), new Variable("x"));

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

            TestException(exp);
        }

        [Fact]
        public void SubVectorNumberTest()
        {
            var exp = new Sub(new Vector(new[] { new Number(1) }), new Number(1));

            TestException(exp);
        }

        [Fact]
        public void SubNumberMatrixTest()
        {
            var exp = new Sub(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            TestException(exp);
        }

        [Fact]
        public void SubMatrixNumberTest()
        {
            var exp = new Sub(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Number(1));

            TestException(exp);
        }

        [Fact]
        public void SubVectorMatrixTest()
        {
            var exp = new Sub(new Vector(new[] { new Number(1) }), new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            TestException(exp);
        }

        [Fact]
        public void SubMatrixVectorTest()
        {
            var exp = new Sub(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Vector(new[] { new Number(1) }));

            TestException(exp);
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
            var exp = new Sub(new Variable("x"), new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void SubThreeVarTest()
        {
            var exp = new Sub(new Add(new Variable("x"), new Variable("x")), new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestUnaryMinusUndefined()
        {
            var exp = new UnaryMinus(new Variable("x"));

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
            Test(new Undefine(new Variable("x")), ResultType.Undefined);
        }

        [Fact]
        public void TestUserFunction()
        {
            Test(new UserFunction("f", 0), ResultType.Undefined);
        }

        [Fact]
        public void TestVariable()
        {
            Test(new Sqrt(new Variable("x")), ResultType.Undefined);
        }

        [Fact]
        public void TestDeletageExpression()
        {
            Test(new DelegateExpression(x => null), ResultType.Undefined);
        }

        #endregion Standard

        #region Matrix

        [Fact]
        public void TestVectorUndefined()
        {
            var exp = new Vector(new IExpression[] { new Number(10), new Variable("x") });

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestVectorNumber()
        {
            var exp = new Vector(new IExpression[] { new Number(10), new Number(10), new Number(10) });

            Test(exp, ResultType.Vector);
        }

        [Fact]
        public void TestVectorException()
        {
            var exp = new Vector(new IExpression[] { new ComplexNumber(10), new Number(10) });

            TestException(exp);
        }

        [Fact]
        public void TestMatrixVector()
        {
            var exp = new Matrix(new Vector[] { new Vector(2), new Vector(2) });

            Test(exp, ResultType.Vector);
        }

        [Fact]
        public void TestDetermenantUndefined()
        {
            var exp = new Determinant(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestDetermenantMatrix()
        {
            var exp = new Determinant(new Matrix(2, 2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestDetermenantException()
        {
            var exp = new Determinant(new ComplexNumber(2, 2));

            TestException(exp);
        }

        [Fact]
        public void TestInverseUndefined()
        {
            var exp = new Inverse(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestInverseMatrix()
        {
            var exp = new Inverse(new Matrix(2, 2));

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestInverseException()
        {
            var exp = new Inverse(new ComplexNumber(2, 2));

            TestException(exp);
        }

        [Fact]
        public void TestTransposeUndefined()
        {
            var exp = new Transpose(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestTransposeVector()
        {
            var exp = new Transpose(new Vector(2));

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestTransposeMatrix()
        {
            var exp = new Transpose(new Matrix(2, 2));

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestTransposeException()
        {
            var exp = new Transpose(new ComplexNumber(2, 2));

            TestException(exp);
        }

        #endregion Matrix

        #region Complex Numbers

        [Fact]
        public void TestComplexNumber()
        {
            var exp = new ComplexNumber(2, 2);

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestConjugateUndefined()
        {
            var exp = new Conjugate(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestConjugateComplexNumber()
        {
            var exp = new Conjugate(new ComplexNumber(2, 3));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestConjugateException()
        {
            var exp = new Conjugate(new Number(2));

            TestException(exp);
        }

        [Fact]
        public void TestImUndefined()
        {
            var exp = new Im(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestImComplexNumber()
        {
            var exp = new Im(new ComplexNumber(2, 3));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestImException()
        {
            var exp = new Im(new Number(2));

            TestException(exp);
        }

        [Fact]
        public void TestPhaseUndefined()
        {
            var exp = new Phase(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestPhaseComplexNumber()
        {
            var exp = new Phase(new ComplexNumber(2, 3));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestPhaseException()
        {
            var exp = new Phase(new Number(2));

            TestException(exp);
        }

        [Fact]
        public void TestReUndefined()
        {
            var exp = new Re(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestReComplexNumber()
        {
            var exp = new Re(new ComplexNumber(2, 3));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestReException()
        {
            var exp = new Re(new Number(2));

            TestException(exp);
        }

        [Fact]
        public void TestReciprocalUndefined()
        {
            var exp = new Reciprocal(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestReciprocalComplexNumber()
        {
            var exp = new Conjugate(new ComplexNumber(2, 3));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestReciprocalException()
        {
            var exp = new Conjugate(new Number(2));

            TestException(exp);
        }

        #endregion Complex Numbers

        #region Trigonometric

        [Fact]
        public void TestArccosUndefined()
        {
            var exp = new Arccos(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArccosNumber()
        {
            var exp = new Arccos(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArccosComplexNumber()
        {
            var exp = new Arccos(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArccosException()
        {
            var exp = new Arccos(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArccotUndefined()
        {
            var exp = new Arccot(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArccotNumber()
        {
            var exp = new Arccot(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArccotComplexNumber()
        {
            var exp = new Arccot(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArccotException()
        {
            var exp = new Arccot(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArccscUndefined()
        {
            var exp = new Arccsc(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArccscNumber()
        {
            var exp = new Arccsc(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArccscComplexNumber()
        {
            var exp = new Arccsc(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArccscException()
        {
            var exp = new Arccsc(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArcsecUndefined()
        {
            var exp = new Arcsec(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcsecNumber()
        {
            var exp = new Arcsec(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcsecComplexNumber()
        {
            var exp = new Arcsec(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcsecException()
        {
            var exp = new Arcsec(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArcsinUndefined()
        {
            var exp = new Arcsin(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcsinNumber()
        {
            var exp = new Arcsin(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcsinComplexNumber()
        {
            var exp = new Arcsin(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcsinException()
        {
            var exp = new Arcsin(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArctanUndefined()
        {
            var exp = new Arctan(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArctanNumber()
        {
            var exp = new Arctan(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArctanComplexNumber()
        {
            var exp = new Arctan(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArctanException()
        {
            var exp = new Arctan(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCosUndefined()
        {
            var exp = new Cos(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCosNumber()
        {
            var exp = new Cos(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCosComplexNumber()
        {
            var exp = new Cos(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCosException()
        {
            var exp = new Cos(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCotUndefined()
        {
            var exp = new Cot(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCotNumber()
        {
            var exp = new Cot(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCotComplexNumber()
        {
            var exp = new Cot(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCotException()
        {
            var exp = new Cot(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCscUndefined()
        {
            var exp = new Csc(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCscNumber()
        {
            var exp = new Csc(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCscComplexNumber()
        {
            var exp = new Csc(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCscException()
        {
            var exp = new Csc(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestSecUndefined()
        {
            var exp = new Sec(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestSecNumber()
        {
            var exp = new Sec(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestSecComplexNumber()
        {
            var exp = new Sec(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestSecException()
        {
            var exp = new Sec(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestSinUndefined()
        {
            var exp = new Sin(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestSinNumber()
        {
            var exp = new Sin(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestSinComplexNumber()
        {
            var exp = new Sin(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestSinException()
        {
            var exp = new Sin(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestTanUndefined()
        {
            var exp = new Tan(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestTanNumber()
        {
            var exp = new Tan(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestTanComplexNumber()
        {
            var exp = new Tan(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestTanException()
        {
            var exp = new Tan(new Bool(false));

            TestException(exp);
        }

        #endregion Trigonometric

        #region Hyperbolic

        [Fact]
        public void TestArcoshUndefined()
        {
            var exp = new Arcosh(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcoshNumber()
        {
            var exp = new Arcosh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcoshComplexNumber()
        {
            var exp = new Arcosh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcoshException()
        {
            var exp = new Arcosh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArcothUndefined()
        {
            var exp = new Arcoth(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcothNumber()
        {
            var exp = new Arcoth(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcothComplexNumber()
        {
            var exp = new Arcoth(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcothException()
        {
            var exp = new Arcoth(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArcschUndefined()
        {
            var exp = new Arcsch(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcschNumber()
        {
            var exp = new Arcsch(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcschComplexNumber()
        {
            var exp = new Arcsch(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcschException()
        {
            var exp = new Arcsch(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArsechUndefined()
        {
            var exp = new Arsech(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArsechNumber()
        {
            var exp = new Arsech(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArsechComplexNumber()
        {
            var exp = new Arsech(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArsechException()
        {
            var exp = new Arsech(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArsinhUndefined()
        {
            var exp = new Arsinh(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArsinhNumber()
        {
            var exp = new Arsinh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArsinhComplexNumber()
        {
            var exp = new Arsinh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArsinhException()
        {
            var exp = new Arsinh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArtanhUndefined()
        {
            var exp = new Artanh(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArtanhNumber()
        {
            var exp = new Artanh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArtanhComplexNumber()
        {
            var exp = new Artanh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArtanhException()
        {
            var exp = new Artanh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCoshUndefined()
        {
            var exp = new Cosh(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCoshNumber()
        {
            var exp = new Cosh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCoshComplexNumber()
        {
            var exp = new Cosh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCoshException()
        {
            var exp = new Cosh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCothUndefined()
        {
            var exp = new Coth(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCothNumber()
        {
            var exp = new Coth(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCothComplexNumber()
        {
            var exp = new Coth(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCothException()
        {
            var exp = new Coth(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCschUndefined()
        {
            var exp = new Csch(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCschNumber()
        {
            var exp = new Csch(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCschComplexNumber()
        {
            var exp = new Csch(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCschException()
        {
            var exp = new Csch(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestSechUndefined()
        {
            var exp = new Sech(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestSechNumber()
        {
            var exp = new Sech(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestSechComplexNumber()
        {
            var exp = new Sech(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestSechException()
        {
            var exp = new Sech(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestSinhUndefined()
        {
            var exp = new Sinh(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestSinhNumber()
        {
            var exp = new Sinh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestSinhComplexNumber()
        {
            var exp = new Sinh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestSinhException()
        {
            var exp = new Sinh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestTanhUndefined()
        {
            var exp = new Tanh(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestTanhNumber()
        {
            var exp = new Tanh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestTanhComplexNumber()
        {
            var exp = new Tanh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestTanhException()
        {
            var exp = new Tanh(new Bool(false));

            TestException(exp);
        }

        #endregion Hyperbolic

        #region Statistical
        #endregion Statistical

    }

}
