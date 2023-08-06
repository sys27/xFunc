// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace xFunc.Maths.Analyzers.Formatters;

/// <summary>
/// Converts expressions into string.
/// </summary>
/// <seealso cref="IFormatter" />
public class CommonFormatter : IFormatter
{
    /// <summary>
    /// Gets the instance of <see cref="CommonFormatter"/>.
    /// </summary>
    public static CommonFormatter Instance { get; } = new CommonFormatter();

    private string ToString(UnaryExpression exp, string format)
    {
        var arg = exp.Argument.Analyze(this);

        return string.Format(CultureInfo.InvariantCulture, format, arg);
    }

    private string ToString(BinaryExpression exp, string format)
    {
        var left = exp.Left.Analyze(this);
        if (exp.Left is BinaryExpression)
            left = $"({left})";

        var right = exp.Right.Analyze(this);
        if (exp.Right is BinaryExpression)
            right = $"({right})";

        return string.Format(CultureInfo.InvariantCulture, format, left, right);
    }

    private string ToString(VariableBinaryExpression exp, string format)
    {
        var left = exp.Variable.Analyze(this);
        var right = exp.Value.Analyze(this);

        return string.Format(CultureInfo.InvariantCulture, format, left, right);
    }

    private string ToString(DifferentParametersExpression exp, string function)
    {
        var sb = new StringBuilder();

        sb.Append(function).Append('(');
        if (exp.ParametersCount > 0)
        {
            foreach (var item in exp.Arguments)
                sb.Append(item).Append(", ");
            sb.Remove(sb.Length - 2, 2);
        }

        sb.Append(')');

        return sb.ToString();
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public virtual string Analyze(IExpression exp)
        => "IExpression";

    #region Standard

    /// <inheritdoc />
    public virtual string Analyze(Abs exp)
        => ToString(exp, "abs({0})");

    /// <inheritdoc />
    public virtual string Analyze(Add exp)
        => ToString(exp, "{0} + {1}");

    /// <inheritdoc />
    public virtual string Analyze(Ceil exp)
        => ToString(exp, "ceil({0})");

    /// <inheritdoc />
    public virtual string Analyze(Assign exp)
        => $"{exp.Key.Analyze(this)} := {exp.Value.Analyze(this)}";

    /// <inheritdoc />
    public virtual string Analyze(Del exp)
        => ToString(exp, "del({0})");

    /// <inheritdoc />
    public virtual string Analyze(Derivative exp)
        => ToString(exp, "deriv");

    /// <inheritdoc />
    public virtual string Analyze(Div exp)
        => ToString(exp, "{0} / {1}");

    /// <inheritdoc />
    public virtual string Analyze(Exp exp)
        => ToString(exp, "exp({0})");

    /// <inheritdoc />
    public virtual string Analyze(Fact exp)
        => ToString(exp, "{0}!");

    /// <inheritdoc />
    public virtual string Analyze(Floor exp)
        => ToString(exp, "floor({0})");

    /// <inheritdoc />
    public virtual string Analyze(Trunc exp)
        => ToString(exp, "trunc({0})");

    /// <inheritdoc />
    public virtual string Analyze(Frac exp)
        => ToString(exp, "frac({0})");

    /// <inheritdoc />
    public virtual string Analyze(GCD exp)
        => ToString(exp, "gcd");

    /// <inheritdoc />
    public virtual string Analyze(Lb exp)
        => ToString(exp, "lb({0})");

    /// <inheritdoc />
    public virtual string Analyze(LCM exp)
        => ToString(exp, "lcm");

    /// <inheritdoc />
    public virtual string Analyze(Lg exp)
        => ToString(exp, "lg({0})");

    /// <inheritdoc />
    public virtual string Analyze(Ln exp)
        => ToString(exp, "ln({0})");

    /// <inheritdoc />
    public virtual string Analyze(Log exp)
        => ToString(exp, "log({0}, {1})");

    /// <inheritdoc />
    public virtual string Analyze(Mod exp)
        => ToString(exp, "{0} % {1}");

    /// <inheritdoc />
    public virtual string Analyze(Mul exp)
        => ToString(exp, "{0} * {1}");

    /// <inheritdoc />
    public virtual string Analyze(Number exp)
        => exp.Value.ToString();

    /// <inheritdoc />
    public virtual string Analyze(Angle exp)
        => exp.Value.ToString();

    /// <inheritdoc />
    public virtual string Analyze(Area exp)
        => exp.Value.ToString();

    /// <inheritdoc />
    public virtual string Analyze(Power exp)
        => exp.Value.ToString();

    /// <inheritdoc />
    public virtual string Analyze(Temperature exp)
        => exp.Value.ToString();

    /// <inheritdoc />
    public virtual string Analyze(Mass exp)
        => exp.Value.ToString();

    /// <inheritdoc />
    public virtual string Analyze(Length exp)
        => exp.Value.ToString();

    /// <inheritdoc />
    public virtual string Analyze(Time exp)
        => exp.Value.ToString();

    /// <inheritdoc />
    public virtual string Analyze(Volume exp)
        => exp.Value.ToString();

    /// <inheritdoc />
    public virtual string Analyze(ToDegree exp)
        => ToString(exp, "todegree({0})");

    /// <inheritdoc />
    public virtual string Analyze(ToRadian exp)
        => ToString(exp, "toradian({0})");

    /// <inheritdoc />
    public virtual string Analyze(ToGradian exp)
        => ToString(exp, "togradian({0})");

    /// <inheritdoc />
    public virtual string Analyze(ToNumber exp)
        => ToString(exp, "tonumber({0})");

    /// <inheritdoc />
    public virtual string Analyze(Pow exp)
        => ToString(exp, "{0} ^ {1}");

    /// <inheritdoc />
    public virtual string Analyze(Root exp)
        => ToString(exp, "root({0}, {1})");

    /// <inheritdoc />
    public virtual string Analyze(Round exp)
        => ToString(exp, "round");

    /// <inheritdoc />
    public virtual string Analyze(Simplify exp)
        => ToString(exp, "simplify({0})");

    /// <inheritdoc />
    public virtual string Analyze(Sqrt exp)
        => ToString(exp, "sqrt({0})");

    /// <inheritdoc />
    public virtual string Analyze(Sub exp)
        => ToString(exp, "{0} - {1}");

    /// <inheritdoc />
    public virtual string Analyze(UnaryMinus exp)
    {
        if (exp.Argument is BinaryExpression or ComplexNumber)
            return ToString(exp, "-({0})");

        return ToString(exp, "-{0}");
    }

    /// <inheritdoc />
    public virtual string Analyze(Unassign exp)
        => $"undef({exp.Key.Analyze(this)})";

    /// <inheritdoc />
    public virtual string Analyze(CallExpression exp)
    {
        if (exp.Function is LambdaExpression)
            return $"({exp.Function})({string.Join(", ", exp.Parameters)})";

        return $"{exp.Function}({string.Join(", ", exp.Parameters)})";
    }

    /// <inheritdoc />
    public virtual string Analyze(LambdaExpression exp)
        => $"{exp.Lambda}";

    /// <inheritdoc />
    public virtual string Analyze(Variable exp) => exp.Name;

    /// <inheritdoc />
    public virtual string Analyze(DelegateExpression exp)
        => "{Delegate Expression}";

    /// <inheritdoc />
    public virtual string Analyze(Sign exp)
        => ToString(exp, "sign({0})");

    /// <inheritdoc />
    public virtual string Analyze(ToBin exp)
        => ToString(exp, "tobin({0})");

    /// <inheritdoc />
    public virtual string Analyze(ToOct exp)
        => ToString(exp, "tooct({0})");

    /// <inheritdoc />
    public virtual string Analyze(ToHex exp)
        => ToString(exp, "tohex({0})");

    /// <inheritdoc />
    public virtual string Analyze(StringExpression exp)
        => $"'{exp.Value}'";

    /// <inheritdoc />
    public virtual string Analyze(Expressions.Units.Convert exp)
    {
        var value = exp.Value.Analyze(this);
        var unit = exp.Unit.Analyze(this);

        return $"convert({value}, {unit})";
    }

    #endregion Standard

    #region Matrix

    /// <inheritdoc />
    public virtual string Analyze(Vector exp)
    {
        var sb = new StringBuilder();

        sb.Append('{');
        foreach (var item in exp.Arguments)
            sb.Append(item).Append(", ");
        sb.Remove(sb.Length - 2, 2).Append('}');

        return sb.ToString();
    }

    /// <inheritdoc />
    public virtual string Analyze(Matrix exp)
    {
        var sb = new StringBuilder();

        sb.Append('{');
        foreach (var item in exp.Vectors)
            sb.Append(item).Append(", ");
        sb.Remove(sb.Length - 2, 2).Append('}');

        return sb.ToString();
    }

    /// <inheritdoc />
    public virtual string Analyze(Determinant exp)
        => ToString(exp, "det({0})");

    /// <inheritdoc />
    public virtual string Analyze(Inverse exp)
        => ToString(exp, "inverse({0})");

    /// <inheritdoc />
    public virtual string Analyze(Transpose exp)
        => ToString(exp, "transpose({0})");

    /// <inheritdoc />
    public virtual string Analyze(DotProduct exp)
        => ToString(exp, "dotProduct({0}, {1})");

    /// <inheritdoc />
    public virtual string Analyze(CrossProduct exp)
        => ToString(exp, "crossProduct({0}, {1})");

    #endregion Matrix

    #region Complex Numbers

    /// <inheritdoc />
    public virtual string Analyze(ComplexNumber exp)
        => exp.Value.Format();

    /// <inheritdoc />
    public virtual string Analyze(Conjugate exp)
        => ToString(exp, "conjugate({0})");

    /// <inheritdoc />
    public virtual string Analyze(Im exp)
        => ToString(exp, "im({0})");

    /// <inheritdoc />
    public virtual string Analyze(Phase exp)
        => ToString(exp, "phase({0})");

    /// <inheritdoc />
    public virtual string Analyze(Re exp)
        => ToString(exp, "re({0})");

    /// <inheritdoc />
    public virtual string Analyze(Reciprocal exp)
        => ToString(exp, "reciprocal({0})");

    /// <inheritdoc />
    public virtual string Analyze(ToComplex exp)
        => ToString(exp, "tocomplex({0})");

    #endregion Complex Numbers

    #region Trigonometric

    /// <inheritdoc />
    public virtual string Analyze(Arccos exp)
        => ToString(exp, "arccos({0})");

    /// <inheritdoc />
    public virtual string Analyze(Arccot exp)
        => ToString(exp, "arccot({0})");

    /// <inheritdoc />
    public virtual string Analyze(Arccsc exp)
        => ToString(exp, "arccsc({0})");

    /// <inheritdoc />
    public virtual string Analyze(Arcsec exp)
        => ToString(exp, "arcsec({0})");

    /// <inheritdoc />
    public virtual string Analyze(Arcsin exp)
        => ToString(exp, "arcsin({0})");

    /// <inheritdoc />
    public virtual string Analyze(Arctan exp)
        => ToString(exp, "arctan({0})");

    /// <inheritdoc />
    public virtual string Analyze(Cos exp)
        => ToString(exp, "cos({0})");

    /// <inheritdoc />
    public virtual string Analyze(Cot exp)
        => ToString(exp, "cot({0})");

    /// <inheritdoc />
    public virtual string Analyze(Csc exp)
        => ToString(exp, "csc({0})");

    /// <inheritdoc />
    public virtual string Analyze(Sec exp)
        => ToString(exp, "sec({0})");

    /// <inheritdoc />
    public virtual string Analyze(Sin exp)
        => ToString(exp, "sin({0})");

    /// <inheritdoc />
    public virtual string Analyze(Tan exp)
        => ToString(exp, "tan({0})");

    #endregion

    #region Hyperbolic

    /// <inheritdoc />
    public virtual string Analyze(Arcosh exp)
        => ToString(exp, "arcosh({0})");

    /// <inheritdoc />
    public virtual string Analyze(Arcoth exp)
        => ToString(exp, "arcoth({0})");

    /// <inheritdoc />
    public virtual string Analyze(Arcsch exp)
        => ToString(exp, "arcsch({0})");

    /// <inheritdoc />
    public virtual string Analyze(Arsech exp)
        => ToString(exp, "arsech({0})");

    /// <inheritdoc />
    public virtual string Analyze(Arsinh exp)
        => ToString(exp, "arsinh({0})");

    /// <inheritdoc />
    public virtual string Analyze(Artanh exp)
        => ToString(exp, "artanh({0})");

    /// <inheritdoc />
    public virtual string Analyze(Cosh exp)
        => ToString(exp, "cosh({0})");

    /// <inheritdoc />
    public virtual string Analyze(Coth exp)
        => ToString(exp, "coth({0})");

    /// <inheritdoc />
    public virtual string Analyze(Csch exp)
        => ToString(exp, "csch({0})");

    /// <inheritdoc />
    public virtual string Analyze(Sech exp)
        => ToString(exp, "sech({0})");

    /// <inheritdoc />
    public virtual string Analyze(Sinh exp)
        => ToString(exp, "sinh({0})");

    /// <inheritdoc />
    public virtual string Analyze(Tanh exp)
        => ToString(exp, "tanh({0})");

    #endregion Hyperbolic

    #region Statistical

    /// <inheritdoc />
    public virtual string Analyze(Avg exp)
        => ToString(exp, "avg");

    /// <summary>
    /// Analyzes the specified expression.
    /// </summary>
    /// <param name="exp">The expresion.</param>
    /// <returns>The result of analysis.</returns>
    public virtual string Analyze(Count exp)
        => ToString(exp, "count");

    /// <inheritdoc />
    public virtual string Analyze(Max exp)
        => ToString(exp, "max");

    /// <inheritdoc />
    public virtual string Analyze(Min exp)
        => ToString(exp, "min");

    /// <inheritdoc />
    public virtual string Analyze(Product exp)
        => ToString(exp, "product");

    /// <inheritdoc />
    public virtual string Analyze(Stdev exp)
        => ToString(exp, "stdev");

    /// <inheritdoc />
    public virtual string Analyze(Stdevp exp)
        => ToString(exp, "stdevp");

    /// <inheritdoc />
    public virtual string Analyze(Sum exp)
        => ToString(exp, "sum");

    /// <inheritdoc />
    public virtual string Analyze(Var exp)
        => ToString(exp, "var");

    /// <inheritdoc />
    public virtual string Analyze(Varp exp)
        => ToString(exp, "varp");

    #endregion Statistical

    #region Logical and Bitwise

    /// <inheritdoc />
    public virtual string Analyze(And exp)
        => ToString(exp, "{0} and {1}");

    /// <inheritdoc />
    public virtual string Analyze(Bool exp)
        => exp.Value.ToString(CultureInfo.InvariantCulture);

    /// <inheritdoc />
    public virtual string Analyze(Equality exp)
        => ToString(exp, "{0} <=> {1}");

    /// <inheritdoc />
    public virtual string Analyze(Implication exp)
        => ToString(exp, "{0} => {1}");

    /// <inheritdoc />
    public virtual string Analyze(NAnd exp)
        => ToString(exp, "{0} nand {1}");

    /// <inheritdoc />
    public virtual string Analyze(NOr exp)
        => ToString(exp, "{0} nor {1}");

    /// <inheritdoc />
    public virtual string Analyze(Not exp)
        => ToString(exp, "not({0})");

    /// <inheritdoc />
    public virtual string Analyze(Or exp)
        => ToString(exp, "{0} or {1}");

    /// <inheritdoc />
    public virtual string Analyze(XOr exp)
        => ToString(exp, "{0} xor {1}");

    #endregion Logical and Bitwise

    #region Programming

    /// <inheritdoc />
    public virtual string Analyze(AddAssign exp)
        => ToString(exp, "{0} += {1}");

    /// <inheritdoc />
    public virtual string Analyze(ConditionalAnd exp)
        => ToString(exp, "{0} && {1}");

    /// <inheritdoc />
    public virtual string Analyze(Dec exp)
    {
        var arg = exp.Variable.Analyze(this);

        return string.Format(CultureInfo.InvariantCulture, "{0}--", arg);
    }

    /// <inheritdoc />
    public virtual string Analyze(DivAssign exp)
        => ToString(exp, "{0} /= {1}");

    /// <inheritdoc />
    public virtual string Analyze(Equal exp)
        => ToString(exp, "{0} == {1}");

    /// <inheritdoc />
    public virtual string Analyze(For exp)
        => ToString(exp, "for");

    /// <inheritdoc />
    public virtual string Analyze(GreaterOrEqual exp)
        => ToString(exp, "{0} >= {1}");

    /// <inheritdoc />
    public virtual string Analyze(GreaterThan exp)
        => ToString(exp, "{0} > {1}");

    /// <inheritdoc />
    public virtual string Analyze(If exp)
        => ToString(exp, "if");

    /// <inheritdoc />
    public virtual string Analyze(Inc exp)
    {
        var arg = exp.Variable.Analyze(this);

        return string.Format(CultureInfo.InvariantCulture, "{0}++", arg);
    }

    /// <inheritdoc />
    public virtual string Analyze(LessOrEqual exp)
        => ToString(exp, "{0} <= {1}");

    /// <inheritdoc />
    public virtual string Analyze(LessThan exp)
        => ToString(exp, "{0} < {1}");

    /// <inheritdoc />
    public virtual string Analyze(MulAssign exp)
        => ToString(exp, "{0} *= {1}");

    /// <inheritdoc />
    public virtual string Analyze(NotEqual exp)
        => ToString(exp, "{0} != {1}");

    /// <inheritdoc />
    public virtual string Analyze(ConditionalOr exp)
        => ToString(exp, "{0} || {1}");

    /// <inheritdoc />
    public virtual string Analyze(SubAssign exp)
        => ToString(exp, "{0} -= {1}");

    /// <inheritdoc />
    public virtual string Analyze(While exp)
        => ToString(exp, "while({0}, {1})");

    /// <inheritdoc />
    public virtual string Analyze(LeftShift exp)
        => ToString(exp, "{0} << {1}");

    /// <inheritdoc />
    public virtual string Analyze(RightShift exp)
        => ToString(exp, "{0} >> {1}");

    /// <inheritdoc />
    public virtual string Analyze(LeftShiftAssign exp)
        => ToString(exp, "{0} <<= {1}");

    /// <inheritdoc />
    public virtual string Analyze(RightShiftAssign exp)
        => ToString(exp, "{0} >>= {1}");

    #endregion Programming
}