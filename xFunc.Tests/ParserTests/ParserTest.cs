// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Tests.ParserTests;

public class ParserTest : BaseParserTests
{
    [Test]
    public void DifferentiatorNull()
        => Assert.Throws<ArgumentNullException>(() => new Parser(null, null, null));

    [Test]
    public void SimplifierNull()
        => Assert.Throws<ArgumentNullException>(() => new Parser(new Differentiator(), null, null));

    [Test]
    public void ConverterNull()
        => Assert.Throws<ArgumentNullException>(() => new Parser(new Differentiator(), new Simplifier(), null));

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void ParseNull(string function)
        => ParseErrorTest<ArgumentNullException>(function);

    [Test]
    public void NotSupportedSymbol()
        => ParseErrorTest<TokenizeException>("@");

    [Test]
    [TestCase("\t2 + 2")]
    [TestCase("\n2 + 2")]
    [TestCase("\r2 + 2")]
    public void TabTest(string function)
        => ParseTest(function, new Add(Number.Two, Number.Two));

    [Test]
    public void UseGreekLettersTest()
        => ParseTest("4 * φ", new Mul(new Number(4), new Variable("φ")));

    [Test]
    public void ParseRoot()
        => ParseTest("root(x, 3)", new Root(Variable.X, new Number(3)));

    [Test]
    public void ParseRootWithOneParam()
        => ParseErrorTest("root(x)");

    [Test]
    [TestCase("deriv(sin(x))")]
    [TestCase("derivative(sin(x))")]
    public void ParseDerivWithOneParam(string function)
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var expected = new Derivative(diff, simp, new Sin(Variable.X));

