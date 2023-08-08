// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static xFunc.Maths.ThrowHelpers;

namespace xFunc.Maths.Analyzers;

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

    private IExpression AnalyzeUnaryArgument(UnaryExpression exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        return exp.Argument.Analyze(this);
    }

    private IExpression AnalyzeUnary(UnaryExpression exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

    private (IExpression Left, IExpression Right) AnalyzeBinaryArgument(BinaryExpression exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var left = exp.Left.Analyze(this);
        var right = exp.Right.Analyze(this);

        return (left, right);
    }

    private IExpression AnalyzeBinary(BinaryExpression exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var (left, right) = AnalyzeBinaryArgument(exp);

        if (IsChanged(exp, left, right))
            return exp.Clone(left, right);

        return exp;
    }

    private IExpression AnalyzeTrigonometric<T>(UnaryExpression exp)
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

    private IExpression AnalyzeDiffParams(DifferentParametersExpression exp)
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

    private IExpression AnalyzeVariableBinary(VariableBinaryExpression exp)
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

            (Number left, Power right)
                => (left.Value + right.Value).AsExpression(),
            (Power left, Number right)
                => (left.Value + right.Value).AsExpression(),
            (Power left, Power right)
                => (left.Value + right.Value).AsExpression(),

            (Number left, Temperature right)
                => (left.Value + right.Value).AsExpression(),
            (Temperature left, Number right)
                => (left.Value + right.Value).AsExpression(),
            (Temperature left, Temperature right)
                => (left.Value + right.Value).AsExpression(),

            (Number left, Mass right)
                => (left.Value + right.Value).AsExpression(),
            (Mass left, Number right)
                => (left.Value + right.Value).AsExpression(),
            (Mass left, Mass right)
                => (left.Value + right.Value).AsExpression(),

            (Number left, Length right)
                => (left.Value + right.Value).AsExpression(),
            (Length left, Number right)
                => (left.Value + right.Value).AsExpression(),
            (Length left, Length right)
                => (left.Value + right.Value).AsExpression(),

            (Number left, Time right)
                => (left.Value + right.Value).AsExpression(),
            (Time left, Number right)
                => (left.Value + right.Value).AsExpression(),
            (Time left, Time right)
                => (left.Value + right.Value).AsExpression(),

            (Number left, Area right)
                => (left.Value + right.Value).AsExpression(),
            (Area left, Number right)
                => (left.Value + right.Value).AsExpression(),
            (Area left, Area right)
                => (left.Value + right.Value).AsExpression(),

            (Number left, Volume right)
                => (left.Value + right.Value).AsExpression(),
            (Volume left, Number right)
                => (left.Value + right.Value).AsExpression(),
            (Volume left, Volume right)
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
    public override IExpression Analyze(Assign exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var value = exp.Value.Analyze(this);
        if (IsChanged(exp.Value, value))
            return new Assign(exp.Key, value);

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

            (Angle left, Number right)
                => (left.Value / right.Value).AsExpression(),
            (Power left, Number right)
                => (left.Value / right.Value).AsExpression(),
            (Temperature left, Number right)
                => (left.Value / right.Value).AsExpression(),
            (Mass left, Number right)
                => (left.Value / right.Value).AsExpression(),
            (Length left, Number right)
                => (left.Value / right.Value).AsExpression(),
            (Time left, Number right)
                => (left.Value / right.Value).AsExpression(),
            (Area left, Number right)
                => (left.Value / right.Value).AsExpression(),
            (Volume left, Number right)
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
            (Number number, Mul(Number, not Number) mul)
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

            (Number left, Power right)
                => (left.Value * right.Value).AsExpression(),
            (Power left, Number right)
                => (left.Value * right.Value).AsExpression(),

            (Number left, Temperature right)
                => (left.Value * right.Value).AsExpression(),
            (Temperature left, Number right)
                => (left.Value * right.Value).AsExpression(),

            (Number left, Mass right)
                => (left.Value * right.Value).AsExpression(),
            (Mass left, Number right)
                => (left.Value * right.Value).AsExpression(),

            (Number left, Length right)
                => (left.Value * right.Value).AsExpression(),
            (Length left, Number right)
                => (left.Value * right.Value).AsExpression(),
            (Length left, Length right)
                => (left.Value * right.Value).AsExpression(),
            (Area left, Length right)
                => (left.Value * right.Value).AsExpression(),
            (Length left, Area right)
                => (left.Value * right.Value).AsExpression(),

            (Number left, Time right)
                => (left.Value * right.Value).AsExpression(),
            (Time left, Number right)
                => (left.Value * right.Value).AsExpression(),

            (Number left, Area right)
                => (left.Value * right.Value).AsExpression(),
            (Area left, Number right)
                => (left.Value * right.Value).AsExpression(),

            (Number left, Volume right)
                => (left.Value * right.Value).AsExpression(),
            (Volume left, Number right)
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

            Angle({ Unit.IsDegree: true }) number => number,

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

            Angle({ Unit.IsRadian: true }) number => number,

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

            Angle({ Unit.IsGradian: true }) number => number,

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
            Power(var power) => new Number(power.Value),
            Temperature(var temperature) => new Number(temperature.Value),
            Mass(var mass) => new Number(mass.Value),
            Length(var length) => new Number(length.Value),
            Time(var time) => new Number(time.Value),
            Area(var area) => new Number(area.Value),
            Volume(var volume) => new Number(volume.Value),
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

            (Number left, Power right)
                => (left.Value - right.Value).AsExpression(),
            (Power left, Number right)
                => (left.Value - right.Value).AsExpression(),
            (Power left, Power right)
                => (left.Value - right.Value).AsExpression(),

            (Number left, Temperature right)
                => (left.Value - right.Value).AsExpression(),
            (Temperature left, Number right)
                => (left.Value - right.Value).AsExpression(),
            (Temperature left, Temperature right)
                => (left.Value - right.Value).AsExpression(),

            (Number left, Mass right)
                => (left.Value - right.Value).AsExpression(),
            (Mass left, Number right)
                => (left.Value - right.Value).AsExpression(),
            (Mass left, Mass right)
                => (left.Value - right.Value).AsExpression(),

            (Number left, Length right)
                => (left.Value - right.Value).AsExpression(),
            (Length left, Number right)
                => (left.Value - right.Value).AsExpression(),
            (Length left, Length right)
                => (left.Value - right.Value).AsExpression(),

            (Number left, Time right)
                => (left.Value - right.Value).AsExpression(),
            (Time left, Number right)
                => (left.Value - right.Value).AsExpression(),
            (Time left, Time right)
                => (left.Value - right.Value).AsExpression(),

            (Number left, Area right)
                => (left.Value - right.Value).AsExpression(),
            (Area left, Number right)
                => (left.Value - right.Value).AsExpression(),
            (Area left, Area right)
                => (left.Value - right.Value).AsExpression(),

            (Number left, Volume right)
                => (left.Value - right.Value).AsExpression(),
            (Volume left, Number right)
                => (left.Value - right.Value).AsExpression(),
            (Volume left, Volume right)
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
    public override IExpression Analyze(CallExpression exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var function = exp.Function.Analyze(this);
        var isChanged = IsChanged(exp.Function, function);
        var parameters = exp.Parameters;

        for (var i = 0; i < exp.Parameters.Length; i++)
        {
            var parameter = exp.Parameters[i];
            var simplified = parameter.Analyze(this);
            if (IsChanged(parameter, simplified))
            {
                parameters = parameters.SetItem(i, simplified);
                isChanged = true;
            }
        }

        if (isChanged)
            return exp.Clone(function, parameters);

        return exp;
    }

    /// <inheritdoc />
    public override IExpression Analyze(LambdaExpression exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var body = exp.Lambda.Body.Analyze(this);
        if (IsChanged(exp.Lambda.Body, body))
        {
            return exp.Clone(new Lambda(exp.Lambda.Parameters, body));
        }

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

        for (var i = 0; i < exp.Vectors.Length; i++)
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
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);
        if (argument is Arccos { Argument: Number inverseNumber } arccos &&
            Arccos.Domain.IsInRange(inverseNumber.Value))
            return arccos.Argument;

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Cot exp)
        => AnalyzeTrigonometric<Arccot>(exp);

    /// <inheritdoc />
    public override IExpression Analyze(Csc exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);
        if (argument is Arccsc { Argument: Number inverseNumber } arccsc &&
            Arccsc.Domain.IsInRange(inverseNumber.Value))
            return arccsc.Argument;

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sec exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);
        if (argument is Arcsec { Argument: Number inverseNumber } arcsec &&
            Arcsec.Domain.IsInRange(inverseNumber.Value))
            return arcsec.Argument;

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sin exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);
        if (argument is Arcsin { Argument: Number inverseNumber } arcsin &&
            Arcsin.Domain.IsInRange(inverseNumber.Value))
            return arcsin.Argument;

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

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
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);
        if (argument is Arcosh { Argument: Number inverseNumber } arcosh &&
            Arcosh.Domain.IsInRange(inverseNumber.Value))
            return arcosh.Argument;

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Coth exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);
        if (argument is Arcoth { Argument: Number inverseNumber } arcoth &&
            Arcoth.Domain.IsInRange(inverseNumber.Value))
            return arcoth.Argument;

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Csch exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);
        if (argument is Arcsch { Argument: Number inverseNumber } arcsch &&
            Arcsch.Domain.IsInRange(inverseNumber.Value))
            return arcsch.Argument;

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sech exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);
        if (argument is Arsech { Argument: Number inverseNumber } arsech &&
            Arsech.Domain.IsInRange(inverseNumber.Value))
            return arsech.Argument;

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sinh exp)
        => AnalyzeTrigonometric<Arsinh>(exp);

    /// <inheritdoc />
    public override IExpression Analyze(Tanh exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var argument = AnalyzeUnaryArgument(exp);
        if (argument is Artanh { Argument: Number inverseNumber } artanh &&
            Artanh.Domain.IsInRange(inverseNumber.Value))
            return artanh.Argument;

        if (IsChanged(exp, argument))
            return exp.Clone(argument);

        return exp;
    }

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