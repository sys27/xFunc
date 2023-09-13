// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.DifferentiatorTests;

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
        => exp.Analyze(differentiator, new DifferentiatorContext());

    private IExpression Differentiate(IExpression exp, Variable variable)
        => exp.Analyze(differentiator, new DifferentiatorContext(variable));

    #region Common

    [Test]
    public void NumberTest()
    {
        var exp = Differentiate(new Number(10));

        Assert.That(exp, Is.EqualTo(zero));
    }

    [Test]
    public void AngleNumberTest()
    {
        var exp = Differentiate(new Angle(AngleValue.Degree(10)));

        Assert.That(exp, Is.EqualTo(zero));
    }

    [Test]
    public void PowerNumberTest()
    {
        var exp = Differentiate(new Power(PowerValue.Watt(10)));

        Assert.That(exp, Is.EqualTo(zero));
    }

    [Test]
    public void TemperatureNumberTest()
    {
        var exp = Differentiate(TemperatureValue.Celsius(10).AsExpression());

        Assert.That(exp, Is.EqualTo(zero));
    }

    [Test]
    public void MassNumberTest()
    {
        var exp = Differentiate(MassValue.Kilogram(10).AsExpression());

        Assert.That(exp, Is.EqualTo(zero));
    }

    [Test]
    public void LengthNumberTest()
    {
        var exp = Differentiate(LengthValue.Kilometer(10).AsExpression());

        Assert.That(exp, Is.EqualTo(zero));
    }

    [Test]
    public void TimeNumberTest()
    {
        var exp = Differentiate(TimeValue.Second(10).AsExpression());

        Assert.That(exp, Is.EqualTo(zero));
    }

    [Test]
    public void AreaNumberTest()
    {
        var exp = Differentiate(AreaValue.Meter(10).AsExpression());

        Assert.That(exp, Is.EqualTo(zero));
    }

    [Test]
    public void VolumeNumberTest()
    {
        var exp = Differentiate(VolumeValue.Meter(10).AsExpression());

        Assert.That(exp, Is.EqualTo(zero));
    }

    [Test]
    public void VariableNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => Differentiate(new Number(10), null));
    }

    [Test]
    public void AbsDerivativeTest1()
    {
        var exp = Differentiate(new Abs(Variable.X));
        var expected = new Mul(
            Number.One,
            new Div(Variable.X, new Abs(Variable.X))
        );

        Assert.That(exp, Is.EqualTo(expected));
    }

    [Test]
    public void AbsPartialDerivativeTest1()
    {
        var exp = new Abs(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            new Mul(Number.One, Variable.Y),
            new Div(
                new Mul(Variable.X, Variable.Y),
                new Abs(new Mul(Variable.X, Variable.Y))
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void AbsPartialDerivativeTest2()
    {
        var exp = new Abs(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Mul(
            new Mul(Variable.X, Number.One),
            new Div(
                new Mul(Variable.X, Variable.Y),
                new Abs(new Mul(Variable.X, Variable.Y))
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void AbsPartialDerivativeTest3()
    {
        var deriv = Differentiate(new Abs(Variable.X), Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void AddDerivativeTest1()
    {
        var exp = new Add(new Mul(Number.Two, Variable.X), new Number(3));
        var deriv = Differentiate(exp);
        var expected = new Mul(Number.Two, Number.One);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void AddPartialDerivativeTest1()
    {
        var exp = new Add(
            new Add(new Mul(Variable.X, Variable.Y), Variable.X),
            Variable.Y
        );
        var deriv = Differentiate(exp);
        var expected = new Add(new Mul(Number.One, Variable.Y), Number.One);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void AddPartialDerivativeTest2()
    {
        var exp = new Add(
            new Add(new Mul(Variable.X, Variable.Y), Variable.X),
            Variable.Y
        );
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Add(new Mul(Variable.X, Number.One), Number.One);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void AddPartialDerivativeTest3()
    {
        var exp = new Add(Variable.X, Number.One);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void DivDerivativeTest1()
    {
        var exp = new Div(Number.One, Variable.X);
        var deriv = Differentiate(exp);
        var expected = new Div(
            new UnaryMinus(new Mul(Number.One, Number.One)),
            new Pow(Variable.X, Number.Two)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void DivPartialDerivativeTest1()
    {
        // (y + x ^ 2) / x
        var exp = new Div(
            new Add(Variable.Y, new Pow(Variable.X, Number.Two)),
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
                    new Add(Variable.Y, new Pow(Variable.X, Number.Two)),
                    Number.One
                )
            ),
            new Pow(Variable.X, Number.Two)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void DivPartialDerivativeTest2()
    {
        var exp = new Div(Variable.Y, Variable.X);
        var deriv = Differentiate(exp);
        var expected = new Div(
            new UnaryMinus(new Mul(Variable.Y, Number.One)),
            new Pow(Variable.X, Number.Two)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void DivPartialDerivativeTest3()
    {
        var exp = new Div(Variable.Y, Variable.X);
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Div(Number.One, Variable.X);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void DivPartialDerivativeTest4()
    {
        // (x + 1) / x
        var exp = new Div(new Add(Variable.X, Number.One), Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void ExpDerivativeTest1()
    {
        var exp = new Exp(Variable.X);
        var deriv = Differentiate(exp);
        var expected = new Mul(Number.One, new Exp(Variable.X));

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ExpDerivativeTest2()
    {
        var exp = new Exp(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            new Mul(Number.Two, Number.One),
            new Exp(new Mul(Number.Two, Variable.X))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ExpPartialDerivativeTest1()
    {
        var exp = new Exp(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            new Mul(Number.One, Variable.Y),
            new Exp(new Mul(Variable.X, Variable.Y))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ExpPartialDerivativeTest2()
    {
        var exp = new Exp(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Mul(
            new Mul(Variable.X, Number.One),
            new Exp(new Mul(Variable.X, Variable.Y))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ExpPartialDerivativeTest3()
    {
        var exp = new Exp(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void LnDerivativeTest1()
    {
        var exp = new Ln(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.Two, Number.One),
            new Mul(Number.Two, Variable.X)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void LnPartialDerivativeTest1()
    {
        // ln(xy)
        var exp = new Ln(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.One, Variable.Y),
            new Mul(Variable.X, Variable.Y)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void LnPartialDerivativeTest2()
    {
        // ln(xy)
        var exp = new Ln(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Div(
            new Mul(Variable.X, Number.One),
            new Mul(Variable.X, Variable.Y)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void LnPartialDerivativeTest3()
    {
        var exp = new Ln(Variable.Y);
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void LgDerivativeTest1()
    {
        var exp = new Lg(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.Two, Number.One),
            new Mul(new Mul(Number.Two, Variable.X), new Ln(new Number(10)))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void LgPartialDerivativeTest1()
    {
        // lg(2xy)
        var exp = new Lg(new Mul(new Mul(Number.Two, Variable.X), Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(new Mul(Number.Two, Number.One), Variable.Y),
            new Mul(
                new Mul(new Mul(Number.Two, Variable.X), Variable.Y),
                new Ln(new Number(10))
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void LgPartialDerivativeTest2()
    {
        // lg(2xy)
        var exp = new Lg(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void LbDerivativeTest1()
    {
        var exp = new Lb(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.Two, Number.One),
            new Mul(new Mul(Number.Two, Variable.X), new Ln(Number.Two))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void LbPartialDerivativeTest1()
    {
        // lb(2xy)
        var exp = new Lb(new Mul(new Mul(Number.Two, Variable.X), Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(new Mul(Number.Two, Number.One), Variable.Y),
            new Mul(
                new Mul(new Mul(Number.Two, Variable.X), Variable.Y),
                new Ln(Number.Two)
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void LbPartialDerivativeTest2()
    {
        // lb(2xy)
        var exp = new Lb(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void LogDerivativeTest1()
    {
        var exp = new Log(Number.Two, Variable.X);
        var deriv = Differentiate(exp);
        var expected = new Div(Number.One, new Mul(Variable.X, new Ln(Number.Two)));

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void LogPartialDerivativeTest1()
    {
        var exp = new Log(Number.Two, Variable.X);
        var deriv = Differentiate(exp, Variable.X);
        var expected = new Div(
            Number.One,
            new Mul(Variable.X, new Ln(Number.Two))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void LogPartialDerivativeTest2()
    {
        var exp = new Log(Number.Two, Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void MulDerivativeTest1()
    {
        var exp = new Mul(Number.Two, Variable.X);
        var deriv = Differentiate(exp);
        var expected = new Mul(Number.Two, Number.One);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void MulPartialDerivativeTest1()
    {
        // (x + 1) * (y + x)
        var exp = new Mul(
            new Add(Variable.X, Number.One),
            new Add(Variable.Y, Variable.X)
        );
        var deriv = Differentiate(exp);
        var expected = new Add(
            new Mul(Number.One, new Add(Variable.Y, Variable.X)),
            new Mul(new Add(Variable.X, Number.One), Number.One)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void MulPartialDerivativeTest2()
    {
        // (y + 1) * (3 + x)
        var exp = new Mul(
            new Add(Variable.Y, Number.One),
            new Add(new Number(3), Variable.X)
        );
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Mul(Number.One, new Add(new Number(3), Variable.X));

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void MulPartialDerivativeTest3()
    {
        // (x + 1) * (y + x)
        var exp = new Mul(
            new Add(Variable.X, Number.One),
            new Add(Variable.Y, Variable.X)
        );
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Mul(new Add(Variable.X, Number.One), Number.One);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void MulPartialDerivativeTest4()
    {
        // (x + 1) * (3 + x)
        var exp = new Mul(
            new Add(Variable.X, Number.One),
            new Add(new Number(3), Variable.X)
        );
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void PowDerivativeTest1()
    {
        var exp = new Pow(Variable.X, new Number(3));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            Number.One,
            new Mul(new Number(3), new Pow(Variable.X, new Sub(new Number(3), Number.One)))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void PowPartialDerivativeTest1()
    {
        // (yx) ^ 3
        var exp = new Pow(new Mul(Variable.Y, Variable.X), new Number(3));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            new Mul(Variable.Y, Number.One),
            new Mul(
                new Number(3),
                new Pow(
                    new Mul(Variable.Y, Variable.X),
                    new Sub(new Number(3), Number.One)
                )
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void PowPartialDerivativeTest2()
    {
        // (yx) ^ 3
        var exp = new Pow(new Mul(Variable.Y, Variable.X), new Number(3));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Mul(
            new Mul(Number.One, Variable.X),
            new Mul(
                new Number(3),
                new Pow(
                    new Mul(Variable.Y, Variable.X),
                    new Sub(new Number(3), Number.One)
                )
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void PowPartialDerivativeTest3()
    {
        var exp = new Pow(Variable.X, new Number(3));
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void RootPartialDerivativeTest1()
    {
        var exp = new Root(new Mul(Variable.X, Variable.Y), new Number(3));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            new Mul(Number.One, Variable.Y),
            new Mul(
                new Div(Number.One, new Number(3)),
                new Pow(
                    new Mul(Variable.X, Variable.Y),
                    new Sub(new Div(Number.One, new Number(3)), Number.One)
                )
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void RootPartialDerivativeTest2()
    {
        var exp = new Root(Variable.Y, new Number(3));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void SqrtDerivativeTest1()
    {
        var exp = new Sqrt(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.Two, Number.One),
            new Mul(Number.Two, new Sqrt(new Mul(Number.Two, Variable.X)))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SqrtPartialDerivativeTest1()
    {
        // sqrt(2xy)
        var exp = new Sqrt(
            new Mul(new Mul(Number.Two, Variable.X), Variable.Y)
        );
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(new Mul(Number.Two, Number.One), Variable.Y),
            new Mul(
                Number.Two,
                new Sqrt(
                    new Mul(new Mul(Number.Two, Variable.X), Variable.Y)
                )
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SqrtPartialDerivativeTest2()
    {
        var exp = new Sqrt(Variable.Y);
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void SubDerivativeTest1()
    {
        // x - sin(x)
        var exp = new Sub(Variable.X, new Sin(Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Sub(
            Number.One,
            new Mul(new Cos(Variable.X), Number.One)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SubPartialDerivativeTest1()
    {
        var exp = new Sub(new Mul(Variable.X, Variable.Y), Variable.Y);
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Sub(new Mul(Variable.X, Number.One), Number.One);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SubPartialDerivativeTest2()
    {
        var exp = new Sub(Variable.X, Variable.Y);
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(Number.One));
    }

    [Test]
    public void SubPartialDerivativeTest3()
    {
        var exp = new Sub(Variable.X, Variable.Y);
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new UnaryMinus(Number.One);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SubPartialDerivativeTest4()
    {
        var exp = new Sub(Variable.X, Number.One);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void UnaryMinusTest()
    {
        var exp = new UnaryMinus(new Sin(Variable.X));
        var deriv = Differentiate(exp);
        var expected = new UnaryMinus(new Mul(new Cos(Variable.X), Number.One));

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void UnaryMinusTest2()
    {
        var exp = new UnaryMinus(new Sin(new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void DiffVarTest()
    {
        var exp = new Mul(Variable.X, Variable.Y);
        var deriv = Differentiate(exp);
        var expected = new Mul(Number.One, Variable.Y);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void VarTest()
    {
        var exp = Variable.Y;
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(Variable.Y));
    }

    #endregion Common

    #region Trigonometric

    [Test]
    public void SinDerivativeTest1()
    {
        var exp = new Sin(Variable.X);
        var deriv = Differentiate(exp);
        var expected = new Mul(new Cos(Variable.X), Number.One);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SinDerivativeTest2()
    {
        var exp = new Sin(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            new Cos(new Mul(Number.Two, Variable.X)),
            new Mul(Number.Two, Number.One)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SinPartialDerivativeTest1()
    {
        var exp = new Sin(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            new Cos(new Mul(Variable.X, Variable.Y)),
            new Mul(Number.One, Variable.Y)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SinPartialDerivativeTest2()
    {
        var exp = new Sin(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Mul(
            new Cos(new Mul(Variable.X, Variable.Y)),
            new Mul(Variable.X, Number.One)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SinPartialDerivativeTest3()
    {
        var exp = new Sin(Variable.Y);
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void CosDerivativeTest1()
    {
        var exp = new Cos(Variable.X);
        var deriv = Differentiate(exp);
        var expected = new UnaryMinus(new Mul(new Sin(Variable.X), Number.One));

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void CosPartialDerivativeTest1()
    {
        var exp = new Cos(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new UnaryMinus(
            new Mul(
                new Sin(new Mul(Variable.X, Variable.Y)),
                new Mul(Number.One, Variable.Y)
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void CosPartialDerivativeTest2()
    {
        var exp = new Cos(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new UnaryMinus(
            new Mul(
                new Sin(new Mul(Variable.X, Variable.Y)),
                new Mul(Variable.X, Number.One)
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void CosPartialDerivativeTest3()
    {
        var exp = new Cos(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void TanDerivativeTest1()
    {
        var exp = new Tan(Variable.X);
        var deriv = Differentiate(exp);
        var expected = new Div(Number.One, new Pow(new Cos(Variable.X), Number.Two));

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void TanDerivativeTest2()
    {
        var exp = new Tan(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.Two, Number.One),
            new Pow(new Cos(new Mul(Number.Two, Variable.X)), Number.Two)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void TanPartialDerivativeTest1()
    {
        var exp = new Tan(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.One, Variable.Y),
            new Pow(new Cos(new Mul(Variable.X, Variable.Y)), Number.Two)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void TanPartialDerivativeTest2()
    {
        var exp = new Tan(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Div(
            new Mul(Variable.X, Number.One),
            new Pow(new Cos(new Mul(Variable.X, Variable.Y)), Number.Two)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void TanPartialDerivativeTest3()
    {
        var exp = new Tan(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void CotDerivativeTest1()
    {
        var exp = new Cot(Variable.X);
        var deriv = Differentiate(exp);
        var expected = new UnaryMinus(
            new Div(Number.One, new Pow(new Sin(Variable.X), Number.Two))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void CotPartialDerivativeTest1()
    {
        var exp = new Cot(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new UnaryMinus(
            new Div(
                new Mul(Number.One, Variable.Y),
                new Pow(new Sin(new Mul(Variable.X, Variable.Y)), Number.Two)
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void CotPartialDerivativeTest2()
    {
        var exp = new Cot(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new UnaryMinus(
            new Div(
                new Mul(Variable.X, Number.One),
                new Pow(new Sin(new Mul(Variable.X, Variable.Y)), Number.Two)
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void CotPartialDerivativeTest3()
    {
        var exp = new Cot(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SecDerivativeZeroTest()
    {
        var exp = new Sec(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArcsinDerivativeTest1()
    {
        var exp = new Arcsin(Variable.X);
        var deriv = Differentiate(exp);
        var expected = new Div(
            Number.One,
            new Sqrt(new Sub(Number.One, new Pow(Variable.X, Number.Two)))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArcsinPartialDerivativeTest1()
    {
        var exp = new Arcsin(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.One, Variable.Y),
            new Sqrt(
                new Sub(
                    Number.One,
                    new Pow(new Mul(Variable.X, Variable.Y), Number.Two)
                )
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArcsinPartialDerivativeTest2()
    {
        var exp = new Arcsin(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Div(
            new Mul(Variable.X, Number.One),
            new Sqrt(
                new Sub(
                    Number.One,
                    new Pow(new Mul(Variable.X, Variable.Y), Number.Two)
                )
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArcsinPartialDerivativeTest3()
    {
        var exp = new Arcsin(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArccosPartialDerivativeTest1()
    {
        var exp = new Arccos(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new UnaryMinus(
            new Div(
                new Mul(Number.One, Variable.Y),
                new Sqrt(
                    new Sub(
                        Number.One,
                        new Pow(new Mul(Variable.X, Variable.Y), Number.Two)
                    )
                )
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArccosPartialDerivativeTest2()
    {
        var exp = new Arccos(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new UnaryMinus(new Div(
                new Mul(Variable.X, Number.One),
                new Sqrt(
                    new Sub(
                        Number.One,
                        new Pow(new Mul(Variable.X, Variable.Y), Number.Two)
                    )
                )
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArccosPartialDerivativeTest3()
    {
        var exp = new Arccos(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void ArctanDerivativeTest1()
    {
        var exp = new Arctan(Variable.X);
        var deriv = Differentiate(exp);
        var expected = new Div(
            Number.One,
            new Add(Number.One, new Pow(Variable.X, Number.Two))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArctanPartialDerivativeTest1()
    {
        var exp = new Arctan(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.One, Variable.Y),
            new Add(
                Number.One,
                new Pow(new Mul(Variable.X, Variable.Y), Number.Two)
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArctanPartialDerivativeTest2()
    {
        var exp = new Arctan(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new Div(
            new Mul(Variable.X, Number.One),
            new Add(
                Number.One,
                new Pow(new Mul(Variable.X, Variable.Y), Number.Two)
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArctanPartialDerivativeTest3()
    {
        var exp = new Arctan(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArccotPartialDerivativeTest1()
    {
        var exp = new Arccot(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp);
        var expected = new UnaryMinus(new Div(
                new Mul(Number.One, Variable.Y),
                new Add(
                    Number.One,
                    new Pow(new Mul(Variable.X, Variable.Y), Number.Two))
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArccotPartialDerivativeTest2()
    {
        var exp = new Arccot(new Mul(Variable.X, Variable.Y));
        var deriv = Differentiate(exp, Variable.Y);
        var expected = new UnaryMinus(
            new Div(
                new Mul(Variable.X, Number.One),
                new Add(
                    Number.One,
                    new Pow(new Mul(Variable.X, Variable.Y), Number.Two)
                )
            )
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArccotPartialDerivativeTest3()
    {
        var exp = new Arccot(Variable.X);
        var deriv = Differentiate(exp, Variable.Y);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArcsecDerivativeZeroTest()
    {
        var exp = new Arcsec(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArccscDerivativeZeroTest()
    {
        var exp = new Arccsc(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    #endregion Trigonometric

    #region Hyperbolic

    [Test]
    public void SinhDerivativeTest()
    {
        var exp = new Sinh(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            new Mul(Number.Two, Number.One),
            new Cosh(new Mul(Number.Two, Variable.X))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SinhDerivativeZeroTest()
    {
        var exp = new Sinh(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void CoshDerivativeTest()
    {
        var exp = new Cosh(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Mul(
            new Mul(Number.Two, Number.One),
            new Sinh(new Mul(Number.Two, Variable.X))
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void CoshDerivativeZeroTest()
    {
        var exp = new Cosh(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
    public void TanhDerivativeTest()
    {
        var exp = new Tanh(new Mul(Number.Two, Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Div(
            new Mul(Number.Two, Number.One),
            new Pow(new Cosh(new Mul(Number.Two, Variable.X)), Number.Two)
        );

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void TanhDerivativeZeroTest()
    {
        var exp = new Tanh(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void CothDerivativeZeroTest()
    {
        var exp = new Coth(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void SechDerivativeZeroTest()
    {
        var exp = new Sech(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void CschDerivativeZeroTest()
    {
        var exp = new Csch(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArsinehDerivativeZeroTest()
    {
        var exp = new Arsinh(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArcoshDerivativeZeroTest()
    {
        var exp = new Arcosh(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArtanhDerivativeZeroTest()
    {
        var exp = new Artanh(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArcothDerivativeZeroTest()
    {
        var exp = new Arcoth(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void ArcschDerivativeZeroTest()
    {
        var exp = new Arcsch(new Mul(Number.Two, new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    #endregion Hyperbolic

    [Test]
    public void DerivSimplify()
    {
        var simp = new Simplifier();
        var exp = new Simplify(simp, new Sin(Variable.X));
        var deriv = Differentiate(exp);
        var expected = new Mul(new Cos(Variable.X), Number.One);

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void DerivSimplify2()
    {
        var simp = new Simplifier();
        var exp = new Simplify(simp, new Sin(new Variable("z")));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void DoubleDiffNoVarTest()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();

        var exp = new Derivative(diff, simp, new Derivative(diff, simp, new Sin(Number.One)));
        var deriv = Differentiate(exp);

        Assert.That(deriv, Is.EqualTo(zero));
    }

    [Test]
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

        Assert.That(deriv, Is.EqualTo(expected));
    }

    [Test]
    public void NotSupportedTest()
    {
        Assert.Throws<NotSupportedException>(() => Differentiate(new Fact(Variable.X)));
    }
}