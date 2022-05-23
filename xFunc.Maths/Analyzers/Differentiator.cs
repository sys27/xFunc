// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using static xFunc.Maths.ThrowHelpers;

namespace xFunc.Maths.Analyzers;

/// <summary>
/// The differentiator of expressions.
/// </summary>
/// <seealso cref="Analyzer{TResult}" />
/// <seealso cref="IDifferentiator" />
public class Differentiator : Analyzer<IExpression, DifferentiatorContext>, IDifferentiator
{
    private void ValidateArguments([NotNull] IExpression? exp, [NotNull] DifferentiatorContext? context)
    {
        if (exp is null)
            ArgNull(ExceptionArgument.exp);
        if (context is null)
            ArgNull(ExceptionArgument.context);
    }

    #region Standard

    /// <inheritdoc />
    public override IExpression Analyze(Abs exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Mul(
            exp.Argument.Analyze(this, context),
            new Div(exp.Argument, exp));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Add exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        var hasVariableInLeft = Helpers.HasVariable(exp.Left, context.Variable);
        var hasVariableInRight = Helpers.HasVariable(exp.Right, context.Variable);

        return (hasVariableInLeft, hasVariableInRight) switch
        {
            (true, true) => new Add(exp.Left.Analyze(this, context), exp.Right.Analyze(this, context)),
            (true, _) => exp.Left.Analyze(this, context),
            (_, true) => exp.Right.Analyze(this, context),
            _ => Number.Zero,
        };
    }

    /// <inheritdoc />
    public override IExpression Analyze(Derivative exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        var diff = exp.Expression;
        if (diff is Derivative)
            diff = diff.Analyze(this, context);

        diff = diff.Analyze(this, context);

        return diff;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Div exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        var hasVariableInLeft = Helpers.HasVariable(exp.Left, context.Variable);
        var hasVariableInRight = Helpers.HasVariable(exp.Right, context.Variable);

        return (hasVariableInLeft, hasVariableInRight) switch
        {
            (true, true) =>
                new Div(
                    new Sub(
                        new Mul(exp.Left.Analyze(this, context), exp.Right),
                        new Mul(exp.Left, exp.Right.Analyze(this, context))),
                    new Pow(exp.Right, Number.Two)),

            (true, _) =>
                new Div(exp.Left.Analyze(this, context), exp.Right),

            (_, true) =>
                new Div(
                    new UnaryMinus(new Mul(exp.Left, exp.Right.Analyze(this, context))),
                    new Pow(exp.Right, Number.Two)),

            _ => Number.Zero,
        };
    }

    /// <inheritdoc />
    public override IExpression Analyze(Exp exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Mul(exp.Argument.Analyze(this, context), exp);
    }

    /// <inheritdoc />
    public override IExpression Analyze(Lb exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Mul(
                exp.Argument,
                new Ln(Number.Two)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Lg exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Mul(
                exp.Argument,
                new Ln(new Number(10))));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Ln exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(exp.Argument.Analyze(this, context), exp.Argument);
    }

    /// <inheritdoc />
    public override IExpression Analyze(Log exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (Helpers.HasVariable(exp.Left, context.Variable))
        {
            var div = new Div(
                new Ln(exp.Right),
                new Ln(exp.Left));

            return Analyze(div, context);
        }

        if (Helpers.HasVariable(exp.Right, context.Variable))
        {
            return new Div(
                exp.Right.Analyze(this, context),
                new Mul(
                    exp.Right,
                    new Ln(exp.Left)));
        }

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Mul exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        var hasVariableInLeft = Helpers.HasVariable(exp.Left, context.Variable);
        var hasVariableInRight = Helpers.HasVariable(exp.Right, context.Variable);

        return (hasVariableInLeft, hasVariableInRight) switch
        {
            (true, true) =>
                new Add(
                    new Mul(exp.Left.Analyze(this, context), exp.Right),
                    new Mul(exp.Left, exp.Right.Analyze(this, context))),

            (true, _) =>
                new Mul(exp.Left.Analyze(this, context), exp.Right),

            (_, true) =>
                new Mul(exp.Left, exp.Right.Analyze(this, context)),

            _ => Number.Zero,
        };
    }

    /// <inheritdoc />
    public override IExpression Analyze(Number exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Angle exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Area exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Power exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Temperature exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Mass exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Length exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Time exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Volume exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return Number.Zero;
    }

    /// <inheritdoc />
    public override IExpression Analyze(Pow exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        var hasVariableInLeft = Helpers.HasVariable(exp.Left, context.Variable);
        var hasVariableInRight = Helpers.HasVariable(exp.Right, context.Variable);

        return (hasVariableInLeft, hasVariableInRight) switch
        {
            (true, true) =>
                new Mul(
                    exp,
                    Analyze(new Mul(exp.Right, new Ln(context.Variable)), context)),
            (true, _) =>
                new Mul(
                    exp.Left.Analyze(this, context),
                    new Mul(
                        exp.Right,
                        new Pow(
                            exp.Left,
                            new Sub(exp.Right, Number.One)))),
            (_, true) =>
                new Mul(
                    new Mul(exp, new Ln(exp.Left)),
                    exp.Right.Analyze(this, context)),
            _ => Number.Zero,
        };
    }

    /// <inheritdoc />
    public override IExpression Analyze(Root exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        var pow = new Pow(
            exp.Left,
            new Div(Number.One, exp.Right));

        return Analyze(pow, context);
    }

