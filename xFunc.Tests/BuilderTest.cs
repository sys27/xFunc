// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests;

public class BuilderTest
{
    [Test]
    public void ExpCtorTest()
    {
        var exp = new Builder(Number.Two).Expression;

        Assert.That(exp, Is.EqualTo(Number.Two));
    }

    [Test]
    public void NumberCtorTest()
    {
        var exp = new Builder(2).Expression;

        Assert.That(exp, Is.EqualTo(Number.Two));
    }

    [Test]
    public void VariableCtorTest()
    {
        var exp = new Builder("x").Expression;

        Assert.That(exp, Is.EqualTo(Variable.X));
    }

    [Test]
    public void ExpressionTest()
    {
        var exp = new Builder(3)
            .Custom(exp => new Sin(exp))
            .Expression;

        Assert.That(exp, Is.EqualTo(new Sin(new Number(3))));
    }

    [Test]
    public void ExpressionNullTest()
    {
        var builder = new Builder(3);

        Assert.Throws<ArgumentNullException>(() => builder.Custom(null));
    }

    [Test]
    public void ExecuteTest()
    {
        var exp = new Builder(3)
            .Add(2)
            .Expression;

        var expected = new NumberValue(5.0);

        Assert.That(expected, Is.EqualTo(exp.Execute()));
    }

    [Test]
    public void ExecuteWithParamsTest()
    {
        var exp = new Builder(3)
            .Add(2)
            .Expression;

        var expected = new NumberValue(5.0);

        Assert.That(expected, Is.EqualTo(exp.Execute(new ExpressionParameters())));
    }

