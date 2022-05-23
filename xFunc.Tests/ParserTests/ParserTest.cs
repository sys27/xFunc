// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Tests.ParserTests;

public class ParserTest : BaseParserTests
{
    [Fact]
    public void DifferentiatorNull()
        => Assert.Throws<ArgumentNullException>(() => new Parser(null, null, null));

    [Fact]
    public void SimplifierNull()
        => Assert.Throws<ArgumentNullException>(() => new Parser(new Differentiator(), null, null));

    [Fact]
    public void ConverterNull()
        => Assert.Throws<ArgumentNullException>(() => new Parser(new Differentiator(), new Simplifier(), null));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ParseNull(string function)
        => ParseErrorTest<ArgumentNullException>(function);

    [Fact]
    public void NotSupportedSymbol()
        => ParseErrorTest<TokenizeException>("@");

    [Theory]
    [InlineData("\t2 + 2")]
    [InlineData("\n2 + 2")]
    [InlineData("\r2 + 2")]
    public void TabTest(string function)
        => ParseTest(function, new Add(Number.Two, Number.Two));

    [Fact]
    public void UseGreekLettersTest()
        => ParseTest("4 * φ", new Mul(new Number(4), new Variable("φ")));

    [Fact]
    public void ParseRoot()
        => ParseTest("root(x, 3)", new Root(Variable.X, new Number(3)));

    [Fact]
    public void ParseRootWithOneParam()
        => ParseErrorTest("root(x)");

    [Theory]
    [InlineData("deriv(sin(x))")]
    [InlineData("derivative(sin(x))")]
    public void ParseDerivWithOneParam(string function)
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var expected = new Derivative(diff, simp, new Sin(Variable.X));

