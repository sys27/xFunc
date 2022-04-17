// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Analyzers;

/// <summary>
/// The abstract class with default Analyzer API realization. It's useful where you don't need to implement whole interface (just a few methods).
/// </summary>
/// <typeparam name="TResult">The type of the result.</typeparam>
/// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
/// <seealso cref="IAnalyzer{TResult,TContext}" />
[ExcludeFromCodeCoverage]
public abstract class Analyzer<TResult, TContext> : IAnalyzer<TResult, TContext>
{
    /// <inheritdoc />
    /// <exception cref="NotSupportedException">Always.</exception>
    public virtual TResult Analyze(IExpression exp, TContext context)
        => throw new NotSupportedException();

    #region Standard

    /// <inheritdoc />
    public virtual TResult Analyze(Abs exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Add exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Ceil exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Define exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Del exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Derivative exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Div exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Exp exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Fact exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Floor exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Trunc exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Frac exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(GCD exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Lb exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(LCM exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Lg exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Ln exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Log exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Mod exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Mul exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Number exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Angle exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Area exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Power exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Temperature exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Mass exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Length exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Time exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Volume exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ToDegree exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ToRadian exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ToGradian exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ToNumber exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Pow exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Root exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Round exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Simplify exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Sqrt exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Sub exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(UnaryMinus exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Undefine exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(UserFunction exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Variable exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(DelegateExpression exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Sign exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ToBin exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ToOct exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ToHex exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(StringExpression exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Expressions.Units.Convert exp, TContext context)
        => Analyze(exp as IExpression, context);

    #endregion Standard

    #region Matrix

    /// <inheritdoc />
    public virtual TResult Analyze(Vector exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Matrix exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Determinant exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Inverse exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Transpose exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(DotProduct exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(CrossProduct exp, TContext context)
        => Analyze(exp as IExpression, context);

    #endregion Matrix

    #region Complex Numbers

    /// <inheritdoc />
    public virtual TResult Analyze(ComplexNumber exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Conjugate exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Im exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Phase exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Re exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Reciprocal exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ToComplex exp, TContext context)
        => Analyze(exp as IExpression, context);

    #endregion Complex Numbers

    #region Trigonometric

    /// <inheritdoc />
    public virtual TResult Analyze(Arccos exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Arccot exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Arccsc exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Arcsec exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Arcsin exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Arctan exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Cos exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Cot exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Csc exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Sec exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Sin exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Tan exp, TContext context)
        => Analyze(exp as IExpression, context);

    #endregion

    #region Hyperbolic

    /// <inheritdoc />
    public virtual TResult Analyze(Arcosh exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Arcoth exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Arcsch exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Arsech exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Arsinh exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Artanh exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Cosh exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Coth exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Csch exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Sech exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Sinh exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Tanh exp, TContext context)
        => Analyze(exp as IExpression, context);

    #endregion Hyperbolic

    #region Statistical

    /// <inheritdoc />
    public virtual TResult Analyze(Avg exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expresion.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    public virtual TResult Analyze(Count exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Max exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Min exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Product exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Stdev exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Stdevp exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Sum exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Var exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Varp exp, TContext context)
        => Analyze(exp as IExpression, context);

    #endregion Statistical

    #region Logical and Bitwise

    /// <inheritdoc />
    public virtual TResult Analyze(And exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Bool exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Equality exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Implication exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(NAnd exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(NOr exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Not exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Or exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(XOr exp, TContext context)
        => Analyze(exp as IExpression, context);

    #endregion Logical and Bitwise

    #region Programming

    /// <inheritdoc />
    public virtual TResult Analyze(AddAssign exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ConditionalAnd exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Dec exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(DivAssign exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Equal exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(For exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(GreaterOrEqual exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(GreaterThan exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(If exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(Inc exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(LessOrEqual exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(LessThan exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(MulAssign exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(NotEqual exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(ConditionalOr exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(SubAssign exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(While exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(LeftShift exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(RightShift exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(LeftShiftAssign exp, TContext context)
        => Analyze(exp as IExpression, context);

    /// <inheritdoc />
    public virtual TResult Analyze(RightShiftAssign exp, TContext context)
        => Analyze(exp as IExpression, context);

    #endregion Programming
}