// Copyright 2012-2021 Dmytro Kyshchenko
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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Analyzers.DifferentiatorTests
{
    public class DifferentiatorTest
    {
        private readonly Differentiator differentiator;

        private readonly Number zero;

        public DifferentiatorTest()
        {
            differentiator = new Differentiator();
            zero = Number.Zero;
        }

        private IExpression Differentiate(IExpression exp)
            => exp.Analyze(differentiator, DifferentiatorContext.Default());

        private IExpression Differentiate(IExpression exp, Variable variable)
            => exp.Analyze(differentiator, new DifferentiatorContext(new ExpressionParameters(), variable));

        private IExpression Differentiate(IExpression exp, Variable variable, ExpressionParameters parameters)
            => exp.Analyze(differentiator, new DifferentiatorContext(parameters, variable));

        #region Common

        [Fact]
        public void NumberTest()
        {
            var exp = Differentiate(new Number(10));

            Assert.Equal(zero, exp);
        }

        [Fact]
        public void AngleNumberTest()
        {
            var exp = Differentiate(new Angle(AngleValue.Degree(10)));

            Assert.Equal(zero, exp);
        }

        [Fact]
        public void PowerNumberTest()
        {
            var exp = Differentiate(new Power(PowerValue.Watt(10)));

            Assert.Equal(zero, exp);
        }

        [Fact]
        public void VariableNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => Differentiate(new Number(10), null));
        }

        [Fact]
        public void AbsDerivativeTest1()
        {
            var exp = Differentiate(new Abs(Variable.X));
            var expected = new Mul(
                Number.One,
                new Div(Variable.X, new Abs(Variable.X))
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AbsPartialDerivativeTest1()
        {
            var exp = new Abs(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(Number.One, new Variable("y")),
                new Div(
                    new Mul(Variable.X, new Variable("y")),
                    new Abs(new Mul(Variable.X, new Variable("y")))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AbsPartialDerivativeTest2()
        {
            var exp = new Abs(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(
                new Mul(Variable.X, Number.One),
                new Div(
                    new Mul(Variable.X, new Variable("y")),
                    new Abs(new Mul(Variable.X, new Variable("y")))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AbsPartialDerivativeTest3()
        {
            var deriv = Differentiate(new Abs(Variable.X), new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void AddDerivativeTest1()
        {
            var exp = new Add(new Mul(Number.Two, Variable.X), new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(Number.Two, Number.One);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddDerivativeTest2()
        {
            var exp = new Add(
                new Mul(Number.Two, Variable.X),
                new Mul(new Number(3), Variable.X)
            );
            var deriv = Differentiate(exp);
            var expected = new Add(
                new Mul(Number.Two, Number.One),
                new Mul(new Number(3), Number.One)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddPartialDerivativeTest1()
        {
            var exp = new Add(
                new Add(new Mul(Variable.X, new Variable("y")), Variable.X),
                new Variable("y")
            );
            var deriv = Differentiate(exp);
            var expected = new Add(new Mul(Number.One, new Variable("y")), Number.One);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddPartialDerivativeTest2()
        {
            var exp = new Add(
                new Add(new Mul(Variable.X, new Variable("y")), Variable.X),
                new Variable("y")
            );
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Add(new Mul(Variable.X, Number.One), Number.One);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddPartialDerivativeTest3()
        {
            var exp = new Add(Variable.X, Number.One);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void DivDerivativeTest1()
        {
            var exp = new Div(Number.One, Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new UnaryMinus(new Mul(Number.One, Number.One)),
                new Pow(Variable.X, Number.Two)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivDerivativeTest2()
        {
            // sin(x) / x
            var exp = new Div(new Sin(Variable.X), Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Sub(
                    new Mul(new Mul(new Cos(Variable.X), Number.One), Variable.X),
                    new Mul(new Sin(Variable.X), Number.One)
                ),
                new Pow(Variable.X, Number.Two)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest1()
        {
            // (y + x ^ 2) / x
            var exp = new Div(
                new Add(new Variable("y"), new Pow(Variable.X, Number.Two)),
                Variable.X
            );
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Sub(
                    new Mul(
                        new Mul(
                            Number.One,
                            new Mul(Number.Two,
                                new Pow(Variable.X, new Sub(Number.Two, Number.One))
                            )
                        ),
                        Variable.X
                    ),
                    new Mul(
                        new Add(new Variable("y"), new Pow(Variable.X, Number.Two)),
                        Number.One
                    )
                ),
                new Pow(Variable.X, Number.Two)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest2()
        {
            var exp = new Div(new Variable("y"), Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new UnaryMinus(new Mul(new Variable("y"), Number.One)),
                new Pow(Variable.X, Number.Two)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest3()
        {
            var exp = new Div(new Variable("y"), Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(Number.One, Variable.X);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest4()
        {
            // (x + 1) / x
            var exp = new Div(new Add(Variable.X, Number.One), Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ExpDerivativeTest1()
        {
            var exp = new Exp(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Mul(Number.One, new Exp(Variable.X));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpDerivativeTest2()
        {
            var exp = new Exp(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(Number.Two, Number.One),
                new Exp(new Mul(Number.Two, Variable.X))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpPartialDerivativeTest1()
        {
            var exp = new Exp(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(Number.One, new Variable("y")),
                new Exp(new Mul(Variable.X, new Variable("y")))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpPartialDerivativeTest2()
        {
            var exp = new Exp(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(
                new Mul(Variable.X, Number.One),
                new Exp(new Mul(Variable.X, new Variable("y")))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpPartialDerivativeTest3()
        {
            var exp = new Exp(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void LnDerivativeTest1()
        {
            var exp = new Ln(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Mul(Number.Two, Variable.X)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LnPartialDerivativeTest1()
        {
            // ln(xy)
            var exp = new Ln(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.One, new Variable("y")),
                new Mul(Variable.X, new Variable("y"))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LnPartialDerivativeTest2()
        {
            // ln(xy)
            var exp = new Ln(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(
                new Mul(Variable.X, Number.One),
                new Mul(Variable.X, new Variable("y"))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LnPartialDerivativeTest3()
        {
            var exp = new Ln(new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void LgDerivativeTest1()
        {
            var exp = new Lg(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Mul(new Mul(Number.Two, Variable.X), new Ln(new Number(10)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LgPartialDerivativeTest1()
        {
            // lg(2xy)
            var exp = new Lg(new Mul(new Mul(Number.Two, Variable.X), new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Mul(Number.Two, Number.One), new Variable("y")),
                new Mul(
                    new Mul(new Mul(Number.Two, Variable.X), new Variable("y")),
                    new Ln(new Number(10))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LgPartialDerivativeTest2()
        {
            // lg(2xy)
            var exp = new Lg(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void LbDerivativeTest1()
        {
            var exp = new Lb(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Mul(new Mul(Number.Two, Variable.X), new Ln(Number.Two))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LbPartialDerivativeTest1()
        {
            // lb(2xy)
            var exp = new Lb(new Mul(new Mul(Number.Two, Variable.X), new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Mul(Number.Two, Number.One), new Variable("y")),
                new Mul(
                    new Mul(new Mul(Number.Two, Variable.X), new Variable("y")),
                    new Ln(Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LbPartialDerivativeTest2()
        {
            // lb(2xy)
            var exp = new Lb(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void LogDerivativeTest1()
        {
            var exp = new Log(Number.Two, Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(Number.One, new Mul(Variable.X, new Ln(Number.Two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogDerivativeTest3()
        {
            var exp = new Log(Variable.X, Number.Two);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new UnaryMinus(
                    new Mul(new Ln(Number.Two), new Div(Number.One, Variable.X))
                ),
                new Pow(new Ln(Variable.X), Number.Two)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogPartialDerivativeTest1()
        {
            var exp = new Log(Number.Two, Variable.X);
            var deriv = Differentiate(exp, Variable.X);
            var expected = new Div(
                Number.One,
                new Mul(Variable.X, new Ln(Number.Two))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogPartialDerivativeTest2()
        {
            var exp = new Log(Number.Two, Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void MulDerivativeTest1()
        {
            var exp = new Mul(Number.Two, Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Mul(Number.Two, Number.One);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest1()
        {
            // (x + 1) * (y + x)
            var exp = new Mul(
                new Add(Variable.X, Number.One),
                new Add(new Variable("y"), Variable.X)
            );
            var deriv = Differentiate(exp);
            var expected = new Add(
                new Mul(Number.One, new Add(new Variable("y"), Variable.X)),
                new Mul(new Add(Variable.X, Number.One), Number.One)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest2()
        {
            // (y + 1) * (3 + x)
            var exp = new Mul(
                new Add(new Variable("y"), Number.One),
                new Add(new Number(3), Variable.X)
            );
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(Number.One, new Add(new Number(3), Variable.X));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest3()
        {
            // (x + 1) * (y + x)
            var exp = new Mul(
                new Add(Variable.X, Number.One),
                new Add(new Variable("y"), Variable.X)
            );
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(new Add(Variable.X, Number.One), Number.One);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest4()
        {
            // (x + 1) * (3 + x)
            var exp = new Mul(
                new Add(Variable.X, Number.One),
                new Add(new Number(3), Variable.X)
            );
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact(DisplayName = "x ^ 3")]
        public void PowDerivativeTest1()
        {
            var exp = new Pow(Variable.X, new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                Number.One,
                new Mul(new Number(3), new Pow(Variable.X, new Sub(new Number(3), Number.One)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact(DisplayName = "2 ^ (3x)")]
        public void PowDerivativeTest2()
        {
            var exp = new Pow(Number.Two, new Mul(new Number(3), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(
                    new Pow(Number.Two, new Mul(new Number(3), Variable.X)),
                    new Ln(Number.Two)
                ),
                new Mul(new Number(3), Number.One)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact(DisplayName = "x ^ x")]
        public void PowXbyX()
        {
            var exp = new Pow(Variable.X, Variable.X);
            var result = Differentiate(exp);
            var expected = new Mul(
                new Pow(Variable.X, Variable.X),
                new Add(
                    new Mul(Number.One, new Ln(Variable.X)),
                    new Mul(Variable.X, new Div(Number.One, Variable.X)))
            );

            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "x ^ 2x")]
        public void PowXby2X()
        {
            var exp = new Pow(Variable.X, new Mul(Number.Two, Variable.X));
            var result = Differentiate(exp);
            var expected = new Mul(
                new Pow(Variable.X, new Mul(Number.Two, Variable.X)),
                new Add(
                    new Mul(new Mul(Number.Two, Number.One), new Ln(Variable.X)),
                    new Mul(new Mul(Number.Two, Variable.X), new Div(Number.One, Variable.X)))
            );

            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "x ^ sin(x)")]
        public void PowXbySinX()
        {
            var exp = new Pow(Variable.X, new Sin(Variable.X));
            var result = Differentiate(exp);
            var expected = new Mul(
                new Pow(Variable.X, new Sin(Variable.X)),
                new Add(
                    new Mul(new Mul(new Cos(Variable.X), Number.One), new Ln(Variable.X)),
                    new Mul(new Sin(Variable.X), new Div(Number.One, Variable.X))
                )
            );

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PowPartialDerivativeTest1()
        {
            // (yx) ^ 3
            var exp = new Pow(new Mul(new Variable("y"), Variable.X), new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Variable("y"), Number.One),
                new Mul(
                    new Number(3),
                    new Pow(
                        new Mul(new Variable("y"), Variable.X),
                        new Sub(new Number(3), Number.One)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowPartialDerivativeTest2()
        {
            // (yx) ^ 3
            var exp = new Pow(new Mul(new Variable("y"), Variable.X), new Number(3));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(
                new Mul(Number.One, Variable.X),
                new Mul(
                    new Number(3),
                    new Pow(
                        new Mul(new Variable("y"), Variable.X),
                        new Sub(new Number(3), Number.One)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowPartialDerivativeTest3()
        {
            var exp = new Pow(Variable.X, new Number(3));
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void RootDerivativeTest1()
        {
            var exp = new Root(Variable.X, new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                Number.One,
                new Mul(
                    new Div(Number.One, new Number(3)),
                    new Pow(
                        Variable.X,
                        new Sub(new Div(Number.One, new Number(3)), Number.One)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void RootPartialDerivativeTest1()
        {
            var exp = new Root(new Mul(Variable.X, new Variable("y")), new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(Number.One, new Variable("y")),
                new Mul(
                    new Div(Number.One, new Number(3)),
                    new Pow(
                        new Mul(Variable.X, new Variable("y")),
                        new Sub(new Div(Number.One, new Number(3)), Number.One)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void RootPartialDerivativeTest2()
        {
            var exp = new Root(new Variable("y"), new Number(3));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void SqrtDerivativeTest1()
        {
            var exp = new Sqrt(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Mul(Number.Two, new Sqrt(new Mul(Number.Two, Variable.X)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SqrtPartialDerivativeTest1()
        {
            // sqrt(2xy)
            var exp = new Sqrt(
                new Mul(new Mul(Number.Two, Variable.X), new Variable("y"))
            );
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Mul(Number.Two, Number.One), new Variable("y")),
                new Mul(
                    Number.Two,
                    new Sqrt(
                        new Mul(new Mul(Number.Two, Variable.X), new Variable("y"))
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SqrtPartialDerivativeTest2()
        {
            var exp = new Sqrt(new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void SubDerivativeTest1()
        {
            // x - sin(x)
            var exp = new Sub(Variable.X, new Sin(Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Sub(
                Number.One,
                new Mul(new Cos(Variable.X), Number.One)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest1()
        {
            var exp = new Sub(new Mul(Variable.X, new Variable("y")), new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Sub(new Mul(Variable.X, Number.One), Number.One);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest2()
        {
            var exp = new Sub(Variable.X, new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal(Number.One, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest3()
        {
            var exp = new Sub(Variable.X, new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(Number.One);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest4()
        {
            var exp = new Sub(Variable.X, Number.One);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void UnaryMinusTest()
        {
            var exp = new UnaryMinus(new Sin(Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(new Cos(Variable.X), Number.One));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void UnaryMinusTest2()
        {
            var exp = new UnaryMinus(new Sin(new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void DiffVarTest()
        {
            var exp = new Mul(Variable.X, new Variable("y"));
            var deriv = Differentiate(exp);
            var expected = new Mul(Number.One, new Variable("y"));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void VarTest()
        {
            var exp = new Variable("y");
            var deriv = Differentiate(exp);

            Assert.Equal(new Variable("y"), deriv);
        }

        #endregion Common

        #region Trigonometric

        [Fact]
        public void SinDerivativeTest1()
        {
            var exp = new Sin(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Mul(new Cos(Variable.X), Number.One);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinDerivativeTest2()
        {
            var exp = new Sin(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Cos(new Mul(Number.Two, Variable.X)),
                new Mul(Number.Two, Number.One)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinPartialDerivativeTest1()
        {
            var exp = new Sin(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Cos(new Mul(Variable.X, new Variable("y"))),
                new Mul(Number.One, new Variable("y"))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinPartialDerivativeTest2()
        {
            var exp = new Sin(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(
                new Cos(new Mul(Variable.X, new Variable("y"))),
                new Mul(Variable.X, Number.One)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinPartialDerivativeTest3()
        {
            var exp = new Sin(new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CosDerivativeTest1()
        {
            var exp = new Cos(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(new Sin(Variable.X), Number.One));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosDerivativeTest2()
        {
            var exp = new Cos(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Mul(
                    new Sin(new Mul(Number.Two, Variable.X)),
                    new Mul(Number.Two, Number.One)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosPartialDerivativeTest1()
        {
            var exp = new Cos(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Mul(
                    new Sin(new Mul(Variable.X, new Variable("y"))),
                    new Mul(Number.One, new Variable("y"))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosPartialDerivativeTest2()
        {
            var exp = new Cos(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(
                new Mul(
                    new Sin(new Mul(Variable.X, new Variable("y"))),
                    new Mul(Variable.X, Number.One)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosPartialDerivativeTest3()
        {
            var exp = new Cos(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void TanDerivativeTest1()
        {
            var exp = new Tan(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(Number.One, new Pow(new Cos(Variable.X), Number.Two));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanDerivativeTest2()
        {
            var exp = new Tan(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Pow(new Cos(new Mul(Number.Two, Variable.X)), Number.Two)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanPartialDerivativeTest1()
        {
            var exp = new Tan(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.One, new Variable("y")),
                new Pow(new Cos(new Mul(Variable.X, new Variable("y"))), Number.Two)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanPartialDerivativeTest2()
        {
            var exp = new Tan(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(
                new Mul(Variable.X, Number.One),
                new Pow(new Cos(new Mul(Variable.X, new Variable("y"))), Number.Two)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanPartialDerivativeTest3()
        {
            var exp = new Tan(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CotDerivativeTest1()
        {
            var exp = new Cot(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(Number.One, new Pow(new Sin(Variable.X), Number.Two))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotDerivativeTest2()
        {
            var exp = new Cot(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Number.Two, Number.One),
                    new Pow(new Sin(new Mul(Number.Two, Variable.X)), Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotPartialDerivativeTest1()
        {
            var exp = new Cot(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Number.One, new Variable("y")),
                    new Pow(new Sin(new Mul(Variable.X, new Variable("y"))), Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotPartialDerivativeTest2()
        {
            var exp = new Cot(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Variable.X, Number.One),
                    new Pow(new Sin(new Mul(Variable.X, new Variable("y"))), Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotPartialDerivativeTest3()
        {
            var exp = new Cot(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void SecDerivativeTest1()
        {
            var exp = new Sec(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(Number.Two, Number.One),
                new Mul(
                    new Tan(new Mul(Number.Two, Variable.X)),
                    new Sec(new Mul(Number.Two, Variable.X))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SecDerivativeZeroTest()
        {
            var exp = new Sec(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CscDerivativeTest()
        {
            var exp = new Csc(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new UnaryMinus(new Mul(Number.Two, Number.One)),
                new Mul(
                    new Cot(new Mul(Number.Two, Variable.X)),
                    new Csc(new Mul(Number.Two, Variable.X))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinDerivativeTest1()
        {
            var exp = new Arcsin(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(
                Number.One,
                new Sqrt(new Sub(Number.One, new Pow(Variable.X, Number.Two)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinDerivativeTest2()
        {
            var exp = new Arcsin(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Sqrt(
                    new Sub(
                        Number.One,
                        new Pow(new Mul(Number.Two, Variable.X), Number.Two)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinPartialDerivativeTest1()
        {
            var exp = new Arcsin(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.One, new Variable("y")),
                new Sqrt(
                    new Sub(
                        Number.One,
                        new Pow(new Mul(Variable.X, new Variable("y")), Number.Two)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinPartialDerivativeTest2()
        {
            var exp = new Arcsin(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(
                new Mul(Variable.X, Number.One),
                new Sqrt(
                    new Sub(
                        Number.One,
                        new Pow(new Mul(Variable.X, new Variable("y")), Number.Two)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinPartialDerivativeTest3()
        {
            var exp = new Arcsin(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArccosDerivativeTest1()
        {
            var exp = new Arccos(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    Number.One,
                    new Sqrt(new Sub(Number.One, new Pow(Variable.X, Number.Two)))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosDerivativeTest2()
        {
            var exp = new Arccos(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Number.Two, Number.One),
                    new Sqrt(
                        new Sub(
                            Number.One,
                            new Pow(new Mul(Number.Two, Variable.X), Number.Two)
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosPartialDerivativeTest1()
        {
            var exp = new Arccos(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Number.One, new Variable("y")),
                    new Sqrt(
                        new Sub(
                            Number.One,
                            new Pow(new Mul(Variable.X, new Variable("y")), Number.Two)
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosPartialDerivativeTest2()
        {
            var exp = new Arccos(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(new Div(
                    new Mul(Variable.X, Number.One),
                    new Sqrt(
                        new Sub(
                            Number.One,
                            new Pow(new Mul(Variable.X, new Variable("y")), Number.Two)
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosPartialDerivativeTest3()
        {
            var exp = new Arccos(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArctanDerivativeTest1()
        {
            var exp = new Arctan(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(
                Number.One,
                new Add(Number.One, new Pow(Variable.X, Number.Two))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanDerivativeTest2()
        {
            var exp = new Arctan(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Add(
                    Number.One,
                    new Pow(new Mul(Number.Two, Variable.X), Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanPartialDerivativeTest1()
        {
            var exp = new Arctan(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.One, new Variable("y")),
                new Add(
                    Number.One,
                    new Pow(new Mul(Variable.X, new Variable("y")), Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanPartialDerivativeTest2()
        {
            var exp = new Arctan(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(
                new Mul(Variable.X, Number.One),
                new Add(
                    Number.One,
                    new Pow(new Mul(Variable.X, new Variable("y")), Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanPartialDerivativeTest3()
        {
            var exp = new Arctan(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArccotDerivativeTest1()
        {
            var exp = new Arccot(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    Number.One,
                    new Add(Number.One, new Pow(Variable.X, Number.Two))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotDerivativeTest2()
        {
            var exp = new Arccot(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Number.Two, Number.One),
                    new Add(
                        Number.One,
                        new Pow(new Mul(Number.Two, Variable.X), Number.Two)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotPartialDerivativeTest1()
        {
            var exp = new Arccot(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
                    new Mul(Number.One, new Variable("y")),
                    new Add(
                        Number.One,
                        new Pow(new Mul(Variable.X, new Variable("y")), Number.Two))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotPartialDerivativeTest2()
        {
            var exp = new Arccot(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Variable.X, Number.One),
                    new Add(
                        Number.One,
                        new Pow(new Mul(Variable.X, new Variable("y")), Number.Two)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotPartialDerivativeTest3()
        {
            var exp = new Arccot(Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArcsecDerivativeTest1()
        {
            var exp = new Arcsec(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Mul(
                    new Abs(new Mul(Number.Two, Variable.X)),
                    new Sqrt(
                        new Sub(
                            new Pow(new Mul(Number.Two, Variable.X), Number.Two),
                            Number.One
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsecDerivativeZeroTest()
        {
            var exp = new Arcsec(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArccscDerivativeTest1()
        {
            var exp = new Arccsc(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
                    new Mul(Number.Two, Number.One),
                    new Mul(
                        new Abs(new Mul(Number.Two, Variable.X)),
                        new Sqrt(
                            new Sub(
                                new Pow(new Mul(Number.Two, Variable.X), Number.Two),
                                Number.One
                            )
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccscDerivativeZeroTest()
        {
            var exp = new Arccsc(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        #endregion Trigonometric

        #region Hyperbolic

        [Fact]
        public void SinhDerivativeTest()
        {
            var exp = new Sinh(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(Number.Two, Number.One),
                new Cosh(new Mul(Number.Two, Variable.X))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinhDerivativeZeroTest()
        {
            var exp = new Sinh(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CoshDerivativeTest()
        {
            var exp = new Cosh(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(Number.Two, Number.One),
                new Sinh(new Mul(Number.Two, Variable.X))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CoshDerivativeZeroTest()
        {
            var exp = new Cosh(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void TanhDerivativeTest()
        {
            var exp = new Tanh(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Pow(new Cosh(new Mul(Number.Two, Variable.X)), Number.Two)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanhDerivativeZeroTest()
        {
            var exp = new Tanh(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CothDerivativeTest()
        {
            var exp = new Coth(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Number.Two, Number.One),
                    new Pow(new Sinh(new Mul(Number.Two, Variable.X)), Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CothDerivativeZeroTest()
        {
            var exp = new Coth(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void SechDerivativeTest()
        {
            var exp = new Sech(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Mul(
                    new Mul(Number.Two, Number.One),
                    new Mul(
                        new Tanh(new Mul(Number.Two, Variable.X)),
                        new Sech(new Mul(Number.Two, Variable.X))
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SechDerivativeZeroTest()
        {
            var exp = new Sech(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CschDerivativeTest()
        {
            var exp = new Csch(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Mul(
                    new Mul(Number.Two, Number.One),
                    new Mul(
                        new Coth(new Mul(Number.Two, Variable.X)),
                        new Csch(new Mul(Number.Two, Variable.X))
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CschDerivativeZeroTest()
        {
            var exp = new Csch(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArsinehDerivativeTest()
        {
            var exp = new Arsinh(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Sqrt(
                    new Add(
                        new Pow(new Mul(Number.Two, Variable.X), Number.Two),
                        Number.One
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArsinehDerivativeZeroTest()
        {
            var exp = new Arsinh(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArcoshDerivativeTest()
        {
            var exp = new Arcosh(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Sqrt(
                    new Sub(
                        new Pow(new Mul(Number.Two, Variable.X), Number.Two),
                        Number.One
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcoshDerivativeZeroTest()
        {
            var exp = new Arcosh(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArtanhDerivativeTest()
        {
            var exp = new Artanh(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Sub(
                    Number.One,
                    new Pow(new Mul(Number.Two, Variable.X), Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArtanhDerivativeZeroTest()
        {
            var exp = new Artanh(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArcothDerivativeTest()
        {
            var exp = new Arcoth(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(Number.Two, Number.One),
                new Sub(
                    Number.One,
                    new Pow(new Mul(Number.Two, Variable.X), Number.Two)
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcothDerivativeZeroTest()
        {
            var exp = new Arcoth(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArsechDerivativeTest()
        {
            var exp = new Arsech(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Number.Two, Number.One),
                    new Mul(
                        new Mul(Number.Two, Variable.X),
                        new Sqrt(
                            new Sub(
                                Number.One,
                                new Pow(new Mul(Number.Two, Variable.X), Number.Two)
                            )
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcschDerivativeTest()
        {
            var exp = new Arcsch(new Mul(Number.Two, Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(Number.Two, Number.One),
                    new Mul(
                        new Abs(new Mul(Number.Two, Variable.X)),
                        new Sqrt(
                            new Add(
                                Number.One,
                                new Pow(new Mul(Number.Two, Variable.X), Number.Two)
                            )
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcschDerivativeZeroTest()
        {
            var exp = new Arcsch(new Mul(Number.Two, new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        #endregion Hyperbolic

        [Fact]
        public void UserFunctionDerivTest()
        {
            var parameters = new FunctionCollection();
            var uf = new UserFunction("f", new IExpression[] { Variable.X });
            parameters.Add(uf, new Sin(Variable.X));

            var diff = Differentiate(uf, "x", parameters);
            var expected = new Mul(new Cos(Variable.X), Number.One);

            Assert.Equal(expected, diff);
        }

        [Fact]
        public void UserFunctionNullParamsDerivTest()
        {
            var uf = new UserFunction("f", new IExpression[] { Variable.X });

            Assert.Throws<InvalidOperationException>(() => Differentiate(uf, "x", null));
        }

        [Fact]
        public void DerivSimplify()
        {
            var simp = new Simplifier();
            var exp = new Simplify(simp, new Sin(Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Cos(Variable.X), Number.One);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DerivSimplify2()
        {
            var simp = new Simplifier();
            var exp = new Simplify(simp, new Sin(new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void DoubleDiffTest()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();

            var exp = new Derivative(
                diff, simp,
                new Derivative(
                    diff, simp,
                    new Sin(Variable.X),
                    Variable.X
                ),
                Variable.X
            );
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new UnaryMinus(new Mul(new Sin(Variable.X), Number.One)), Number.One
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DoubleDiffNoVarTest()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();

            var exp = new Derivative(diff, simp, new Derivative(diff, simp, new Sin(Number.One)));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void TripleDiffTest()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();

            var exp = new Derivative(
                diff, simp,
                new Derivative(
                    diff, simp,
                    new Derivative(
                        diff, simp,
                        new Sin(Variable.X),
                        Variable.X
                    ),
                    Variable.X
                ),
                Variable.X
            );
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new UnaryMinus(
                    new Mul(new Mul(new Cos(Variable.X), Number.One), Number.One)
                ),
                Number.One
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void NotSupportedTest()
        {
            Assert.Throws<NotSupportedException>(() => Differentiate(new Fact(Variable.X)));
        }
    }
}