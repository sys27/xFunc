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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths.Analyzers
{

    public class TypeAnalyzer : IAnalyzer<ResultType>
    {

        public TypeAnalyzer() { }

        private ResultType CheckUnary(UnaryExpression exp, ResultType expected)
        {
            var actualType = exp.Argument.Analyze(this);
            if (actualType == ResultType.None)
                return ResultType.Undefined;

            if ((expected & actualType) == ResultType.None)
                throw new ParameterTypeMismatchException(expected, actualType);

            return actualType;
        }

        private ResultType CheckNumberUnary(UnaryExpression exp, ResultType expected = ResultType.Number, ResultType resultType = ResultType.Number)
        {
            CheckUnary(exp, expected);

            return resultType;
        }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Abs exp)
        {
            return CheckNumberUnary(exp, ResultType.Number | ResultType.ComplexNumber | ResultType.Vector, ResultType.Number);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Add exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Ceil exp)
        {
            return CheckNumberUnary(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Define exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Del exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Derivative exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Div exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Exp exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Fact exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Floor exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(GCD exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Lb exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(LCM exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Lg exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Ln exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Log exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Mod exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Mul exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Number exp)
        {
            return ResultType.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Pow exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Root exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Round exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Simplify exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Sqrt exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Sub exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(UnaryMinus exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Undefine exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(UserFunction exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Variable exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(DelegateExpression exp) { throw new NotSupportedException(); }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Vector exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Matrix exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Determinant exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Inverse exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Transpose exp) { throw new NotSupportedException(); }

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(ComplexNumber exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Conjugate exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Im exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Phase exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Re exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Reciprocal exp) { throw new NotSupportedException(); }

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arccos exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arccot exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arccsc exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arcsec exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arcsin exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arctan exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Cos exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Cot exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Csc exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Sec exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Sin exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Tan exp) { throw new NotSupportedException(); }

        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arcosh exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arcoth exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arcsch exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arsech exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Arsinh exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Artanh exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Cosh exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Coth exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Csch exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Sech exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Sinh exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Tanh exp) { throw new NotSupportedException(); }

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Avg exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Count exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Max exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Min exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Product exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Stdev exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Stdevp exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Sum exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Var exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Varp exp) { throw new NotSupportedException(); }

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Expressions.LogicalAndBitwise.And exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Bool exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Equality exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Implication exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(NAnd exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(NOr exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Not exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Expressions.LogicalAndBitwise.Or exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(XOr exp) { throw new NotSupportedException(); }

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(AddAssign exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Expressions.Programming.And exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Dec exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(DivAssign exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Equal exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(For exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(GreaterOrEqual exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(GreaterThan exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(If exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Inc exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(LessOrEqual exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(LessThan exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(MulAssign exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(NotEqual exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(Expressions.Programming.Or exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(SubAssign exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultType Analyze(While exp) { throw new NotSupportedException(); }

        #endregion Programming

    }

}
