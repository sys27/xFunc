// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests;

public class BuilderTest
{
    [Fact]
    public void ExpCtorTest()
    {
        var exp = new Builder((IExpression)Number.Two).Expression;

        Assert.Equal(exp, Number.Two);
    }

    [Fact]
    public void NumberCtorTest()
    {
        var exp = new Builder(2).Expression;

        Assert.Equal(exp, Number.Two);
    }

    [Fact]
    public void VariableCtorTest()
    {
        var exp = new Builder("x").Expression;

        Assert.Equal(exp, Variable.X);
    }

    [Fact]
    public void ExpressionTest()
    {
        var exp = new Builder(3)
            .Custom(exp => new Sin(exp))
            .Expression;

        Assert.Equal(exp, new Sin(new Number(3)));
    }

    [Fact]
    public void ExpressionNullTest()
    {
        var builder = new Builder(3);

        Assert.Throws<ArgumentNullException>(() => builder.Custom(null));
    }

    [Fact]
    public void ExecuteTest()
    {
        var exp = new Builder(3)
            .Add(2)
            .Expression;

        var expected = new NumberValue(5.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteWithParamsTest()
    {
        var exp = new Builder(3)
            .Add(2)
            .Expression;

        var expected = new NumberValue(5.0);

        Assert.Equal(expected, exp.Execute(new ExpressionParameters()));
    }

    [Fact]
    public void NullParamTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Builder((IExpression)null));
    }

    #region Standart

    [Fact]
    public void AddExpTest()
    {
        var builder = new Builder(3).Add((IExpression)Number.Two);

        Assert.Equal(builder.Expression, new Add(new Number(3), Number.Two));
    }

    [Fact]
    public void AddNumberTest()
    {
        var builder = new Builder(3).Add(2);

        Assert.Equal(builder.Expression, new Add(new Number(3), Number.Two));
    }

    [Fact]
    public void AddVariableTest()
    {
        var builder = new Builder(3).Add("x");

        Assert.Equal(builder.Expression, new Add(new Number(3), Variable.X));
    }

    [Fact]
    public void SubExpTest()
    {
        var builder = new Builder(3).Sub((IExpression)Number.Two);

        Assert.Equal(builder.Expression, new Sub(new Number(3), Number.Two));
    }

    [Fact]
    public void SubNumberTest()
    {
        var builder = new Builder(3).Sub(2);

        Assert.Equal(builder.Expression, new Sub(new Number(3), Number.Two));
    }

    [Fact]
    public void SubVariableTest()
    {
        var builder = new Builder(3).Sub("x");

        Assert.Equal(builder.Expression, new Sub(new Number(3), Variable.X));
    }

    [Fact]
    public void MulExpTest()
    {
        var builder = new Builder(3).Mul((IExpression)Number.Two);

        Assert.Equal(builder.Expression, new Mul(new Number(3), Number.Two));
    }

    [Fact]
    public void MulNumberTest()
    {
        var builder = new Builder(3).Mul(2);

        Assert.Equal(builder.Expression, new Mul(new Number(3), Number.Two));
    }

    [Fact]
    public void MulVariableTest()
    {
        var builder = new Builder(3).Mul("x");

        Assert.Equal(builder.Expression, new Mul(new Number(3), Variable.X));
    }

    [Fact]
    public void DivExpTest()
    {
        var builder = new Builder(3).Div((IExpression)Number.Two);

        Assert.Equal(builder.Expression, new Div(new Number(3), Number.Two));
    }

    [Fact]
    public void DivNumberTest()
    {
        var builder = new Builder(3).Div(2);

        Assert.Equal(builder.Expression, new Div(new Number(3), Number.Two));
    }

    [Fact]
    public void DivVariableTest()
    {
        var builder = new Builder(3).Div("x");

        Assert.Equal(builder.Expression, new Div(new Number(3), Variable.X));
    }

    [Fact]
    public void PowExpTest()
    {
        var builder = new Builder(3).Pow((IExpression)Number.Two);

        Assert.Equal(builder.Expression, new Pow(new Number(3), Number.Two));
    }

    [Fact]
    public void PowNumberTest()
    {
        var builder = new Builder(3).Pow(2);

        Assert.Equal(builder.Expression, new Pow(new Number(3), Number.Two));
    }

    [Fact]
    public void PowVariableTest()
    {
        var builder = new Builder(3).Pow("x");

        Assert.Equal(builder.Expression, new Pow(new Number(3), Variable.X));
    }

    [Fact]
    public void SqrtNumberTest()
    {
        var builder = new Builder(3).Sqrt();

        Assert.Equal(builder.Expression, new Sqrt(new Number(3)));
    }

    [Fact]
    public void RootExpTest()
    {
        var builder = new Builder(3).Root((IExpression)Number.Two);

        Assert.Equal(builder.Expression, new Root(new Number(3), Number.Two));
    }

    [Fact]
    public void RootNumberTest()
    {
        var builder = new Builder(3).Root(2);

        Assert.Equal(builder.Expression, new Root(new Number(3), Number.Two));
    }

    [Fact]
    public void RootVariableTest()
    {
        var builder = new Builder(3).Root("x");

        Assert.Equal(builder.Expression, new Root(new Number(3), Variable.X));
    }

    [Fact]
    public void AbsNumberTest()
    {
        var builder = new Builder(3).Abs();

        Assert.Equal(builder.Expression, new Abs(new Number(3)));
    }

    [Fact]
    public void LogExpTest()
    {
        var builder = new Builder(3).Log((IExpression)Number.Two);

        Assert.Equal(builder.Expression, new Log(Number.Two, new Number(3)));
    }