        ParseTest(function, expected);
    }

    [Fact]
    public void ParseDerivWithTwoParam()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var expected = new Derivative(diff, simp, new Sin(Variable.X), Variable.X);

        ParseTest("deriv(sin(x), x)", expected);
    }

    [Fact]
    public void ErrorWhileParsingTree()
        => ParseErrorTest("sin(x)2");

    [Fact]
    public void UserFunc()
    {
        var expected = new Add(
            Number.One,
            new UserFunction("func", new IExpression[] { Variable.X })
        );

        ParseTest("1 + func(x)", expected);
    }

    [Fact]
    public void CosAddSinTest()
    {
        var expected = new Add(new Cos(Variable.X), new Sin(Variable.X));

        ParseTest("cos(x) + sin(x)", expected);
    }

    [Fact]
    public void SimplifyTest()
    {
        var simp = new Simplifier();
        var expected = new Simplify(simp, Variable.X);

        ParseTest("simplify(x)", expected);
    }

    [Theory]
    [InlineData("factorial(4)")]
    [InlineData("fact(4)")]
    [InlineData("4!")]
    public void FactorialTest(string function)
        => ParseTest(function, new Fact(new Number(4)));

    [Theory]
    [InlineData("!")]
    [InlineData("true!")]
    public void FactWithoutNumberTest(string function)
        => ParseErrorTest(function);

    [Fact]
    public void SumToTest()
    {
        var expected = new Sum(new IExpression[] { Variable.X, new Number(20) });

        ParseTest("sum(x, 20)", expected);
    }

    [Fact]
    public void ProductToTest()
    {
        var expected = new Product(new IExpression[] { Variable.X, new Number(20) });

        ParseTest("product(x, 20)", expected);
    }

    [Fact]
    public void VectorTest()
    {
        var expected = new Vector(new IExpression[] { Number.Two, new Number(3), new Number(4) });

        ParseTest("{2, 3, 4}", expected);
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{1, }")]
    public void VectorErrorTest(string exp)
        => ParseErrorTest(exp);

    [Fact]
    public void MatrixTest()
    {
        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.Two, new Number(3) }),
            new Vector(new IExpression[] { new Number(4), new Number(7) })
        });

        ParseTest("{{2, 3}, {4, 7}}", expected);
    }

    [Fact]
    public void MatrixErrorTest()
        => ParseErrorTest("{{1, 2}, }");

    [Fact]
    public void MatrixWithDiffVectorSizeTest()
        => ParseErrorTest<MatrixIsInvalidException>("{{2, 3}, {4, 7, 2}}");

    [Fact]
    public void TooMuchParamsTest()
        => ParseErrorTest("sin(x, 3)");

    [Fact]
    public void ModuloTest()
    {
        var expected = new Mod(new Number(7), Number.Two);

        ParseTest("7 % 2", expected);
    }

    [Theory]
    [InlineData("2 + 7 % 2")]
    [InlineData("2 + 7 mod 2")]
    public void ModuloAddTest(string function)
    {
        var expected = new Add(Number.Two, new Mod(new Number(7), Number.Two));

        ParseTest(function, expected);
    }

    [Fact]
    public void MinTest()
    {
        var expected = new Min(new IExpression[] { Number.One, Number.Two });

        ParseTest("min(1, 2)", expected);
    }

    [Fact]
    public void MaxTest()
    {
        var expected = new Max(new IExpression[] { Number.One, Number.Two });

        ParseTest("max(1, 2)", expected);
    }

    [Fact]
    public void AvgTest()
    {
        var expected = new Avg(new IExpression[] { Number.One, Number.Two });

        ParseTest("avg(1, 2)", expected);
    }

    [Fact]
    public void CountTest()
    {
        var expected = new Count(new IExpression[] { Number.One, Number.Two });

        ParseTest("count(1, 2)", expected);
    }

    [Fact]
    public void VarTest()
    {
        var expected = new Var(new IExpression[] { new Number(4), new Number(9) });

        ParseTest("var(4, 9)", expected);
    }

    [Fact]
    public void VarpTest()
    {
        var expected = new Varp(new IExpression[] { new Number(4), new Number(9) });

        ParseTest("varp(4, 9)", expected);
    }

    [Fact]
    public void StdevTest()
    {
        var expected = new Stdev(new IExpression[] { new Number(4), new Number(9) });

        ParseTest("stdev(4, 9)", expected);
    }

    [Fact]
    public void StdevpTest()
    {
        var expected = new Stdevp(new IExpression[] { new Number(4), new Number(9) });

        ParseTest("stdevp(4, 9)", expected);
    }

    [Theory]
    [InlineData("del(2*x + 3*y + 4*z)")]
    [InlineData("nabla(2*x + 3*y + 4*z)")]
    public void DelTest(string function)
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var expected = new Del(
            diff,
            simp,
            new Add(
                new Add(
                    new Mul(Number.Two, Variable.X),
                    new Mul(new Number(3), Variable.Y)
                ),
                new Mul(new Number(4), new Variable("z"))
            )
        );

        ParseTest(function, expected);
    }

    [Fact]
    public void AddTest()
    {
        var expected = new Add(Number.One, Number.Two);

        ParseTest("add(1, 2)", expected);
    }

    [Fact]
    public void AddMoreParamsTest()
        => ParseErrorTest("add(1, 2, 3)");

    [Theory]
    [InlineData("sub(1, 2)")]
    [InlineData("1 - 2")]
    [InlineData("1 − 2")]
    public void SubTest(string function)
    {
        var expected = new Sub(Number.One, Number.Two);

        ParseTest(function, expected);
    }

    [Theory]
    [InlineData("mul(1, 2)")]
    [InlineData("1 * 2")]
    [InlineData("1 × 2")]
    public void MulTest(string function)
    {
        var expected = new Mul(Number.One, Number.Two);

        ParseTest(function, expected);
    }

    [Theory]
    [InlineData("2 |")]
    [InlineData("2 ->")]
    [InlineData("2 <->")]
    [InlineData("2 nand")]
    [InlineData("2 nor")]
    [InlineData("2 and")]
    [InlineData("2 or")]
    [InlineData("2 xor")]
    [InlineData("2 eq")]
    [InlineData("2 impl")]
    [InlineData("2 ==")]
    [InlineData("2 !=")]
    [InlineData("2 <")]
    [InlineData("2 >")]
    [InlineData("2 <=")]
    [InlineData("2 >=")]
    [InlineData("2 <<")]
    [InlineData("2 >>")]
    [InlineData("2 +")]
    [InlineData("2 -")]
    [InlineData("2 *")]
    [InlineData("2 /")]
    [InlineData("2 %")]
    public void MissingSecondPartTest(string exp)
        => ParseErrorTest(exp);

    [Theory]
    [InlineData("div(1, 2)")]
    [InlineData("1 / 2")]
    public void DivFuncTest(string function)
    {
        var expected = new Div(Number.One, Number.Two);

        ParseTest(function, expected);
    }

    [Fact]
    public void UnaryMinusTest()
        => ParseTest("-2", new UnaryMinus(Number.Two));

    [Fact]
    public void SignTest()
    {
        var expected = new Sign(new UnaryMinus(new Number(10)));

        ParseTest("sign(-10)", expected);
    }

    [Theory]
    [InlineData("2~")]
    [InlineData("(2 + 1)~")]
    public void NotAfterNumberTest(string function)
        => ParseErrorTest(function);

    [Fact]
    public void AbsTest()
        => ParseTest("abs(2)", new Abs(Number.Two));

    [Fact]
    public void ExpTest()
        => ParseTest("exp(2)", new Exp(Number.Two));

    [Fact]
    public void FloorTest()
        => ParseTest("floor(2)", new Floor(Number.Two));

    [Fact]
    public void CeilTest()
        => ParseTest("ceil(2)", new Ceil(Number.Two));

    [Theory]
    [InlineData("trunc(2)")]
    [InlineData("truncate(2)")]
    public void TruncTest(string function)
        => ParseTest(function, new Trunc(Number.Two));

    [Fact]
    public void FracTest()
        => ParseTest("frac(2)", new Frac(Number.Two));

    [Fact]
    public void RoundTest()
        => ParseTest("round(2, 3)", new Round(Number.Two, new Number(3)));

    [Fact]
    public void SqrtTest()
        => ParseTest("sqrt(2)", new Sqrt(Number.Two));

    [Fact]
    public void DotProductTest()
    {
        var expected = new DotProduct(
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) })
        );

        ParseTest("dotproduct({1, 2}, {3, 4})", expected);
    }

    [Fact]
    public void CrossProductTest()
    {
        var expected = new CrossProduct(
            new Vector(new IExpression[] { Number.One, Number.Two, new Number(3) }),
            new Vector(new IExpression[] { new Number(4), new Number(5), new Number(6) })
        );

        ParseTest("crossproduct({1, 2, 3}, {4, 5, 6})", expected);
    }

    [Fact]
    public void TransposeTest()
    {
        var expected = new Transpose(
            new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.Two, new Number(3) }),
                new Vector(new IExpression[] { new Number(4), new Number(7) })
            })
        );

        ParseTest("transpose({{2, 3}, {4, 7}})", expected);
    }

    [Theory]
    [InlineData("determinant({{2, 3}, {4, 7}})")]
    [InlineData("det({{2, 3}, {4, 7}})")]
    public void DeterminantTest(string function)
    {
        var expected = new Determinant(
            new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.Two, new Number(3) }),
                new Vector(new IExpression[] { new Number(4), new Number(7) })
            })
        );

        ParseTest(function, expected);
    }

    [Fact]
    public void InverseTest()
    {
        var expected = new Inverse(
            new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.Two, new Number(3) }),
                new Vector(new IExpression[] { new Number(4), new Number(7) })
            })
        );

        ParseTest("inverse({{2, 3}, {4, 7}})", expected);
    }

    [Fact]
    public void RoundNotEnoughParameters()
        => ParseErrorTest<ArgumentException>("round()");

    [Fact]
    public void NotEnoughParamsTest()
        => ParseErrorTest("derivative(x,)");

    [Fact]
    public void NumAndVar()
    {
        var expected = new Mul(
            new UnaryMinus(Number.Two),
            Variable.X
        );

        ParseTest("-2 * x", expected);
    }

    [Fact]
    public void NumAndFunc()
    {
        var expected = new Mul(
            new Number(5),
            new Cos(Variable.X)
        );

        ParseTest("5*cos(x)", expected);
    }

    [Fact]
    public void Pi()
    {
        var expected = new Mul(
            new Number(3),
            new Variable("pi")
        );

        ParseTest("3pi", expected);
    }

    [Fact]
    public void NumberMulBracketTest()
    {
        var expected = new Mul(
            Number.Two,
            new Add(
                Variable.X,
                Variable.Y
            )
        );

        ParseTest("2 * (x + y)", expected);
    }

    [Theory]
    [InlineData("2 * {1, 2}")]
    [InlineData("2{1, 2}")]
    public void NumberMulVectorTest(string function)
    {
        var expected = new Mul(
            Number.Two,
            new Vector(new IExpression[] { Number.One, Number.Two })
        );

        ParseTest(function, expected);
    }

    [Theory]
    [InlineData("2 * {{1, 2}, {3, 4}}")]
    [InlineData("2{{1, 2}, {3, 4}}")]
    public void NumberMulMatrixTest(string function)
    {
        var expected = new Mul(
            Number.Two,
            new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One, Number.Two }),
                new Vector(new IExpression[] { new Number(3), new Number(4) })
            })
        );

        ParseTest(function, expected);
    }

    [Fact]
    public void MatrixMissingCloseBraceTest()
        => ParseErrorTest("{{1}");

    [Fact]
    public void UnaryPlusTest()
        => ParseTest("(+2)", Number.Two);

    [Fact]
    public void SinUnaryPlusTest()
        => ParseTest("sin(+2)", new Sin(Number.Two));

    [Fact]
    public void UnaryPlusVariableTest()
        => ParseTest("sin(+x)", new Sin(Variable.X));

    [Fact]
    public void Integer()
    {
        var expected = new Add(
            new UnaryMinus(new Number(2764786)),
            new Number(46489879)
        );

        ParseTest("-2764786 + 46489879", expected);
    }

    [Fact]
    public void Double()
    {
        var expected = new Add(
            new UnaryMinus(new Number(45.3)),
            new Number(87.64)
        );

        ParseTest("-45.3 + 87.64", expected);
    }

    [Fact]
    public void SubAfterOpenBracket()
    {
        var expected = new UnaryMinus(Number.Two);

        ParseTest("(-2)", expected);
    }

    [Fact]
    public void ParseSinWithIncorrectParametersCount()
        => ParseErrorTest("sin(1, 2) + cos()");

    [Fact]
    public void ParseSinWithoutParameters()
        => ParseErrorTest("sin(1)cos()");

    [Fact]
    public void ParseCosWithIncorrectParametersCount()
        => ParseErrorTest("cos(sin(1), 2)+");

    [Fact]
    public void ParseCosWithoutParam()
        => ParseErrorTest("cos()1");

    [Fact]
    public void LeftShiftTest()
    {
        var expected = new LeftShift(Number.One, new Number(10));

        ParseTest("1 << 10", expected);
    }

    [Fact]
    public void RightShiftTest()
    {
        var expected = new RightShift(Number.One, new Number(10));

        ParseTest("1 >> 10", expected);
    }

    [Fact]
    public void LeftShiftComplexTest()
    {
        var expected = new LeftShift(
            new LeftShift(Number.One, Number.Two),
            new Number(3)
        );

        ParseTest("1 << 2 << 3", expected);
    }

    [Fact]
    public void TrueConstCaseInsensitive()
        => ParseTest("tRuE", Bool.True);

    [Fact]
    public void SinCaseInsensitive()
        => ParseTest("sIn(x)", new Sin(Variable.X));

    [Fact]
    public void VarWithNumber1()
        => ParseTest("x1", new Variable("x1"));

    [Fact]
    public void VarWithNumber2()
        => ParseTest("xdsa13213", new Variable("xdsa13213"));

    [Fact]
    public void VarWithNumber3()
        => ParseTest("x1b2v3", new Variable("x1b2v3"));

    [Fact]
    public void HugeFunctionDeclaration()
    {
        var sb = new StringBuilder();
        sb.Append("func(");

        var i = 0;
        for (; i < 100; i++)
            sb.AppendFormat("x{0}, ", i);

        sb.AppendFormat("x{0}", i);
        sb.Append(") := 0");

        var function = sb.ToString();
        var exp = parser.Parse(function);

        Assert.NotNull(exp);
    }

    [Fact]
    public void ToBinTest()
        => ParseTest("tobin(10)", new ToBin(new Number(10)));

    [Fact]
    public void ToOctTest()
        => ParseTest("tooct(10)", new ToOct(new Number(10)));

    [Fact]
    public void ToHexTest()
        => ParseTest("tohex(10)", new ToHex(new Number(10)));

    [Fact]
    public void LeadingSpaces()
        => ParseTest("  1", Number.One);

    [Fact]
    public void TrailingSpaces()
        => ParseTest("1  ", Number.One);

}