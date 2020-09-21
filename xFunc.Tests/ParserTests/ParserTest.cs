// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Text;
using xFunc.Maths;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Tests.ParserTests
{
    public class ParserTest : BaseParserTests
    {
        [Fact]
        public void DifferentiatorNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Parser(null, null));
        }

        [Fact]
        public void SimplifierNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Parser(new Differentiator(), null));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ParseNull(string function)
        {
            Assert.Throws<ArgumentNullException>(() => parser.Parse(function));
        }

        [Fact]
        public void NotSupportedSymbol()
        {
            Assert.Throws<TokenizeException>(() => parser.Parse("@"));
        }

        [Theory]
        [InlineData("\t2 + 2")]
        [InlineData("\n2 + 2")]
        [InlineData("\r2 + 2")]
        public void TabTest(string function)
        {
            var expected = new Add(Number.Two, Number.Two);

            ParseTest(function, expected);
        }

        [Fact]
        public void UseGreekLettersTest()
        {
            var expected = new Mul(new Number(4), new Variable("φ"));

            ParseTest("4 * φ", expected);
        }

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
        public void MatrixWithDiffVectorSizeTest()
            => ErrorTest<MatrixIsInvalidException>("{{2, 3}, {4, 7, 2}}");

        [Fact]
        public void TooMuchParamsTest()
            => ParseErrorTest("sin(x, 3)");

        [Fact]
        public void ConditionalAndTest()
        {
            var expected = new ConditionalAnd(
                new Equal(Variable.X, Number.Zero),
                new NotEqual(new Variable("y"), Number.Zero)
            );

            ParseTest("x == 0 && y != 0", expected);
        }

        [Fact]
        public void ConditionalOrTest()
        {
            var expected = new ConditionalOr(
                new Equal(Variable.X, Number.Zero),
                new NotEqual(new Variable("y"), Number.Zero)
            );

            ParseTest("x == 0 || y != 0", expected);
        }

        [Theory]
        [InlineData("x == 0 &&")]
        [InlineData("x == 0 ||")]
        public void ConditionalMissingSecondOperand(string function)
            => ParseErrorTest(function);

        [Fact]
        public void EqualTest()
        {
            var expected = new Equal(Variable.X, Number.Zero);

            ParseTest("x == 0", expected);
        }

        [Fact]
        public void NotEqualTest()
        {
            var expected = new NotEqual(Variable.X, Number.Zero);

            ParseTest("x != 0", expected);
        }

        [Fact]
        public void LessThenTest()
        {
            var expected = new LessThan(Variable.X, Number.Zero);

            ParseTest("x < 0", expected);
        }

        [Fact]
        public void LessOrEqualTest()
        {
            var expected = new LessOrEqual(Variable.X, Number.Zero);

            ParseTest("x <= 0", expected);
        }

        [Fact]
        public void GreaterThenTest()
        {
            var expected = new GreaterThan(Variable.X, Number.Zero);

            ParseTest("x > 0", expected);
        }

        [Fact]
        public void GreaterOrEqualTest()
        {
            var expected = new GreaterOrEqual(Variable.X, Number.Zero);

            ParseTest("x >= 0", expected);
        }

        [Fact]
        public void IncTest()
            => ParseTest("x++", new Inc(Variable.X));

        [Fact]
        public void IncAsExpressionTest()
            => ParseTest("1 + x++", new Add(Number.One, new Inc(Variable.X)));

        [Theory]
        [InlineData("x--")]
        [InlineData("x−−")]
        public void DecTest(string function)
        {
            var expected = new Dec(Variable.X);

            ParseTest(function, expected);
        }

        [Fact]
        public void DecAsExpressionTest()
            => ParseTest("1 + x--", new Add(Number.One, new Dec(Variable.X)));

        [Theory]
        [InlineData("true & false")]
        [InlineData("true and false")]
        public void BoolConstTest(string function)
        {
            var expected = new And(Bool.True, Bool.False);

            ParseTest(function, expected);
        }

        [Fact]
        public void LogicAddPriorityTest()
        {
            var expected = new And(
                new GreaterThan(new Number(3), new Number(4)),
                new LessThan(Number.One, new Number(3))
            );

            ParseTest("3 > 4 & 1 < 3", expected);
        }

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
                        new Mul(new Number(3), new Variable("y"))
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
        [InlineData("div(1, 2)")]
        [InlineData("1 / 2")]
        public void DivFuncTest(string function)
        {
            var expected = new Div(Number.One, Number.Two);

            ParseTest(function, expected);
        }

        [Theory]
        [InlineData("pow(1, 2)")]
        [InlineData("1 ^ 2")]
        public void PowFuncTest(string function)
        {
            var expected = new Pow(Number.One, Number.Two);

            ParseTest(function, expected);
        }

        [Fact]
        public void PowRightAssociativityTest()
        {
            var expected = new Pow(Number.One, new Pow(Number.Two, new Number(3)));

            ParseTest("1 ^ 2 ^ 3", expected);
        }

        [Fact]
        public void PowUnaryMinusTest()
        {
            var expected = new UnaryMinus(new Pow(Number.One, Number.Two));

            ParseTest("-1 ^ 2", expected);
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
        [InlineData("~2")]
        [InlineData("not(2)")]
        public void NotTest(string function)
            => ParseTest(function, new Not(Number.Two));

        [Theory]
        [InlineData("2~")]
        [InlineData("(2 + 1)~")]
        public void NotAfterNumberTest(string function)
            => ParseErrorTest(function);

        [Theory]
        [InlineData("1 | 2")]
        [InlineData("1 or 2")]
        public void OrTest(string function)
        {
            var expected = new Or(Number.One, Number.Two);

            ParseTest(function, expected);
        }

        [Fact]
        public void XOrTest()
        {
            var expected = new XOr(Number.One, Number.Two);

            ParseTest("1 xor 2", expected);
        }

        [Fact]
        public void NOrTest()
        {
            var expected = new NOr(Bool.True, Bool.True);

            ParseTest("true nor true", expected);
        }

        [Fact]
        public void NAndTest()
        {
            var expected = new NAnd(Bool.True, Bool.True);

            ParseTest("true nand true", expected);
        }

        [Theory]
        [InlineData("true -> true")]
        [InlineData("true −> true")]
        [InlineData("true => true")]
        [InlineData("true impl true")]
        public void ImplicationTest(string function)
            => ParseTest(function, new Implication(Bool.True, Bool.True));

        [Theory]
        [InlineData("true <-> true")]
        [InlineData("true <−> true")]
        [InlineData("true <=> true")]
        [InlineData("true eq true")]
        public void EqualityTest(string function)
            => ParseTest(function, new Equality(Bool.True, Bool.True));

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
            => ErrorTest<ArgumentException>("round()");

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
                    new Variable("y")
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
        public void ImplicitMulAndPowerFunction()
        {
            var expected = new Mul(
                Number.Two,
                new Pow(new Sin(Variable.X), Number.Two)
            );

            ParseTest("2sin(x) ^ 2", expected);
        }

        [Fact]
        public void ImplicitMulAndPowerVariable()
        {
            var expected = new Mul(
                Number.Two,
                new Pow(Variable.X, Number.Two)
            );

            ParseTest("2x^2", expected);
        }

        [Fact]
        public void ImplicitNegativeNumberMulAndPowerVariable()
        {
            var expected = new Mul(
                new UnaryMinus(Number.Two),
                new Pow(Variable.X, Number.Two)
            );

            ParseTest("-2x^2", expected);
        }

        [Fact]
        public void PowerWithUnaryMinus()
        {
            var expected = new Pow(Number.Two, new UnaryMinus(Number.Two));

            ParseTest("2 ^ -2", expected);
        }

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
    }
}