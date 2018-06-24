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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths.Analyzers.TypeAnalyzers
{

    /// <summary>
    /// Type Analyzer checks the expression tree for argument type and result type. If result type is Undefined, then Type Analyzer cannot determine the right type and bypass current expression.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Analyzers.IAnalyzer{ResultType}" />
    public class TypeAnalyzer : ITypeAnalyzer
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeAnalyzer"/> class.
        /// </summary>
        public TypeAnalyzer() { }

        private ResultType CheckTrigonometric(UnaryExpression exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined)
                return ResultType.Undefined;

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, result);
        }

        private ResultType CheckStatistical(DifferentParametersExpression exp)
        {
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Contains(ResultType.Undefined))
                return ResultType.Undefined;

            if (results.Count == 1)
            {
                if (results[0] == ResultType.Number || results[0] == ResultType.Vector)
                    return ResultType.Number;

                throw new DifferentParameterTypeMismatchException(ResultType.Number | ResultType.Vector, results[0], 0);
            }

            for (var i = 0; i < results.Count; i++)
                if (results[i] != ResultType.Number)
                    throw new DifferentParameterTypeMismatchException(ResultType.Number, results[i], i);

            return ResultType.Number;
        }

        /// <summary>
        /// Analyzes the specified expression. This method should be only used for expessions which are not supported by xFunc (custom expression create by extendening library).
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual ResultType Analyze(IExpression exp)
        {
            return ResultType.None;
        }

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

            throw new ParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber | ResultType.Vector, result);
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

            if (leftResult == ResultType.ComplexNumber)
            {
                if (rightResult == ResultType.ComplexNumber || rightResult == ResultType.Number)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.ComplexNumber)
            {
                if (leftResult == ResultType.Number)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, rightResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Matrix)
            {
                if (rightResult == ResultType.Matrix)
                    return ResultType.Matrix;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Matrix)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Vector)
            {
                if (rightResult == ResultType.Vector)
                    return ResultType.Vector;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Vector)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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
            if (result == ResultType.Undefined || result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException(ResultType.Number, result);
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
            return ResultType.Vector;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Derivative exp)
        {
            if (exp.ParametersCount == 1)
                return ResultType.Expression;

            if (exp.ParametersCount == 2 && exp.Arguments[1] is Variable)
                return ResultType.Expression;

            if (exp.ParametersCount == 3 && exp.Arguments[1] is Variable && exp.Arguments[2] is Number)
                return ResultType.Number;

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

            if (leftResult == ResultType.ComplexNumber)
            {
                if (rightResult == ResultType.Number || rightResult == ResultType.ComplexNumber)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.ComplexNumber)
            {
                if (leftResult == ResultType.Number)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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

            throw new ParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Fact exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException(ResultType.Number, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Floor exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException(ResultType.Number, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(GCD exp)
        {
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Count == 0)
                return ResultType.Undefined;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] == ResultType.Undefined)
                    return ResultType.Undefined;
                if (results[i] != ResultType.Number)
                    throw new DifferentParameterTypeMismatchException(ResultType.Number, results[i], i);
            }

            return ResultType.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Lb exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException(ResultType.Number, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(LCM exp)
        {
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Count == 0)
                return ResultType.Undefined;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] == ResultType.Undefined)
                    return ResultType.Undefined;
                if (results[i] != ResultType.Number)
                    throw new DifferentParameterTypeMismatchException(ResultType.Number, results[i], i);
            }

            return ResultType.Number;
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

            throw new ParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, result);
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

            throw new ParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, result);
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

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;
                if (rightResult == ResultType.ComplexNumber)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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

            if (leftResult == ResultType.ComplexNumber)
            {
                if (rightResult == ResultType.ComplexNumber || rightResult == ResultType.Number)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.ComplexNumber)
            {
                if (leftResult == ResultType.Number)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Matrix)
            {
                if (rightResult == ResultType.Number || rightResult == ResultType.Matrix || rightResult == ResultType.Vector)
                    return ResultType.Matrix;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.Matrix | ResultType.Vector, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Matrix)
            {
                if (leftResult == ResultType.Number || leftResult == ResultType.Matrix || leftResult == ResultType.Vector)
                    return ResultType.Matrix;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.Matrix | ResultType.Vector, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Vector)
            {
                if (rightResult == ResultType.Number || rightResult == ResultType.Matrix || rightResult == ResultType.Vector)
                    return ResultType.Vector;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.Matrix | ResultType.Vector, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Vector)
            {
                if (leftResult == ResultType.Number || leftResult == ResultType.Matrix || leftResult == ResultType.Vector)
                    return ResultType.Vector;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.Matrix | ResultType.Vector, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            if (leftResult == ResultType.Number && rightResult == ResultType.Number)
                return ResultType.Undefined;

            if (leftResult == ResultType.ComplexNumber)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Number || rightResult == ResultType.ComplexNumber)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (leftResult == ResultType.Undefined || rightResult == ResultType.Undefined)
                return ResultType.Undefined;

            throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
                    return ResultType.Undefined;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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
            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] != ResultType.Undefined && results[i] != ResultType.Number)
                    throw new DifferentParameterTypeMismatchException(ResultType.Number, results[i], i);
            }

            return ResultType.Number;
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

            if (leftResult == ResultType.ComplexNumber)
            {
                if (rightResult == ResultType.ComplexNumber || rightResult == ResultType.Number)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.ComplexNumber)
            {
                if (leftResult == ResultType.Number)
                    return ResultType.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Matrix)
            {
                if (rightResult == ResultType.Matrix)
                    return ResultType.Matrix;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Matrix)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Vector)
            {
                if (rightResult == ResultType.Vector)
                    return ResultType.Vector;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Vector)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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

            if (result == ResultType.Number)
                return ResultType.Number;

            if (result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultType.Number | ResultType.ComplexNumber, result);
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

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sign exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.Number)
                return ResultType.Number;

            throw new ParameterTypeMismatchException(ResultType.Number, result);
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
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Count == 0)
                return ResultType.Vector;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] == ResultType.Undefined)
                    return ResultType.Undefined;
                if (results[i] != ResultType.Number)
                    throw new DifferentParameterTypeMismatchException(ResultType.Number, results[i], i);
            }

            return ResultType.Vector;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Matrix exp)
        {
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Count == 0)
                return ResultType.Matrix;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] == ResultType.Undefined)
                    return ResultType.Undefined;
                if (results[i] != ResultType.Vector)
                    throw new DifferentParameterTypeMismatchException(ResultType.Vector, results[i], i);
            }

            return ResultType.Matrix;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Determinant exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.Matrix)
                return ResultType.Number;

            throw new ParameterTypeMismatchException(ResultType.Matrix, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Inverse exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.Matrix)
                return ResultType.Matrix;

            throw new ParameterTypeMismatchException(ResultType.Matrix, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Transpose exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.Vector || result == ResultType.Matrix)
                return ResultType.Matrix;

            throw new ParameterTypeMismatchException(ResultType.Vector | ResultType.Matrix, result);
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
            if (result == ResultType.Undefined || result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultType.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Im exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.ComplexNumber)
                return ResultType.Number;

            throw new ParameterTypeMismatchException(ResultType.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Phase exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.ComplexNumber)
                return ResultType.Number;

            throw new ParameterTypeMismatchException(ResultType.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Re exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.ComplexNumber)
                return ResultType.Number;

            throw new ParameterTypeMismatchException(ResultType.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Reciprocal exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultType.Undefined || result == ResultType.ComplexNumber)
                return ResultType.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultType.ComplexNumber, result);
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
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arccot exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arccsc exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arcsec exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arcsin exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arctan exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Cos exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Cot exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Csc exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sec exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sin exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Tan exp)
        {
            return CheckTrigonometric(exp);
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
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arcoth exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arcsch exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arsech exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Arsinh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Artanh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Cosh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Coth exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Csch exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sech exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sinh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Tanh exp)
        {
            return CheckTrigonometric(exp);
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
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Count exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Max exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Min exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Product exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Stdev exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Stdevp exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Sum exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Var exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultType Analyze(Varp exp)
        {
            return CheckStatistical(exp);
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

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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
            if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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
            if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
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

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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
            if (conditionResult == ResultType.Undefined || conditionResult == ResultType.Boolean)
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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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

            throw new DifferentParameterTypeMismatchException(ResultType.Boolean, conditionResult, 0);
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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

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
            if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
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

            if (leftResult == ResultType.Number)
            {
                if (rightResult == ResultType.Number)
                    return ResultType.Number;

                throw new BinaryParameterTypeMismatchException(ResultType.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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
            if (leftResult == ResultType.Undefined || leftResult == ResultType.Boolean)
            {
                if (rightResult == ResultType.Undefined || rightResult == ResultType.Boolean)
                    return ResultType.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultType.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultType.Boolean, leftResult, BinaryParameterType.Left);
            }

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
            if (rightResult == ResultType.Undefined || rightResult == ResultType.Number)
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
            if (rightResult == ResultType.Undefined || rightResult == ResultType.Boolean)
                return ResultType.Undefined;

            throw new ParameterTypeMismatchException();
        }

        #endregion Programming

    }

}
