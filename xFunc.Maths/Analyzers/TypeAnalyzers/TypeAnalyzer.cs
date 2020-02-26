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

// TODO:
#pragma warning disable CA1062

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
    /// <seealso cref="IAnalyzer{ResultType}" />
    public class TypeAnalyzer : ITypeAnalyzer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeAnalyzer"/> class.
        /// </summary>
        public TypeAnalyzer()
        {
        }

        private ResultTypes CheckTrigonometric(UnaryExpression exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (result == ResultTypes.Number)
                return ResultTypes.Number;

            if (result == ResultTypes.ComplexNumber)
                return ResultTypes.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, result);
        }

        private ResultTypes CheckStatistical(DifferentParametersExpression exp)
        {
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Contains(ResultTypes.Undefined))
                return ResultTypes.Undefined;

            if (results.Count == 1)
            {
                if (results[0] == ResultTypes.Number || results[0] == ResultTypes.Vector)
                    return ResultTypes.Number;

                throw new DifferentParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Vector, results[0], 0);
            }

            for (var i = 0; i < results.Count; i++)
                if (results[i] != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, results[i], i);

            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression. This method should be only used for expressions which are not supported by xFunc (custom expression create by extending library).
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual ResultTypes Analyze(IExpression exp)
        {
            return ResultTypes.None;
        }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Abs exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (result == ResultTypes.Number || result == ResultTypes.ComplexNumber || result == ResultTypes.Vector)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber | ResultTypes.Vector, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Add exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.ComplexNumber)
            {
                if (rightResult == ResultTypes.ComplexNumber || rightResult == ResultTypes.Number)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.ComplexNumber)
            {
                if (leftResult == ResultTypes.Number)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, rightResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Matrix)
            {
                if (rightResult == ResultTypes.Matrix)
                    return ResultTypes.Matrix;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Matrix)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Vector)
            {
                if (rightResult == ResultTypes.Vector)
                    return ResultTypes.Vector;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Vector)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Ceil exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.Number, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Define exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Del exp)
        {
            return ResultTypes.Vector;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Derivative exp)
        {
            if (exp.ParametersCount == 1)
                return ResultTypes.Expression;

            if (exp.ParametersCount == 2 && exp.Arguments[1] is Variable)
                return ResultTypes.Expression;

            if (exp.ParametersCount == 3 && exp.Arguments[1] is Variable && exp.Arguments[2] is Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Div exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.ComplexNumber)
            {
                if (rightResult == ResultTypes.Number || rightResult == ResultTypes.ComplexNumber)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.ComplexNumber)
            {
                if (leftResult == ResultTypes.Number)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Exp exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (result == ResultTypes.Number)
                return ResultTypes.Number;

            if (result == ResultTypes.ComplexNumber)
                return ResultTypes.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Fact exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.Number, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Floor exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.Number, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(GCD exp)
        {
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Count == 0)
                return ResultTypes.Undefined;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] == ResultTypes.Undefined)
                    return ResultTypes.Undefined;
                if (results[i] != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, results[i], i);
            }

            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Lb exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.Number, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(LCM exp)
        {
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Count == 0)
                return ResultTypes.Undefined;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] == ResultTypes.Undefined)
                    return ResultTypes.Undefined;
                if (results[i] != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, results[i], i);
            }

            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Lg exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (result == ResultTypes.Number)
                return ResultTypes.Number;

            if (result == ResultTypes.ComplexNumber)
                return ResultTypes.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Ln exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (result == ResultTypes.Number)
                return ResultTypes.Number;

            if (result == ResultTypes.ComplexNumber)
                return ResultTypes.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Log exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;
                if (rightResult == ResultTypes.ComplexNumber)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Mod exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Mul exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.ComplexNumber)
            {
                if (rightResult == ResultTypes.ComplexNumber || rightResult == ResultTypes.Number)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.ComplexNumber)
            {
                if (leftResult == ResultTypes.Number)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Matrix)
            {
                if (rightResult == ResultTypes.Number || rightResult == ResultTypes.Matrix || rightResult == ResultTypes.Vector)
                    return ResultTypes.Matrix;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Matrix | ResultTypes.Vector, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Matrix)
            {
                if (leftResult == ResultTypes.Number || leftResult == ResultTypes.Matrix || leftResult == ResultTypes.Vector)
                    return ResultTypes.Matrix;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Matrix | ResultTypes.Vector, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Vector)
            {
                if (rightResult == ResultTypes.Number || rightResult == ResultTypes.Matrix || rightResult == ResultTypes.Vector)
                    return ResultTypes.Vector;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Matrix | ResultTypes.Vector, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Vector)
            {
                if (leftResult == ResultTypes.Number || leftResult == ResultTypes.Matrix || leftResult == ResultTypes.Vector)
                    return ResultTypes.Vector;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Matrix | ResultTypes.Vector, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Number exp)
        {
            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Pow exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            if (leftResult == ResultTypes.Number && rightResult == ResultTypes.Number)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.ComplexNumber)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number || rightResult == ResultTypes.ComplexNumber)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Root exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                    return ResultTypes.Undefined;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Round exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] != ResultTypes.Undefined && results[i] != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, results[i], i);
            }

            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Simplify exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sqrt exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sub exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.ComplexNumber)
            {
                if (rightResult == ResultTypes.ComplexNumber || rightResult == ResultTypes.Number)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.ComplexNumber)
            {
                if (leftResult == ResultTypes.Number)
                    return ResultTypes.ComplexNumber;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Matrix)
            {
                if (rightResult == ResultTypes.Matrix)
                    return ResultTypes.Matrix;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Matrix)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Vector)
            {
                if (rightResult == ResultTypes.Vector)
                    return ResultTypes.Vector;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Vector)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(UnaryMinus exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (result == ResultTypes.Number)
                return ResultTypes.Number;

            if (result == ResultTypes.ComplexNumber)
                return ResultTypes.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultTypes.Number | ResultTypes.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Undefine exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(UserFunction exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Variable exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(DelegateExpression exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sign exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.Number, result);
        }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Vector exp)
        {
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Count == 0)
                return ResultTypes.Vector;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] == ResultTypes.Undefined)
                    return ResultTypes.Undefined;
                if (results[i] != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, results[i], i);
            }

            return ResultTypes.Vector;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Matrix exp)
        {
            var results = exp.Arguments?.Where(x => x != null).Select(x => x.Analyze(this)).ToList();
            if (results == null || results.Count == 0)
                return ResultTypes.Matrix;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i] == ResultTypes.Undefined)
                    return ResultTypes.Undefined;
                if (results[i] != ResultTypes.Vector)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Vector, results[i], i);
            }

            return ResultTypes.Matrix;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Determinant exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Matrix)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.Matrix, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Inverse exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Matrix)
                return ResultTypes.Matrix;

            throw new ParameterTypeMismatchException(ResultTypes.Matrix, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Transpose exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Vector || result == ResultTypes.Matrix)
                return ResultTypes.Matrix;

            throw new ParameterTypeMismatchException(ResultTypes.Vector | ResultTypes.Matrix, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(DotProduct exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.Vector)
            {
                if (rightResult == ResultTypes.Vector)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Vector, rightResult, BinaryParameterType.Right);
            }

            throw new BinaryParameterTypeMismatchException(ResultTypes.Vector, leftResult, BinaryParameterType.Left);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(CrossProduct exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.Vector)
            {
                if (rightResult == ResultTypes.Vector)
                    return ResultTypes.Vector;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Vector, rightResult, BinaryParameterType.Right);
            }

            throw new BinaryParameterTypeMismatchException(ResultTypes.Vector, leftResult, BinaryParameterType.Left);
        }

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(ComplexNumber exp)
        {
            return ResultTypes.ComplexNumber;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Conjugate exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.ComplexNumber)
                return ResultTypes.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Im exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.ComplexNumber)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Phase exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.ComplexNumber)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Re exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.ComplexNumber)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Reciprocal exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.ComplexNumber)
                return ResultTypes.ComplexNumber;

            throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
        }

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arccos exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arccot exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arccsc exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arcsec exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arcsin exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arctan exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Cos exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Cot exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Csc exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sec exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sin exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Tan exp)
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
        public virtual ResultTypes Analyze(Arcosh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arcoth exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arcsch exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arsech exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arsinh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Artanh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Cosh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Coth exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Csch exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sech exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sinh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Tanh exp)
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
        public virtual ResultTypes Analyze(Avg exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Count exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Max exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Min exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Product exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Stdev exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Stdevp exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sum exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Var exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Varp exp)
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
        public virtual ResultTypes Analyze(Expressions.LogicalAndBitwise.And exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Bool exp)
        {
            return ResultTypes.Boolean;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Equality exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Implication exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(NAnd exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(NOr exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Not exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (result == ResultTypes.Number)
                return ResultTypes.Number;

            if (result == ResultTypes.Boolean)
                return ResultTypes.Boolean;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Expressions.LogicalAndBitwise.Or exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(XOr exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
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
        public virtual ResultTypes Analyze(AddAssign exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Expressions.Programming.And exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Dec exp)
        {
            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(DivAssign exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Equal exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(For exp)
        {
            var conditionResult = exp.Condition.Analyze(this);
            if (conditionResult == ResultTypes.Undefined || conditionResult == ResultTypes.Boolean)
                return ResultTypes.Undefined;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(GreaterOrEqual exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(GreaterThan exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(If exp)
        {
            var conditionResult = exp.Condition.Analyze(this);
            if (conditionResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            var thenResult = exp.Then.Analyze(this);
            if (conditionResult == ResultTypes.Boolean)
                return exp.ParametersCount == 2 ? thenResult : ResultTypes.Undefined;

            throw new DifferentParameterTypeMismatchException(ResultTypes.Boolean, conditionResult, 0);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Inc exp)
        {
            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(LessOrEqual exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(LessThan exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(MulAssign exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(NotEqual exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || rightResult == ResultTypes.Undefined)
                return ResultTypes.Undefined;

            if (leftResult == ResultTypes.Number)
            {
                if (rightResult == ResultTypes.Number)
                    return ResultTypes.Number;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Number)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Number, leftResult, BinaryParameterType.Left);
            }

            if (leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Expressions.Programming.Or exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);
            if (leftResult == ResultTypes.Undefined || leftResult == ResultTypes.Boolean)
            {
                if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Boolean)
                    return ResultTypes.Boolean;

                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, rightResult, BinaryParameterType.Right);
            }

            if (rightResult == ResultTypes.Boolean)
            {
                throw new BinaryParameterTypeMismatchException(ResultTypes.Boolean, leftResult, BinaryParameterType.Left);
            }

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(SubAssign exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(While exp)
        {
            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Boolean)
                return ResultTypes.Undefined;

            throw new ParameterTypeMismatchException();
        }

        #endregion Programming
    }
}

#pragma warning restore CA1062