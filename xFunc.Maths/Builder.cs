// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths;

#pragma warning disable CA1815

/// <summary>
/// The expression builder.
/// </summary>
public struct Builder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Builder"/> struct.
    /// </summary>
    /// <param name="initial">The initial value of builder.</param>
    public Builder(IExpression initial)
        => Expression = initial ?? throw new ArgumentNullException(nameof(initial));

    /// <summary>
    /// Initializes a new instance of the <see cref="Builder"/> struct.
    /// </summary>
    /// <param name="number">The initial value of builder.</param>
    public Builder(double number) => Expression = new Number(number);

    /// <summary>
    /// Initializes a new instance of the <see cref="Builder"/> struct.
    /// </summary>
    /// <param name="variable">The initial value of builder.</param>
    public Builder(string variable) => Expression = new Variable(variable);

    /// <summary>
    /// Inserts a custom expression to builder.
    /// </summary>
    /// <param name="customExpression">The custom expression.</param>
    /// <returns>The Current instance of builder.</returns>
    public Builder Custom(Func<IExpression, IExpression> customExpression)
    {
        if (customExpression is null)
            throw new ArgumentNullException(nameof(customExpression));

        Expression = customExpression(Expression);

        return this;
    }

    #region Standart

    /// <summary>
    /// Creates the <seealso cref="Expressions.Abs"/> function. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Abs()
    {
        Expression = new Abs(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Add"/> operation. The Current state is used as summand.
    /// </summary>
    /// <param name="summand">The summand (expression).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Add(IExpression summand)
    {
        Expression = new Add(Expression, summand);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Add"/> operation. The Current state is used as summand.
    /// </summary>
    /// <param name="summand">The summand (number).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Add(double summand)
        => Add((IExpression)new Number(summand));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Add"/> operation. The Current state is used as summand.
    /// </summary>
    /// <param name="summand">The summand (variable).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Add(string summand)
        => Add((IExpression)new Variable(summand));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Div"/> operation. The Current state is used as numerator.
    /// </summary>
    /// <param name="denominator">The denominator (expression).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Div(IExpression denominator)
    {
        Expression = new Div(Expression, denominator);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Div"/> operation. The Current state is used as numerator.
    /// </summary>
    /// <param name="denominator">The denominator (number).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Div(double denominator)
        => Div((IExpression)new Number(denominator));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Div"/> operation. The Current state is used as numerator.
    /// </summary>
    /// <param name="denominator">The denominator (variable).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Div(string denominator)
        => Div((IExpression)new Variable(denominator));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Exp"/> function. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Exp()
    {
        Expression = new Exp(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Mul"/> operation. The Current state is used as factor.
    /// </summary>
    /// <param name="factor">The factor (expression).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Mul(IExpression factor)
    {
        Expression = new Mul(Expression, factor);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Mul"/> operation. The Current state is used as factor.
    /// </summary>
    /// <param name="factor">The factor (number).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Mul(double factor)
        => Mul((IExpression)new Number(factor));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Mul"/> operation. The Current state is used as factor.
    /// </summary>
    /// <param name="factor">The factor (variable).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Mul(string factor)
        => Mul((IExpression)new Variable(factor));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Log"/> operation. The Current state is used as argument.
    /// </summary>
    /// <param name="base">The base (expression).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Log(IExpression @base)
    {
        Expression = new Log(@base, Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Log"/> operation. The Current state is used as argument.
    /// </summary>
    /// <param name="base">The base (number).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Log(double @base)
        => Log((IExpression)new Number(@base));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Log"/> operation. The Current state is used as argument.
    /// </summary>
    /// <param name="base">The base (variable).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Log(string @base)
        => Log((IExpression)new Variable(@base));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Ln"/> function. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Ln()
    {
        Expression = new Ln(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Lg"/> function. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Lg()
    {
        Expression = new Lg(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Lb"/> function. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Lb()
    {
        Expression = new Lb(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Pow"/> operation. The Current state is used as base of power.
    /// </summary>
    /// <param name="exponent">The exponent (expression).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Pow(IExpression exponent)
    {
        Expression = new Pow(Expression, exponent);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Pow"/> operation. The Current state is used as base of power.
    /// </summary>
    /// <param name="exponent">The exponent (number).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Pow(double exponent)
        => Pow((IExpression)new Number(exponent));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Pow"/> operation. The Current state is used as base of power.
    /// </summary>
    /// <param name="exponent">The exponent (variable).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Pow(string exponent)
        => Pow((IExpression)new Variable(exponent));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Root"/> operation. The Current state is used as radicand.
    /// </summary>
    /// <param name="degree">The degree (expression).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Root(IExpression degree)
    {
        Expression = new Root(Expression, degree);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Root"/> operation. The Current state is used as radicand.
    /// </summary>
    /// <param name="degree">The degree (number).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Root(double degree)
        => Root((IExpression)new Number(degree));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Root"/> operation. The Current state is used as radicand.
    /// </summary>
    /// <param name="degree">The degree (variable).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Root(string degree)
        => Root((IExpression)new Variable(degree));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Sqrt"/> function. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Sqrt()
    {
        Expression = new Sqrt(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Sub"/> operation. The Current state is used as minuend.
    /// </summary>
    /// <param name="subtrahend">The subtrahend (expression).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Sub(IExpression subtrahend)
    {
        Expression = new Sub(Expression, subtrahend);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Sub"/> operation. The Current state is used as minuend.
    /// </summary>
    /// <param name="subtrahend">The subtrahend (number).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Sub(double subtrahend)
        => Sub((IExpression)new Number(subtrahend));

    /// <summary>
    /// Creates the <seealso cref="Expressions.Sub"/> operation. The Current state is used as minuend.
    /// </summary>
    /// <param name="subtrahend">The subtrahend (variable).</param>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Sub(string subtrahend)
        => Sub((IExpression)new Variable(subtrahend));

    #endregion Standart

    #region Trigonometric

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Sin"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Sin()
    {
        Expression = new Sin(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Cos"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Cos()
    {
        Expression = new Cos(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Tan"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Tan()
    {
        Expression = new Tan(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Cot"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Cot()
    {
        Expression = new Cot(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Sec"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Sec()
    {
        Expression = new Sec(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Csc"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Csc()
    {
        Expression = new Csc(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Arcsin"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arcsin()
    {
        Expression = new Arcsin(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Arccos"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arccos()
    {
        Expression = new Arccos(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Arctan"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arctan()
    {
        Expression = new Arctan(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Arccot"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arccot()
    {
        Expression = new Arccot(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Arcsec"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arcsec()
    {
        Expression = new Arcsec(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Trigonometric.Arccsc"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arccsc()
    {
        Expression = new Arccsc(Expression);

        return this;
    }

    #endregion Trigonometric

    #region Hyperbolic

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Sinh"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Sinh()
    {
        Expression = new Sinh(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Cosh"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Cosh()
    {
        Expression = new Cosh(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Tanh"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Tanh()
    {
        Expression = new Tanh(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Coth"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Coth()
    {
        Expression = new Coth(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Sech"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Sech()
    {
        Expression = new Sech(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Csch"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Csch()
    {
        Expression = new Csch(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Arsinh"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arsinh()
    {
        Expression = new Arsinh(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Arcosh"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arcosh()
    {
        Expression = new Arcosh(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Artanh"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Artanh()
    {
        Expression = new Artanh(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Arcoth"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arcoth()
    {
        Expression = new Arcoth(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Arsech"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arsech()
    {
        Expression = new Arsech(Expression);

        return this;
    }

    /// <summary>
    /// Creates the <seealso cref="Expressions.Hyperbolic.Arcsch"/> operation. The Current state is used as argument.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
    public Builder Arcsch()
    {
        Expression = new Arcsch(Expression);

        return this;
    }

    #endregion Hyperbolic

    /// <summary>
    /// Gets the current expression.
    /// </summary>
    public IExpression Expression { get; private set; }
}

#pragma warning restore CA1815