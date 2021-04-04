// Copyright 2012-2021 Dmytro Kyshchenko
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
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions.Units;
using xFunc.Maths.Expressions.Units.AngleUnits;
using static xFunc.Maths.ThrowHelpers;

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
                ArgNull(ExceptionArgument.exp);

            return exp.Argument.Analyze(this);
        }

        private IExpression AnalyzeUnary([NotNull] UnaryExpression exp)
        {
            if (exp is null)
                ArgNull(ExceptionArgument.exp);

            var argument = AnalyzeUnaryArgument(exp);

            if (IsChanged(exp, argument))
                return exp.Clone(argument);

            return exp;
        }

        private (IExpression Left, IExpression Right) AnalyzeBinaryArgument([NotNull] BinaryExpression exp)
        {
            if (exp is null)
                ArgNull(ExceptionArgument.exp);

            var left = exp.Left.Analyze(this);
            var right = exp.Right.Analyze(this);

            return (left, right);
        }

        private IExpression AnalyzeBinary([NotNull] BinaryExpression exp)
        {
            if (exp is null)
                ArgNull(ExceptionArgument.exp);

            var (left, right) = AnalyzeBinaryArgument(exp);

            if (IsChanged(exp, left, right))
                return exp.Clone(left, right);

            return exp;
        }

        private IExpression AnalyzeTrigonometric<T>([NotNull] UnaryExpression exp)
            where T : UnaryExpression
        {
            if (exp is null)
                ArgNull(ExceptionArgument.exp);

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
                ArgNull(ExceptionArgument.exp);

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
                ArgNull(ExceptionArgument.exp);

            var value = exp.Value.Analyze(this);

            if (IsChanged(exp.Value, value))
                return exp.Clone(value: value);

            return exp;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override IExpression Analyze(IExpression exp)
        {
            if (exp is null)
                ArgNull(ExceptionArgument.exp);

            return exp;
        }

        #region Standard

        /// <inheritdoc />
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

        /// <inheritdoc />
        public override IExpression Analyze(Add exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // 2 + x -> x + 2
                (Number number, Variable variable)
                    => Analyze(new Add(variable, number)),

                // 2 + (x + 2) -> (x + 2) + 2
                (Number number, Add(Variable, Number) add)
                    => Analyze(new Add(add, number)),

                // x + ax -> ax + x
                (Variable variable, Mul(Number, Variable) mul)
                    => Analyze(new Add(mul, variable)),

                // plus zero
                (Number(var number), _) when number == 0
                    => exp.Right,
                (_, Number(var number)) when number == 0
                    => exp.Left,

                // const + const
                (Number left, Number right)
                    => new Number(left.Value + right.Value),
                (Number left, Angle right)
                    => (left.Value + right.Value).AsExpression(),
                (Angle left, Number right)
                    => (left.Value + right.Value).AsExpression(),
                (Angle left, Angle right)
                    => (left.Value + right.Value).AsExpression(),

                // x + x
                (Variable left, Variable right) when left.Name == right.Name
                    => new Mul(Number.Two, left),

                // -y + x
                (UnaryMinus minus, var right)
                    => Analyze(new Sub(right, minus.Argument)),

                // x + (-y)
                (var left, UnaryMinus minus)
                    => Analyze(new Sub(left, minus.Argument)),

                // (x + 2) + 2
                // (2 + x) + 2
                // 2 + (2 + x)
                // 2 + (x + 2)
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

                // ax + x
                // xa + x
                // x + bx
                // x + xb
                (Mul(Number a, var x1), var x2) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value + 1), x1)),

                // ax + bx
                // ax + xb
                // xa + bx
                // xa + xb
                (Mul(Number a, var x1), Mul(Number b, var x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value + b.Value), x1)),

                var (left, right) when IsChanged(exp, left, right)
                    => new Add(left, right),

                _ => exp,
            };
        }

        /// <inheritdoc />
        public override IExpression Analyze(Ceil exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Define exp)
        {
            if (exp is null)
                ArgNull(ExceptionArgument.exp);

            var value = exp.Value.Analyze(this);
            if (IsChanged(exp.Value, value))
                return new Define(exp.Key, value);

            return exp;
        }

        /// <inheritdoc />
        public override IExpression Analyze(Del exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Derivative exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Div exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // 0 / 0
                (Number(var left), Number(var right)) when left == 0 && right == 0
                    => new Number(double.NaN),

                // 0 / x
                (Number(var number), _) when number == 0
                    => Number.Zero,

                // x / 0
                (_, Number(var number)) when number == 0
                    => throw new DivideByZeroException(),

                // x / 1
                (var left, Number(var number)) when number == 1
                    => left,

                // const / const
                (Number left, Number right)
                    => new Number(left.Value / right.Value),
                (Number left, Angle right)
                    => (left.Value / right.Value).AsExpression(),
                (Angle left, Number right)
                    => (left.Value / right.Value).AsExpression(),
                (Angle left, Angle right)
                    => (left.Value / right.Value).AsExpression(),

                // x / x
                (Variable left, Variable right) when left.Equals(right)
                    => Number.One,

                // (2 * x) / 2
                // (x * 2) / 2
                (Mul(Number left, var right), Number number)
                    => Analyze(new Div(right, new Number(number.Value / left.Value))),

                // 2 / (2 * x)
                // 2 / (x * 2)
                (Number number, Mul(Number left, var right))
                    => Analyze(new Div(new Number(number.Value / left.Value), right)),

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

                var (left, right) when IsChanged(exp, left, right)
                    => new Div(left, right),

                _ => exp,
            };
        }

        /// <inheritdoc />
        public override IExpression Analyze(Exp exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            if (argument is Ln ln)
                return ln.Argument;

            if (IsChanged(exp, argument))
                return new Exp(argument);

            return exp;
        }

        /// <inheritdoc />
        public override IExpression Analyze(Fact exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Floor exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Trunc exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Frac exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(GCD exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Lb exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                // lb(2)
                Number(var number) when number == 2 => Number.One,
                var arg when IsChanged(exp, arg) => new Lb(arg),
                _ => exp,
            };
        }

        /// <inheritdoc />
        public override IExpression Analyze(LCM exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Lg exp)
        {
            var result = AnalyzeUnaryArgument(exp);

            return result switch
            {
                // lg(10)
                Number(var number) when number == 10 => Number.One,
                var arg when IsChanged(exp, arg) => new Lg(arg),
                _ => exp,
            };
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public override IExpression Analyze(Log exp)
        {
            var (left, right) = AnalyzeBinaryArgument(exp);

            if (left.Equals(right))
                return Number.One;

            if (IsChanged(exp, left, right))
                return new Log(left, right);

            return exp;
        }

        /// <inheritdoc />
        public override IExpression Analyze(Mod exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Mul exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // xa -> ax
                (Variable variable, Number number)
                    => Analyze(new Mul(number, variable)),

                // x * ax -> ax * x
                (Variable variable, Mul(Number, Variable) mul)
                    => Analyze(new Mul(mul, variable)),

                // 2 * (2 * x) -> (2 * x) * 2
                (Number number, Mul(Number, Variable) mul)
                    => Analyze(new Mul(mul, number)),

                // mul by zero
                (Number(var number), _) when number == 0
                    => Number.Zero,
                (_, Number(var number)) when number == 0
                    => Number.Zero,

                // mul by 1
                (Number(var number), var right) when number == 1
                    => right,
                (var left, Number(var number)) when number == 1
                    => left,

                // mul by -1
                (Number(var number), var right) when number == -1
                    => new UnaryMinus(right),
                (var left, Number(var number)) when number == -1
                    => new UnaryMinus(left),

                // const * const
                (Number left, Number right)
                    => new Number(left.Value * right.Value),
                (Number left, Angle right)
                    => (left.Value * right.Value).AsExpression(),
                (Angle left, Number right)
                    => (left.Value * right.Value).AsExpression(),
                (Angle left, Angle right)
                    => (left.Value * right.Value).AsExpression(),

                // x * -y
                (var left, UnaryMinus minus)
                    => new UnaryMinus(new Mul(left, minus.Argument)),

                // x * x
                (Variable left, Variable right) when left.Equals(right)
                    => new Pow(left, Number.Two),

                // (2 * x) * 2
                // (x * 2) * 2
                // 2 * (2 * x)
                // 2 * (x * 2)
                (Mul(Number left, var right), Number number)
                    => Analyze(new Mul(new Number(number.Value * left.Value), right)),

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

                // ax * x
                // xa * x
                // x * bx
                // x * xb
                (Mul(Number a, var x1), var x2) when x1.Equals(x2)
                    => Analyze(new Mul(a, new Pow(x1, Number.Two))),

                // ax + bx
                // ax + xb
                // xa + bx
                // xa + xb
                (Mul(Number a, var x1), Mul(Number b, var x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value * b.Value), new Pow(x1, Number.Two))),

                // x * (1 / x)
                (Variable x1, Div(Number number, Variable x2)) when x1.Equals(x2)
                    => number,

                // (2 * x) * (1 / x)
                // (x * 2) * (1 / x)
                (Mul(Number a, Variable x1), Div(Number b, Variable x2)) when x1.Equals(x2)
                    => new Number(a.Value * b.Value),

                var (left, right) when IsChanged(exp, left, right)
                    => new Mul(left, right),

                _ => exp,
            };
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public override IExpression Analyze(ToNumber exp)
        {
            var argument = AnalyzeUnaryArgument(exp);

            return argument switch
            {
                Angle(var angle) => new Number(angle.Angle),
                var arg when IsChanged(exp, arg) => new ToNumber(arg),
                _ => exp,
            };
        }

        /// <inheritdoc />
        public override IExpression Analyze(Pow exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // x^0
                (_, Number(var number)) when number == 0
                    => Number.One,

                // 0^x
                (Number(var number), _) when number == 0
                    => Number.Zero,

                // x^1
                (var left, Number(var number)) when number == 1
                    => left,

                // x ^ log(x, y) -> y
                (var left, Log log) when left.Equals(log.Left)
                    => log.Right,

                // e ^ ln(y) -> y
                (Variable("e"), Ln ln)
                    => ln.Argument,

                // 10 ^ lg(y) -> y
                (Number number, Lg lg) when number.Value == 10
                    => lg.Argument,

                // 2 ^ lb(y) -> y
                (Number number, Lb lb) when number.Value == 2
                    => lb.Argument,

                var (left, right) when IsChanged(exp, left, right)
                    => new Pow(left, right),

                _ => exp,
            };
        }

        /// <inheritdoc />
        public override IExpression Analyze(Root exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // root(x, 1)
                (var left, Number(var number)) when number == 1
                    => left,

                var (left, right) when IsChanged(exp, left, right)
                    => new Root(left, right),

                _ => exp,
            };
        }

        /// <inheritdoc />
        public override IExpression Analyze(Round exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Simplify exp)
            => AnalyzeUnaryArgument(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Sqrt exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Sub exp)
        {
            var result = AnalyzeBinaryArgument(exp);

            return result switch
            {
                // plus zero
                (Number(var number), var right) when number == 0
                    => Analyze(new UnaryMinus(right)),
                (var left, Number(var number)) when number == 0
                    => left,

                // const - const
                (Number left, Number right)
                    => new Number(left.Value - right.Value),
                (Number left, Angle right)
                    => (left.Value - right.Value).AsExpression(),
                (Angle left, Number right)
                    => (left.Value - right.Value).AsExpression(),
                (Angle left, Angle right)
                    => (left.Value - right.Value).AsExpression(),

                // x + x
                (Variable left, Variable right) when left.Name == right.Name
                    => Number.Zero,

                // x - -y
                (var left, UnaryMinus minus)
                    => new Add(left, minus.Argument),

                // (2 + x) - 2
                // (x + 2) - 2
                (Add(var left, Number right), Number number)
                    => Analyze(new Add(left, new Number(number.Value - right.Value))),

                // 2 - (2 + x)
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

                // x - bx
                // x - xb
                (var x1, Mul(Number b, var x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(1 - b.Value), x1)),

                // ax - x
                // xa - x
                (Mul(Number a, var x1), var x2) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value - 1), x1)),

                // ax - bx
                // ax - xb
                // xa - bx
                // xa - xb
                (Mul(Number a, var x1), Mul(Number b, var x2)) when x1.Equals(x2)
                    => Analyze(new Mul(new Number(a.Value - b.Value), x1)),

                var (left, right) when IsChanged(exp, left, right)
                    => new Sub(left, right),

                _ => exp,
            };
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public override IExpression Analyze(UserFunction exp)
        {
            if (exp is null)
                ArgNull(ExceptionArgument.exp);

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

        /// <inheritdoc />
        public override IExpression Analyze(Vector exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Matrix exp)
        {
            if (exp is null)
                ArgNull(ExceptionArgument.exp);

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

        /// <inheritdoc />
        public override IExpression Analyze(Determinant exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Inverse exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Transpose exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(DotProduct exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(CrossProduct exp)
            => AnalyzeBinary(exp);

        #endregion Matrix

        #region Complex Numbers

        /// <inheritdoc />
        public override IExpression Analyze(Conjugate exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Im exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Phase exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Re exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Reciprocal exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(ToComplex exp)
            => AnalyzeUnary(exp);

        #endregion Complex Numbers

        #region Trigonometric

        /// <inheritdoc />
        public override IExpression Analyze(Arccos exp)
            => AnalyzeTrigonometric<Cos>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Arccot exp)
            => AnalyzeTrigonometric<Cot>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Arccsc exp)
            => AnalyzeTrigonometric<Csc>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Arcsec exp)
            => AnalyzeTrigonometric<Sec>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Arcsin exp)
            => AnalyzeTrigonometric<Sin>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Arctan exp)
            => AnalyzeTrigonometric<Tan>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Cos exp)
            => AnalyzeTrigonometric<Arccos>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Cot exp)
            => AnalyzeTrigonometric<Arccot>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Csc exp)
            => AnalyzeTrigonometric<Arccsc>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Sec exp)
            => AnalyzeTrigonometric<Arcsec>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Sin exp)
            => AnalyzeTrigonometric<Arcsin>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Tan exp)
            => AnalyzeTrigonometric<Arctan>(exp);

        #endregion

        #region Hyperbolic

        /// <inheritdoc />
        public override IExpression Analyze(Arcosh exp)
            => AnalyzeTrigonometric<Cosh>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Arcoth exp)
            => AnalyzeTrigonometric<Coth>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Arcsch exp)
            => AnalyzeTrigonometric<Csch>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Arsech exp)
            => AnalyzeTrigonometric<Sech>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Arsinh exp)
            => AnalyzeTrigonometric<Sinh>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Artanh exp)
            => AnalyzeTrigonometric<Tanh>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Cosh exp)
            => AnalyzeTrigonometric<Arcosh>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Coth exp)
            => AnalyzeTrigonometric<Arcoth>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Csch exp)
            => AnalyzeTrigonometric<Arcsch>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Sech exp)
            => AnalyzeTrigonometric<Arsech>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Sinh exp)
            => AnalyzeTrigonometric<Arsinh>(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Tanh exp)
            => AnalyzeTrigonometric<Artanh>(exp);

        #endregion Hyperbolic

        #region Statistical

        /// <inheritdoc />
        public override IExpression Analyze(Avg exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Count exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Max exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Min exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Product exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Stdev exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Stdevp exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Sum exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Var exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Varp exp)
            => AnalyzeDiffParams(exp);

        #endregion Statistical

        #region Logical and Bitwise

        /// <inheritdoc />
        public override IExpression Analyze(And exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Equality exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Implication exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(NAnd exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(NOr exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Not exp)
            => AnalyzeUnary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Or exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(XOr exp)
            => AnalyzeBinary(exp);

        #endregion Logical and Bitwise

        #region Programming

        /// <inheritdoc />
        public override IExpression Analyze(AddAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(ConditionalAnd exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(DivAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(Equal exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(For exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(GreaterOrEqual exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(GreaterThan exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(If exp)
            => AnalyzeDiffParams(exp);

        /// <inheritdoc />
        public override IExpression Analyze(LessOrEqual exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(LessThan exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(MulAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(NotEqual exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(ConditionalOr exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(SubAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(While exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(LeftShift exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(RightShift exp)
            => AnalyzeBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(LeftShiftAssign exp)
            => AnalyzeVariableBinary(exp);

        /// <inheritdoc />
        public override IExpression Analyze(RightShiftAssign exp)
            => AnalyzeVariableBinary(exp);

        #endregion Programming
    }
}