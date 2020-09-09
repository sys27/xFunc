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
#pragma warning disable SA1513
#pragma warning disable SA1515
#pragma warning disable CA1062

using System;
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

namespace xFunc.Maths.Analyzers
{
    /// <summary>
    /// The simplifier of expressions.
    /// </summary>
    /// <seealso cref="IAnalyzer{TResult}" />
    /// <seealso cref="ISimplifier" />
    public class Simplifier : ISimplifier
    {
        private T AnalyzeUnary<T>(T exp) where T : UnaryExpression
        {
            exp.Argument = exp.Argument.Analyze(this);

            return exp;
        }

        private T AnalyzeBinary<T>(T exp) where T : BinaryExpression
        {
            exp.Left = exp.Left.Analyze(this);
            exp.Right = exp.Right.Analyze(this);

            return exp;
        }

        private IExpression AnalyzeTrigonometric<T>(UnaryExpression exp) where T : UnaryExpression
        {
            exp.Argument = exp.Argument.Analyze(this);
            if (exp.Argument is T trigonometric)
                return trigonometric.Argument;

            return exp;
        }

        private IExpression AnalyzeDiffParams(DifferentParametersExpression exp)
        {
            for (var i = 0; i < exp.ParametersCount; i++)
                exp[i] = exp[i].Analyze(this);

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
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Abs exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Add exp)
        {
            exp = AnalyzeBinary(exp);

            return exp switch
            {
                // plus zero
                (Number(var number), _) when MathExtensions.Equals(number, 0) => exp.Right,
                (_, Number(var number)) when MathExtensions.Equals(number, 0) => exp.Left,

                // const + const
                (Number left, Number right) => new Number(left + right),

                // x + x
                (Variable left, Variable right) when left.Name == right.Name
                    => new Mul(Number.Two, left),

                // -y + x
                (UnaryMinus minus, _) => Analyze(new Sub(exp.Right, minus.Argument)),

                // x + (-y)
                (_, UnaryMinus minus) => Analyze(new Sub(exp.Left, minus.Argument)),

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
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Ceil exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Define exp)
        {
            exp.Value = exp.Value.Analyze(this);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Del exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Derivative exp)
        {
            exp.Expression = exp.Expression.Analyze(this);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Div exp)
        {
            exp = AnalyzeBinary(exp);

            return exp switch
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
                (var left, Number(var number)) when MathExtensions.Equals(number, 1) => left,

                // const / const
                (Number left, Number right) => new Number(left.Value / right.Value),

                // x / x
                (Variable left, Variable right) when left.Equals(right) => Number.One,

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
        public virtual IExpression Analyze(Exp exp)
        {
            exp = AnalyzeUnary(exp);

            // exp(ln(y)) -> y
            if (exp.Argument is Ln ln)
                return ln.Argument;

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Fact exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Floor exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Trunc exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Frac exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(GCD exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Lb exp)
        {
            exp = AnalyzeUnary(exp);

            return exp switch
            {
                (Number(var number)) _ when MathExtensions.Equals(number, 2) => Number.One,
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
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(LCM exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Lg exp)
        {
            exp = AnalyzeUnary(exp);

            return exp switch
            {
                // lg(10)
                (Number(var number)) _ when MathExtensions.Equals(number, 10) => Number.One,
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
        public virtual IExpression Analyze(Ln exp)
        {
            exp = AnalyzeUnary(exp);

            // ln(e)
            if (exp.Argument is Variable("e"))
                return Number.One;

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Log exp)
        {
            exp = AnalyzeBinary(exp);

            // log(4x, 4x)
            if (exp.Left.Equals(exp.Right))
                return Number.One;

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Mod exp) => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Mul exp)
        {
            exp = AnalyzeBinary(exp);

            return exp switch
            {
                // mul by zero
                (Number(var number), _) when MathExtensions.Equals(number, 0) => Number.Zero,
                (_, Number(var number)) when MathExtensions.Equals(number, 0) => Number.Zero,

                // mul by 1
                (Number(var number), var right) when MathExtensions.Equals(number, 1) => right,
                (var left, Number(var number)) when MathExtensions.Equals(number, 1) => left,

                // mul by -1
                (Number(var number), var right) when MathExtensions.Equals(number, -1)
                    => new UnaryMinus(right),
                (var left, Number(var number)) when MathExtensions.Equals(number, -1)
                    => new UnaryMinus(left),

                // const * const
                (Number left, Number right) => new Number(left.Value * right.Value),

                // x * -y
                (var left, UnaryMinus minus) => new UnaryMinus(new Mul(left, minus.Argument)),

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
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Number exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Angle exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual IExpression Analyze(ToDegree exp)
        {
            exp = AnalyzeUnary(exp);

            return exp.Argument switch
            {
                Number number => AngleValue.Degree(number.Value).AsExpression(),
                Angle({ Unit: AngleUnit.Degree }) number => number,
                Angle(var angle) => angle.ToDegree().AsExpression(),
                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual IExpression Analyze(ToRadian exp)
        {
            exp = AnalyzeUnary(exp);

            return exp.Argument switch
            {
                Number number => AngleValue.Radian(number.Value).AsExpression(),
                Angle({ Unit: AngleUnit.Radian }) number => number,
                Angle(var angle) => angle.ToRadian().AsExpression(),
                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual IExpression Analyze(ToGradian exp)
        {
            exp = AnalyzeUnary(exp);

            return exp.Argument switch
            {
                Number number => AngleValue.Gradian(number.Value).AsExpression(),
                Angle({ Unit: AngleUnit.Gradian }) number => number,
                Angle(var angle) => angle.ToGradian().AsExpression(),
                _ => exp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public virtual IExpression Analyze(ToNumber exp)
        {
            exp = AnalyzeUnary(exp);

            return exp.Argument switch
            {
                Angle(var angle) => new Number(angle.Value),
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
        public virtual IExpression Analyze(Pow exp)
        {
            exp = AnalyzeBinary(exp);

            return (exp.Left, exp.Right) switch
            {
                // x^0
                (_, Number(var number)) when MathExtensions.Equals(number, 0) => Number.One,
                // 0^x
                (Number(var number), _) when MathExtensions.Equals(number, 0) => Number.Zero,
                // x^1
                (var left, Number(var number)) when MathExtensions.Equals(number, 1) => left,
                // x ^ log(x, y) -> y
                (var left, Log log) when left.Equals(log.Left) => log.Right,
                // e ^ ln(y) -> y
                (Variable("e"), Ln ln) => ln.Argument,
                // 10 ^ lg(y) -> y
                (Number number, Lg lg) when MathExtensions.Equals(number.Value, 10) => lg.Argument,
                // 2 ^ lb(y) -> y
                (Number number, Lb lb) when MathExtensions.Equals(number.Value, 2) => lb.Argument,
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
        public virtual IExpression Analyze(Root exp)
        {
            exp = AnalyzeBinary(exp);

            return exp switch
            {
                // root(x, 1)
                (var left, Number(var number)) when MathExtensions.Equals(number, 1) => left,
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
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Round exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Simplify exp) => exp.Argument.Analyze(this);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Sqrt exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Sub exp)
        {
            exp = AnalyzeBinary(exp);

            return exp switch
            {
                // plus zero
                (Number(var number), _) when MathExtensions.Equals(number, 0)
                    => Analyze(new UnaryMinus(exp.Right)),
                (_, Number(var number)) when MathExtensions.Equals(number, 0)
                    => exp.Left,

                // const - const
                (Number left, Number right) => new Number(left - right),

                // x + x
                (Variable left, Variable right) when left.Name == right.Name => Number.Zero,

                // x - -y
                (_, UnaryMinus minus) => new Add(exp.Left, minus.Argument),

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
        public virtual IExpression Analyze(UnaryMinus exp)
        {
            exp = AnalyzeUnary(exp);

            return exp switch
            {
                (UnaryMinus minus) _ => minus.Argument,
                (Number number) _ => new Number(-number.Value),
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
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Undefine exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(UserFunction exp)
        {
            for (var i = 0; i < exp.ParametersCount; i++)
                exp[i] = exp[i].Analyze(this);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Variable exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(DelegateExpression exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Sign exp) => exp;

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Vector exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Matrix exp)
        {
            for (var i = 0; i < exp.Rows; i++)
                exp[i] = (Vector)exp[i].Analyze(this);

            return exp;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Determinant exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Inverse exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Transpose exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(DotProduct exp) => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(CrossProduct exp) => AnalyzeBinary(exp);

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(ComplexNumber exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Conjugate exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Im exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Phase exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Re exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Reciprocal exp) => AnalyzeUnary(exp);

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arccos exp) => AnalyzeTrigonometric<Cos>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arccot exp) => AnalyzeTrigonometric<Cot>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arccsc exp) => AnalyzeTrigonometric<Csc>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arcsec exp) => AnalyzeTrigonometric<Sec>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arcsin exp) => AnalyzeTrigonometric<Sin>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arctan exp) => AnalyzeTrigonometric<Tan>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Cos exp)
        {
            var simplifiedExp = AnalyzeTrigonometric<Arccos>(exp);

            return simplifiedExp switch
            {
                Cos(Number(var number)) when MathExtensions.Equals(number, 0) => Number.One,
                _ => simplifiedExp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Cot exp)
        {
            var simplifiedExp = AnalyzeTrigonometric<Arccot>(exp);

            return simplifiedExp switch
            {
                Cot(Number(var number)) when MathExtensions.Equals(number, 0)
                    => new Number(double.PositiveInfinity),
                _ => simplifiedExp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Csc exp)
        {
            var simplifiedExp = AnalyzeTrigonometric<Arccsc>(exp);

            return simplifiedExp switch
            {
                Csc(Number(var number)) when MathExtensions.Equals(number, 0)
                    => new Number(double.PositiveInfinity),
                _ => simplifiedExp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Sec exp)
        {
            var simplifiedExp = AnalyzeTrigonometric<Arcsec>(exp);

            return simplifiedExp switch
            {
                Sec(Number(var number)) when MathExtensions.Equals(number, 0) => Number.One,
                _ => simplifiedExp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Sin exp)
        {
            var simplifiedExp = AnalyzeTrigonometric<Arcsin>(exp);

            return simplifiedExp switch
            {
                Sin(Number(var number)) when MathExtensions.Equals(number, 0) => Number.Zero,
                _ => simplifiedExp,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Tan exp)
        {
            var simplifiedExp = AnalyzeTrigonometric<Arctan>(exp);

            return simplifiedExp switch
            {
                Tan(Number(var number)) when MathExtensions.Equals(number, 0) => Number.Zero,
                _ => simplifiedExp,
            };
        }

        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arcosh exp) => AnalyzeTrigonometric<Cosh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arcoth exp) => AnalyzeTrigonometric<Coth>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arcsch exp) => AnalyzeTrigonometric<Csch>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arsech exp) => AnalyzeTrigonometric<Sech>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Arsinh exp) => AnalyzeTrigonometric<Sinh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Artanh exp) => AnalyzeTrigonometric<Tanh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Cosh exp) => AnalyzeTrigonometric<Arcosh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Coth exp) => AnalyzeTrigonometric<Arcoth>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Csch exp) => AnalyzeTrigonometric<Arcsch>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Sech exp) => AnalyzeTrigonometric<Arsech>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Sinh exp) => AnalyzeTrigonometric<Arsinh>(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public virtual IExpression Analyze(Tanh exp) => AnalyzeTrigonometric<Artanh>(exp);

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Avg exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Count exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Max exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Min exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Product exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Stdev exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Stdevp exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Sum exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Var exp) => AnalyzeDiffParams(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Varp exp) => AnalyzeDiffParams(exp);

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(And exp) => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Bool exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Equality exp) => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Implication exp) => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(NAnd exp) => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(NOr exp) => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Not exp) => AnalyzeUnary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Or exp) => AnalyzeBinary(exp);

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(XOr exp) => AnalyzeBinary(exp);

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(AddAssign exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(ConditionalAnd exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Dec exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(DivAssign exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Equal exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(For exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(GreaterOrEqual exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(GreaterThan exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(If exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(Inc exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(LessOrEqual exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(LessThan exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(MulAssign exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(NotEqual exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(ConditionalOr exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(SubAssign exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(While exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(LeftShift exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(RightShift exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(LeftShiftAssign exp) => exp;

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual IExpression Analyze(RightShiftAssign exp) => exp;

        #endregion Programming
    }
}

#pragma warning restore SA1513
#pragma warning restore SA1515
#pragma warning restore CA1062