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

using System.Diagnostics.CodeAnalysis;
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
    /// <seealso cref="ITypeAnalyzer"/>
    /// <seealso cref="IAnalyzer{ResultType}" />
    public class TypeAnalyzer : ITypeAnalyzer
    {
        private ResultTypes CheckArgument([NotNull] IExpression exp, ResultTypes result)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            return result;
        }

        private ResultTypes CheckNumericConversion([NotNull] UnaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.String,
                ResultTypes.Number => ResultTypes.String,
                _ => ResultTypes.Number.ThrowFor(result),
            };
        }

        private ResultTypes CheckTrigonometric([NotNull] UnaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.Number,
                ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
                _ => ResultTypes.NumberOrAngleOrComplex.ThrowFor(result),
            };
        }

        private ResultTypes CheckInverseTrigonometric([NotNull] UnaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.AngleNumber,
                ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
                _ => ResultTypes.NumberOrComplex.ThrowFor(result),
            };
        }

        private ResultTypes CheckStatistical([NotNull] DifferentParametersExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            if (exp.ParametersCount == 1)
            {
                var result = exp[0].Analyze(this);
                if (result == ResultTypes.Number || result == ResultTypes.Vector)
                    return ResultTypes.Number;

                throw new DifferentParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Vector, result, 0);
            }

            using var enumerator = exp.Arguments.GetEnumerator();
            for (var i = 0; enumerator.MoveNext(); i++)
            {
                var item = enumerator.Current.Analyze(this);
                if (item == ResultTypes.Undefined)
                    return ResultTypes.Undefined;
                if (item != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, item, i);
            }

            return ResultTypes.Number;
        }

        private ResultTypes AnalyzeForNumber([NotNull] BinaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Number, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Undefined, ResultTypes.Number) => ResultTypes.Boolean,
                (ResultTypes.AngleNumber, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Undefined, ResultTypes.AngleNumber) => ResultTypes.Boolean,

                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Boolean,
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) => ResultTypes.Boolean,

                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

                (_, ResultTypes.AngleNumber) => ResultTypes.AngleNumber.ThrowForLeft(leftResult),
                (ResultTypes.AngleNumber, _) => ResultTypes.AngleNumber.ThrowForRight(rightResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        private ResultTypes AnalyzeLogical([NotNull] BinaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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

        private ResultTypes AnalyzeLogicalAndBitwise([NotNull] BinaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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

        private ResultTypes AnalyzeEquality([NotNull] BinaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Number, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Undefined, ResultTypes.Number) => ResultTypes.Boolean,
                (ResultTypes.Boolean, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Undefined, ResultTypes.Boolean) => ResultTypes.Boolean,
                (ResultTypes.AngleNumber, ResultTypes.Undefined) => ResultTypes.Boolean,
                (ResultTypes.Undefined, ResultTypes.AngleNumber) => ResultTypes.Boolean,

                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Boolean,
                (ResultTypes.Boolean, ResultTypes.Boolean) => ResultTypes.Boolean,
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) => ResultTypes.Boolean,

                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

                (_, ResultTypes.Boolean) => ResultTypes.Boolean.ThrowForLeft(leftResult),
                (ResultTypes.Boolean, _) => ResultTypes.Boolean.ThrowForRight(rightResult),

                (_, ResultTypes.AngleNumber) => ResultTypes.AngleNumber.ThrowForLeft(leftResult),
                (ResultTypes.AngleNumber, _) => ResultTypes.AngleNumber.ThrowForRight(rightResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        private ResultTypes AnalyzeBinaryAssign([NotNull] VariableBinaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var rightResult = exp.Value.Analyze(this);

            return rightResult switch
            {
                ResultTypes.Undefined => ResultTypes.Number,
                ResultTypes.Number => ResultTypes.Number,
                _ => throw new ParameterTypeMismatchException(),
            };
        }

        private ResultTypes AnalyzeShift([NotNull] BinaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var leftResult = exp.Left.Analyze(this);
            var rightResult = exp.Right.Analyze(this);

            return (leftResult, rightResult) switch
            {
                (ResultTypes.Undefined, ResultTypes.Undefined) => ResultTypes.Number,
                (ResultTypes.Undefined, ResultTypes.Number) => ResultTypes.Number,
                (ResultTypes.Number, ResultTypes.Undefined) => ResultTypes.Number,
                (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

                (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),
                (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
                _ => throw new ParameterTypeMismatchException(),
            };
        }

        private ResultTypes AngleConversion([NotNull] UnaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.AngleNumber,
                ResultTypes.Number => ResultTypes.AngleNumber,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.NumberOrAngle.ThrowFor(result),
            };
        }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Abs exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                ResultTypes.ComplexNumber => ResultTypes.Number,
                ResultTypes.Vector => ResultTypes.Number,
                _ => ResultTypes.NumberOrAngleOrComplexOrVector.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Add exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Ceil exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.NumberOrAngle.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Define exp)
            => CheckArgument(exp, ResultTypes.String);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Del exp)
            => CheckArgument(exp, ResultTypes.Vector);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Derivative exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Div exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Exp exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Fact exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Floor exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.NumberOrAngle.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Trunc exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.NumberOrAngle.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Frac exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                _ => ResultTypes.NumberOrAngle.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(GCD exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            using var enumerator = exp.Arguments.GetEnumerator();
            for (var i = 0; enumerator.MoveNext(); i++)
            {
                var item = enumerator.Current.Analyze(this);
                if (item == ResultTypes.Undefined)
                    return ResultTypes.Undefined;
                if (item != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, item, i);
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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(LCM exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            using var enumerator = exp.Arguments.GetEnumerator();
            for (var i = 0; enumerator.MoveNext(); i++)
            {
                var item = enumerator.Current.Analyze(this);
                if (item == ResultTypes.Undefined)
                    return ResultTypes.Undefined;
                if (item != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, item, i);
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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Ln exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Log exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Mod exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Mul exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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

                (_, ResultTypes.Vector) => ResultTypes.NumberOrVectorOrMatrix.ThrowForLeft(leftResult),
                (ResultTypes.Vector, _) => ResultTypes.NumberOrVectorOrMatrix.ThrowForRight(rightResult),

                (_, ResultTypes.Matrix) => ResultTypes.NumberOrVectorOrMatrix.ThrowForLeft(leftResult),
                (ResultTypes.Matrix, _) => ResultTypes.NumberOrVectorOrMatrix.ThrowForRight(rightResult),

                _ => throw new ParameterTypeMismatchException(),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Number exp) => CheckArgument(exp, ResultTypes.Number);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Angle exp) => CheckArgument(exp, ResultTypes.AngleNumber);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(ToDegree exp)
            => AngleConversion(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(ToRadian exp)
            => AngleConversion(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(ToGradian exp)
            => AngleConversion(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(ToNumber exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var result = exp.Argument.Analyze(this);

            return result switch
            {
                ResultTypes.Undefined => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.Number,
                _ => ResultTypes.AngleNumber.ThrowFor(result),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Pow exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Root exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Round exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            using var enumerator = exp.Arguments.GetEnumerator();
            for (var i = 0; enumerator.MoveNext(); i++)
            {
                var item = enumerator.Current.Analyze(this);
                if (item != ResultTypes.Undefined && item != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, item, i);
            }

            return ResultTypes.Number;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Simplify exp)
            => CheckArgument(exp, ResultTypes.Undefined);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sqrt exp)
            => CheckArgument(exp, ResultTypes.Undefined);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sub exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(UnaryMinus exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Undefine exp)
            => CheckArgument(exp, ResultTypes.String);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(UserFunction exp)
            => CheckArgument(exp, ResultTypes.Undefined);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Variable exp)
            => CheckArgument(exp, ResultTypes.Undefined);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(DelegateExpression exp)
            => CheckArgument(exp, ResultTypes.Undefined);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sign exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(ToBin exp)
            => CheckNumericConversion(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(ToOct exp)
            => CheckNumericConversion(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(ToHex exp)
            => CheckNumericConversion(exp);

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Vector exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            using var enumerator = exp.Arguments.GetEnumerator();
            for (var i = 0; enumerator.MoveNext(); i++)
            {
                var item = enumerator.Current.Analyze(this);
                if (item == ResultTypes.Undefined)
                    return ResultTypes.Undefined;
                if (item != ResultTypes.Number)
                    throw new DifferentParameterTypeMismatchException(ResultTypes.Number, item, i);
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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            foreach (var item in exp.Vectors)
            {
                var result = item.Analyze(this);
                if (result == ResultTypes.Undefined)
                    return ResultTypes.Undefined;
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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(DotProduct exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(CrossProduct exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(ComplexNumber exp)
            => CheckArgument(exp, ResultTypes.ComplexNumber);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Conjugate exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arccot exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arccsc exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arcsec exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arcsin exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arctan exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Cos exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Cot exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Csc exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sec exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sin exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Tan exp)
            => CheckTrigonometric(exp);

        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arcosh exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arcoth exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arcsch exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arsech exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Arsinh exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Artanh exp)
            => CheckInverseTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Cosh exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Coth exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Csch exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sech exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sinh exp)
            => CheckTrigonometric(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Tanh exp)
            => CheckTrigonometric(exp);

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Avg exp)
            => CheckStatistical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Count exp)
            => CheckStatistical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Max exp)
            => CheckStatistical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Min exp)
            => CheckStatistical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Product exp)
            => CheckStatistical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Stdev exp)
            => CheckStatistical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Stdevp exp)
            => CheckStatistical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Sum exp)
            => CheckStatistical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Var exp)
            => CheckStatistical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Varp exp)
            => CheckStatistical(exp);

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(And exp)
            => AnalyzeLogicalAndBitwise(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Bool exp)
            => CheckArgument(exp, ResultTypes.Boolean);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Equality exp)
            => AnalyzeLogical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Implication exp)
            => AnalyzeLogical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(NAnd exp)
            => AnalyzeLogical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(NOr exp)
            => AnalyzeLogical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Not exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(Or exp)
            => AnalyzeLogicalAndBitwise(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(XOr exp)
            => AnalyzeLogicalAndBitwise(exp);

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(AddAssign exp)
            => AnalyzeBinaryAssign(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(ConditionalAnd exp)
            => AnalyzeLogical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Dec exp)
            => CheckArgument(exp, ResultTypes.Number);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(DivAssign exp)
            => AnalyzeBinaryAssign(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(Equal exp)
            => AnalyzeEquality(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(For exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
        public virtual ResultTypes Analyze(GreaterOrEqual exp)
            => AnalyzeForNumber(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(GreaterThan exp)
            => AnalyzeForNumber(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(If exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

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
            => CheckArgument(exp, ResultTypes.Number);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(LessOrEqual exp)
            => AnalyzeForNumber(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(LessThan exp)
            => AnalyzeForNumber(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(MulAssign exp)
            => AnalyzeBinaryAssign(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(NotEqual exp)
            => AnalyzeEquality(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(ConditionalOr exp)
            => AnalyzeLogical(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(SubAssign exp)
            => AnalyzeBinaryAssign(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(While exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var rightResult = exp.Right.Analyze(this);
            if (rightResult == ResultTypes.Undefined || rightResult == ResultTypes.Boolean)
                return ResultTypes.Undefined;

            throw new ParameterTypeMismatchException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(LeftShift exp)
            => AnalyzeShift(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(RightShift exp)
            => AnalyzeShift(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(LeftShiftAssign exp)
            => AnalyzeBinaryAssign(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual ResultTypes Analyze(RightShiftAssign exp)
            => AnalyzeBinaryAssign(exp);

        #endregion Programming
    }
}