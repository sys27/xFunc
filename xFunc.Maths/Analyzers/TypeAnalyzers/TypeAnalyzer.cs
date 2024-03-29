// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Analyzers.TypeAnalyzers;

/// <summary>
/// Type Analyzer checks the expression tree for argument type and result type. If result type is Undefined, then Type Analyzer cannot determine the right type and bypass current expression.
/// </summary>
/// <seealso cref="ITypeAnalyzer" />
/// <seealso cref="IAnalyzer{ResultType}" />
/// <seealso cref="ResultTypes" />
public class TypeAnalyzer : ITypeAnalyzer
{
    private const ResultTypes NumberOrComplex = ResultTypes.Number | ResultTypes.ComplexNumber;

    private const ResultTypes NumberOrAngle = ResultTypes.Number | ResultTypes.AngleNumber;

    private const ResultTypes NumberOrPower = ResultTypes.Number | ResultTypes.PowerNumber;

    private const ResultTypes NumberOrTemperature = ResultTypes.Number | ResultTypes.TemperatureNumber;

    private const ResultTypes NumberOrMass = ResultTypes.Number | ResultTypes.MassNumber;

    private const ResultTypes NumberOrLength = ResultTypes.Number | ResultTypes.LengthNumber;

    private const ResultTypes NumberOrTime = ResultTypes.Number | ResultTypes.TimeNumber;

    private const ResultTypes NumberOrArea = ResultTypes.Number | ResultTypes.AreaNumber;

    private const ResultTypes NumberOrVolume = ResultTypes.Number | ResultTypes.VolumeNumber;

    private const ResultTypes NumberOrAngleOrComplex =
        NumberOrAngle |
        ResultTypes.ComplexNumber;

    private const ResultTypes NumberOrVectorOrMatrix =
        ResultTypes.Number |
        ResultTypes.Vector |
        ResultTypes.Matrix;

    private const ResultTypes Units =
        ResultTypes.AngleNumber |
        ResultTypes.PowerNumber |
        ResultTypes.TemperatureNumber |
        ResultTypes.MassNumber |
        ResultTypes.LengthNumber |
        ResultTypes.TimeNumber |
        ResultTypes.AreaNumber |
        ResultTypes.VolumeNumber;

    private const ResultTypes NumberOrUnits = ResultTypes.Number | Units;

    private const ResultTypes NumbersOrComplex =
        NumberOrUnits |
        ResultTypes.ComplexNumber;

    private const ResultTypes NumbersOrComplexOrVector =
        NumbersOrComplex |
        ResultTypes.Vector;

