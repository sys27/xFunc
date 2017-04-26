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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
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

    }

}