        ParseTest(function, expected);
    }

    [Test]
    public void ParseDerivWithTwoParam()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var expected = new Derivative(diff, simp, new Sin(Variable.X), Variable.X);

        ParseTest("deriv(sin(x), x)", expected);
    }

    [Test]
    public void ErrorWhileParsingTree()
        => ParseErrorTest("sin(x)2");

    [Test]
    public void ParseCallExpression()
    {
        var expected = new Add(
            Number.One,
            new CallExpression(
                new Variable("func"),
                new IExpression[] { Variable.X }.ToImmutableArray())
        );

        ParseTest("1 + func(x)", expected);
    }

    [Test]
    public void ParseCallExpressionZeroParameters()
    {
        var expected = new Add(
            Number.One,
            new CallExpression(
                new Variable("func"),
                ImmutableArray<IExpression>.Empty)
        );

        ParseTest("1 + func()", expected);
    }

    [Test]
    public void CosAddSinTest()
    {
        var expected = new Add(new Cos(Variable.X), new Sin(Variable.X));

        ParseTest("cos(x) + sin(x)", expected);
    }

    [Test]
    public void SimplifyTest()
    {
        var simp = new Simplifier();
        var expected = new Simplify(simp, Variable.X);

        ParseTest("simplify(x)", expected);
    }

    [Test]
    [TestCase("factorial(4)")]
    [TestCase("fact(4)")]
    [TestCase("4!")]
    public void FactorialTest(string function)
        => ParseTest(function, new Fact(new Number(4)));

    [Test]
    [TestCase("factorial(x)")]
    [TestCase("fact(x)")]
    [TestCase("x!")]
    public void FactorialVariableTest(string function)
        => ParseTest(function, new Fact(Variable.X));

    [Test]
    [TestCase("factorial(n - m)")]
    [TestCase("fact(n - m)")]
    [TestCase("(n - m)!")]
    public void FactorialComplexTest(string function)
        => ParseTest(function, new Fact(new Sub(new Variable("n"), new Variable("m"))));

    [Test]
    public void FactorialLambdaTest()
    {
        var expected = new CallExpression(
            new Lambda(
                new[] { Variable.X.Name },
                new Fact(new Sub(Variable.X, Number.One))
            ).AsExpression(),
            Number.Two);

        ParseTest("((x) => (x - 1)!)(2)", expected);
    }

    [Test]
    [TestCase("!")]
    public void FactIncorrectTest(string function)
        => ParseErrorTest(function);

    [Test]
    public void SumToTest()
    {
        var expected = new Sum(new IExpression[] { Variable.X, new Number(20) });

        ParseTest("sum(x, 20)", expected);
    }

    [Test]
    public void ProductToTest()
    {
        var expected = new Product(new IExpression[] { Variable.X, new Number(20) });

        ParseTest("product(x, 20)", expected);
    }

    [Test]
    public void VectorTest()
    {
        var expected = new Vector(new IExpression[] { Number.Two, new Number(3), new Number(4) });

        ParseTest("{2, 3, 4}", expected);
    }

    [Test]
    [TestCase("{}")]
    [TestCase("{1, }")]
    public void VectorErrorTest(string exp)
        => ParseErrorTest(exp);

    [Test]
    public void MatrixTest()
    {
        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.Two, new Number(3) }),
            new Vector(new IExpression[] { new Number(4), new Number(7) })
        });

        ParseTest("{{2, 3}, {4, 7}}", expected);
    }

    [Test]
    public void MatrixErrorTest()
        => ParseErrorTest("{{1, 2}, }");

    [Test]
    public void MatrixWithDiffVectorSizeTest()
        => ParseErrorTest<InvalidMatrixException>("{{2, 3}, {4, 7, 2}}");

    [Test]
    public void TooMuchParamsTest()
        => ParseErrorTest("sin(x, 3)");

    [Test]
    public void ModuloTest()
    {
        var expected = new Mod(new Number(7), Number.Two);

        ParseTest("7 % 2", expected);
    }

    [Test]
    [TestCase("2 + 7 % 2")]
    [TestCase("2 + 7 mod 2")]
    public void ModuloAddTest(string function)
    {
        var expected = new Add(Number.Two, new Mod(new Number(7), Number.Two));

        ParseTest(function, expected);
    }

    [Test]
    public void MinTest()
    {
        var expected = new Min(new IExpression[] { Number.One, Number.Two });

        ParseTest("min(1, 2)", expected);
    }

    [Test]
    public void MaxTest()
    {
        var expected = new Max(new IExpression[] { Number.One, Number.Two });

        ParseTest("max(1, 2)", expected);
    }

    [Test]
    public void AvgTest()
    {
        var expected = new Avg(new IExpression[] { Number.One, Number.Two });

        ParseTest("avg(1, 2)", expected);
    }

    [Test]
    public void CountTest()
    {
        var expected = new Count(new IExpression[] { Number.One, Number.Two });

        ParseTest("count(1, 2)", expected);
    }

    [Test]
    public void VarTest()
    {
        var expected = new Var(new IExpression[] { new Number(4), new Number(9) });

        ParseTest("var(4, 9)", expected);
    }

    [Test]
    public void VarpTest()
    {
        var expected = new Varp(new IExpression[] { new Number(4), new Number(9) });

        ParseTest("varp(4, 9)", expected);
    }

    [Test]
    public void StdevTest()
    {
        var expected = new Stdev(new IExpression[] { new Number(4), new Number(9) });

        ParseTest("stdev(4, 9)", expected);
    }

    [Test]
    public void StdevpTest()
    {
        var expected = new Stdevp(new IExpression[] { new Number(4), new Number(9) });

        ParseTest("stdevp(4, 9)", expected);
    }

    [Test]
    [TestCase("del(2*x + 3*y + 4*z)")]
    [TestCase("nabla(2*x + 3*y + 4*z)")]
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

    [Test]
    public void AddTest()
    {
        var expected = new Add(Number.One, Number.Two);

        ParseTest("add(1, 2)", expected);
    }

    [Test]
    public void AddMoreParamsTest()
        => ParseErrorTest("add(1, 2, 3)");

    [Test]
    [TestCase("sub(1, 2)")]
    [TestCase("1 - 2")]
    [TestCase("1 − 2")]
    public void SubTest(string function)
    {
        var expected = new Sub(Number.One, Number.Two);

        ParseTest(function, expected);
    }

    [Test]
    [TestCase("mul(1, 2)")]
    [TestCase("1 * 2")]
    [TestCase("1 × 2")]
    public void MulTest(string function)
    {
        var expected = new Mul(Number.One, Number.Two);

        ParseTest(function, expected);
    }

    [Test]
    [TestCase("2 |")]
    [TestCase("2 ->")]
    [TestCase("2 <->")]
    [TestCase("2 nand")]
    [TestCase("2 nor")]
    [TestCase("2 and")]
    [TestCase("2 or")]
    [TestCase("2 xor")]
    [TestCase("2 eq")]
    [TestCase("2 impl")]
    [TestCase("2 ==")]
    [TestCase("2 !=")]
    [TestCase("2 <")]
    [TestCase("2 >")]
    [TestCase("2 <=")]
    [TestCase("2 >=")]
    [TestCase("2 <<")]
    [TestCase("2 >>")]
    [TestCase("2 +")]
    [TestCase("2 -")]
    [TestCase("2 *")]
    [TestCase("2 /")]
    [TestCase("2 %")]
    public void MissingSecondPartTest(string exp)
        => ParseErrorTest(exp);

    [Test]
    [TestCase("div(1, 2)")]
    [TestCase("1 / 2")]
    public void DivFuncTest(string function)
    {
        var expected = new Div(Number.One, Number.Two);

        ParseTest(function, expected);
    }

    [Test]
    public void UnaryMinusTest()
        => ParseTest("-2", new UnaryMinus(Number.Two));

    [Test]
    public void SignTest()
    {
        var expected = new Sign(new UnaryMinus(new Number(10)));

        ParseTest("sign(-10)", expected);
    }

    [Test]
    [TestCase("2~")]
    [TestCase("(2 + 1)~")]
    public void NotAfterNumberTest(string function)
        => ParseErrorTest(function);

    [Test]
    public void AbsTest()
        => ParseTest("abs(2)", new Abs(Number.Two));

    [Test]
    public void ExpTest()
        => ParseTest("exp(2)", new Exp(Number.Two));

    [Test]
    public void FloorTest()
        => ParseTest("floor(2)", new Floor(Number.Two));

    [Test]
    public void CeilTest()
        => ParseTest("ceil(2)", new Ceil(Number.Two));

    [Test]
    [TestCase("trunc(2)")]
    [TestCase("truncate(2)")]
    public void TruncTest(string function)
        => ParseTest(function, new Trunc(Number.Two));

    [Test]
    public void FracTest()
        => ParseTest("frac(2)", new Frac(Number.Two));

    [Test]
    public void RoundTest()
        => ParseTest("round(2, 3)", new Round(Number.Two, new Number(3)));

    [Test]
    public void SqrtTest()
        => ParseTest("sqrt(2)", new Sqrt(Number.Two));

    [Test]
    public void DotProductTest()
    {
        var expected = new DotProduct(
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) })
        );

        ParseTest("dotproduct({1, 2}, {3, 4})", expected);
    }

    [Test]
    public void CrossProductTest()
    {
        var expected = new CrossProduct(
            new Vector(new IExpression[] { Number.One, Number.Two, new Number(3) }),
            new Vector(new IExpression[] { new Number(4), new Number(5), new Number(6) })
        );

        ParseTest("crossproduct({1, 2, 3}, {4, 5, 6})", expected);
    }

    [Test]
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

    [Test]
    [TestCase("determinant({{2, 3}, {4, 7}})")]
    [TestCase("det({{2, 3}, {4, 7}})")]
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

    [Test]
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

    [Test]
    public void RoundNotEnoughParameters()
        => ParseErrorTest<ArgumentException>("round()");

    [Test]
    public void NotEnoughParamsTest()
        => ParseErrorTest("derivative(x,)");

    [Test]
    public void NumAndVar()
    {
        var expected = new Mul(
            new UnaryMinus(Number.Two),
            Variable.X
        );

        ParseTest("-2 * x", expected);
    }

    [Test]
    public void NumAndFunc()
    {
        var expected = new Mul(
            new Number(5),
            new Cos(Variable.X)
        );

        ParseTest("5*cos(x)", expected);
    }

    [Test]
    public void Pi()
    {
        var expected = new Mul(
            new Number(3),
            new Variable("pi")
        );

        ParseTest("3pi", expected);
    }

    [Test]
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

    [Test]
    public void NumberMulVectorTest()
    {
        var expected = new Mul(
            Number.Two,
            new Vector(new IExpression[] { Number.One, Number.Two })
        );

        ParseTest("2{1, 2}", expected);
    }

    [Test]
    public void NumberMulMatrixTest()
    {
        var expected = new Mul(
            Number.Two,
            new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One, Number.Two }),
                new Vector(new IExpression[] { new Number(3), new Number(4) })
            })
        );

        ParseTest("2{{1, 2}, {3, 4}}", expected);
    }

    [Test]
    public void MatrixMissingCloseBraceTest()
        => ParseErrorTest("{{1}");

    [Test]
    public void UnaryPlusTest()
        => ParseTest("(+2)", Number.Two);

    [Test]
    public void SinUnaryPlusTest()
        => ParseTest("sin(+2)", new Sin(Number.Two));

    [Test]
    public void UnaryPlusVariableTest()
        => ParseTest("sin(+x)", new Sin(Variable.X));

    [Test]
    public void Integer()
    {
        var expected = new Add(
            new UnaryMinus(new Number(2764786)),
            new Number(46489879)
        );

        ParseTest("-2764786 + 46489879", expected);
    }

    [Test]
    public void Double()
    {
        var expected = new Add(
            new UnaryMinus(new Number(45.3)),
            new Number(87.64)
        );

        ParseTest("-45.3 + 87.64", expected);
    }

    [Test]
    public void SubAfterOpenBracket()
    {
        var expected = new UnaryMinus(Number.Two);

        ParseTest("(-2)", expected);
    }

    [Test]
    public void ParseSinWithIncorrectParametersCount()
        => ParseErrorTest("sin(1, 2) + cos()");

    [Test]
    public void ParseSinWithoutParameters()
        => ParseErrorTest("sin(1)cos()");

    [Test]
    public void ParseCosWithIncorrectParametersCount()
        => ParseErrorTest("cos(sin(1), 2)+");

    [Test]
    public void ParseCosWithoutParam()
        => ParseErrorTest("cos()1");

    [Test]
    public void LeftShiftTest()
    {
        var expected = new LeftShift(Number.One, new Number(10));

        ParseTest("1 << 10", expected);
    }

    [Test]
    public void RightShiftTest()
    {
        var expected = new RightShift(Number.One, new Number(10));

        ParseTest("1 >> 10", expected);
    }

    [Test]
    public void LeftShiftComplexTest()
    {
        var expected = new LeftShift(
            new LeftShift(Number.One, Number.Two),
            new Number(3)
        );

        ParseTest("1 << 2 << 3", expected);
    }

    [Test]
    public void TrueConstCaseInsensitive()
        => ParseTest("tRuE", Bool.True);

    [Test]
    public void SinCaseInsensitive()
        => ParseTest("sIn(x)", new Sin(Variable.X));

    [Test]
    public void VariableCaseSensitive()
        => ParseTest("X", new Variable("X"));

    [Test]
    public void VarWithNumber1()
        => ParseTest("x1", new Variable("x1"));

    [Test]
    public void VarWithNumber2()
        => ParseTest("xdsa13213", new Variable("xdsa13213"));

    [Test]
    public void VarWithNumber3()
        => ParseTest("x1b2v3", new Variable("x1b2v3"));

    [Test]
    public void ToBinTest()
        => ParseTest("tobin(10)", new ToBin(new Number(10)));

    [Test]
    public void ToOctTest()
        => ParseTest("tooct(10)", new ToOct(new Number(10)));

    [Test]
    public void ToHexTest()
        => ParseTest("tohex(10)", new ToHex(new Number(10)));

    [Test]
    public void LeadingSpaces()
        => ParseTest("  1", Number.One);

    [Test]
    public void TrailingSpaces()
        => ParseTest("1  ", Number.One);

    [Test]
    public void BufferOverflow()
        => parser.Parse("a1 := (a2 := (a3 := (a4 := (a5 := (a6 := (a7 := (a8 := (a9 := (a10 := (a11 := (a12 := (a13 := (a14 := (a15 := (a16 := (a17 := (a18 := 1)))))))))))))))))");

    [Test]
    public void RationalParse()
        => ParseTest("1 // 2", new Rational(Number.One, Number.Two));

    [Test]
    public void ToRationalParse()
        => ParseTest("torational(1)", new ToRational(Number.One));
}