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

    // todo: exceptions!!!

    public class TypeAnalyzer : IAnalyzer<ResultType>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeAnalyzer"/> class.
        /// </summary>
        public TypeAnalyzer() { }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Abs exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number || result == ResultType.ComplexNumber || result == ResultType.Vector)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Add exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if ((leftResult == ResultType.ComplexNumber && (rightResult == ResultType.ComplexNumber || rightResult == ResultType.Number)))
                return ResultType.ComplexNumber;

            if ((rightResult == ResultType.ComplexNumber && (leftResult == ResultType.ComplexNumber || leftResult == ResultType.Number)))
                return ResultType.ComplexNumber;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            if (leftResult == ResultType.Matrix && rightResult == ResultType.Matrix)
                return ResultType.Matrix;

            if (leftResult == ResultType.Vector && rightResult == ResultType.Vector)
                return ResultType.Vector;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Ceil exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Define exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Del exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Expression)
                return ResultType.Vector;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Derivative exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Div exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Exp exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Fact exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Floor exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(GCD exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Lb exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(LCM exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Lg exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Ln exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Log exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Mod exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Mul exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Number exp)
        {
            return ResultType.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Pow exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Root exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Round exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Simplify exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sqrt exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sub exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(UnaryMinus exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Undefine exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(UserFunction exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Variable exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(DelegateExpression exp) { throw new NotSupportedException(); }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Vector exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Matrix exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Determinant exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Inverse exp) { throw new NotSupportedException(); }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Transpose exp) { throw new NotSupportedException(); }

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(ComplexNumber exp)
        {
            return ResultType.ComplexNumber;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Conjugate exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Im exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.ComplexNumber)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Phase exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.ComplexNumber)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Re exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.ComplexNumber)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Reciprocal exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arccos exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arccot exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arccsc exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arcsec exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arcsin exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arctan exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Cos exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Cot exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Csc exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sec exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sin exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Tan exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arcosh exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arcoth exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arcsch exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arsech exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arsinh exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Artanh exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Cosh exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Coth exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Csch exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sech exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sinh exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Tanh exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Avg exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Count exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Max exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Min exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Product exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Stdev exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Stdevp exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sum exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Var exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Varp exp) { throw new NotSupportedException(); }

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Expressions.LogicalAndBitwise.And exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Bool exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Equality exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Implication exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(NAnd exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(NOr exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Not exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Expressions.LogicalAndBitwise.Or exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(XOr exp) { throw new NotSupportedException(); }

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(AddAssign exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Expressions.Programming.And exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Dec exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(DivAssign exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Equal exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(For exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(GreaterOrEqual exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(GreaterThan exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(If exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Inc exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(LessOrEqual exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(LessThan exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(MulAssign exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(NotEqual exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Expressions.Programming.Or exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(SubAssign exp) { throw new NotSupportedException(); }
        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(While exp) { throw new NotSupportedException(); }

        #endregion Programming

    }

}
