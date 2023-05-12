// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static xFunc.Maths.Tokenization.TokenKind;

namespace xFunc.Maths;

/// <summary>
/// The parser for mathematical expressions.
/// </summary>
public partial class Parser
{
    private IExpression CreateFunction(in Token token, ImmutableArray<IExpression> arguments)
    {
        Debug.Assert(token.IsId(), "Token should be Id.");
        Debug.Assert(!string.IsNullOrWhiteSpace(token.StringValue), "Id is empty.");

        return token.StringValue.ToLowerInvariant() switch
        {
            "add" => new Add(arguments),
            "sub" => new Sub(arguments),
            "mul" => new Mul(arguments),
            "div" => new Div(arguments),
            "pow" => new Pow(arguments),
            "exp" => new Exp(arguments),
            "abs" => new Abs(arguments),
            "sqrt" => new Sqrt(arguments),
            "root" => new Root(arguments),

            "fact" or "factorial" => new Fact(arguments),

            "ln" => new Ln(arguments),
            "lg" => new Lg(arguments),
            "lb" or "log2" => new Lb(arguments),
            "log" => new Log(arguments),

            "todeg" or "todegree" => new ToDegree(arguments),
            "torad" or "toradian" => new ToRadian(arguments),
            "tograd" or "togradian" => new ToGradian(arguments),
            "tonumber" => new ToNumber(arguments),

            "sin" => new Sin(arguments),
            "cos" => new Cos(arguments),
            "tan" or "tg" => new Tan(arguments),
            "cot" or "ctg" => new Cot(arguments),
            "sec" => new Sec(arguments),
            "csc" or "cosec" => new Csc(arguments),

            "arcsin" => new Arcsin(arguments),
            "arccos" => new Arccos(arguments),
            "arctan" or "arctg" => new Arctan(arguments),
            "arccot" or "arcctg" => new Arccot(arguments),
            "arcsec" => new Arcsec(arguments),
            "arccsc" or "arccosec" => new Arccsc(arguments),

            "sh" or "sinh" => new Sinh(arguments),
            "ch" or "cosh" => new Cosh(arguments),
            "th" or "tanh" => new Tanh(arguments),
            "cth" or "coth" => new Coth(arguments),
            "sech" => new Sech(arguments),
            "csch" => new Csch(arguments),

            "arsh" or "arsinh" => new Arsinh(arguments),
            "arch" or "arcosh" => new Arcosh(arguments),
            "arth" or "artanh" => new Artanh(arguments),
            "arcth" or "arcoth" => new Arcoth(arguments),
            "arsch" or "arsech" => new Arsech(arguments),
            "arcsch" => new Arcsch(arguments),

            "gcd" or "gcf" or "hcf" => new GCD(arguments),
            "lcm" or "scm" => new LCM(arguments),

            "round" => new Round(arguments),
            "floor" => new Floor(arguments),
            "ceil" => new Ceil(arguments),
            "truncate" or "trunc" => new Trunc(arguments),
            "frac" => new Frac(arguments),

            "deriv" or "derivative" => new Derivative(differentiator, simplifier, arguments),
            "simplify" => new Simplify(simplifier, arguments),

            "del" => new Del(differentiator, simplifier, arguments),
            "nabla" => new Del(differentiator, simplifier, arguments),

            "transpose" => new Transpose(arguments),
            "det" or "determinant" => new Determinant(arguments),
            "inverse" => new Inverse(arguments),
            "dotproduct" => new DotProduct(arguments),
            "crossproduct" => new CrossProduct(arguments),

            "im" or "imaginary" => new Im(arguments),
            "re" or "real" => new Re(arguments),
            "phase" => new Phase(arguments),
            "conjugate" => new Conjugate(arguments),
            "reciprocal" => new Reciprocal(arguments),
            "tocomplex" => new ToComplex(arguments),

            "sum" => new Sum(arguments),
            "product" => new Product(arguments),
            "min" => new Min(arguments),
            "max" => new Max(arguments),
            "avg" => new Avg(arguments),
            "count" => new Count(arguments),
            "var" => new Var(arguments),
            "varp" => new Varp(arguments),
            "stdev" => new Stdev(arguments),
            "stdevp" => new Stdevp(arguments),

            "sign" => new Sign(arguments),
            "tobin" => new ToBin(arguments),
            "tooct" => new ToOct(arguments),
            "tohex" => new ToHex(arguments),

            "convert" => new Expressions.Units.Convert(converter, arguments),

            var id => new CallExpression(new Variable(id), arguments),
        };
    }

    private IExpression CreateBinaryAssign(
        in Token token,
        Variable first,
        IExpression second)
    {
        if (token.Is(AddAssignOperator))
            return new AddAssign(first, second);
        if (token.Is(SubAssignOperator))
            return new SubAssign(first, second);
        if (token.Is(MulAssignOperator))
            return new MulAssign(first, second);
        if (token.Is(DivAssignOperator))
            return new DivAssign(first, second);
        if (token.Is(LeftShiftAssignOperator))
            return new LeftShiftAssign(first, second);

        Debug.Assert(token.Is(RightShiftAssignOperator), "Only '+=', '-=', '*=', '/=', '<<=', '>>=' operators are allowed here.");

        return new RightShiftAssign(first, second);
    }

    private IExpression CreateBitwiseOperator(
        in Token token,
        IExpression first,
        IExpression second)
    {
        if (token.Is(ImplKeyword))
            return new Implication(first, second);
        if (token.Is(EqKeyword))
            return new Equality(first, second);

        if (token.Is(NAndKeyword))
            return new NAnd(first, second);

        Debug.Assert(token.Is(NOrKeyword), "Incorrect token type.");

        return new NOr(first, second);
    }

    private IExpression CreateEqualityOperator(
        in Token token,
        IExpression first,
        IExpression second)
    {
        if (token.Is(EqualOperator))
            return new Equal(first, second);

        Debug.Assert(token.Is(NotEqualOperator), "Incorrect token type.");

        return new NotEqual(first, second);
    }

    private IExpression CreateRelationalOperator(
        in Token token,
        IExpression first,
        IExpression second)
    {
        if (token.Is(LessThanOperator))
            return new LessThan(first, second);
        if (token.Is(LessOrEqualOperator))
            return new LessOrEqual(first, second);
        if (token.Is(GreaterThanOperator))
            return new GreaterThan(first, second);

        Debug.Assert(token.Is(GreaterOrEqualOperator), "Incorrect token type.");

        return new GreaterOrEqual(first, second);
    }

    private IExpression CreateShift(in Token token, IExpression first, IExpression second)
    {
        if (token.Is(LeftShiftOperator))
            return new LeftShift(first, second);

        Debug.Assert(token.Is(RightShiftOperator), "Only '<<', '>>' are allowed here.");

        return new RightShift(first, second);
    }

    private IExpression CreateAddSub(in Token token, IExpression first, IExpression second)
    {
        if (token.Is(PlusOperator))
            return new Add(first, second);

        Debug.Assert(token.Is(MinusOperator), "Only '+', '-' are allowed here.");

        return new Sub(first, second);
    }

    private IExpression CreateMulDivMod(in Token token, IExpression first, IExpression second)
    {
        if (token.Is(MultiplicationOperator))
            return new Mul(first, second);
        if (token.Is(DivisionOperator))
            return new Div(first, second);

        Debug.Assert(token.Is(ModuloOperator) || token.Is(ModKeyword), "Only '*', '/', '%', 'mod' are allowed here.");

        return new Mod(first, second);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Variable CreateVariable(in Token token)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(token.StringValue), "Id is null.");

        return new Variable(token.StringValue);
    }
}