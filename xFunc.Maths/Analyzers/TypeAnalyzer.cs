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
using System.Linq;
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
    // todo: remove Flag and ResultType.All
    // todo: optimize

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
            return ResultType.Undefined;
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

            return ResultType.Vector;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Derivative exp)
        {
            if ((exp.ParametersCount >= 2 && exp.Arguments[1] is Variable) ||
                (exp.ParametersCount >= 3 && exp.Arguments[2] is Number))
                return exp.ParametersCount == 3 ? ResultType.Number : ResultType.Expression;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Div exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.ComplexNumber || rightResult == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

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
        public virtual ResultType Analyze(GCD exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this));
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

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
        public virtual ResultType Analyze(LCM exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this));
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

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
        public virtual ResultType Analyze(Log exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            if (leftResult == ResultType.Number && rightResult == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Mod exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Mul exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            // todo: !!!

            throw new ParameterTypeMismatchException();
        }

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
        public virtual ResultType Analyze(Pow exp)
        {
            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Root exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number || rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Round exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Simplify exp)
        {
            return ResultType.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sqrt exp)
        {
            return ResultType.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sub exp)
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
        public virtual ResultType Analyze(UnaryMinus exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            if (result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Undefine exp)
        {
            return ResultType.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(UserFunction exp)
        {
            return ResultType.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Variable exp)
        {
            return ResultType.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(DelegateExpression exp)
        {
            return ResultType.Undefined;
        }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Vector exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this));
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Vector;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Matrix exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this));
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Matrix;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Determinant exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Matrix)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Inverse exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Matrix)
                return ResultType.Matrix;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Transpose exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Vector || result == ResultType.Matrix)
                return ResultType.Matrix;

            throw new ParameterTypeMismatchException();
        }

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
        public virtual ResultType Analyze(Avg exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Count exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Max exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Min exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Product exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Stdev exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Stdevp exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sum exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Var exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Varp exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1 && (results[0] == ResultType.Number || results[0] == ResultType.Vector))
                return ResultType.Number;

            if (results.All(x => x == ResultType.Number))
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Expressions.LogicalAndBitwise.And exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number || rightResult == ResultType.Number)
                return ResultType.Number;

            if (leftResult == ResultType.Boolean || rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Bool exp)
        {
            return ResultType.Boolean;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Equality exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Boolean || rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Implication exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Boolean || rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(NAnd exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Boolean || rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(NOr exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Boolean || rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Not exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Expressions.LogicalAndBitwise.Or exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number || rightResult == ResultType.Number)
                return ResultType.Number;

            if (leftResult == ResultType.Boolean || rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(XOr exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number || rightResult == ResultType.Number)
                return ResultType.Number;

            if (leftResult == ResultType.Boolean || rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(AddAssign exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Expressions.Programming.And exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Boolean || rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Dec exp)
        {
            return ResultType.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(DivAssign exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Equal exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            if (leftResult == ResultType.Boolean && rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(For exp)
        {
            var conditionResult = exp.Condition.Analyze(this);
            if (conditionResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (conditionResult == ResultType.Boolean)
                return ResultType.Undefined;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(GreaterOrEqual exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(GreaterThan exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(If exp)
        {
            var conditionResult = exp.Condition.Analyze(this);
            if (conditionResult == ResultType.Undefined)
                return ResultType.Undefined;

            var thenResult = exp.Then.Analyze(this);
            if (conditionResult == ResultType.Boolean)
                return exp.ParametersCount == 2 ? thenResult : ResultType.Undefined;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Inc exp)
        {
            return ResultType.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(LessOrEqual exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(LessThan exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(MulAssign exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(NotEqual exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Number;

            if (leftResult == ResultType.Boolean && rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Expressions.Programming.Or exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (leftResult == ResultType.Boolean || rightResult == ResultType.Boolean)
                return ResultType.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(SubAssign exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (rightResult == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(While exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            if (rightResult == ResultType.Boolean)
                return ResultType.Undefined;

            throw new ParameterTypeMismatchException();
        }

        #endregion Programming

    }

}
