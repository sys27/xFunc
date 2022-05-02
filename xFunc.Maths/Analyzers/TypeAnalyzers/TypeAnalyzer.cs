// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using static xFunc.Maths.ThrowHelpers;

namespace xFunc.Maths.Analyzers.TypeAnalyzers;

/// <summary>
/// Type Analyzer checks the expression tree for argument type and result type. If result type is Undefined, then Type Analyzer cannot determine the right type and bypass current expression.
/// </summary>
/// <seealso cref="ITypeAnalyzer"/>
/// <seealso cref="IAnalyzer{ResultType}" />
public class TypeAnalyzer : ITypeAnalyzer
{
    private ResultTypes CheckArgument([NotNull] IExpression? exp, ResultTypes result)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        return result;
    }

    private ResultTypes CheckNumericConversion([NotNull] UnaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined or ResultTypes.Number => ResultTypes.String,
            _ => ResultTypes.Number.ThrowFor(result),
        };
    }

    private ResultTypes CheckTrigonometric([NotNull] UnaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number or ResultTypes.AngleNumber => ResultTypes.Number,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            _ => ResultTypes.NumberOrAngleOrComplex.ThrowFor(result),
        };
    }

    private ResultTypes CheckInverseTrigonometric([NotNull] UnaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.AngleNumber,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            _ => ResultTypes.NumberOrComplex.ThrowFor(result),
        };
    }

    private ResultTypes CheckStatistical([NotNull] DifferentParametersExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        if (exp.ParametersCount == 1)
        {
            var result = exp[0].Analyze(this);
            if (result == ResultTypes.Number || result == ResultTypes.Vector)
                return ResultTypes.Number;

            throw new DifferentParameterTypeMismatchException(ResultTypes.Number | ResultTypes.Vector, result, 0);
        }

        var enumerator = exp.Arguments.GetEnumerator();
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

    private ResultTypes AnalyzeRelational([NotNull] BinaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, ResultTypes.Undefined) or
                (ResultTypes.Number, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.Number) or
                (ResultTypes.AngleNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.AngleNumber) or
                (ResultTypes.PowerNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.PowerNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.TemperatureNumber) or
                (ResultTypes.Number, ResultTypes.Number) or
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) or
                (ResultTypes.PowerNumber, ResultTypes.PowerNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.TemperatureNumber)
                => ResultTypes.Boolean,

            (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.AngleNumber) => ResultTypes.AngleNumber.ThrowForLeft(leftResult),
            (ResultTypes.AngleNumber, _) => ResultTypes.AngleNumber.ThrowForRight(rightResult),

            (_, ResultTypes.PowerNumber) => ResultTypes.PowerNumber.ThrowForLeft(leftResult),
            (ResultTypes.PowerNumber, _) => ResultTypes.PowerNumber.ThrowForRight(rightResult),

            (_, ResultTypes.TemperatureNumber) => ResultTypes.TemperatureNumber.ThrowForLeft(leftResult),
            (ResultTypes.TemperatureNumber, _) => ResultTypes.TemperatureNumber.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    private ResultTypes AnalyzeLogical([NotNull] BinaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, ResultTypes.Undefined) or
                (ResultTypes.Boolean, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.Boolean) or
                (ResultTypes.Boolean, ResultTypes.Boolean)
                => ResultTypes.Boolean,

            (_, ResultTypes.Boolean) => ResultTypes.Boolean.ThrowForLeft(leftResult),
            (ResultTypes.Boolean, _) => ResultTypes.Boolean.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    private ResultTypes AnalyzeLogicalAndBitwise([NotNull] BinaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,
            (ResultTypes.Boolean, ResultTypes.Boolean) => ResultTypes.Boolean,

            (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.Boolean) => ResultTypes.Boolean.ThrowForLeft(leftResult),
            (ResultTypes.Boolean, _) => ResultTypes.Boolean.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    private ResultTypes AnalyzeEquality([NotNull] BinaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, ResultTypes.Undefined) or
                (ResultTypes.Number, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.Number) or
                (ResultTypes.Boolean, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.Boolean) or
                (ResultTypes.AngleNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.AngleNumber) or
                (ResultTypes.PowerNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.PowerNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.TemperatureNumber) or
                (ResultTypes.Number, ResultTypes.Number) or
                (ResultTypes.Boolean, ResultTypes.Boolean) or
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) or
                (ResultTypes.PowerNumber, ResultTypes.PowerNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.TemperatureNumber)
                => ResultTypes.Boolean,

            (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.Boolean) => ResultTypes.Boolean.ThrowForLeft(leftResult),
            (ResultTypes.Boolean, _) => ResultTypes.Boolean.ThrowForRight(rightResult),

            (_, ResultTypes.AngleNumber) => ResultTypes.AngleNumber.ThrowForLeft(leftResult),
            (ResultTypes.AngleNumber, _) => ResultTypes.AngleNumber.ThrowForRight(rightResult),

            (_, ResultTypes.PowerNumber) => ResultTypes.PowerNumber.ThrowForLeft(leftResult),
            (ResultTypes.PowerNumber, _) => ResultTypes.PowerNumber.ThrowForRight(rightResult),

            (_, ResultTypes.TemperatureNumber) => ResultTypes.TemperatureNumber.ThrowForLeft(leftResult),
            (ResultTypes.TemperatureNumber, _) => ResultTypes.TemperatureNumber.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    private ResultTypes AnalyzeBinaryAssign([NotNull] VariableBinaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var rightResult = exp.Value.Analyze(this);

        return rightResult switch
        {
            ResultTypes.Undefined or ResultTypes.Number
                => ResultTypes.Number,

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    private ResultTypes AnalyzeShift([NotNull] BinaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.Number) or
                (ResultTypes.Number, ResultTypes.Undefined) or
                (ResultTypes.Number, ResultTypes.Number)
                => ResultTypes.Number,

            (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),
            (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
            _ => throw new ParameterTypeMismatchException(),
        };
    }

    private ResultTypes AngleConversion([NotNull] UnaryExpression? exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined or ResultTypes.Number or ResultTypes.AngleNumber
                => ResultTypes.AngleNumber,

            _ => ResultTypes.NumberOrAngle.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public virtual ResultTypes Analyze(IExpression exp)
        => ResultTypes.Undefined;

    #region Standard

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Abs exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number or ResultTypes.ComplexNumber or ResultTypes.Vector
                => ResultTypes.Number,
            ResultTypes.AngleNumber => ResultTypes.AngleNumber,
            ResultTypes.PowerNumber => ResultTypes.PowerNumber,
            ResultTypes.TemperatureNumber => ResultTypes.TemperatureNumber,
            ResultTypes.LengthNumber => ResultTypes.LengthNumber,
            ResultTypes.MassNumber => ResultTypes.MassNumber,
            ResultTypes.TimeNumber => ResultTypes.TimeNumber,
            ResultTypes.AreaNumber => ResultTypes.AreaNumber,
            ResultTypes.VolumeNumber => ResultTypes.VolumeNumber,
            _ => ResultTypes.NumbersOrComplexOrVector.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Add exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

            (ResultTypes.Number, ResultTypes.AngleNumber) or
                (ResultTypes.AngleNumber, ResultTypes.Number) or
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber)
                => ResultTypes.AngleNumber,

            (ResultTypes.Number, ResultTypes.PowerNumber) or
                (ResultTypes.PowerNumber, ResultTypes.Number) or
                (ResultTypes.PowerNumber, ResultTypes.PowerNumber)
                => ResultTypes.PowerNumber,

            (ResultTypes.Number, ResultTypes.TemperatureNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.Number) or
                (ResultTypes.TemperatureNumber, ResultTypes.TemperatureNumber)
                => ResultTypes.TemperatureNumber,

            (ResultTypes.Number, ResultTypes.MassNumber) or
                (ResultTypes.MassNumber, ResultTypes.Number) or
                (ResultTypes.MassNumber, ResultTypes.MassNumber)
                => ResultTypes.MassNumber,

            (ResultTypes.Number, ResultTypes.LengthNumber) or
                (ResultTypes.LengthNumber, ResultTypes.Number) or
                (ResultTypes.LengthNumber, ResultTypes.LengthNumber)
                => ResultTypes.LengthNumber,

            (ResultTypes.Number, ResultTypes.TimeNumber) or
                (ResultTypes.TimeNumber, ResultTypes.Number) or
                (ResultTypes.TimeNumber, ResultTypes.TimeNumber)
                => ResultTypes.TimeNumber,

            (ResultTypes.Number, ResultTypes.AreaNumber) or
                (ResultTypes.AreaNumber, ResultTypes.Number) or
                (ResultTypes.AreaNumber, ResultTypes.AreaNumber)
                => ResultTypes.AreaNumber,

            (ResultTypes.Number, ResultTypes.VolumeNumber) or
                (ResultTypes.VolumeNumber, ResultTypes.Number) or
                (ResultTypes.VolumeNumber, ResultTypes.VolumeNumber)
                => ResultTypes.VolumeNumber,

            (ResultTypes.Number, ResultTypes.ComplexNumber) or
                (ResultTypes.ComplexNumber, ResultTypes.Number) or
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber)
                => ResultTypes.ComplexNumber,

            (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Vector,
            (ResultTypes.Matrix, ResultTypes.Matrix) => ResultTypes.Matrix,

            (ResultTypes.String, _) or
                (_, ResultTypes.String)
                => ResultTypes.String,

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

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Ceil exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.AngleNumber => ResultTypes.AngleNumber,
            ResultTypes.PowerNumber => ResultTypes.PowerNumber,
            ResultTypes.TemperatureNumber => ResultTypes.TemperatureNumber,
            ResultTypes.MassNumber => ResultTypes.MassNumber,
            ResultTypes.LengthNumber => ResultTypes.LengthNumber,
            ResultTypes.TimeNumber => ResultTypes.TimeNumber,
            ResultTypes.AreaNumber => ResultTypes.AreaNumber,
            ResultTypes.VolumeNumber => ResultTypes.VolumeNumber,
            _ => ResultTypes.Numbers.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Define exp)
        => CheckArgument(exp, ResultTypes.String);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Del exp)
        => CheckArgument(exp, ResultTypes.Vector);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Derivative exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        if (exp.ParametersCount == 1)
            return ResultTypes.Expression;

        if (exp.ParametersCount == 2 && exp[1] is Variable)
            return ResultTypes.Expression;

        if (exp.ParametersCount == 3 && exp[1] is Variable && exp[2] is Number)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException();
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Div exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

            (ResultTypes.AngleNumber, ResultTypes.Number)
                => ResultTypes.AngleNumber,

            (ResultTypes.PowerNumber, ResultTypes.Number)
                => ResultTypes.PowerNumber,

            (ResultTypes.TemperatureNumber, ResultTypes.Number)
                => ResultTypes.TemperatureNumber,

            (ResultTypes.MassNumber, ResultTypes.Number)
                => ResultTypes.MassNumber,

            (ResultTypes.LengthNumber, ResultTypes.Number)
                => ResultTypes.LengthNumber,

            (ResultTypes.TimeNumber, ResultTypes.Number)
                => ResultTypes.TimeNumber,

            (ResultTypes.AreaNumber, ResultTypes.Number)
                => ResultTypes.AreaNumber,

            (ResultTypes.VolumeNumber, ResultTypes.Number)
                => ResultTypes.VolumeNumber,

            (ResultTypes.Number, ResultTypes.ComplexNumber) or
                (ResultTypes.ComplexNumber, ResultTypes.Number) or
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber)
                => ResultTypes.ComplexNumber,

            (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.ComplexNumber) => ResultTypes.NumberOrComplex.ThrowForLeft(leftResult),
            (ResultTypes.ComplexNumber, _) => ResultTypes.NumberOrComplex.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Exp exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            _ => ResultTypes.NumberOrComplex.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Fact exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result == ResultTypes.Undefined || result == ResultTypes.Number)
            return ResultTypes.Number;

        return ResultTypes.Number.ThrowFor(result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Floor exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.AngleNumber => ResultTypes.AngleNumber,
            ResultTypes.PowerNumber => ResultTypes.PowerNumber,
            ResultTypes.TemperatureNumber => ResultTypes.TemperatureNumber,
            ResultTypes.MassNumber => ResultTypes.MassNumber,
            ResultTypes.LengthNumber => ResultTypes.LengthNumber,
            ResultTypes.TimeNumber => ResultTypes.TimeNumber,
            ResultTypes.AreaNumber => ResultTypes.AreaNumber,
            ResultTypes.VolumeNumber => ResultTypes.VolumeNumber,
            _ => ResultTypes.NumberOrAngle.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Trunc exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.AngleNumber => ResultTypes.AngleNumber,
            ResultTypes.PowerNumber => ResultTypes.PowerNumber,
            ResultTypes.TemperatureNumber => ResultTypes.TemperatureNumber,
            ResultTypes.MassNumber => ResultTypes.MassNumber,
            ResultTypes.LengthNumber => ResultTypes.LengthNumber,
            ResultTypes.TimeNumber => ResultTypes.TimeNumber,
            ResultTypes.AreaNumber => ResultTypes.AreaNumber,
            ResultTypes.VolumeNumber => ResultTypes.VolumeNumber,
            _ => ResultTypes.NumberOrAngle.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Frac exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.AngleNumber => ResultTypes.AngleNumber,
            ResultTypes.PowerNumber => ResultTypes.PowerNumber,
            ResultTypes.TemperatureNumber => ResultTypes.TemperatureNumber,
            ResultTypes.MassNumber => ResultTypes.MassNumber,
            ResultTypes.LengthNumber => ResultTypes.LengthNumber,
            ResultTypes.TimeNumber => ResultTypes.TimeNumber,
            ResultTypes.AreaNumber => ResultTypes.AreaNumber,
            ResultTypes.VolumeNumber => ResultTypes.VolumeNumber,
            _ => ResultTypes.NumberOrAngle.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(GCD exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var enumerator = exp.Arguments.GetEnumerator();
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

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Lb exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result == ResultTypes.Undefined || result == ResultTypes.Number)
            return ResultTypes.Number;

        return ResultTypes.Number.ThrowFor(result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(LCM exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var enumerator = exp.Arguments.GetEnumerator();
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

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Lg exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            _ => ResultTypes.NumberOrComplex.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Ln exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            _ => ResultTypes.NumberOrComplex.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Log exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,
            (ResultTypes.Number, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,

            (ResultTypes.Number, _) => ResultTypes.NumberOrComplex.ThrowForRight(rightResult),

            _ => ResultTypes.Number.ThrowForLeft(leftResult),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Mod exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.Number) or
                (ResultTypes.Number, ResultTypes.Undefined) or
                (ResultTypes.Number, ResultTypes.Number)
                => ResultTypes.Number,

            (ResultTypes.Undefined, _) => ResultTypes.Number.ThrowForRight(rightResult),
            (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),
            (_, ResultTypes.Undefined) => ResultTypes.Number.ThrowForLeft(leftResult),
            (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Mul exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

            (ResultTypes.Number, ResultTypes.AngleNumber) or
                (ResultTypes.AngleNumber, ResultTypes.Number)
                => ResultTypes.AngleNumber,

            (ResultTypes.Number, ResultTypes.PowerNumber) or
                (ResultTypes.PowerNumber, ResultTypes.Number)
                => ResultTypes.PowerNumber,

            (ResultTypes.Number, ResultTypes.TemperatureNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.Number)
                => ResultTypes.TemperatureNumber,

            (ResultTypes.Number, ResultTypes.MassNumber) or
                (ResultTypes.MassNumber, ResultTypes.Number)
                => ResultTypes.MassNumber,

            (ResultTypes.Number, ResultTypes.LengthNumber) or
                (ResultTypes.LengthNumber, ResultTypes.Number)
                => ResultTypes.LengthNumber,

            (ResultTypes.Number, ResultTypes.TimeNumber) or
                (ResultTypes.TimeNumber, ResultTypes.Number)
                => ResultTypes.TimeNumber,

            (ResultTypes.Number, ResultTypes.AreaNumber) or
                (ResultTypes.AreaNumber, ResultTypes.Number)
                => ResultTypes.AreaNumber,

            (ResultTypes.Number, ResultTypes.VolumeNumber) or
                (ResultTypes.VolumeNumber, ResultTypes.Number)
                => ResultTypes.VolumeNumber,

            (ResultTypes.Number, ResultTypes.ComplexNumber) or
                (ResultTypes.ComplexNumber, ResultTypes.Number) or
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber)
                => ResultTypes.ComplexNumber,

            (ResultTypes.Vector, ResultTypes.Number) or
                (ResultTypes.Number, ResultTypes.Vector) or
                (ResultTypes.Vector, ResultTypes.Vector)
                => ResultTypes.Vector,

            (ResultTypes.Matrix, ResultTypes.Number) or
                (ResultTypes.Matrix, ResultTypes.Vector) or
                (ResultTypes.Number, ResultTypes.Matrix) or
                (ResultTypes.Vector, ResultTypes.Matrix) or
                (ResultTypes.Matrix, ResultTypes.Matrix)
                => ResultTypes.Matrix,

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

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Number exp)
        => CheckArgument(exp, ResultTypes.Number);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Angle exp)
        => CheckArgument(exp, ResultTypes.AngleNumber);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Area exp)
        => CheckArgument(exp, ResultTypes.AreaNumber);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Power exp)
        => CheckArgument(exp, ResultTypes.PowerNumber);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Temperature exp)
        => CheckArgument(exp, ResultTypes.TemperatureNumber);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Mass exp)
        => CheckArgument(exp, ResultTypes.MassNumber);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Length exp)
        => CheckArgument(exp, ResultTypes.LengthNumber);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Time exp)
        => CheckArgument(exp, ResultTypes.TimeNumber);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Volume exp)
        => CheckArgument(exp, ResultTypes.VolumeNumber);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToDegree exp)
        => AngleConversion(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToRadian exp)
        => AngleConversion(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToGradian exp)
        => AngleConversion(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToNumber exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined or
                ResultTypes.AngleNumber or
                ResultTypes.PowerNumber or
                ResultTypes.TemperatureNumber or
                ResultTypes.MassNumber or
                ResultTypes.LengthNumber or
                ResultTypes.TimeNumber or
                ResultTypes.AreaNumber or
                ResultTypes.VolumeNumber
                => ResultTypes.Number,
            _ => ResultTypes.AngleNumber.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Pow exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.ComplexNumber, ResultTypes.Undefined) or
                (ResultTypes.Number, ResultTypes.ComplexNumber) or
                (ResultTypes.ComplexNumber, ResultTypes.Number) or
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber)
                => ResultTypes.ComplexNumber,

            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Number, ResultTypes.Number)
                => ResultTypes.Undefined,

            _ => ResultTypes.Number.ThrowForLeft(leftResult),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Root exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.Number) or
                (ResultTypes.Number, ResultTypes.Undefined) or
                (ResultTypes.Number, ResultTypes.Number)
                => ResultTypes.Undefined,

            (ResultTypes.Undefined, _) => ResultTypes.Number.ThrowForRight(rightResult),
            (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),
            (_, ResultTypes.Undefined) => ResultTypes.Number.ThrowForLeft(leftResult),
            (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Round exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var number = exp.Arguments[0].Analyze(this);
        var digits = exp.Arguments[1]?.Analyze(this) ?? ResultTypes.None;

        if (digits is ResultTypes.None or ResultTypes.Undefined or ResultTypes.Number)
        {
            return number switch
            {
                ResultTypes.Undefined => ResultTypes.Undefined,
                ResultTypes.Number => ResultTypes.Number,
                ResultTypes.AngleNumber => ResultTypes.AngleNumber,
                ResultTypes.PowerNumber => ResultTypes.PowerNumber,
                ResultTypes.TemperatureNumber => ResultTypes.TemperatureNumber,
                ResultTypes.MassNumber => ResultTypes.MassNumber,
                ResultTypes.LengthNumber => ResultTypes.LengthNumber,
                ResultTypes.TimeNumber => ResultTypes.TimeNumber,
                ResultTypes.AreaNumber => ResultTypes.AreaNumber,
                ResultTypes.VolumeNumber => ResultTypes.VolumeNumber,
                _ => throw new DifferentParameterTypeMismatchException(
                    ResultTypes.Undefined | ResultTypes.Numbers,
                    number,
                    0),
            };
        }

        throw new DifferentParameterTypeMismatchException(
            ResultTypes.None | ResultTypes.Undefined | ResultTypes.Number,
            digits,
            1);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Simplify exp)
        => CheckArgument(exp, ResultTypes.Undefined);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sqrt exp)
        => CheckArgument(exp, ResultTypes.Undefined);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sub exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,

            (ResultTypes.Number, ResultTypes.AngleNumber) or
                (ResultTypes.AngleNumber, ResultTypes.Number) or
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber)
                => ResultTypes.AngleNumber,

            (ResultTypes.Number, ResultTypes.PowerNumber) or
                (ResultTypes.PowerNumber, ResultTypes.Number) or
                (ResultTypes.PowerNumber, ResultTypes.PowerNumber)
                => ResultTypes.PowerNumber,

            (ResultTypes.Number, ResultTypes.TemperatureNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.Number) or
                (ResultTypes.TemperatureNumber, ResultTypes.TemperatureNumber)
                => ResultTypes.TemperatureNumber,

            (ResultTypes.Number, ResultTypes.MassNumber) or
                (ResultTypes.MassNumber, ResultTypes.Number) or
                (ResultTypes.MassNumber, ResultTypes.MassNumber)
                => ResultTypes.MassNumber,

            (ResultTypes.Number, ResultTypes.LengthNumber) or
                (ResultTypes.LengthNumber, ResultTypes.Number) or
                (ResultTypes.LengthNumber, ResultTypes.LengthNumber)
                => ResultTypes.LengthNumber,

            (ResultTypes.Number, ResultTypes.TimeNumber) or
                (ResultTypes.TimeNumber, ResultTypes.Number) or
                (ResultTypes.TimeNumber, ResultTypes.TimeNumber)
                => ResultTypes.TimeNumber,

            (ResultTypes.Number, ResultTypes.AreaNumber) or
                (ResultTypes.AreaNumber, ResultTypes.Number) or
                (ResultTypes.AreaNumber, ResultTypes.AreaNumber)
                => ResultTypes.AreaNumber,

            (ResultTypes.Number, ResultTypes.VolumeNumber) or
                (ResultTypes.VolumeNumber, ResultTypes.Number) or
                (ResultTypes.VolumeNumber, ResultTypes.VolumeNumber)
                => ResultTypes.VolumeNumber,

            (ResultTypes.Number, ResultTypes.ComplexNumber) or
                (ResultTypes.ComplexNumber, ResultTypes.Number) or
                (ResultTypes.ComplexNumber, ResultTypes.ComplexNumber)
                => ResultTypes.ComplexNumber,

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

    /// <inheritdoc />
    public virtual ResultTypes Analyze(UnaryMinus exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.AngleNumber => ResultTypes.AngleNumber,
            ResultTypes.PowerNumber => ResultTypes.PowerNumber,
            ResultTypes.TemperatureNumber => ResultTypes.TemperatureNumber,
            ResultTypes.MassNumber => ResultTypes.MassNumber,
            ResultTypes.LengthNumber => ResultTypes.LengthNumber,
            ResultTypes.TimeNumber => ResultTypes.TimeNumber,
            ResultTypes.AreaNumber => ResultTypes.AreaNumber,
            ResultTypes.VolumeNumber => ResultTypes.VolumeNumber,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            _ => ResultTypes.NumberOrComplex.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Undefine exp)
        => CheckArgument(exp, ResultTypes.String);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(UserFunction exp)
        => CheckArgument(exp, ResultTypes.Undefined);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Variable exp)
        => CheckArgument(exp, ResultTypes.Undefined);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(DelegateExpression exp)
        => CheckArgument(exp, ResultTypes.Undefined);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sign exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number or
                ResultTypes.AngleNumber or
                ResultTypes.PowerNumber or
                ResultTypes.TemperatureNumber or
                ResultTypes.MassNumber or
                ResultTypes.LengthNumber or
                ResultTypes.TimeNumber or
                ResultTypes.AreaNumber or
                ResultTypes.VolumeNumber
                => ResultTypes.Number,

            _ => ResultTypes.Numbers.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToBin exp)
        => CheckNumericConversion(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToOct exp)
        => CheckNumericConversion(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToHex exp)
        => CheckNumericConversion(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(StringExpression exp)
        => CheckArgument(exp, ResultTypes.String);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Expressions.Units.Convert exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var valueResult = exp.Value.Analyze(this);

        return valueResult switch
        {
            ResultTypes.Undefined or
                ResultTypes.Number => ResultTypes.Undefined,
            ResultTypes.AngleNumber => ResultTypes.AngleNumber,
            ResultTypes.PowerNumber => ResultTypes.PowerNumber,
            ResultTypes.TemperatureNumber => ResultTypes.TemperatureNumber,
            ResultTypes.MassNumber => ResultTypes.MassNumber,
            ResultTypes.LengthNumber => ResultTypes.LengthNumber,

            _ => ResultTypes.Numbers.ThrowFor(valueResult),
        };
    }

    #endregion Standard

    #region Matrix

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Vector exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var enumerator = exp.Arguments.GetEnumerator();
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

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Matrix exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        foreach (var item in exp.Vectors)
        {
            var result = item.Analyze(this);
            if (result == ResultTypes.Undefined)
                return ResultTypes.Undefined;
        }

        return ResultTypes.Matrix;
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Determinant exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result == ResultTypes.Undefined || result == ResultTypes.Matrix)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException(ResultTypes.Matrix, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Inverse exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result == ResultTypes.Undefined || result == ResultTypes.Matrix)
            return ResultTypes.Matrix;

        throw new ParameterTypeMismatchException(ResultTypes.Matrix, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Transpose exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.Vector or ResultTypes.Matrix)
            return ResultTypes.Matrix;

        throw new ParameterTypeMismatchException(ResultTypes.Vector | ResultTypes.Matrix, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(DotProduct exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Number,

            (ResultTypes.Vector, _) => ResultTypes.Vector.ThrowForRight(rightResult),

            _ => ResultTypes.Vector.ThrowForLeft(leftResult),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(CrossProduct exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Vector,

            (ResultTypes.Vector, _) => ResultTypes.Vector.ThrowForRight(rightResult),

            _ => ResultTypes.Vector.ThrowForLeft(leftResult),
        };
    }

    #endregion Matrix

    #region Complex Numbers

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ComplexNumber exp)
        => CheckArgument(exp, ResultTypes.ComplexNumber);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Conjugate exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.ComplexNumber)
            return ResultTypes.ComplexNumber;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Im exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result == ResultTypes.Undefined || result == ResultTypes.ComplexNumber)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Phase exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result == ResultTypes.Undefined || result == ResultTypes.ComplexNumber)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Re exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result == ResultTypes.Undefined || result == ResultTypes.ComplexNumber)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Reciprocal exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.ComplexNumber)
            return ResultTypes.ComplexNumber;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToComplex exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined or ResultTypes.Number
                => ResultTypes.ComplexNumber,
            _ => ResultTypes.Number.ThrowFor(result),
        };
    }

    #endregion Complex Numbers

    #region Trigonometric

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arccos exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arccot exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arccsc exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arcsec exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arcsin exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arctan exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Cos exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Cot exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Csc exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sec exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sin exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Tan exp)
        => CheckTrigonometric(exp);

    #endregion

    #region Hyperbolic

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arcosh exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arcoth exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arcsch exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arsech exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Arsinh exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Artanh exp)
        => CheckInverseTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Cosh exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Coth exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Csch exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sech exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sinh exp)
        => CheckTrigonometric(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Tanh exp)
        => CheckTrigonometric(exp);

    #endregion Hyperbolic

    #region Statistical

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Avg exp)
        => CheckStatistical(exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    public virtual ResultTypes Analyze(Count exp)
        => CheckStatistical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Max exp)
        => CheckStatistical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Min exp)
        => CheckStatistical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Product exp)
        => CheckStatistical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Stdev exp)
        => CheckStatistical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Stdevp exp)
        => CheckStatistical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sum exp)
        => CheckStatistical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Var exp)
        => CheckStatistical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Varp exp)
        => CheckStatistical(exp);

    #endregion Statistical

    #region Logical and Bitwise

    /// <inheritdoc />
    public virtual ResultTypes Analyze(And exp)
        => AnalyzeLogicalAndBitwise(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Bool exp)
        => CheckArgument(exp, ResultTypes.Boolean);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Equality exp)
        => AnalyzeLogical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Implication exp)
        => AnalyzeLogical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(NAnd exp)
        => AnalyzeLogical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(NOr exp)
        => AnalyzeLogical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Not exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.Boolean => ResultTypes.Boolean,
            _ => throw new ParameterTypeMismatchException(),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Or exp)
        => AnalyzeLogicalAndBitwise(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(XOr exp)
        => AnalyzeLogicalAndBitwise(exp);

    #endregion Logical and Bitwise

    #region Programming

    /// <inheritdoc />
    public virtual ResultTypes Analyze(AddAssign exp)
        => AnalyzeBinaryAssign(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ConditionalAnd exp)
        => AnalyzeLogical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Dec exp)
        => CheckArgument(exp, ResultTypes.Number);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(DivAssign exp)
        => AnalyzeBinaryAssign(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Equal exp)
        => AnalyzeEquality(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(For exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var result = exp.Condition.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.Boolean)
            return ResultTypes.Undefined;

        throw new ParameterTypeMismatchException();
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(GreaterOrEqual exp)
        => AnalyzeRelational(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(GreaterThan exp)
        => AnalyzeRelational(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(If exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var conditionResult = exp.Condition.Analyze(this);
        if (conditionResult == ResultTypes.Undefined)
            return ResultTypes.Undefined;

        var thenResult = exp.Then.Analyze(this);
        if (conditionResult == ResultTypes.Boolean)
            return exp.ParametersCount == 2 ? thenResult : ResultTypes.Undefined;

        throw new DifferentParameterTypeMismatchException(ResultTypes.Boolean, conditionResult, 0);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Inc exp)
        => CheckArgument(exp, ResultTypes.Number);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(LessOrEqual exp)
        => AnalyzeRelational(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(LessThan exp)
        => AnalyzeRelational(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(MulAssign exp)
        => AnalyzeBinaryAssign(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(NotEqual exp)
        => AnalyzeEquality(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ConditionalOr exp)
        => AnalyzeLogical(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(SubAssign exp)
        => AnalyzeBinaryAssign(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(While exp)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);

        var rightResult = exp.Right.Analyze(this);
        if (rightResult is ResultTypes.Undefined or ResultTypes.Boolean)
            return ResultTypes.Undefined;

        throw new ParameterTypeMismatchException();
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(LeftShift exp)
        => AnalyzeShift(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(RightShift exp)
        => AnalyzeShift(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(LeftShiftAssign exp)
        => AnalyzeBinaryAssign(exp);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(RightShiftAssign exp)
        => AnalyzeBinaryAssign(exp);

    #endregion Programming
}