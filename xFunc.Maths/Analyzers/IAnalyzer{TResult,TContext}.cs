// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Analyzers;

/// <summary>
/// The interface for analyzers.
/// </summary>
/// <typeparam name="TResult">The type of the result of analysis.</typeparam>
/// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
public interface IAnalyzer<out TResult, in TContext>
{
    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(IExpression exp, TContext context);

    #region Standard

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Abs exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Add exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Ceil exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Assign exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Del exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Derivative exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Div exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Exp exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Fact exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Floor exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Trunc exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Frac exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(GCD exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Lb exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LCM exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Lg exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Ln exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Log exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Mod exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Mul exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Number exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Angle exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Area exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Power exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Temperature exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Mass exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Length exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Time exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Volume exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToDegree exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToRadian exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToGradian exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToNumber exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Pow exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Root exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Round exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Simplify exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sqrt exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sub exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(UnaryMinus exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Unassign exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(CallExpression exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LambdaExpression exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Variable exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(DelegateExpression exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sign exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToBin exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToOct exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToHex exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(StringExpression exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Expressions.Units.Convert exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Rational exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToRational exp, TContext context);

    #endregion Standard

    #region Matrix

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Vector exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Matrix exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Determinant exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Inverse exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Transpose exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(DotProduct exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(CrossProduct exp, TContext context);

    #endregion Matrix

    #region Complex Numbers

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ComplexNumber exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Conjugate exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Im exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Phase exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Re exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Reciprocal exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToComplex exp, TContext context);

    #endregion Complex Numbers

    #region Trigonometric

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arccos exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arccot exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arccsc exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcsec exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcsin exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arctan exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Cos exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Cot exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Csc exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sec exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sin exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Tan exp, TContext context);

    #endregion

    #region Hyperbolic

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcosh exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcoth exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcsch exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arsech exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arsinh exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Artanh exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Cosh exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Coth exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Csch exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sech exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sinh exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Tanh exp, TContext context);

    #endregion Hyperbolic

    #region Statistical

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Avg exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expresion.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Count exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Max exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Min exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Product exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Stdev exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Stdevp exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sum exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Var exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Varp exp, TContext context);

    #endregion Statistical

    #region Logical and Bitwise

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(And exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Bool exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Equality exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Implication exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(NAnd exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(NOr exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Not exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Or exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(XOr exp, TContext context);

    #endregion Logical and Bitwise

    #region Programming

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(AddAssign exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ConditionalAnd exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Dec exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(DivAssign exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Equal exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(For exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(GreaterOrEqual exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(GreaterThan exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(If exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Inc exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LessOrEqual exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LessThan exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(MulAssign exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(NotEqual exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ConditionalOr exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(SubAssign exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(While exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LeftShift exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(RightShift exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LeftShiftAssign exp, TContext context);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <param name="context">The context.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(RightShiftAssign exp, TContext context);

    #endregion Programming
}