    [Test]
    public void NullParamTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Builder((IExpression)null));
    }

    #region Standart

    [Test]
    public void AddExpTest()
    {
        var builder = new Builder(3).Add(Number.Two);

        Assert.That(builder.Expression, Is.EqualTo(new Add(new Number(3), Number.Two)));
    }

    [Test]
    public void AddNumberTest()
    {
        var builder = new Builder(3).Add(2);

        Assert.That(builder.Expression, Is.EqualTo(new Add(new Number(3), Number.Two)));
    }

    [Test]
    public void AddVariableTest()
    {
        var builder = new Builder(3).Add("x");

        Assert.That(builder.Expression, Is.EqualTo(new Add(new Number(3), Variable.X)));
    }

    [Test]
    public void SubExpTest()
    {
        var builder = new Builder(3).Sub(Number.Two);

        Assert.That(builder.Expression, Is.EqualTo(new Sub(new Number(3), Number.Two)));
    }

    [Test]
    public void SubNumberTest()
    {
        var builder = new Builder(3).Sub(2);

        Assert.That(builder.Expression, Is.EqualTo(new Sub(new Number(3), Number.Two)));
    }

    [Test]
    public void SubVariableTest()
    {
        var builder = new Builder(3).Sub("x");

        Assert.That(builder.Expression, Is.EqualTo(new Sub(new Number(3), Variable.X)));
    }

    [Test]
    public void MulExpTest()
    {
        var builder = new Builder(3).Mul(Number.Two);

        Assert.That(builder.Expression, Is.EqualTo(new Mul(new Number(3), Number.Two)));
    }

    [Test]
    public void MulNumberTest()
    {
        var builder = new Builder(3).Mul(2);

        Assert.That(builder.Expression, Is.EqualTo(new Mul(new Number(3), Number.Two)));
    }

    [Test]
    public void MulVariableTest()
    {
        var builder = new Builder(3).Mul("x");

        Assert.That(builder.Expression, Is.EqualTo(new Mul(new Number(3), Variable.X)));
    }

    [Test]
    public void DivExpTest()
    {
        var builder = new Builder(3).Div(Number.Two);

        Assert.That(builder.Expression, Is.EqualTo(new Div(new Number(3), Number.Two)));
    }

    [Test]
    public void DivNumberTest()
    {
        var builder = new Builder(3).Div(2);

        Assert.That(builder.Expression, Is.EqualTo(new Div(new Number(3), Number.Two)));
    }

    [Test]
    public void DivVariableTest()
    {
        var builder = new Builder(3).Div("x");

        Assert.That(builder.Expression, Is.EqualTo(new Div(new Number(3), Variable.X)));
    }

    [Test]
    public void PowExpTest()
    {
        var builder = new Builder(3).Pow(Number.Two);

        Assert.That(builder.Expression, Is.EqualTo(new Pow(new Number(3), Number.Two)));
    }

    [Test]
    public void PowNumberTest()
    {
        var builder = new Builder(3).Pow(2);

        Assert.That(builder.Expression, Is.EqualTo(new Pow(new Number(3), Number.Two)));
    }

    [Test]
    public void PowVariableTest()
    {
        var builder = new Builder(3).Pow("x");

        Assert.That(builder.Expression, Is.EqualTo(new Pow(new Number(3), Variable.X)));
    }

    [Test]
    public void SqrtNumberTest()
    {
        var builder = new Builder(3).Sqrt();

        Assert.That(builder.Expression, Is.EqualTo(new Sqrt(new Number(3))));
    }

    [Test]
    public void RootExpTest()
    {
        var builder = new Builder(3).Root(Number.Two);

        Assert.That(builder.Expression, Is.EqualTo(new Root(new Number(3), Number.Two)));
    }

    [Test]
    public void RootNumberTest()
    {
        var builder = new Builder(3).Root(2);

        Assert.That(builder.Expression, Is.EqualTo(new Root(new Number(3), Number.Two)));
    }

    [Test]
    public void RootVariableTest()
    {
        var builder = new Builder(3).Root("x");

        Assert.That(builder.Expression, Is.EqualTo(new Root(new Number(3), Variable.X)));
    }

    [Test]
    public void AbsNumberTest()
    {
        var builder = new Builder(3).Abs();

        Assert.That(builder.Expression, Is.EqualTo(new Abs(new Number(3))));
    }

    [Test]
    public void LogExpTest()
    {
        var builder = new Builder(3).Log(Number.Two);

        Assert.That(builder.Expression, Is.EqualTo(new Log(Number.Two, new Number(3))));
    }

    [Test]
    public void LogNumberTest()
    {
        var builder = new Builder(3).Log(2);

        Assert.That(builder.Expression, Is.EqualTo(new Log(Number.Two, new Number(3))));
    }

    [Test]
    public void LogVariableTest()
    {
        var builder = new Builder(3).Log("x");

        Assert.That(builder.Expression, Is.EqualTo(new Log(Variable.X, new Number(3))));
    }

    [Test]
    public void LnNumberTest()
    {
        var builder = new Builder(3).Ln();

        Assert.That(builder.Expression, Is.EqualTo(new Ln(new Number(3))));
    }

    [Test]
    public void LgNumberTest()
    {
        var builder = new Builder(3).Lg();

        Assert.That(builder.Expression, Is.EqualTo(new Lg(new Number(3))));
    }

    [Test]
    public void LbNumberTest()
    {
        var builder = new Builder(3).Lb();

        Assert.That(builder.Expression, Is.EqualTo(new Lb(new Number(3))));
    }

    [Test]
    public void ExpNumberTest()
    {
        var builder = new Builder(3).Exp();

        Assert.That(builder.Expression, Is.EqualTo(new Exp(new Number(3))));
    }

    #endregion Standart

    #region Trigonometric

    [Test]
    public void SinTest()
    {
        var builder = new Builder(3).Sin();

        Assert.That(builder.Expression, Is.EqualTo(new Sin(new Number(3))));
    }

    [Test]
    public void CosTest()
    {
        var builder = new Builder(3).Cos();

        Assert.That(builder.Expression, Is.EqualTo(new Cos(new Number(3))));
    }

    [Test]
    public void TanTest()
    {
        var builder = new Builder(3).Tan();

        Assert.That(builder.Expression, Is.EqualTo(new Tan(new Number(3))));
    }

    [Test]
    public void CotTest()
    {
        var builder = new Builder(3).Cot();

        Assert.That(builder.Expression, Is.EqualTo(new Cot(new Number(3))));
    }

    [Test]
    public void SecTest()
    {
        var builder = new Builder(3).Sec();

        Assert.That(builder.Expression, Is.EqualTo(new Sec(new Number(3))));
    }

    [Test]
    public void CscTest()
    {
        var builder = new Builder(3).Csc();

        Assert.That(builder.Expression, Is.EqualTo(new Csc(new Number(3))));
    }

    [Test]
    public void ArcsinTest()
    {
        var builder = new Builder(3).Arcsin();

        Assert.That(builder.Expression, Is.EqualTo(new Arcsin(new Number(3))));
    }

    [Test]
    public void ArccosTest()
    {
        var builder = new Builder(3).Arccos();

        Assert.That(builder.Expression, Is.EqualTo(new Arccos(new Number(3))));
    }

    [Test]
    public void ArctanTest()
    {
        var builder = new Builder(3).Arctan();

        Assert.That(builder.Expression, Is.EqualTo(new Arctan(new Number(3))));
    }

    [Test]
    public void ArccotTest()
    {
        var builder = new Builder(3).Arccot();

        Assert.That(builder.Expression, Is.EqualTo(new Arccot(new Number(3))));
    }

    [Test]
    public void ArcsecTest()
    {
        var builder = new Builder(3).Arcsec();

        Assert.That(builder.Expression, Is.EqualTo(new Arcsec(new Number(3))));
    }

    [Test]
    public void ArccscTest()
    {
        var builder = new Builder(3).Arccsc();

        Assert.That(builder.Expression, Is.EqualTo(new Arccsc(new Number(3))));
    }

    #endregion Trigonometric

    #region Hyperbolic

    [Test]
    public void SinhTest()
    {
        var builder = new Builder(3).Sinh();

        Assert.That(builder.Expression, Is.EqualTo(new Sinh(new Number(3))));
    }

    [Test]
    public void CoshTest()
    {
        var builder = new Builder(3).Cosh();

        Assert.That(builder.Expression, Is.EqualTo(new Cosh(new Number(3))));
    }

    [Test]
    public void TanhTest()
    {
        var builder = new Builder(3).Tanh();

        Assert.That(builder.Expression, Is.EqualTo(new Tanh(new Number(3))));
    }

    [Test]
    public void CothTest()
    {
        var builder = new Builder(3).Coth();

        Assert.That(builder.Expression, Is.EqualTo(new Coth(new Number(3))));
    }

    [Test]
    public void SechTest()
    {
        var builder = new Builder(3).Sech();

        Assert.That(builder.Expression, Is.EqualTo(new Sech(new Number(3))));
    }

    [Test]
    public void CschTest()
    {
        var builder = new Builder(3).Csch();

        Assert.That(builder.Expression, Is.EqualTo(new Csch(new Number(3))));
    }

    [Test]
    public void ArsinhTest()
    {
        var builder = new Builder(3).Arsinh();

        Assert.That(builder.Expression, Is.EqualTo(new Arsinh(new Number(3))));
    }

    [Test]
    public void ArcoshTest()
    {
        var builder = new Builder(3).Arcosh();

        Assert.That(builder.Expression, Is.EqualTo(new Arcosh(new Number(3))));
    }

    [Test]
    public void ArtanhTest()
    {
        var builder = new Builder(3).Artanh();

        Assert.That(builder.Expression, Is.EqualTo(new Artanh(new Number(3))));
    }

    [Test]
    public void ArcothTest()
    {
        var builder = new Builder(3).Arcoth();

        Assert.That(builder.Expression, Is.EqualTo(new Arcoth(new Number(3))));
    }

    [Test]
    public void ArsechTest()
    {
        var builder = new Builder(3).Arsech();

        Assert.That(builder.Expression, Is.EqualTo(new Arsech(new Number(3))));
    }

    [Test]
    public void ArcschTest()
    {
        var builder = new Builder(3).Arcsch();

        Assert.That(builder.Expression, Is.EqualTo(new Arcsch(new Number(3))));
    }

    #endregion Hyperbolic
}