    /// <inheritdoc />
    public override IExpression Analyze(Simplify exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return exp.Argument.Analyze(this, context);
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sqrt exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Mul(Number.Two, exp));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sub exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        var hasVariableInLeft = Helpers.HasVariable(exp.Left, context.Variable);
        var hasVariableInRight = Helpers.HasVariable(exp.Right, context.Variable);

        return (hasVariableInLeft, hasVariableInRight) switch
        {
            (true, true) => new Sub(exp.Left.Analyze(this, context), exp.Right.Analyze(this, context)),
            (true, _) => exp.Left.Analyze(this, context),
            (_, true) => new UnaryMinus(exp.Right.Analyze(this, context)),
            _ => Number.Zero,
        };
    }

    /// <inheritdoc />
    public override IExpression Analyze(UnaryMinus exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(exp.Argument.Analyze(this, context));
    }

    /// <inheritdoc />
    public override IExpression Analyze(UserFunction exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (context.Parameters is null)
            throw new InvalidOperationException();

        return context.Parameters.Functions[exp].Analyze(this, context);
    }

    /// <inheritdoc />
    public override IExpression Analyze(Variable exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (exp.Equals(context.Variable))
            return Number.One;

        return exp;
    }

    #endregion Standard

    #region Trigonometric

    /// <inheritdoc />
    public override IExpression Analyze(Arccos exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(
            new Div(
                exp.Argument.Analyze(this, context),
                new Sqrt(
                    new Sub(
                        Number.One,
                        new Pow(exp.Argument, Number.Two)))));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Arccot exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(
            new Div(
                exp.Argument.Analyze(this, context),
                new Add(
                    Number.One,
                    new Pow(exp.Argument, Number.Two))));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Arccsc exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(
            new Div(
                exp.Argument.Analyze(this, context),
                new Mul(
                    new Abs(exp.Argument),
                    new Sqrt(
                        new Sub(
                            new Pow(exp.Argument, Number.Two),
                            Number.One)))));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Arcsec exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Mul(
                new Abs(exp.Argument),
                new Sqrt(
                    new Sub(
                        new Pow(exp.Argument, Number.Two),
                        Number.One))));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Arcsin exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Sqrt(
                new Sub(
                    Number.One,
                    new Pow(exp.Argument, Number.Two))));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Arctan exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Add(
                Number.One,
                new Pow(exp.Argument, Number.Two)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Cos exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(
            new Mul(
                new Sin(exp.Argument),
                exp.Argument.Analyze(this, context)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Cot exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(
            new Div(
                exp.Argument.Analyze(this, context),
                new Pow(
                    new Sin(exp.Argument),
                    Number.Two)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Csc exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return new Mul(
            new UnaryMinus(exp.Argument.Analyze(this, context)),
            new Mul(
                new Cot(exp.Argument),
                new Csc(exp.Argument)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sec exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Mul(
            exp.Argument.Analyze(this, context),
            new Mul(
                new Tan(exp.Argument),
                new Sec(exp.Argument)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sin exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Mul(
            new Cos(exp.Argument),
            exp.Argument.Analyze(this, context));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Tan exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Pow(
                new Cos(exp.Argument),
                Number.Two));
    }

    #endregion

    #region Hyperbolic

    /// <inheritdoc />
    public override IExpression Analyze(Arcosh exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Sqrt(
                new Sub(
                    new Pow(exp.Argument, Number.Two),
                    Number.One)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Arcoth exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Sub(
                Number.One,
                new Pow(exp.Argument, Number.Two)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Arcsch exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(
            new Div(
                exp.Argument.Analyze(this, context),
                new Mul(
                    new Abs(exp.Argument),
                    new Sqrt(
                        new Add(
                            Number.One,
                            new Pow(exp.Argument, Number.Two))))));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Arsech exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        return new UnaryMinus(
            new Div(
                exp.Argument.Analyze(this, context),
                new Mul(
                    exp.Argument,
                    new Sqrt(
                        new Sub(
                            Number.One,
                            new Pow(exp.Argument, Number.Two))))));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Arsinh exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Sqrt(
                new Add(
                    new Pow(exp.Argument, Number.Two),
                    Number.One)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Artanh exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Sub(
                Number.One,
                new Pow(exp.Argument, Number.Two)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Cosh exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Mul(
            exp.Argument.Analyze(this, context),
            new Sinh(exp.Argument));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Coth exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(
            new Div(
                exp.Argument.Analyze(this, context),
                new Pow(
                    new Sinh(exp.Argument),
                    Number.Two)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Csch exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(
            new Mul(
                exp.Argument.Analyze(this, context),
                new Mul(
                    new Coth(exp.Argument),
                    exp)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sech exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new UnaryMinus(
            new Mul(
                exp.Argument.Analyze(this, context),
                new Mul(
                    new Tanh(exp.Argument),
                    exp)));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Sinh exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Mul(
            exp.Argument.Analyze(this, context),
            new Cosh(exp.Argument));
    }

    /// <inheritdoc />
    public override IExpression Analyze(Tanh exp, DifferentiatorContext context)
    {
        ValidateArguments(exp, context);

        if (!Helpers.HasVariable(exp, context.Variable))
            return Number.Zero;

        return new Div(
            exp.Argument.Analyze(this, context),
            new Pow(
                new Cosh(exp.Argument),
                Number.Two));
    }

    #endregion Hyperbolic
}