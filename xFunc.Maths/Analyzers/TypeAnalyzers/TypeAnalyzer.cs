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

using System.Linq;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
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
        private ResultTypes CheckTrigonometric(UnaryExpression exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.Number,
                ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
                _ => ResultTypes.NumberOrComplex.ThrowFor(result),
            };
        }

        private ResultTypes CheckInverseTrigonometric(UnaryExpression exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.AngleNumber,
                ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
                _ => ResultTypes.NumberOrComplex.ThrowFor(result),
            };
        }

        private ResultTypes CheckStatistical(DifferentParametersExpression exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
            if (results.Contains(ResultTypes.Undefined))
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

        private ResultTypes AnalyzeForNumber(BinaryExpression exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Number, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Undefined, ResultTypes.Number) => ResultTypes.Boolean,
                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Boolean,

                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        private ResultTypes AnalyzeLogical(BinaryExpression exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Boolean, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Undefined, ResultTypes.Boolean) => ResultTypes.Boolean,
                (ResultTypes.Boolean, ResultTypes.Boolean) => ResultTypes.Boolean,

                (_, ResultTypes.Boolean) => ResultTypes.Boolean.ThrowForLeft(leftResult),
                (ResultTypes.Boolean, _) => ResultTypes.Boolean.ThrowForRight(rightResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        private ResultTypes AnalyzeLogicalAndBitwise(BinaryExpression exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, _) => ResultTypes.Undefined,
                (_, ResultTypes.Undefined) => ResultTypes.Undefined,

                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,
                (ResultTypes.Boolean, ResultTypes.Boolean) => ResultTypes.Boolean,

                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

                (_, ResultTypes.Boolean) => ResultTypes.Boolean.ThrowForLeft(leftResult),
                (ResultTypes.Boolean, _) => ResultTypes.Boolean.ThrowForRight(rightResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Abs exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                ResultTypes.ComplexNumber => ResultTypes.Number,
                ResultTypes.Vector => ResultTypes.Number,
                _ => throw new ParameterTypeMismatchException(ResultTypes.NumberOrComplex | ResultTypes.Vector, result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Add exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, _) => ResultTypes.Undefined,
                (_, ResultTypes.Undefined) => ResultTypes.Undefined,

                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

                (ResultTypes.Number, ResultTypes.AngleNumber) => ResultTypes.AngleNumber,
                (ResultTypes.AngleNumber, ResultTypes.Number) => ResultTypes.AngleNumber,
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) => ResultTypes.AngleNumber,

                (ResultTypes.Number, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.Number) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,

                (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Vector,
                (ResultTypes.Matrix, ResultTypes.Matrix) => ResultTypes.Matrix,

                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

                (_, ResultTypes.ComplexNumber) => ResultTypes.NumberOrComplex.ThrowForLeft(leftResult),
                (ResultTypes.ComplexNumber, _) => ResultTypes.NumberOrComplex.ThrowForRight(rightResult),

                (_, ResultTypes.Vector) => ResultTypes.Vector.ThrowForLeft(leftResult),
                (ResultTypes.Vector, _) => ResultTypes.Vector.ThrowForRight(rightResult),

                (_, ResultTypes.Matrix) => ResultTypes.Matrix.ThrowForLeft(leftResult),
                (ResultTypes.Matrix, _) => ResultTypes.Matrix.ThrowForRight(rightResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Ceil exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.Number.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Define exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Del exp)
        {
            return ResultTypes.Vector;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Derivative exp)
        {
            if (exp.ParametersCount == 1)
                return ResultTypes.Expression;

            if (exp.ParametersCount == 2 && exp[1] is Variable)
                return ResultTypes.Expression;

            if (exp.ParametersCount == 3 && exp[1] is Variable && exp[2] is Number)
                return ResultTypes.Number;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Div exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, _) => ResultTypes.Undefined,
                (_, ResultTypes.Undefined) => ResultTypes.Undefined,

                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

                (ResultTypes.Number, ResultTypes.AngleNumber) => ResultTypes.AngleNumber,
                (ResultTypes.AngleNumber, ResultTypes.Number) => ResultTypes.AngleNumber,
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) => ResultTypes.AngleNumber,

                (ResultTypes.Number, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.Number) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,

                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

                (_, ResultTypes.ComplexNumber) => ResultTypes.NumberOrComplex.ThrowForLeft(leftResult),
                (ResultTypes.ComplexNumber, _) => ResultTypes.NumberOrComplex.ThrowForRight(rightResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Exp exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
                _ => ResultTypes.NumberOrComplex.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Fact exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Number)
                return ResultTypes.Number;

            return ResultTypes.Number.ThrowFor(result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Floor exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.Number.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(GCD exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
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
        public ResultTypes Analyze(Lb exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Number)
                return ResultTypes.Number;

            return ResultTypes.Number.ThrowFor(result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(LCM exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
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
        public ResultTypes Analyze(Lg exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
                _ => ResultTypes.NumberOrComplex.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Ln exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
                _ => ResultTypes.NumberOrComplex.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Log exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, _) => ResultTypes.Undefined,
                (_, ResultTypes.Undefined) => ResultTypes.Undefined,

                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,
                (ResultTypes.Number, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,

                (ResultTypes.Number, _) => ResultTypes.NumberOrComplex.ThrowForRight(rightResult),

                _ => ResultTypes.Number.ThrowForLeft(leftResult),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Mod exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, ResultTypes.Undefined) => ResultTypes.Number,
                (ResultTypes.Undefined, ResultTypes.Number) => ResultTypes.Number,
                (ResultTypes.Number, ResultTypes.Undefined) => ResultTypes.Number,
                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

                (ResultTypes.Undefined, _) => ResultTypes.Number.ThrowForRight(rightResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),
                (_, ResultTypes.Undefined) => ResultTypes.Number.ThrowForLeft(leftResult),
                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Mul exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, _) => ResultTypes.Undefined,
                (_, ResultTypes.Undefined) => ResultTypes.Undefined,

                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

                (ResultTypes.Number, ResultTypes.AngleNumber) => ResultTypes.AngleNumber,
                (ResultTypes.AngleNumber, ResultTypes.Number) => ResultTypes.AngleNumber,
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) => ResultTypes.AngleNumber,

                (ResultTypes.Number, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.Number) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,

                (ResultTypes.Vector, ResultTypes.Number) => ResultTypes.Vector,
                (ResultTypes.Number, ResultTypes.Vector) => ResultTypes.Vector,
                (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Vector,

                (ResultTypes.Matrix, ResultTypes.Number) => ResultTypes.Matrix,
                (ResultTypes.Matrix, ResultTypes.Vector) => ResultTypes.Matrix,
                (ResultTypes.Number, ResultTypes.Matrix) => ResultTypes.Matrix,
                (ResultTypes.Vector, ResultTypes.Matrix) => ResultTypes.Matrix,
                (ResultTypes.Matrix, ResultTypes.Matrix) => ResultTypes.Matrix,

                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

                (_, ResultTypes.ComplexNumber) => ResultTypes.NumberOrComplex.ThrowForLeft(leftResult),
                (ResultTypes.ComplexNumber, _) => ResultTypes.NumberOrComplex.ThrowForRight(rightResult),

                (_, ResultTypes.Vector) => throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Matrix | ResultTypes.Vector, leftResult, BinaryParameterType.Left),
                (ResultTypes.Vector, _) => throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Matrix | ResultTypes.Vector, rightResult, BinaryParameterType.Right),

                (_, ResultTypes.Matrix) => throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Matrix | ResultTypes.Vector, leftResult, BinaryParameterType.Left),
                (ResultTypes.Matrix, _) => throw new BinaryParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Matrix | ResultTypes.Vector, rightResult, BinaryParameterType.Right),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Number exp) => ResultTypes.Number;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(AngleNumber exp)
            => ResultTypes.AngleNumber;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(ToDegree exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Number => ResultTypes.AngleNumber,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.NumberOrAngle.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(ToRadian exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Number => ResultTypes.AngleNumber,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.NumberOrAngle.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(ToGradian exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Number => ResultTypes.AngleNumber,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.NumberOrAngle.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(ToNumber exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.AngleNumber => ResultTypes.Number,
                _ => ResultTypes.AngleNumber.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Pow exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.ComplexNumber, ResultTypes.Undefined) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.Number) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,

                (ResultTypes.Undefined, _) => ResultTypes.Undefined,
                (_, ResultTypes.Undefined) => ResultTypes.Undefined,

                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Undefined,

                _ => ResultTypes.Number.ThrowForLeft(leftResult),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Root exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, ResultTypes.Undefined) => ResultTypes.Undefined,
                (ResultTypes.Undefined, ResultTypes.Number) => ResultTypes.Undefined,
                (ResultTypes.Number, ResultTypes.Undefined) => ResultTypes.Undefined,
                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Undefined,

                (ResultTypes.Undefined, _) => ResultTypes.Number.ThrowForRight(rightResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),
                (_, ResultTypes.Undefined) => ResultTypes.Number.ThrowForLeft(leftResult),
                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Round exp)
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
        public ResultTypes Analyze(Simplify exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Sqrt exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Sub exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, _) => ResultTypes.Undefined,
                (_, ResultTypes.Undefined) => ResultTypes.Undefined,

                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

                (ResultTypes.Number, ResultTypes.AngleNumber) => ResultTypes.AngleNumber,
                (ResultTypes.AngleNumber, ResultTypes.Number) => ResultTypes.AngleNumber,
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) => ResultTypes.AngleNumber,

                (ResultTypes.Number, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.Number) => ResultTypes.ComplexNumber,
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,

                (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Vector,
                (ResultTypes.Matrix, ResultTypes.Matrix) => ResultTypes.Matrix,

                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

                (_, ResultTypes.ComplexNumber) => ResultTypes.NumberOrComplex.ThrowForLeft(leftResult),
                (ResultTypes.ComplexNumber, _) => ResultTypes.NumberOrComplex.ThrowForRight(rightResult),

                (_, ResultTypes.Vector) => ResultTypes.Vector.ThrowForLeft(leftResult),
                (ResultTypes.Vector, _) => ResultTypes.Vector.ThrowForRight(rightResult),

                (_, ResultTypes.Matrix) => ResultTypes.Matrix.ThrowForLeft(leftResult),
                (ResultTypes.Matrix, _) => ResultTypes.Matrix.ThrowForRight(rightResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(UnaryMinus exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
                _ => ResultTypes.NumberOrComplex.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Undefine exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(UserFunction exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Variable exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(DelegateExpression exp)
        {
            return ResultTypes.Undefined;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Sign exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Number)
                return ResultTypes.Number;

            return ResultTypes.Number.ThrowFor(result);
        }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Vector exp)
        {
            var results = exp.Arguments.Select(x => x.Analyze(this)).ToList();
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
        public ResultTypes Analyze(Matrix exp)
        {
            // TODO: pool?
            var results = exp.Vectors.Select(x => x.Analyze(this)).ToList();
            if (results.Any(result => result == ResultTypes.Undefined))
                return ResultTypes.Undefined;

            return ResultTypes.Matrix;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Determinant exp)
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
        public ResultTypes Analyze(Inverse exp)
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
        public ResultTypes Analyze(Transpose exp)
        {
            var result = exp.Argument.Analyze(this);
            if (result == ResultTypes.Undefined ||
                result == ResultTypes.Vector ||
                result == ResultTypes.Matrix)
                return ResultTypes.Matrix;

            throw new ParameterTypeMismatchException(ResultTypes.Vector | ResultTypes.Matrix, result);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(DotProduct exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, _) => ResultTypes.Undefined,
                (_, ResultTypes.Undefined) => ResultTypes.Undefined,

                (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Number,

                (ResultTypes.Vector, _) => ResultTypes.Vector.ThrowForRight(rightResult),

                _ => ResultTypes.Vector.ThrowForLeft(leftResult),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(CrossProduct exp)
        {
            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, _) => ResultTypes.Undefined,
                (_, ResultTypes.Undefined) => ResultTypes.Undefined,

                (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Vector,

                (ResultTypes.Vector, _) => ResultTypes.Vector.ThrowForRight(rightResult),

                _ => ResultTypes.Vector.ThrowForLeft(leftResult),
            };
        }

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(ComplexNumber exp)
        {
            return ResultTypes.ComplexNumber;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Conjugate exp)
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
        public ResultTypes Analyze(Im exp)
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
        public ResultTypes Analyze(Phase exp)
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
        public ResultTypes Analyze(Re exp)
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
        public ResultTypes Analyze(Reciprocal exp)
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
        public ResultTypes Analyze(Arccos exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Arccot exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Arccsc exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Arcsec exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Arcsin exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Arctan exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Cos exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Cot exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Csc exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Sec exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Sin exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Tan exp)
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
        public ResultTypes Analyze(Arcosh exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Arcoth exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Arcsch exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Arsech exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Arsinh exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Artanh exp)
        {
            return CheckInverseTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Cosh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Coth exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Csch exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Sech exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Sinh exp)
        {
            return CheckTrigonometric(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Tanh exp)
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
        public ResultTypes Analyze(Avg exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Count exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Max exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Min exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Product exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Stdev exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Stdevp exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Sum exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Var exp)
        {
            return CheckStatistical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Varp exp)
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
        public ResultTypes Analyze(And exp)
        {
            return AnalyzeLogicalAndBitwise(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Bool exp)
        {
            return ResultTypes.Boolean;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Equality exp)
        {
            return AnalyzeLogical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Implication exp)
        {
            return AnalyzeLogical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(NAnd exp)
        {
            return AnalyzeLogical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(NOr exp)
        {
            return AnalyzeLogical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Not exp)
        {
            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.Boolean => ResultTypes.Boolean,
                _ => throw new ParameterTypeMismatchException(),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Or exp)
        {
            return AnalyzeLogicalAndBitwise(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(XOr exp)
        {
            return AnalyzeLogicalAndBitwise(exp);
        }

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(AddAssign exp)
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
        public ResultTypes Analyze(ConditionalAnd exp)
        {
            return AnalyzeLogical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(Dec exp)
        {
            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(DivAssign exp)
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
        public ResultTypes Analyze(Equal exp)
        {
            return AnalyzeLogicalAndBitwise(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(For exp)
        {
            var result = exp.Condition.Analyze(this);
            if (result == ResultTypes.Undefined || result == ResultTypes.Boolean)
                return ResultTypes.Undefined;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(GreaterOrEqual exp)
        {
            return AnalyzeForNumber(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(GreaterThan exp)
        {
            return AnalyzeForNumber(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(If exp)
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
        public ResultTypes Analyze(Inc exp)
        {
            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(LessOrEqual exp)
        {
            return AnalyzeForNumber(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(LessThan exp)
        {
            return AnalyzeForNumber(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(MulAssign exp)
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
        public ResultTypes Analyze(NotEqual exp)
        {
            return AnalyzeLogicalAndBitwise(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(ConditionalOr exp)
        {
            return AnalyzeLogical(exp);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public ResultTypes Analyze(SubAssign exp)
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
        public ResultTypes Analyze(While exp)
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