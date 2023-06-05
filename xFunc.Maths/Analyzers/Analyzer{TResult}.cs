// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Analyzers;

/// <summary>
/// The abstract class with default Analyzer API realization. It's useful where you don't need to implement whole interface (just a few methods).
/// </summary>
/// <typeparam name="TResult">The type of the result.</typeparam>
/// <seealso cref="IAnalyzer{TResult}" />
[ExcludeFromCodeCoverage]
public abstract class Analyzer<TResult> : IAnalyzer<TResult>
{
    /// <inheritdoc />
    /// <exception cref="NotSupportedException">Always.</exception>
    public virtual TResult Analyze(IExpression exp)
        => throw new NotSupportedException();

    #region Standard

    /// <inheritdoc />
    public virtual TResult Analyze(Abs exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Add exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Ceil exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Define exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Del exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Derivative exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Div exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Exp exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Fact exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Floor exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Trunc exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Frac exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(GCD exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Lb exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(LCM exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Lg exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Ln exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Log exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Mod exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Mul exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Number exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Angle exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Area exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Power exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Temperature exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Mass exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Length exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Time exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Volume exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ToDegree exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ToRadian exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ToGradian exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ToNumber exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Pow exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Root exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Round exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Simplify exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Sqrt exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Sub exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(UnaryMinus exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Undefine exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(CallExpression exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(LambdaExpression exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Variable exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(DelegateExpression exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Sign exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ToBin exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ToOct exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ToHex exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(StringExpression exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Expressions.Units.Convert exp)
        => Analyze(exp as IExpression);

    #endregion Standard

    #region Matrix

    /// <inheritdoc />
    public virtual TResult Analyze(Vector exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Matrix exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Determinant exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Inverse exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Transpose exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(DotProduct exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(CrossProduct exp)
        => Analyze(exp as IExpression);

    #endregion Matrix

    #region Complex Numbers

    /// <inheritdoc />
    public virtual TResult Analyze(ComplexNumber exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Conjugate exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Im exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Phase exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Re exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Reciprocal exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ToComplex exp)
        => Analyze(exp as IExpression);

    #endregion Complex Numbers

    #region Trigonometric

    /// <inheritdoc />
    public virtual TResult Analyze(Arccos exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Arccot exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Arccsc exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Arcsec exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Arcsin exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Arctan exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Cos exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Cot exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Csc exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Sec exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Sin exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Tan exp)
        => Analyze(exp as IExpression);

    #endregion

    #region Hyperbolic

    /// <inheritdoc />
    public virtual TResult Analyze(Arcosh exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Arcoth exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Arcsch exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Arsech exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Arsinh exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Artanh exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Cosh exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Coth exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Csch exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Sech exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Sinh exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Tanh exp)
        => Analyze(exp as IExpression);

    #endregion Hyperbolic

    #region Statistical

    /// <inheritdoc />
    public virtual TResult Analyze(Avg exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Count exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Max exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Min exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Product exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Stdev exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Stdevp exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Sum exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Var exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Varp exp)
        => Analyze(exp as IExpression);

    #endregion Statistical

    #region Logical and Bitwise

    /// <inheritdoc />
    public virtual TResult Analyze(And exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Bool exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Equality exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Implication exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(NAnd exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(NOr exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Not exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Or exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(XOr exp)
        => Analyze(exp as IExpression);

    #endregion Logical and Bitwise

    #region Programming

    /// <inheritdoc />
    public virtual TResult Analyze(AddAssign exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ConditionalAnd exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Dec exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(DivAssign exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Equal exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(For exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(GreaterOrEqual exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(GreaterThan exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(If exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(Inc exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(LessOrEqual exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(LessThan exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(MulAssign exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(NotEqual exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(ConditionalOr exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(SubAssign exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(While exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(LeftShift exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(RightShift exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(LeftShiftAssign exp)
        => Analyze(exp as IExpression);

    /// <inheritdoc />
    public virtual TResult Analyze(RightShiftAssign exp)
        => Analyze(exp as IExpression);

    #endregion Programming
}