    [Fact]
    public void LogNumberTest()
    {
        var builder = new Builder(3).Log(2);

        Assert.Equal(builder.Expression, new Log(Number.Two, new Number(3)));
    }

    [Fact]
    public void LogVariableTest()
    {
        var builder = new Builder(3).Log("x");

        Assert.Equal(builder.Expression, new Log(Variable.X, new Number(3)));
    }

    [Fact]
    public void LnNumberTest()
    {
        var builder = new Builder(3).Ln();

        Assert.Equal(builder.Expression, new Ln(new Number(3)));
    }

    [Fact]
    public void LgNumberTest()
    {
        var builder = new Builder(3).Lg();

        Assert.Equal(builder.Expression, new Lg(new Number(3)));
    }

    [Fact]
    public void LbNumberTest()
    {
        var builder = new Builder(3).Lb();

        Assert.Equal(builder.Expression, new Lb(new Number(3)));
    }

    [Fact]
    public void ExpNumberTest()
    {
        var builder = new Builder(3).Exp();

        Assert.Equal(builder.Expression, new Exp(new Number(3)));
    }

    #endregion Standart

    #region Trigonometric

    [Fact]
    public void SinTest()
    {
        var builder = new Builder(3).Sin();

        Assert.Equal(builder.Expression, new Sin(new Number(3)));
    }

    [Fact]
    public void CosTest()
    {
        var builder = new Builder(3).Cos();

        Assert.Equal(builder.Expression, new Cos(new Number(3)));
    }

    [Fact]
    public void TanTest()
    {
        var builder = new Builder(3).Tan();

        Assert.Equal(builder.Expression, new Tan(new Number(3)));
    }

    [Fact]
    public void CotTest()
    {
        var builder = new Builder(3).Cot();

        Assert.Equal(builder.Expression, new Cot(new Number(3)));
    }

    [Fact]
    public void SecTest()
    {
        var builder = new Builder(3).Sec();

        Assert.Equal(builder.Expression, new Sec(new Number(3)));
    }

    [Fact]
    public void CscTest()
    {
        var builder = new Builder(3).Csc();

        Assert.Equal(builder.Expression, new Csc(new Number(3)));
    }

    [Fact]
    public void ArcsinTest()
    {
        var builder = new Builder(3).Arcsin();

        Assert.Equal(builder.Expression, new Arcsin(new Number(3)));
    }

    [Fact]
    public void ArccosTest()
    {
        var builder = new Builder(3).Arccos();

        Assert.Equal(builder.Expression, new Arccos(new Number(3)));
    }

    [Fact]
    public void ArctanTest()
    {
        var builder = new Builder(3).Arctan();

        Assert.Equal(builder.Expression, new Arctan(new Number(3)));
    }

    [Fact]
    public void ArccotTest()
    {
        var builder = new Builder(3).Arccot();

        Assert.Equal(builder.Expression, new Arccot(new Number(3)));
    }

    [Fact]
    public void ArcsecTest()
    {
        var builder = new Builder(3).Arcsec();

        Assert.Equal(builder.Expression, new Arcsec(new Number(3)));
    }

    [Fact]
    public void ArccscTest()
    {
        var builder = new Builder(3).Arccsc();

        Assert.Equal(builder.Expression, new Arccsc(new Number(3)));
    }

    #endregion Trigonometric

    #region Hyperbolic

    [Fact]
    public void SinhTest()
    {
        var builder = new Builder(3).Sinh();

        Assert.Equal(builder.Expression, new Sinh(new Number(3)));
    }

    [Fact]
    public void CoshTest()
    {
        var builder = new Builder(3).Cosh();

        Assert.Equal(builder.Expression, new Cosh(new Number(3)));
    }

    [Fact]
    public void TanhTest()
    {
        var builder = new Builder(3).Tanh();

        Assert.Equal(builder.Expression, new Tanh(new Number(3)));
    }

    [Fact]
    public void CothTest()
    {
        var builder = new Builder(3).Coth();

        Assert.Equal(builder.Expression, new Coth(new Number(3)));
    }

    [Fact]
    public void SechTest()
    {
        var builder = new Builder(3).Sech();

        Assert.Equal(builder.Expression, new Sech(new Number(3)));
    }

    [Fact]
    public void CschTest()
    {
        var builder = new Builder(3).Csch();

        Assert.Equal(builder.Expression, new Csch(new Number(3)));
    }

    [Fact]
    public void ArsinhTest()
    {
        var builder = new Builder(3).Arsinh();

        Assert.Equal(builder.Expression, new Arsinh(new Number(3)));
    }

    [Fact]
    public void ArcoshTest()
    {
        var builder = new Builder(3).Arcosh();

        Assert.Equal(builder.Expression, new Arcosh(new Number(3)));
    }

    [Fact]
    public void ArtanhTest()
    {
        var builder = new Builder(3).Artanh();

        Assert.Equal(builder.Expression, new Artanh(new Number(3)));
    }

    [Fact]
    public void ArcothTest()
    {
        var builder = new Builder(3).Arcoth();

        Assert.Equal(builder.Expression, new Arcoth(new Number(3)));
    }

    [Fact]
    public void ArsechTest()
    {
        var builder = new Builder(3).Arsech();

        Assert.Equal(builder.Expression, new Arsech(new Number(3)));
    }

    [Fact]
    public void ArcschTest()
    {
        var builder = new Builder(3).Arcsch();

        Assert.Equal(builder.Expression, new Arcsch(new Number(3)));
    }

    #endregion Hyperbolic
}