    private ResultTypes CheckArgument([NotNull] IExpression? exp, ResultTypes result)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        return result;
    }

    private ResultTypes CheckNumericConversion([NotNull] UnaryExpression? exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number or ResultTypes.AngleNumber => ResultTypes.Number,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            _ => NumberOrAngleOrComplex.ThrowFor(result),
        };
    }

    private ResultTypes CheckInverseTrigonometric([NotNull] UnaryExpression? exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.AngleNumber,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            _ => NumberOrComplex.ThrowFor(result),
        };
    }

    private ResultTypes CheckStatistical([NotNull] DifferentParametersExpression? exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        if (exp.ParametersCount == 1)
        {
            var result = exp[0].Analyze(this);
            if (result is ResultTypes.Number or ResultTypes.Vector)
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
            throw new ArgumentNullException(nameof(exp));

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, ResultTypes.Undefined) or
                (ResultTypes.Number, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.Number) or
                (ResultTypes.Number, ResultTypes.Number) or
                (ResultTypes.AngleNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.AngleNumber) or
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) or
                (ResultTypes.PowerNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.PowerNumber) or
                (ResultTypes.PowerNumber, ResultTypes.PowerNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.TemperatureNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.TemperatureNumber) or
                (ResultTypes.MassNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.MassNumber) or
                (ResultTypes.MassNumber, ResultTypes.MassNumber) or
                (ResultTypes.LengthNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.LengthNumber) or
                (ResultTypes.LengthNumber, ResultTypes.LengthNumber) or
                (ResultTypes.TimeNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.TimeNumber) or
                (ResultTypes.TimeNumber, ResultTypes.TimeNumber) or
                (ResultTypes.AreaNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.AreaNumber) or
                (ResultTypes.AreaNumber, ResultTypes.AreaNumber) or
                (ResultTypes.VolumeNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.VolumeNumber) or
                (ResultTypes.VolumeNumber, ResultTypes.VolumeNumber)
                => ResultTypes.Boolean,

            (_, ResultTypes.Number) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.Number, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.AngleNumber) => ResultTypes.AngleNumber.ThrowForLeft(leftResult),
            (ResultTypes.AngleNumber, _) => ResultTypes.AngleNumber.ThrowForRight(rightResult),

            (_, ResultTypes.PowerNumber) => ResultTypes.PowerNumber.ThrowForLeft(leftResult),
            (ResultTypes.PowerNumber, _) => ResultTypes.PowerNumber.ThrowForRight(rightResult),

            (_, ResultTypes.TemperatureNumber) => ResultTypes.TemperatureNumber.ThrowForLeft(leftResult),
            (ResultTypes.TemperatureNumber, _) => ResultTypes.TemperatureNumber.ThrowForRight(rightResult),

            (_, ResultTypes.MassNumber) => ResultTypes.MassNumber.ThrowForLeft(leftResult),
            (ResultTypes.MassNumber, _) => ResultTypes.MassNumber.ThrowForRight(rightResult),

            (_, ResultTypes.LengthNumber) => ResultTypes.LengthNumber.ThrowForLeft(leftResult),
            (ResultTypes.LengthNumber, _) => ResultTypes.LengthNumber.ThrowForRight(rightResult),

            (_, ResultTypes.TimeNumber) => ResultTypes.TimeNumber.ThrowForLeft(leftResult),
            (ResultTypes.TimeNumber, _) => ResultTypes.TimeNumber.ThrowForRight(rightResult),

            (_, ResultTypes.AreaNumber) => ResultTypes.AreaNumber.ThrowForLeft(leftResult),
            (ResultTypes.AreaNumber, _) => ResultTypes.AreaNumber.ThrowForRight(rightResult),

            (_, ResultTypes.VolumeNumber) => ResultTypes.VolumeNumber.ThrowForLeft(leftResult),
            (ResultTypes.VolumeNumber, _) => ResultTypes.VolumeNumber.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    private ResultTypes AnalyzeLogical([NotNull] BinaryExpression? exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

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
                (ResultTypes.MassNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.MassNumber) or
                (ResultTypes.LengthNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.LengthNumber) or
                (ResultTypes.TimeNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.TimeNumber) or
                (ResultTypes.AreaNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.AreaNumber) or
                (ResultTypes.VolumeNumber, ResultTypes.Undefined) or
                (ResultTypes.Undefined, ResultTypes.VolumeNumber) or
                (ResultTypes.Number, ResultTypes.Number) or
                (ResultTypes.Boolean, ResultTypes.Boolean) or
                (ResultTypes.AngleNumber, ResultTypes.AngleNumber) or
                (ResultTypes.PowerNumber, ResultTypes.PowerNumber) or
                (ResultTypes.TemperatureNumber, ResultTypes.TemperatureNumber) or
                (ResultTypes.MassNumber, ResultTypes.MassNumber) or
                (ResultTypes.LengthNumber, ResultTypes.LengthNumber) or
                (ResultTypes.TimeNumber, ResultTypes.TimeNumber) or
                (ResultTypes.AreaNumber, ResultTypes.AreaNumber) or
                (ResultTypes.VolumeNumber, ResultTypes.VolumeNumber)
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

            (_, ResultTypes.MassNumber) => ResultTypes.MassNumber.ThrowForLeft(leftResult),
            (ResultTypes.MassNumber, _) => ResultTypes.MassNumber.ThrowForRight(rightResult),

            (_, ResultTypes.LengthNumber) => ResultTypes.LengthNumber.ThrowForLeft(leftResult),
            (ResultTypes.LengthNumber, _) => ResultTypes.LengthNumber.ThrowForRight(rightResult),

            (_, ResultTypes.TimeNumber) => ResultTypes.TimeNumber.ThrowForLeft(leftResult),
            (ResultTypes.TimeNumber, _) => ResultTypes.TimeNumber.ThrowForRight(rightResult),

            (_, ResultTypes.AreaNumber) => ResultTypes.AreaNumber.ThrowForLeft(leftResult),
            (ResultTypes.AreaNumber, _) => ResultTypes.AreaNumber.ThrowForRight(rightResult),

            (_, ResultTypes.VolumeNumber) => ResultTypes.VolumeNumber.ThrowForLeft(leftResult),
            (ResultTypes.VolumeNumber, _) => ResultTypes.VolumeNumber.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    private ResultTypes AnalyzeBinaryAssign([NotNull] VariableBinaryExpression? exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined or ResultTypes.Number or ResultTypes.AngleNumber
                => ResultTypes.AngleNumber,

            _ => NumberOrAngle.ThrowFor(result),
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
            throw new ArgumentNullException(nameof(exp));

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
            ResultTypes.RationalNumber => ResultTypes.RationalNumber,
            _ => NumbersOrComplexOrVector.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Add exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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

            (ResultTypes.RationalNumber, ResultTypes.RationalNumber) or
                (ResultTypes.Number, ResultTypes.RationalNumber) or
                (ResultTypes.RationalNumber, ResultTypes.Number)
                => ResultTypes.RationalNumber,

            (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Vector,
            (ResultTypes.Matrix, ResultTypes.Matrix) => ResultTypes.Matrix,

            (ResultTypes.String, _) or
                (_, ResultTypes.String)
                => ResultTypes.String,

            (_, ResultTypes.Number) => NumbersOrComplex.ThrowForLeft(leftResult),
            (ResultTypes.Number, _) => NumbersOrComplex.ThrowForRight(rightResult),

            (_, ResultTypes.ComplexNumber) => NumberOrComplex.ThrowForLeft(leftResult),
            (ResultTypes.ComplexNumber, _) => NumberOrComplex.ThrowForRight(rightResult),

            (_, ResultTypes.Vector) => ResultTypes.Vector.ThrowForLeft(leftResult),
            (ResultTypes.Vector, _) => ResultTypes.Vector.ThrowForRight(rightResult),

            (_, ResultTypes.Matrix) => ResultTypes.Matrix.ThrowForLeft(leftResult),
            (ResultTypes.Matrix, _) => ResultTypes.Matrix.ThrowForRight(rightResult),

            (_, ResultTypes.AngleNumber) => NumberOrAngle.ThrowForLeft(leftResult),
            (ResultTypes.AngleNumber, _) => NumberOrAngle.ThrowForRight(rightResult),

            (_, ResultTypes.PowerNumber) => NumberOrPower.ThrowForLeft(leftResult),
            (ResultTypes.PowerNumber, _) => NumberOrPower.ThrowForRight(rightResult),

            (_, ResultTypes.TemperatureNumber) => NumberOrTemperature.ThrowForLeft(leftResult),
            (ResultTypes.TemperatureNumber, _) => NumberOrTemperature.ThrowForRight(rightResult),

            (_, ResultTypes.MassNumber) => NumberOrMass.ThrowForLeft(leftResult),
            (ResultTypes.MassNumber, _) => NumberOrMass.ThrowForRight(rightResult),

            (_, ResultTypes.LengthNumber) => NumberOrLength.ThrowForLeft(leftResult),
            (ResultTypes.LengthNumber, _) => NumberOrLength.ThrowForRight(rightResult),

            (_, ResultTypes.TimeNumber) => NumberOrTime.ThrowForLeft(leftResult),
            (ResultTypes.TimeNumber, _) => NumberOrTime.ThrowForRight(rightResult),

            (_, ResultTypes.AreaNumber) => NumberOrArea.ThrowForLeft(leftResult),
            (ResultTypes.AreaNumber, _) => NumberOrArea.ThrowForRight(rightResult),

            (_, ResultTypes.VolumeNumber) => NumberOrVolume.ThrowForLeft(leftResult),
            (ResultTypes.VolumeNumber, _) => NumberOrVolume.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Ceil exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            _ => NumberOrUnits.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Assign exp)
        => CheckArgument(exp, ResultTypes.String);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Del exp)
        => CheckArgument(exp, ResultTypes.Function);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Derivative exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        if (exp.ParametersCount == 1)
            return ResultTypes.Function;

        if (exp.ParametersCount == 2 && exp[1] is Variable)
            return ResultTypes.Function;

        if (exp.ParametersCount == 3 && exp[1] is Variable && exp[2] is Number)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException();
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Div exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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

            (ResultTypes.RationalNumber, ResultTypes.RationalNumber) or
                (ResultTypes.Number, ResultTypes.RationalNumber) or
                (ResultTypes.RationalNumber, ResultTypes.Number)
                => ResultTypes.RationalNumber,

            (_, ResultTypes.Number) => NumbersOrComplex.ThrowForLeft(leftResult),
            (ResultTypes.Number, _) => NumbersOrComplex.ThrowForRight(rightResult),

            (_, ResultTypes.ComplexNumber) => NumberOrComplex.ThrowForLeft(leftResult),
            (ResultTypes.ComplexNumber, _) => NumberOrComplex.ThrowForRight(rightResult),

            (ResultTypes.AngleNumber, _) or
                (ResultTypes.PowerNumber, _) or
                (ResultTypes.TemperatureNumber, _) or
                (ResultTypes.MassNumber, _) or
                (ResultTypes.LengthNumber, _) or
                (ResultTypes.TimeNumber, _) or
                (ResultTypes.AreaNumber, _) or
                (ResultTypes.VolumeNumber, _)
                => ResultTypes.Number.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Exp exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            _ => NumberOrComplex.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Fact exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.Number)
            return ResultTypes.Number;

        return ResultTypes.Number.ThrowFor(result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Floor exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            _ => NumberOrUnits.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Trunc exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            _ => NumberOrUnits.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Frac exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            _ => NumberOrUnits.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(GCD exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.Number or ResultTypes.RationalNumber)
            return ResultTypes.Number;

        return (ResultTypes.Number | ResultTypes.RationalNumber).ThrowFor(result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(LCM exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            ResultTypes.RationalNumber => ResultTypes.Number,

            _ => NumberOrComplex.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Ln exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);

        return result switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.Number,
            ResultTypes.ComplexNumber => ResultTypes.ComplexNumber,
            ResultTypes.RationalNumber => ResultTypes.Number,

            _ => NumberOrComplex.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Log exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or
                (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Number, ResultTypes.Number) => ResultTypes.Number,
            (ResultTypes.Number, ResultTypes.ComplexNumber) => ResultTypes.ComplexNumber,
            (ResultTypes.Number, ResultTypes.RationalNumber) => ResultTypes.Number,

            (ResultTypes.Number, _) => NumberOrComplex.ThrowForRight(rightResult),

            _ => ResultTypes.Number.ThrowForLeft(leftResult),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Mod exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

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

            (ResultTypes.LengthNumber, ResultTypes.LengthNumber)
                => ResultTypes.AreaNumber,

            (ResultTypes.AreaNumber, ResultTypes.LengthNumber) or
                (ResultTypes.LengthNumber, ResultTypes.AreaNumber)
                => ResultTypes.VolumeNumber,

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

            (ResultTypes.RationalNumber, ResultTypes.RationalNumber) or
                (ResultTypes.Number, ResultTypes.RationalNumber) or
                (ResultTypes.RationalNumber, ResultTypes.Number)
                => ResultTypes.RationalNumber,

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

            (_, ResultTypes.ComplexNumber) => NumberOrComplex.ThrowForLeft(leftResult),
            (ResultTypes.ComplexNumber, _) => NumberOrComplex.ThrowForRight(rightResult),

            (_, ResultTypes.Vector) => NumberOrVectorOrMatrix.ThrowForLeft(leftResult),
            (ResultTypes.Vector, _) => NumberOrVectorOrMatrix.ThrowForRight(rightResult),

            (_, ResultTypes.Matrix) => NumberOrVectorOrMatrix.ThrowForLeft(leftResult),
            (ResultTypes.Matrix, _) => NumberOrVectorOrMatrix.ThrowForRight(rightResult),

            (_, ResultTypes.AngleNumber) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.AngleNumber, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.PowerNumber) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.PowerNumber, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.TemperatureNumber) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.TemperatureNumber, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.MassNumber) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.MassNumber, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.LengthNumber) =>
                (ResultTypes.Number | ResultTypes.LengthNumber | ResultTypes.AreaNumber).ThrowForLeft(leftResult),
            (ResultTypes.LengthNumber, _) =>
                (ResultTypes.Number | ResultTypes.LengthNumber | ResultTypes.AreaNumber).ThrowForRight(rightResult),

            (_, ResultTypes.TimeNumber) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.TimeNumber, _) => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.AreaNumber)
                => (ResultTypes.Number | ResultTypes.LengthNumber).ThrowForLeft(leftResult),
            (ResultTypes.AreaNumber, _)
                => (ResultTypes.Number | ResultTypes.LengthNumber).ThrowForRight(rightResult),

            (_, ResultTypes.VolumeNumber) => ResultTypes.Number.ThrowForLeft(leftResult),
            (ResultTypes.VolumeNumber, _) => ResultTypes.Number.ThrowForRight(rightResult),

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
            throw new ArgumentNullException(nameof(exp));

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
                ResultTypes.VolumeNumber or
                ResultTypes.RationalNumber
                => ResultTypes.Number,
            _ => Units.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Pow exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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

            (ResultTypes.RationalNumber, ResultTypes.Number)
                => ResultTypes.RationalNumber,

            _ => ResultTypes.Number.ThrowForLeft(leftResult),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Root exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

        var number = exp.Argument.Analyze(this);
        var digits = exp.Digits?.Analyze(this) ?? ResultTypes.None;

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
                    ResultTypes.Undefined | NumberOrUnits,
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
        => CheckArgument(exp, ResultTypes.Function);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sqrt exp)
        => CheckArgument(exp, ResultTypes.Undefined);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Sub exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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

            (ResultTypes.RationalNumber, ResultTypes.RationalNumber) or
                (ResultTypes.Number, ResultTypes.RationalNumber) or
                (ResultTypes.RationalNumber, ResultTypes.Number)
                => ResultTypes.RationalNumber,

            (ResultTypes.Vector, ResultTypes.Vector) => ResultTypes.Vector,
            (ResultTypes.Matrix, ResultTypes.Matrix) => ResultTypes.Matrix,

            (_, ResultTypes.Number) => NumbersOrComplex.ThrowForLeft(leftResult),
            (ResultTypes.Number, _) => NumbersOrComplex.ThrowForRight(rightResult),

            (_, ResultTypes.ComplexNumber) => NumberOrComplex.ThrowForLeft(leftResult),
            (ResultTypes.ComplexNumber, _) => NumberOrComplex.ThrowForRight(rightResult),

            (_, ResultTypes.Vector) => ResultTypes.Vector.ThrowForLeft(leftResult),
            (ResultTypes.Vector, _) => ResultTypes.Vector.ThrowForRight(rightResult),

            (_, ResultTypes.Matrix) => ResultTypes.Matrix.ThrowForLeft(leftResult),
            (ResultTypes.Matrix, _) => ResultTypes.Matrix.ThrowForRight(rightResult),

            (_, ResultTypes.AngleNumber) => NumberOrAngle.ThrowForLeft(leftResult),
            (ResultTypes.AngleNumber, _) => NumberOrAngle.ThrowForRight(rightResult),

            (_, ResultTypes.PowerNumber) => NumberOrPower.ThrowForLeft(leftResult),
            (ResultTypes.PowerNumber, _) => NumberOrPower.ThrowForRight(rightResult),

            (_, ResultTypes.TemperatureNumber) => NumberOrTemperature.ThrowForLeft(leftResult),
            (ResultTypes.TemperatureNumber, _) => NumberOrTemperature.ThrowForRight(rightResult),

            (_, ResultTypes.MassNumber) => NumberOrMass.ThrowForLeft(leftResult),
            (ResultTypes.MassNumber, _) => NumberOrMass.ThrowForRight(rightResult),

            (_, ResultTypes.LengthNumber) => NumberOrLength.ThrowForLeft(leftResult),
            (ResultTypes.LengthNumber, _) => NumberOrLength.ThrowForRight(rightResult),

            (_, ResultTypes.TimeNumber) => NumberOrTime.ThrowForLeft(leftResult),
            (ResultTypes.TimeNumber, _) => NumberOrTime.ThrowForRight(rightResult),

            (_, ResultTypes.AreaNumber) => NumberOrArea.ThrowForLeft(leftResult),
            (ResultTypes.AreaNumber, _) => NumberOrArea.ThrowForRight(rightResult),

            (_, ResultTypes.VolumeNumber) => NumberOrVolume.ThrowForLeft(leftResult),
            (ResultTypes.VolumeNumber, _) => NumberOrVolume.ThrowForRight(rightResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(UnaryMinus exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            ResultTypes.RationalNumber => ResultTypes.RationalNumber,
            _ => NumberOrComplex.ThrowFor(result),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Unassign exp)
        => CheckArgument(exp, ResultTypes.String);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(CallExpression exp)
        => CheckArgument(exp, ResultTypes.Undefined);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(LambdaExpression exp)
        => CheckArgument(exp, ResultTypes.Function);

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Curry exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        if (exp.Parameters.Length == 0)
        {
            var functionResult = exp.Function.Analyze(this);
            if (functionResult == ResultTypes.Function)
                return ResultTypes.Function;
        }

        return ResultTypes.Undefined;
    }

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
            throw new ArgumentNullException(nameof(exp));

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

            _ => NumberOrUnits.ThrowFor(result),
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
            throw new ArgumentNullException(nameof(exp));

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
            ResultTypes.TimeNumber => ResultTypes.TimeNumber,
            ResultTypes.AreaNumber => ResultTypes.AreaNumber,
            ResultTypes.VolumeNumber => ResultTypes.VolumeNumber,

            _ => NumberOrUnits.ThrowFor(valueResult),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Rational exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var leftResult = exp.Left.Analyze(this);
        var rightResult = exp.Right.Analyze(this);

        return (leftResult, rightResult) switch
        {
            (ResultTypes.Undefined, _) or (_, ResultTypes.Undefined)
                => ResultTypes.Undefined,

            (ResultTypes.Number, ResultTypes.Number)
                => ResultTypes.RationalNumber,

            (ResultTypes.Number, _)
                => ResultTypes.Number.ThrowForRight(rightResult),

            (_, ResultTypes.Number)
                => ResultTypes.Number.ThrowForLeft(leftResult),

            _ => throw new ParameterTypeMismatchException(),
        };
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToRational exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var argumentResult = exp.Argument.Analyze(this);

        return argumentResult switch
        {
            ResultTypes.Undefined => ResultTypes.Undefined,
            ResultTypes.Number => ResultTypes.RationalNumber,

            _ => throw new ParameterTypeMismatchException(ResultTypes.Number, argumentResult),
        };
    }

    #endregion Standard

    #region Matrix

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Vector exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.Matrix)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException(ResultTypes.Matrix, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Inverse exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.Matrix)
            return ResultTypes.Matrix;

        throw new ParameterTypeMismatchException(ResultTypes.Matrix, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Transpose exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.Vector or ResultTypes.Matrix)
            return ResultTypes.Matrix;

        throw new ParameterTypeMismatchException(ResultTypes.Vector | ResultTypes.Matrix, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(DotProduct exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.ComplexNumber)
            return ResultTypes.ComplexNumber;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Im exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.ComplexNumber)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Phase exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.ComplexNumber)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Re exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.ComplexNumber)
            return ResultTypes.Number;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(Reciprocal exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

        var result = exp.Argument.Analyze(this);
        if (result is ResultTypes.Undefined or ResultTypes.ComplexNumber)
            return ResultTypes.ComplexNumber;

        throw new ParameterTypeMismatchException(ResultTypes.ComplexNumber, result);
    }

    /// <inheritdoc />
    public virtual ResultTypes Analyze(ToComplex exp)
    {
        if (exp is null)
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

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
            throw new ArgumentNullException(nameof(exp));

        var rightResult = exp.Condition.Analyze(this);
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