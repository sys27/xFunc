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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Analyzers
{
    public class DifferentiatorTest
    {
        private readonly Number zero;

        public DifferentiatorTest()
        {
            zero = new Number(0);
        }

        private IExpression Differentiate(IExpression exp)
            => exp.Analyze(new Differentiator());

        private IExpression Differentiate(IExpression exp, Variable variable)
            => exp.Analyze(new Differentiator(variable));

        private IExpression Differentiate(IExpression exp, Variable variable, ExpressionParameters parameters)
            => exp.Analyze(new Differentiator(parameters, variable));

        #region Args

        [Fact]
        public void VariableIsNullTest()
        {
            var exp = Differentiate(new Number(10), null, null);

            Assert.Equal(zero, exp);
        }

        #endregion

        #region Common

        [Fact]
        public void NumberTest()
        {
            var exp = Differentiate(new Number(10));

            Assert.Equal(zero, exp);
        }

        [Fact]
        public void AbsDerivativeTest1()
        {
            var exp = Differentiate(new Abs(Variable.X));
            var expected = new Mul(
                new Number(1),
                new Div(Variable.X, new Abs(Variable.X))
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AbsDerivativeTest2()
        {
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Abs(mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(2), new Number(1)),
                new Div(
                    new Mul(new Number(2), Variable.X),
                    new Abs(new Mul(new Number(2), Variable.X))
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(3);
            var abs = new Abs(new Mul(new Number(3), Variable.X));
            Assert.Equal(abs, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AbsPartialDerivativeTest1()
        {
            var exp = new Abs(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(1), new Variable("y")),
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
                new Mul(Variable.X, new Number(1)),
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
            var exp = new Add(new Mul(new Number(2), Variable.X), new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Number(2), new Number(1));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddDerivativeTest2()
        {
            var exp = new Add(
                new Mul(new Number(2), Variable.X),
                new Mul(new Number(3), Variable.X)
            );
            var deriv = Differentiate(exp);
            var expected = new Add(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Number(3), new Number(1))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddDerivativeTest3()
        {
            // 2x + 3
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Add(mul, new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Number(2), new Number(1));

            Assert.Equal(expected, deriv);

            mul.Left = new Number(5);
            var add = new Add(new Mul(new Number(5), Variable.X), new Number(3));
            Assert.Equal(add, exp);
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
            var expected = new Add(new Mul(new Number(1), new Variable("y")), new Number(1));

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
            var expected = new Add(new Mul(Variable.X, new Number(1)), new Number(1));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddPartialDerivativeTest3()
        {
            var exp = new Add(Variable.X, new Number(1));
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void DivDerivativeTest1()
        {
            var exp = new Div(new Number(1), Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new UnaryMinus(new Mul(new Number(1), new Number(1))),
                new Pow(Variable.X, new Number(2))
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
                    new Mul(new Mul(new Cos(Variable.X), new Number(1)), Variable.X),
                    new Mul(new Sin(Variable.X), new Number(1))
                ),
                new Pow(Variable.X, new Number(2))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivDerivativeTest3()
        {
            // (2x) / (3x)
            var mul1 = new Mul(new Number(2), Variable.X);
            var mul2 = new Mul(new Number(3), Variable.X);

            var exp = new Div(mul1, mul2);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Sub(
                    new Mul(
                        new Mul(new Number(2), new Number(1)),
                        new Mul(new Number(3), Variable.X)
                    ),
                    new Mul(
                        new Mul(new Number(2), Variable.X),
                        new Mul(new Number(3), new Number(1))
                    )
                ),
                new Pow(new Mul(new Number(3), Variable.X), new Number(2))
            );

            Assert.Equal(expected, deriv);

            mul1.Left = new Number(4);
            mul2.Left = new Number(5);
            var div = new Div(
                new Mul(new Number(4), Variable.X),
                new Mul(new Number(5), Variable.X)
            );
            Assert.Equal(div, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest1()
        {
            // (y + x ^ 2) / x
            var exp = new Div(
                new Add(new Variable("y"), new Pow(Variable.X, new Number(2))),
                Variable.X
            );
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Sub(
                    new Mul(
                        new Mul(
                            new Number(1),
                            new Mul(new Number(2),
                                new Pow(Variable.X, new Sub(new Number(2), new Number(1)))
                            )
                        ),
                        Variable.X
                    ),
                    new Mul(
                        new Add(new Variable("y"), new Pow(Variable.X, new Number(2))),
                        new Number(1)
                    )
                ),
                new Pow(Variable.X, new Number(2))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest2()
        {
            var exp = new Div(new Variable("y"), Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new UnaryMinus(new Mul(new Variable("y"), new Number(1))),
                new Pow(Variable.X, new Number(2))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest3()
        {
            var exp = new Div(new Variable("y"), Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(new Number(1), Variable.X);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest4()
        {
            // (x + 1) / x
            var exp = new Div(new Add(Variable.X, new Number(1)), Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ExpDerivativeTest1()
        {
            var exp = new Exp(Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Mul(new Number(1), new Exp(Variable.X));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpDerivativeTest2()
        {
            var exp = new Exp(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(2), new Number(1)),
                new Exp(new Mul(new Number(2), Variable.X))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpDerivativeTest3()
        {
            // exp(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Exp(mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(2), new Number(1)),
                new Exp(new Mul(new Number(2), Variable.X))
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(6);
            var exp2 = new Exp(new Mul(new Number(6), Variable.X));
            Assert.Equal(exp2, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpPartialDerivativeTest1()
        {
            var exp = new Exp(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(1), new Variable("y")),
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
                new Mul(Variable.X, new Number(1)),
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
            var exp = new Ln(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Number(2), Variable.X)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LnDerivativeTest2()
        {
            // ln(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Ln(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Number(2), Variable.X)
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(5);
            var ln = new Ln(new Mul(new Number(5), Variable.X));
            Assert.Equal(ln, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LnPartialDerivativeTest1()
        {
            // ln(xy)
            var exp = new Ln(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(1), new Variable("y")),
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
                new Mul(Variable.X, new Number(1)),
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
            var exp = new Lg(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Mul(new Number(2), Variable.X), new Ln(new Number(10)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LgDerivativeTest2()
        {
            // lg(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Lg(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Mul(new Number(2), Variable.X), new Ln(new Number(10)))
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(3);
            var lg = new Lg(new Mul(new Number(3), Variable.X));
            Assert.Equal(lg, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LgPartialDerivativeTest1()
        {
            // lg(2xy)
            var exp = new Lg(new Mul(new Mul(new Number(2), Variable.X), new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Mul(new Number(2), new Number(1)), new Variable("y")),
                new Mul(
                    new Mul(new Mul(new Number(2), Variable.X), new Variable("y")),
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
            var exp = new Lb(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Mul(new Number(2), Variable.X), new Ln(new Number(2)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LbDerivativeTest2()
        {
            // lb(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Lb(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Mul(new Number(2), Variable.X), new Ln(new Number(2)))
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(3);
            var lb = new Lb(new Mul(new Number(3), Variable.X));
            Assert.Equal(lb, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LbPartialDerivativeTest1()
        {
            // lb(2xy)
            var exp = new Lb(new Mul(new Mul(new Number(2), Variable.X), new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Mul(new Number(2), new Number(1)), new Variable("y")),
                new Mul(
                    new Mul(new Mul(new Number(2), Variable.X), new Variable("y")),
                    new Ln(new Number(2))
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
            var exp = new Log(new Number(2), Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(new Number(1), new Mul(Variable.X, new Ln(new Number(2))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogDerivativeTest2()
        {
            // log(x, 2)
            var exp = new Log(new Number(2), Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Div(new Number(1), new Mul(Variable.X, new Ln(new Number(2))));

            Assert.Equal(expected, deriv);

            exp.Left = new Number(4);
            var log = new Log(new Number(4), Variable.X);
            Assert.Equal(log, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogDerivativeTest3()
        {
            var exp = new Log(Variable.X, new Number(2));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new UnaryMinus(
                    new Mul(new Ln(new Number(2)), new Div(new Number(1), Variable.X))
                ),
                new Pow(new Ln(Variable.X), new Number(2))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogPartialDerivativeTest1()
        {
            var exp = new Log(new Number(2), Variable.X);
            var deriv = Differentiate(exp, Variable.X);
            var expected = new Div(
                new Number(1),
                new Mul(Variable.X, new Ln(new Number(2)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogPartialDerivativeTest2()
        {
            var exp = new Log(new Number(2), Variable.X);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void MulDerivativeTest1()
        {
            var exp = new Mul(new Number(2), Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Mul(new Number(2), new Number(1));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulDerivativeTest2()
        {
            // 2x
            var exp = new Mul(new Number(2), Variable.X);
            var deriv = Differentiate(exp);
            var expected = new Mul(new Number(2), new Number(1));

            Assert.Equal(expected, deriv);

            exp.Left = new Number(3);
            var mul = new Mul(new Number(3), Variable.X);
            Assert.Equal(mul, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest1()
        {
            // (x + 1) * (y + x)
            var exp = new Mul(
                new Add(Variable.X, new Number(1)),
                new Add(new Variable("y"), Variable.X)
            );
            var deriv = Differentiate(exp);
            var expected = new Add(
                new Mul(new Number(1), new Add(new Variable("y"), Variable.X)),
                new Mul(new Add(Variable.X, new Number(1)), new Number(1))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest2()
        {
            // (y + 1) * (3 + x)
            var exp = new Mul(
                new Add(new Variable("y"), new Number(1)),
                new Add(new Number(3), Variable.X)
            );
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(new Number(1), new Add(new Number(3), Variable.X));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest3()
        {
            // (x + 1) * (y + x)
            var exp = new Mul(
                new Add(Variable.X, new Number(1)),
                new Add(new Variable("y"), Variable.X)
            );
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(new Add(Variable.X, new Number(1)), new Number(1));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest4()
        {
            // (x + 1) * (3 + x)
            var exp = new Mul(
                new Add(Variable.X, new Number(1)),
                new Add(new Number(3), Variable.X)
            );
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void PowDerivativeTest1()
        {
            var exp = new Pow(Variable.X, new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Number(1),
                new Mul(new Number(3), new Pow(Variable.X, new Sub(new Number(3), new Number(1))))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowDerivativeTest2()
        {
            // 2 ^ (3x)
            var exp = new Pow(new Number(2), new Mul(new Number(3), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(
                    new Ln(new Number(2)),
                    new Pow(new Number(2), new Mul(new Number(3), Variable.X))
                ),
                new Mul(new Number(3), new Number(1))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowDerivativeTest3()
        {
            // x ^ 3
            var exp = new Pow(Variable.X, new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Number(1),
                new Mul(
                    new Number(3),
                    new Pow(Variable.X, new Sub(new Number(3), new Number(1)))
                )
            );

            Assert.Equal(expected, deriv);

            exp.Right = new Number(4);
            var pow1 = new Pow(Variable.X, new Number(4));
            Assert.Equal(pow1, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowDerivativeTest4()
        {
            // 2 ^ (3x)
            var mul = new Mul(new Number(3), Variable.X);

            var exp = new Pow(new Number(2), mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(
                    new Ln(new Number(2)),
                    new Pow(new Number(2), new Mul(new Number(3), Variable.X))
                ),
                new Mul(new Number(3), new Number(1))
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(4);
            var pow2 = new Pow(new Number(2), new Mul(new Number(4), Variable.X));
            Assert.Equal(pow2, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowPartialDerivativeTest1()
        {
            // (yx) ^ 3
            var exp = new Pow(new Mul(new Variable("y"), Variable.X), new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Variable("y"), new Number(1)),
                new Mul(
                    new Number(3),
                    new Pow(
                        new Mul(new Variable("y"), Variable.X),
                        new Sub(new Number(3), new Number(1))
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
                new Mul(new Number(1), Variable.X),
                new Mul(
                    new Number(3),
                    new Pow(
                        new Mul(new Variable("y"), Variable.X),
                        new Sub(new Number(3), new Number(1))
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
                new Number(1),
                new Mul(
                    new Div(new Number(1), new Number(3)),
                    new Pow(
                        Variable.X,
                        new Sub(new Div(new Number(1), new Number(3)), new Number(1))
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void RootDerivativeTest2()
        {
            // root(x, 3)
            var exp = new Root(Variable.X, new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Number(1),
                new Mul(
                    new Div(new Number(1), new Number(3)),
                    new Pow(
                        Variable.X,
                        new Sub(new Div(new Number(1), new Number(3)), new Number(1))
                    )
                )
            );

            Assert.Equal(expected, deriv);

            exp.Right = new Number(4);
            var root = new Root(Variable.X, new Number(4));
            Assert.Equal(root, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void RootPartialDerivativeTest1()
        {
            var exp = new Root(new Mul(Variable.X, new Variable("y")), new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(1), new Variable("y")),
                new Mul(
                    new Div(new Number(1), new Number(3)),
                    new Pow(
                        new Mul(Variable.X, new Variable("y")),
                        new Sub(new Div(new Number(1), new Number(3)), new Number(1))
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
            var exp = new Sqrt(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Number(2), new Sqrt(new Mul(new Number(2), Variable.X)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SqrtDerivativeTest2()
        {
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Sqrt(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Number(2), new Sqrt(new Mul(new Number(2), Variable.X)))
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(3);
            var sqrt = new Sqrt(new Mul(new Number(3), Variable.X));
            Assert.Equal(sqrt, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SqrtPartialDerivativeTest1()
        {
            // sqrt(2xy)
            var exp = new Sqrt(
                new Mul(new Mul(new Number(2), Variable.X), new Variable("y"))
            );
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Mul(new Number(2), new Number(1)), new Variable("y")),
                new Mul(
                    new Number(2),
                    new Sqrt(
                        new Mul(new Mul(new Number(2), Variable.X), new Variable("y"))
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
                new Number(1),
                new Mul(new Cos(Variable.X), new Number(1))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubDerivativeTest2()
        {
            var mul1 = new Mul(new Number(2), Variable.X);
            var mul2 = new Mul(new Number(3), Variable.X);

            var exp = new Sub(mul1, mul2);
            var deriv = Differentiate(exp);
            var expected = new Sub(
                new Mul(new Number(2), new Number(1)),
                new Mul(new Number(3), new Number(1))
            );

            Assert.Equal(expected, deriv);

            mul1.Left = new Number(5);
            mul2.Left = new Number(4);
            var sub = new Sub(
                new Mul(new Number(5), Variable.X),
                new Mul(new Number(4), Variable.X)
            );
            Assert.Equal(sub, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest1()
        {
            var exp = new Sub(new Mul(Variable.X, new Variable("y")), new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Sub(new Mul(Variable.X, new Number(1)), new Number(1));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest2()
        {
            var exp = new Sub(Variable.X, new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal(new Number(1), deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest3()
        {
            var exp = new Sub(Variable.X, new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(new Number(1));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest4()
        {
            var exp = new Sub(Variable.X, new Number(1));
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void UnaryMinusTest()
        {
            var exp = new UnaryMinus(new Sin(Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(new Cos(Variable.X), new Number(1)));

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
            var expected = new Mul(new Number(1), new Variable("y"));

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
            var expected = new Mul(new Cos(Variable.X), new Number(1));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinDerivativeTest2()
        {
            var exp = new Sin(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Cos(new Mul(new Number(2), Variable.X)),
                new Mul(new Number(2), new Number(1))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinDerivativeTest3()
        {
            // sin(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Sin(mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Cos(new Mul(new Number(2), Variable.X)),
                new Mul(new Number(2), new Number(1))
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(3);
            var sin = new Sin(new Mul(new Number(3), Variable.X));
            Assert.Equal(sin, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinPartialDerivativeTest1()
        {
            var exp = new Sin(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Cos(new Mul(Variable.X, new Variable("y"))),
                new Mul(new Number(1), new Variable("y"))
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
                new Mul(Variable.X, new Number(1))
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
            var expected = new UnaryMinus(new Mul(new Sin(Variable.X), new Number(1)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosDerivativeTest2()
        {
            var exp = new Cos(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Mul(
                    new Sin(new Mul(new Number(2), Variable.X)),
                    new Mul(new Number(2), new Number(1))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosDerivativeTest3()
        {
            // cos(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Cos(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Mul(
                    new Sin(new Mul(new Number(2), Variable.X)),
                    new Mul(new Number(2), new Number(1))
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(7);
            var cos = new Cos(new Mul(new Number(7), Variable.X));
            Assert.Equal(cos, exp);
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
                    new Mul(new Number(1), new Variable("y"))
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
                    new Mul(Variable.X, new Number(1))
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
            var expected = new Div(new Number(1), new Pow(new Cos(Variable.X), new Number(2)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanDerivativeTest2()
        {
            var exp = new Tan(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Pow(new Cos(new Mul(new Number(2), Variable.X)), new Number(2))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanDerivativeTest3()
        {
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Tan(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Pow(new Cos(new Mul(new Number(2), Variable.X)), new Number(2))
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(5);
            var tan = new Tan(new Mul(new Number(5), Variable.X));
            Assert.Equal(tan, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanPartialDerivativeTest1()
        {
            var exp = new Tan(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(1), new Variable("y")),
                new Pow(new Cos(new Mul(Variable.X, new Variable("y"))), new Number(2))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanPartialDerivativeTest2()
        {
            var exp = new Tan(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(
                new Mul(Variable.X, new Number(1)),
                new Pow(new Cos(new Mul(Variable.X, new Variable("y"))), new Number(2))
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
                new Div(new Number(1), new Pow(new Sin(Variable.X), new Number(2)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotDerivativeTest2()
        {
            var exp = new Cot(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Pow(new Sin(new Mul(new Number(2), Variable.X)), new Number(2))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotDerivativeTest3()
        {
            // cot(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Cot(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Pow(new Sin(new Mul(new Number(2), Variable.X)), new Number(2))
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(3);
            var cot = new Cot(new Mul(new Number(3), Variable.X));
            Assert.Equal(cot, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotPartialDerivativeTest1()
        {
            var exp = new Cot(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(1), new Variable("y")),
                    new Pow(new Sin(new Mul(Variable.X, new Variable("y"))), new Number(2))
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
                    new Mul(Variable.X, new Number(1)),
                    new Pow(new Sin(new Mul(Variable.X, new Variable("y"))), new Number(2))
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
            var exp = new Sec(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(2), new Number(1)),
                new Mul(
                    new Tan(new Mul(new Number(2), Variable.X)),
                    new Sec(new Mul(new Number(2), Variable.X))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SecDerivativeTest2()
        {
            // sec(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Sec(mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(2), new Number(1)),
                new Mul(
                    new Tan(new Mul(new Number(2), Variable.X)),
                    new Sec(new Mul(new Number(2), Variable.X))
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(4);
            var sec = new Sec(new Mul(new Number(4), Variable.X));
            Assert.Equal(sec, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SecDerivativeZeroTest()
        {
            var exp = new Sec(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CscDerivativeTest()
        {
            var exp = new Csc(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new UnaryMinus(new Mul(new Number(2), new Number(1))),
                new Mul(
                    new Cot(new Mul(new Number(2), Variable.X)),
                    new Csc(new Mul(new Number(2), Variable.X))
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
                new Number(1),
                new Sqrt(new Sub(new Number(1), new Pow(Variable.X, new Number(2))))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinDerivativeTest2()
        {
            var exp = new Arcsin(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Sqrt(
                    new Sub(
                        new Number(1),
                        new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinDerivativeTest3()
        {
            // arcsin(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Arcsin(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Sqrt(
                    new Sub(
                        new Number(1),
                        new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                    )
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(5);
            var arcsin = new Arcsin(new Mul(new Number(5), Variable.X));
            Assert.Equal(arcsin, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinPartialDerivativeTest1()
        {
            var exp = new Arcsin(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(1), new Variable("y")),
                new Sqrt(
                    new Sub(
                        new Number(1),
                        new Pow(new Mul(Variable.X, new Variable("y")), new Number(2))
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
                new Mul(Variable.X, new Number(1)),
                new Sqrt(
                    new Sub(
                        new Number(1),
                        new Pow(new Mul(Variable.X, new Variable("y")), new Number(2))
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
                    new Number(1),
                    new Sqrt(new Sub(new Number(1), new Pow(Variable.X, new Number(2))))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosDerivativeTest2()
        {
            var exp = new Arccos(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Sqrt(
                        new Sub(
                            new Number(1),
                            new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosDerivativeTest3()
        {
            // arccos(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Arccos(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Sqrt(
                        new Sub(
                            new Number(1),
                            new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(6);
            var arccos = new Arccos(new Mul(new Number(6), Variable.X));
            Assert.Equal(arccos, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosPartialDerivativeTest1()
        {
            var exp = new Arccos(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(1), new Variable("y")),
                    new Sqrt(
                        new Sub(
                            new Number(1),
                            new Pow(new Mul(Variable.X, new Variable("y")), new Number(2))
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
                    new Mul(Variable.X, new Number(1)),
                    new Sqrt(
                        new Sub(
                            new Number(1),
                            new Pow(new Mul(Variable.X, new Variable("y")), new Number(2))
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
                new Number(1),
                new Add(new Number(1), new Pow(Variable.X, new Number(2)))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanDerivativeTest2()
        {
            var exp = new Arctan(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Add(
                    new Number(1),
                    new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanDerivativeTest3()
        {
            // arctan(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Arctan(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Add(
                    new Number(1),
                    new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(6);
            var arctan = new Arctan(new Mul(new Number(6), Variable.X));
            Assert.Equal(arctan, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanPartialDerivativeTest1()
        {
            var exp = new Arctan(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(1), new Variable("y")),
                new Add(
                    new Number(1),
                    new Pow(new Mul(Variable.X, new Variable("y")), new Number(2))
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
                new Mul(Variable.X, new Number(1)),
                new Add(
                    new Number(1),
                    new Pow(new Mul(Variable.X, new Variable("y")), new Number(2))
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
                    new Number(1),
                    new Add(new Number(1), new Pow(Variable.X, new Number(2)))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotDerivativeTest2()
        {
            var exp = new Arccot(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Add(
                        new Number(1),
                        new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotDerivativeTest3()
        {
            // arccot(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Arccot(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Add(
                        new Number(1),
                        new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                    )
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(4);
            var arccot = new Arccot(new Mul(new Number(4), Variable.X));
            Assert.Equal(arccot, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotPartialDerivativeTest1()
        {
            var exp = new Arccot(new Mul(Variable.X, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
                    new Mul(new Number(1), new Variable("y")),
                    new Add(
                        new Number(1),
                        new Pow(new Mul(Variable.X, new Variable("y")), new Number(2)))
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
                    new Mul(Variable.X, new Number(1)),
                    new Add(
                        new Number(1),
                        new Pow(new Mul(Variable.X, new Variable("y")), new Number(2))
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
            var exp = new Arcsec(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(
                    new Abs(new Mul(new Number(2), Variable.X)),
                    new Sqrt(
                        new Sub(
                            new Pow(new Mul(new Number(2), Variable.X), new Number(2)),
                            new Number(1)
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsecDerivativeTest2()
        {
            // arcsec(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Arcsec(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Mul(
                    new Abs(new Mul(new Number(2), Variable.X)),
                    new Sqrt(
                        new Sub(
                            new Pow(new Mul(new Number(2), Variable.X), new Number(2)),
                            new Number(1)
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(4);
            var arcsec = new Arcsec(new Mul(new Number(4), Variable.X));
            Assert.Equal(arcsec, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsecDerivativeZeroTest()
        {
            var exp = new Arcsec(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArccscDerivativeTest1()
        {
            var exp = new Arccsc(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Mul(
                        new Abs(new Mul(new Number(2), Variable.X)),
                        new Sqrt(
                            new Sub(
                                new Pow(new Mul(new Number(2), Variable.X), new Number(2)),
                                new Number(1)
                            )
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccscDerivativeTest2()
        {
            // arccsc(2x)
            var mul = new Mul(new Number(2), Variable.X);

            var exp = new Arccsc(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Mul(
                        new Abs(new Mul(new Number(2), Variable.X)),
                        new Sqrt(
                            new Sub(
                                new Pow(new Mul(new Number(2), Variable.X), new Number(2)),
                                new Number(1)
                            )
                        )
                    )
                )
            );

            Assert.Equal(expected, deriv);

            mul.Left = new Number(4);
            var arccsc = new Arccsc(new Mul(new Number(4), Variable.X));
            Assert.Equal(arccsc, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccscDerivativeZeroTest()
        {
            var exp = new Arccsc(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        #endregion Trigonometric

        #region Hyperbolic

        [Fact]
        public void SinhDerivativeTest()
        {
            var exp = new Sinh(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(2), new Number(1)),
                new Cosh(new Mul(new Number(2), Variable.X))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinhDerivativeZeroTest()
        {
            var exp = new Sinh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CoshDerivativeTest()
        {
            var exp = new Cosh(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Number(2), new Number(1)),
                new Sinh(new Mul(new Number(2), Variable.X))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CoshDerivativeZeroTest()
        {
            var exp = new Cosh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void TanhDerivativeTest()
        {
            var exp = new Tanh(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Pow(new Cosh(new Mul(new Number(2), Variable.X)), new Number(2))
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanhDerivativeZeroTest()
        {
            var exp = new Tanh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CothDerivativeTest()
        {
            var exp = new Coth(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Pow(new Sinh(new Mul(new Number(2), Variable.X)), new Number(2))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CothDerivativeZeroTest()
        {
            var exp = new Coth(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void SechDerivativeTest()
        {
            var exp = new Sech(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Mul(
                    new Mul(new Number(2), new Number(1)),
                    new Mul(
                        new Tanh(new Mul(new Number(2), Variable.X)),
                        new Sech(new Mul(new Number(2), Variable.X))
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SechDerivativeZeroTest()
        {
            var exp = new Sech(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CschDerivativeTest()
        {
            var exp = new Csch(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Mul(
                    new Mul(new Number(2), new Number(1)),
                    new Mul(
                        new Coth(new Mul(new Number(2), Variable.X)),
                        new Csch(new Mul(new Number(2), Variable.X))
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CschDerivativeZeroTest()
        {
            var exp = new Csch(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArsinehDerivativeTest()
        {
            var exp = new Arsinh(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Sqrt(
                    new Add(
                        new Pow(new Mul(new Number(2), Variable.X), new Number(2)),
                        new Number(1)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArsinehDerivativeZeroTest()
        {
            var exp = new Arsinh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArcoshDerivativeTest()
        {
            var exp = new Arcosh(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Sqrt(
                    new Sub(
                        new Pow(new Mul(new Number(2), Variable.X), new Number(2)),
                        new Number(1)
                    )
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcoshDerivativeZeroTest()
        {
            var exp = new Arcosh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArtanhDerivativeTest()
        {
            var exp = new Artanh(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Sub(
                    new Number(1),
                    new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArtanhDerivativeZeroTest()
        {
            var exp = new Artanh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArcothDerivativeTest()
        {
            var exp = new Arcoth(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Number(2), new Number(1)),
                new Sub(
                    new Number(1),
                    new Pow(new Mul(new Number(2), Variable.X), new Number(2))
                )
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcothDerivativeZeroTest()
        {
            var exp = new Arcoth(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArsechDerivativeTest()
        {
            var exp = new Arsech(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Mul(
                        new Mul(new Number(2), Variable.X),
                        new Sqrt(
                            new Sub(
                                new Number(1),
                                new Pow(new Mul(new Number(2), Variable.X), new Number(2))
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
            var exp = new Arcsch(new Mul(new Number(2), Variable.X));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(
                new Div(
                    new Mul(new Number(2), new Number(1)),
                    new Mul(
                        new Abs(new Mul(new Number(2), Variable.X)),
                        new Sqrt(
                            new Add(
                                new Number(1),
                                new Pow(new Mul(new Number(2), Variable.X), new Number(2))
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
            var exp = new Arcsch(new Mul(new Number(2), new Variable("z")));
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
            var expected = new Mul(new Cos(Variable.X), new Number(1));

            Assert.Equal(expected, diff);
        }

        [Fact]
        public void UserFunctionDerivNullTest()
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
            var expected = new Mul(new Cos(Variable.X), new Number(1));

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
                new UnaryMinus(new Mul(new Sin(Variable.X), new Number(1))), new Number(1)
            );

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DoubleDiffNoVarTest()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();

            var exp = new Derivative(diff, simp, new Derivative(diff, simp, new Sin(new Number(1))));
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
                    new Mul(new Mul(new Cos(Variable.X), new Number(1)), new Number(1))
                ),
                new Number(1)
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