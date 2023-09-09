// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Analyzers;

/// <summary>
/// The interface for analyzers.
/// </summary>
/// <typeparam name="TResult">The type of the result of analysis.</typeparam>
public interface IAnalyzer<out TResult>
{
    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(IExpression exp);

    #region Standard

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Abs exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Add exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Ceil exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Assign exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Del exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Derivative exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Div exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Exp exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Fact exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Floor exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Trunc exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Frac exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(GCD exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Lb exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LCM exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Lg exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Ln exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Log exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Mod exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Mul exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Number exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Angle exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Area exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Power exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Temperature exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Mass exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Length exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Time exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Volume exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToDegree exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToRadian exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToGradian exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToNumber exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Pow exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Root exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Round exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Simplify exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sqrt exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sub exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(UnaryMinus exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Unassign exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(CallExpression exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LambdaExpression exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Curry exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Variable exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(DelegateExpression exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sign exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToBin exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToOct exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToHex exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(StringExpression exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Expressions.Units.Convert exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Rational exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToRational exp);

    #endregion Standard

    #region Matrix

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Vector exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Matrix exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Determinant exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Inverse exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Transpose exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(DotProduct exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(CrossProduct exp);

    #endregion Matrix

    #region Complex Numbers

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ComplexNumber exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Conjugate exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Im exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Phase exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Re exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Reciprocal exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ToComplex exp);

    #endregion Complex Numbers

    #region Trigonometric

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arccos exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arccot exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arccsc exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcsec exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcsin exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arctan exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Cos exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Cot exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Csc exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sec exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sin exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Tan exp);

    #endregion

    #region Hyperbolic

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcosh exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcoth exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arcsch exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arsech exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Arsinh exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Artanh exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Cosh exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Coth exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Csch exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sech exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sinh exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Tanh exp);

    #endregion Hyperbolic

    #region Statistical

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Avg exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expresion.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Count exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Max exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Min exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Product exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Stdev exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Stdevp exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Sum exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Var exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Varp exp);

    #endregion Statistical

    #region Logical and Bitwise

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(And exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Bool exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Equality exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Implication exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(NAnd exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(NOr exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Not exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Or exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(XOr exp);

    #endregion Logical and Bitwise

    #region Programming

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(AddAssign exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ConditionalAnd exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Dec exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(DivAssign exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Equal exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(For exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(GreaterOrEqual exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(GreaterThan exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(If exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(Inc exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LessOrEqual exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LessThan exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(MulAssign exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(NotEqual exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(ConditionalOr exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(SubAssign exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(While exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LeftShift exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(RightShift exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(LeftShiftAssign exp);

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expression.</param>
    /// <returns>The result of analysis.</returns>
    TResult Analyze(RightShiftAssign exp);

    #endregion Programming
}