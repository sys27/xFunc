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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths.Analyzers
{
    /// <summary>
    /// The simplifier of expressions.
    /// </summary>
    /// <seealso cref="IAnalyzer{TResult}" />
    /// <seealso cref="ISimplifier" />
    public class Simplifier : Analyzer<IExpression>, ISimplifier
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsChanged(IExpression old, IExpression @new)
            => old != @new;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsChanged(UnaryExpression old, IExpression argument)
            => old.Argument != argument;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsChanged(BinaryExpression old, IExpression left, IExpression right)
            => old.Left != left || old.Right != right;

        private IExpression AnalyzeUnaryArgument([NotNull] UnaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            return exp.Argument.Analyze(this);
        }

        private IExpression AnalyzeUnary([NotNull] UnaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var argument = AnalyzeUnaryArgument(exp);

            if (IsChanged(exp, argument))
                return exp.Clone(argument);

            return exp;
        }

        private (IExpression Left, IExpression Right) AnalyzeBinaryArgument([NotNull] BinaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var left = exp.Left.Analyze(this);
            var right = exp.Right.Analyze(this);

            return (left, right);
        }

        private IExpression AnalyzeBinary([NotNull] BinaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var (left, right) = AnalyzeBinaryArgument(exp);

            if (IsChanged(exp, left, right))
                return exp.Clone(left, right);

            return exp;
        }

        private IExpression AnalyzeTrigonometric<T>([NotNull] UnaryExpression exp)
            where T : UnaryExpression
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var argument = AnalyzeUnaryArgument(exp);
            if (argument is T trigonometric)
                return trigonometric.Argument;

            if (IsChanged(exp, argument))
                return exp.Clone(argument);

            return exp;
        }

        private IExpression AnalyzeDiffParams([NotNull] DifferentParametersExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var arguments = exp.Arguments;
            var isExpChanged = false;

            for (var i = 0; i < exp.ParametersCount; i++)
            {
                var expression = exp[i].Analyze(this);
                if (IsChanged(exp[i], expression))
                {
                    isExpChanged = true;
                    arguments = arguments.SetItem(i, expression);
                }
            }

            if (isExpChanged)
                return exp.Clone(arguments);

            return exp;
        }

        private IExpression AnalyzeVariableBinary([NotNull] VariableBinaryExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var value = exp.Value.Analyze(this);

            if (IsChanged(exp.Value, value))
                return exp.Clone(value: value);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public override IExpression Analyze(IExpression exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            return exp;
        }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Abs exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                UnaryMinus minus => minus.Argument,
                Abs abs => abs,
                var arg when IsChanged(exp, arg) => new Abs(arg),
                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Add exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // plus zero
                (Number(var number), _) when MathExtensions.Equals(number, 0)
                    => exp.Right,
                (_, Number(var number)) when MathExtensions.Equals(number, 0)
                    => exp.Left,

                // const + const
                (Number left, Number right)
                    => new Number(left + right),

                // x + x
                (Variable left, Variable right) when left.Name == right.Name
                    => new Mul(Number.Two, left),

                // -y + x
                (UnaryMinus minus, var right)
                    => Analyze(new Sub(right, minus.Argument)),

                // x + (-y)
                (var left, UnaryMinus minus)
                    => Analyze(new Sub(left, minus.Argument)),

                // 2 + (2 + x)
                (Number number, Add(Number left, var right))
                    => Analyze(new Add(right, new Number(number.Value + left.Value))),

                // 2 + (x + 2)
                (Number number, Add(var left, Number right))
                    => Analyze(new Add(left, new Number(number.Value + right.Value))),

                // (2 + x) + 2
                (Add(Number left, var right), Number number)
                    => Analyze(new Add(right, new Number(number.Value + left.Value))),

                // (x + 2) + 2
                (Add(var left, Number right), Number number)
                    => Analyze(new Add(left, new Number(number.Value + right.Value))),

                // 2 + (2 - x)
                (Number number, Sub(Number left, var right))
                    => Analyze(new Sub(new Number(number.Value + left.Value), right)),

                // 2 + (x - 2)
                (Number number, Sub(var left, Number right))
                    => Analyze(new Add(new Number(number.Value - right.Value), left)),

                // (2 - x) + 2
                (Sub(Number left, var right), Number number)
                    => Analyze(new Sub(new Number(number.Value + left.Value), right)),

                // (x - 2) + 2
                (Sub(var left, Number right), Number number)
                    => Analyze(new Add(new Number(number.Value - right.Value), left)),

                // TODO: nested complex 'x'

                // x + xb
                (Variable x1, Mul(Variable x2, Number b)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(b.Value + 1), x1)),

                // x + bx
                (Variable x1, Mul(Number b, Variable x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(b.Value + 1), x1)),

                // ax + x
                (Mul(Number a, Variable x1), Variable x2) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value + 1), x1)),

                // xa + x
                (Mul(Variable x1, Number a), Variable x2) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value + 1), x1)),

                // ax + bx
                (Mul(Number a, Variable x1), Mul(Number b, Variable x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value + b.Value), x1)),

                // ax + xb
                (Mul(Number a, Variable x1), Mul(Variable x2, Number b)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value + b.Value), x1)),

                // xa + bx
                (Mul(Variable x1, Number a), Mul(Number b, Variable x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value + b.Value), x1)),

                // xa + xb
                (Mul(Variable x1, Number a), Mul(Variable x2, Number b)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value + b.Value), x1)),

                var (left, right) when IsChanged(exp, left, right) => new Add(left, right),

                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Ceil exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Define exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var value = exp.Value.Analyze(this);
            if (IsChanged(exp.Value, value))
                return new Define(exp.Key, value);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Del exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Derivative exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Div exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // 0 / 0
                (Number(var left), Number(var right))
                    when MathExtensions.Equals(left, 0) && MathExtensions.Equals(right, 0)
                    => new Number(double.NaN),

                // 0 / x
                (Number(var number), _) when MathExtensions.Equals(number, 0)
                    => Number.Zero,

                // x / 0
                (_, Number(var number)) when MathExtensions.Equals(number, 0)
                    => throw new DivideByZeroException(),

                // x / 1
                (var left, Number(var number)) when MathExtensions.Equals(number, 1)
                    => left,

                // const / const
                (Number left, Number right)
                    => new Number(left.Value / right.Value),

                // x / x
                (Variable left, Variable right) when left.Equals(right)
                    => Number.One,

                // (2 * x) / 2
                (Mul(Number left, var right), Number number)
                    => Analyze(new Div(right, new Number(number.Value / left.Value))),

                // (x * 2) / 2
                (Mul(var left, Number right), Number number)
                    => Analyze(new Div(left, new Number(number.Value / right.Value))),

                // 2 / (2 * x)
                (Number number, Mul(Number left, var right))
                    => Analyze(new Div(new Number(number.Value / left.Value), right)),

                // 2 / (x * 2)
                (Number number, Mul(var left, Number right))
                    => Analyze(new Div(new Number(number.Value / right.Value), left)),

                // (2 / x) / 2
                (Div(Number left, var right), Number number)
                    => Analyze(new Div(new Number(left.Value / number.Value), right)),

                // (x / 2) / 2
                (Div(var left, Number right), Number number)
                    => Analyze(new Div(left, new Number(right.Value * number.Value))),

                // 2 / (2 / x)
                (Number number, Div(Number left, var right))
                    => Analyze(new Mul(new Number(number.Value / left.Value), right)),

                // 2 / (x / 2)
                (Number number, Div(var left, Number right))
                    => Analyze(new Div(new Number(number.Value * right.Value), left)),

                var (left, right) when IsChanged(exp, left, right) => new Div(left, right),

                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Exp exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            if (argument is Ln ln)
                return ln.Argument;

            if (IsChanged(exp, argument))
                return new Exp(argument);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Fact exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Floor exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Trunc exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(Frac exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(GCD exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Lb exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                // lb(2)
                Number(var number) when MathExtensions.Equals(number, 2) => Number.One,
                var arg when IsChanged(exp, arg) => new Lb(arg),
                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(LCM exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Lg exp)
        {
            var result = AnalyzeUnaryArgument(exp);

            return result switch
            {
                // lg(10)
                Number(var number) when MathExtensions.Equals(number, 10) => Number.One,
                var arg when IsChanged(exp, arg) => new Lg(arg),
                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Ln exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                // ln(e)
                Variable("e") => Number.One,
                var arg when IsChanged(exp, arg) => new Ln(arg),
                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Log exp)
        {
            var (left, right) = AnalyzeBinaryArgument(exp);

            if (left.Equals(right))
                return Number.One;

            if (IsChanged(exp, left, right))
                return new Log(left, right);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Mod exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Mul exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // mul by zero
                (Number(var number), _) when MathExtensions.Equals(number, 0)
                    => Number.Zero,
                (_, Number(var number)) when MathExtensions.Equals(number, 0)
                    => Number.Zero,

                // mul by 1
                (Number(var number), var right) when MathExtensions.Equals(number, 1)
                    => right,
                (var left, Number(var number)) when MathExtensions.Equals(number, 1)
                    => left,

                // mul by -1
                (Number(var number), var right) when MathExtensions.Equals(number, -1)
                    => new UnaryMinus(right),
                (var left, Number(var number)) when MathExtensions.Equals(number, -1)
                    => new UnaryMinus(left),

                // const * const
                (Number left, Number right)
                    => new Number(left.Value * right.Value),

                // x * -y
                (var left, UnaryMinus minus)
                    => new UnaryMinus(new Mul(left, minus.Argument)),

                // x * x
                (Variable left, Variable right) when left.Equals(right)
                    => new Pow(left, Number.Two),

                // 2 * (2 * x)
                (Number number, Mul(Number left, var right))
                    => Analyze(new Mul(new Number(number.Value * left.Value), right)),

                // 2 * (x * 2)
                (Number number, Mul(var left, Number right))
                    => Analyze(new Mul(new Number(number.Value * right.Value), left)),

                // (2 * x) * 2
                (Mul(Number left, var right), Number number)
                    => Analyze(new Mul(new Number(number.Value * left.Value), right)),

                // (x * 2) * 2
                (Mul(var left, Number right), Number number)
                    => Analyze(new Mul(new Number(number.Value * right.Value), left)),

                // 2 * (2 / x)
                (Number number, Div(Number left, var right))
                    => Analyze(new Div(new Number(number.Value * left.Value), right)),

                // 2 * (x / 2)
                (Number number, Div(var left, Number right))
                    => Analyze(new Mul(new Number(number.Value / right.Value), left)),

                // (2 / x) * 2
                (Div(Number left, var right), Number number)
                    => Analyze(new Div(new Number(number.Value * left.Value), right)),

                // (x / 2) * 2
                (Div(var left, Number right), Number number)
                    => Analyze(new Mul(new Number(number.Value / right.Value), left)),

                // TODO: nested complex 'x'

                // x * xb
                (Variable x1, Mul(Variable x2, Number b)) when x1.Equals(x2)
                    => Analyze(new Mul(b, new Pow(x1, Number.Two))),

                // x * bx
                (Variable x1, Mul(Number b, Variable x2)) when x1.Equals(x2)
                    => Analyze(new Mul(b, new Pow(x1, Number.Two))),

                // ax * x
                (Mul(Number a, Variable x1), Variable x2) when x1.Equals(x2)
                    => Analyze(new Mul(a, new Pow(x1, Number.Two))),

                // xa * x
                (Mul(Variable x1, Number a), Variable x2) when x1.Equals(x2)
                    => Analyze(new Mul(a, new Pow(x1, Number.Two))),

                // ax + bx
                (Mul(Number a, Variable x1), Mul(Number b, Variable x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value * b.Value), new Pow(x1, Number.Two))),

                // ax + xb
                (Mul(Number a, Variable x1), Mul(Variable x2, Number b)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value * b.Value), new Pow(x1, Number.Two))),

                // xa + bx
                (Mul(Variable x1, Number a), Mul(Number b, Variable x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value * b.Value), new Pow(x1, Number.Two))),

                // xa + xb
                (Mul(Variable x1, Number a), Mul(Variable x2, Number b)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value * b.Value), new Pow(x1, Number.Two))),

                var (left, right) when IsChanged(exp, left, right)
                    => new Mul(left, right),

                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(ToDegree exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                Number number => AngleValue.Degree(number.Value).AsExpression(),

                Angle({ Unit: AngleUnit.Degree }) number => number,

                Angle(var angle) => angle.ToDegree().AsExpression(),

                var arg when IsChanged(exp, arg) => new ToDegree(arg),

                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(ToRadian exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                Number number => AngleValue.Radian(number.Value).AsExpression(),

                Angle({ Unit: AngleUnit.Radian }) number => number,

                Angle(var angle) => angle.ToRadian().AsExpression(),

                var arg when IsChanged(exp, arg) => new ToRadian(arg),

                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(ToGradian exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                Number number => AngleValue.Gradian(number.Value).AsExpression(),

                Angle({ Unit: AngleUnit.Gradian }) number => number,

                Angle(var angle) => angle.ToGradian().AsExpression(),

                var arg when IsChanged(exp, arg) => new ToGradian(arg),

                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(ToNumber exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                Angle(var angle) => new Number(angle.Value),
                var arg when IsChanged(exp, arg) => new ToNumber(arg),
                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Pow exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // x^0
                (_, Number(var number)) when MathExtensions.Equals(number, 0)
                    => Number.One,

                // 0^x
                (Number(var number), _) when MathExtensions.Equals(number, 0)
                    => Number.Zero,

                // x^1
                (var left, Number(var number)) when MathExtensions.Equals(number, 1)
                    => left,

                // x ^ log(x, y) -> y
                (var left, Log log) when left.Equals(log.Left)
                    => log.Right,

                // e ^ ln(y) -> y
                (Variable("e"), Ln ln)
                    => ln.Argument,

                // 10 ^ lg(y) -> y
                (Number number, Lg lg) when MathExtensions.Equals(number.Value, 10)
                    => lg.Argument,

                // 2 ^ lb(y) -> y
                (Number number, Lb lb) when MathExtensions.Equals(number.Value, 2)
                    => lb.Argument,

                var (left, right) when IsChanged(exp, left, right)
                    => new Pow(left, right),

                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Root exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // root(x, 1)
                (var left, Number(var number)) when MathExtensions.Equals(number, 1)
                    => left,

                var (left, right) when IsChanged(exp, left, right)
                    => new Root(left, right),

                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Round exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Simplify exp)
            => AnalyzeUnaryArgument(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sqrt exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sub exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // plus zero
                (Number(var number), var right) when MathExtensions.Equals(number, 0)
                    => Analyze(new UnaryMinus(right)),
                (var left, Number(var number)) when MathExtensions.Equals(number, 0)
                    => left,

                // const - const
                (Number left, Number right)
                    => new Number(left - right),

                // x + x
                (Variable left, Variable right) when left.Name == right.Name
                    => Number.Zero,

                // x - -y
                (var left, UnaryMinus minus)
                    => new Add(left, minus.Argument),

                // (2 + x) - 2
                (Add(Number left, var right), Number number)
                    => Analyze(new Add(right, new Number(number.Value - left.Value))),

                // (x + 2) - 2
                (Add(var left, Number right), Number number)
                    => Analyze(new Add(left, new Number(number.Value - right.Value))),

                // 2 - (2 + x)
                (Number number, Add(Number left, var right))
                    => Analyze(new Sub(new Number(number.Value - left.Value), right)),

                // 2 - (x + 2)
                (Number number, Add(var left, Number right))
                    => Analyze(new Sub(new Number(number.Value - right.Value), left)),

                // (2 - x) - 2
                (Sub(Number left, var right), Number number)
                    => Analyze(new Sub(new Number(number.Value - left.Value), right)),

                // (x - 2) - 2
                (Sub(var left, Number right), Number number)
                    => Analyze(new Sub(left, new Number(number.Value + right.Value))),

                // 2 - (2 - x)
                (Number number, Sub(Number left, var right))
                    => Analyze(new Add(new Number(number.Value - left.Value), right)),

                // 2 - (x - 2)
                (Number number, Sub(var left, Number right))
                    => Analyze(new Sub(new Number(number.Value + right.Value), left)),

                // TODO: nested complex 'x'

                // x - xb
                (Variable x1, Mul(Variable x2, Number b)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(1 - b.Value), x1)),

                // x - bx
                (Variable x1, Mul(Number b, Variable x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(1 - b.Value), x1)),

                // ax - x
                (Mul(Number a, Variable x1), Variable x2) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value - 1), x1)),

                // xa - x
                (Mul(Variable x1, Number a), Variable x2) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value - 1), x1)),

                // ax - bx
                (Mul(Number a, Variable x1), Mul(Number b, Variable x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value - b.Value), x1)),

                // ax - xb
                (Mul(Number a, Variable x1), Mul(Variable x2, Number b)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value - b.Value), x1)),

                // xa - bx
                (Mul(Variable x1, Number a), Mul(Number b, Variable x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value - b.Value), x1)),

                // xa - xb
                (Mul(Variable x1, Number a), Mul(Variable x2, Number b)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value - b.Value), x1)),

                var (left, right) when IsChanged(exp, left, right)
                    => new Sub(left, right),

                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(UnaryMinus exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                UnaryMinus minus => minus.Argument,
                Number number => new Number(-number.Value),
                var arg when IsChanged(exp, arg) => new UnaryMinus(arg),
                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(UserFunction exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var arguments = exp.Arguments;
            var isExpChanged = false;

            for (var i = 0; i < exp.ParametersCount; i++)
            {
                var expression = exp[i].Analyze(this);
                if (IsChanged(exp[i], expression))
                {
                    isExpChanged = true;
                    arguments = arguments.SetItem(i, expression);
                }
            }

            if (isExpChanged)
                return exp.Clone(arguments);

            return exp;
        }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Vector exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Matrix exp)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();

            var arguments = exp.Vectors;
            var isExpChanged = false;

            for (var i = 0; i < exp.Rows; i++)
            {
                var expression = exp[i].Analyze(this);
                if (IsChanged(exp[i], expression))
                {
                    isExpChanged = true;
                    arguments = arguments.SetItem(i, (Vector)expression);
                }
            }

            if (isExpChanged)
                return exp.Clone(arguments);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Determinant exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Inverse exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Transpose exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(DotProduct exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(CrossProduct exp)
            => AnalyzeBinary(exp);

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Conjugate exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Im exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Phase exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Re exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Reciprocal exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(ToComplex exp)
            => AnalyzeUnary(exp);

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arccos exp)
            => AnalyzeTrigonometric<Cos>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arccot exp)
            => AnalyzeTrigonometric<Cot>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arccsc exp)
            => AnalyzeTrigonometric<Csc>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcsec exp)
            => AnalyzeTrigonometric<Sec>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcsin exp)
            => AnalyzeTrigonometric<Sin>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arctan exp)
            => AnalyzeTrigonometric<Tan>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Cos exp)
            => AnalyzeTrigonometric<Arccos>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Cot exp)
            => AnalyzeTrigonometric<Arccot>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Csc exp)
            => AnalyzeTrigonometric<Arccsc>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sec exp)
            => AnalyzeTrigonometric<Arcsec>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sin exp)
            => AnalyzeTrigonometric<Arcsin>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Tan exp)
            => AnalyzeTrigonometric<Arctan>(exp);

        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcosh exp)
            => AnalyzeTrigonometric<Cosh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcoth exp)
            => AnalyzeTrigonometric<Coth>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcsch exp)
            => AnalyzeTrigonometric<Csch>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arsech exp)
            => AnalyzeTrigonometric<Sech>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arsinh exp)
            => AnalyzeTrigonometric<Sinh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Artanh exp)
            => AnalyzeTrigonometric<Tanh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Cosh exp)
            => AnalyzeTrigonometric<Arcosh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Coth exp)
            => AnalyzeTrigonometric<Arcoth>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Csch exp)
            => AnalyzeTrigonometric<Arcsch>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sech exp)
            => AnalyzeTrigonometric<Arsech>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sinh exp)
            => AnalyzeTrigonometric<Arsinh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Tanh exp)
            => AnalyzeTrigonometric<Artanh>(exp);

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Avg exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Count exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Max exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Min exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Product exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Stdev exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Stdevp exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sum exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Var exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Varp exp)
            => AnalyzeDiffParams(exp);

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(And exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Equality exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Implication exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(NAnd exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(NOr exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Not exp)
            => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Or exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(XOr exp)
            => AnalyzeBinary(exp);

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(AddAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(ConditionalAnd exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(DivAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Equal exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(For exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(GreaterOrEqual exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(GreaterThan exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(If exp)
            => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(LessOrEqual exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(LessThan exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(MulAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(NotEqual exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(ConditionalOr exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(SubAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(While exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(LeftShift exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(RightShift exp)
            => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(LeftShiftAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override IExpression Analyze(RightShiftAssign exp)
            => AnalyzeVariableBinary(exp);

        #endregion Programming
    }
}