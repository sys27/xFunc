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
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests
{
    public class BuilderTest
    {
        [Fact]
        public void ExpCtorTest()
        {
            var builder = new Builder((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Number(2));
        }

        [Fact]
        public void NumberCtorTest()
        {
            var builder = new Builder(2);

            Assert.Equal(builder.Current, new Number(2));
        }

        [Fact]
        public void VariableCtorTest()
        {
            var builder = new Builder("x");

            Assert.Equal(builder.Current, Variable.X);
        }

        [Fact]
        public void CreateExpCtorTest()
        {
            var builder = Builder.Create((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Number(2));
        }

        [Fact]
        public void CreateNumberCtorTest()
        {
            var builder = Builder.Create(2);

            Assert.Equal(builder.Current, new Number(2));
        }

        [Fact]
        public void CreateVariableCtorTest()
        {
            var builder = Builder.Create("x");

            Assert.Equal(builder.Current, Variable.X);
        }

        [Fact]
        public void ExpressionTest()
        {
            var builder = new Builder(3).Expression(exp => new Sin(exp));

            Assert.Equal(builder.Current, new Sin(new Number(3)));
        }

        [Fact]
        public void ExpressionNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Builder(3).Expression(null));
        }

        [Fact]
        public void ExecuteTest()
        {
            var builder = new Builder(3).Add(2);

            Assert.Equal(5.0, builder.Execute());
        }

        [Fact]
        public void ExecuteWithParamsTest()
        {
            var builder = new Builder(3).Add(2);

            Assert.Equal(5.0, builder.Execute(new ExpressionParameters()));
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
            var builder = new Builder(3).Add((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Add(new Number(3), new Number(2)));
        }

        [Fact]
        public void AddNumberTest()
        {
            var builder = new Builder(3).Add(2);

            Assert.Equal(builder.Current, new Add(new Number(3), new Number(2)));
        }

        [Fact]
        public void AddVariableTest()
        {
            var builder = new Builder(3).Add("x");

            Assert.Equal(builder.Current, new Add(new Number(3), Variable.X));
        }

        [Fact]
        public void SubExpTest()
        {
            var builder = new Builder(3).Sub((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Sub(new Number(3), new Number(2)));
        }

        [Fact]
        public void SubNumberTest()
        {
            var builder = new Builder(3).Sub(2);

            Assert.Equal(builder.Current, new Sub(new Number(3), new Number(2)));
        }

        [Fact]
        public void SubVariableTest()
        {
            var builder = new Builder(3).Sub("x");

            Assert.Equal(builder.Current, new Sub(new Number(3), Variable.X));
        }

        [Fact]
        public void MulExpTest()
        {
            var builder = new Builder(3).Mul((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Mul(new Number(3), new Number(2)));
        }

        [Fact]
        public void MulNumberTest()
        {
            var builder = new Builder(3).Mul(2);

            Assert.Equal(builder.Current, new Mul(new Number(3), new Number(2)));
        }

        [Fact]
        public void MulVariableTest()
        {
            var builder = new Builder(3).Mul("x");

            Assert.Equal(builder.Current, new Mul(new Number(3), Variable.X));
        }

        [Fact]
        public void DivExpTest()
        {
            var builder = new Builder(3).Div((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Div(new Number(3), new Number(2)));
        }

        [Fact]
        public void DivNumberTest()
        {
            var builder = new Builder(3).Div(2);

            Assert.Equal(builder.Current, new Div(new Number(3), new Number(2)));
        }

        [Fact]
        public void DivVariableTest()
        {
            var builder = new Builder(3).Div("x");

            Assert.Equal(builder.Current, new Div(new Number(3), Variable.X));
        }

        [Fact]
        public void PowExpTest()
        {
            var builder = new Builder(3).Pow((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Pow(new Number(3), new Number(2)));
        }

        [Fact]
        public void PowNumberTest()
        {
            var builder = new Builder(3).Pow(2);

            Assert.Equal(builder.Current, new Pow(new Number(3), new Number(2)));
        }

        [Fact]
        public void PowVariableTest()
        {
            var builder = new Builder(3).Pow("x");

            Assert.Equal(builder.Current, new Pow(new Number(3), Variable.X));
        }

        [Fact]
        public void SqrtNumberTest()
        {
            var builder = new Builder(3).Sqrt();

            Assert.Equal(builder.Current, new Sqrt(new Number(3)));
        }

        [Fact]
        public void RootExpTest()
        {
            var builder = new Builder(3).Root((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Root(new Number(3), new Number(2)));
        }

        [Fact]
        public void RootNumberTest()
        {
            var builder = new Builder(3).Root(2);

            Assert.Equal(builder.Current, new Root(new Number(3), new Number(2)));
        }

        [Fact]
        public void RootVariableTest()
        {
            var builder = new Builder(3).Root("x");

            Assert.Equal(builder.Current, new Root(new Number(3), Variable.X));
        }

        [Fact]
        public void AbsNumberTest()
        {
            var builder = new Builder(3).Abs();

            Assert.Equal(builder.Current, new Abs(new Number(3)));
        }

        [Fact]
        public void LogExpTest()
        {
            var builder = new Builder(3).Log((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Log(new Number(2), new Number(3)));
        }

        [Fact]
        public void LogNumberTest()
        {
            var builder = new Builder(3).Log(2);

            Assert.Equal(builder.Current, new Log(new Number(2), new Number(3)));
        }

        [Fact]
        public void LogVariableTest()
        {
            var builder = new Builder(3).Log("x");

            Assert.Equal(builder.Current, new Log(Variable.X, new Number(3)));
        }

        [Fact]
        public void LnNumberTest()
        {
            var builder = new Builder(3).Ln();

            Assert.Equal(builder.Current, new Ln(new Number(3)));
        }

        [Fact]
        public void LgNumberTest()
        {
            var builder = new Builder(3).Lg();

            Assert.Equal(builder.Current, new Lg(new Number(3)));
        }

        [Fact]
        public void LbNumberTest()
        {
            var builder = new Builder(3).Lb();

            Assert.Equal(builder.Current, new Lb(new Number(3)));
        }

        [Fact]
        public void ExpNumberTest()
        {
            var builder = new Builder(3).Exp();

            Assert.Equal(builder.Current, new Exp(new Number(3)));
        }

        #endregion Standart

        #region Trigonometric

        [Fact]
        public void SinTest()
        {
            var builder = new Builder(3).Sin();

            Assert.Equal(builder.Current, new Sin(new Number(3)));
        }

        [Fact]
        public void CosTest()
        {
            var builder = new Builder(3).Cos();

            Assert.Equal(builder.Current, new Cos(new Number(3)));
        }

        [Fact]
        public void TanTest()
        {
            var builder = new Builder(3).Tan();

            Assert.Equal(builder.Current, new Tan(new Number(3)));
        }

        [Fact]
        public void CotTest()
        {
            var builder = new Builder(3).Cot();

            Assert.Equal(builder.Current, new Cot(new Number(3)));
        }

        [Fact]
        public void SecTest()
        {
            var builder = new Builder(3).Sec();

            Assert.Equal(builder.Current, new Sec(new Number(3)));
        }

        [Fact]
        public void CscTest()
        {
            var builder = new Builder(3).Csc();

            Assert.Equal(builder.Current, new Csc(new Number(3)));
        }

        [Fact]
        public void ArcsinTest()
        {
            var builder = new Builder(3).Arcsin();

            Assert.Equal(builder.Current, new Arcsin(new Number(3)));
        }

        [Fact]
        public void ArccosTest()
        {
            var builder = new Builder(3).Arccos();

            Assert.Equal(builder.Current, new Arccos(new Number(3)));
        }

        [Fact]
        public void ArctanTest()
        {
            var builder = new Builder(3).Arctan();

            Assert.Equal(builder.Current, new Arctan(new Number(3)));
        }

        [Fact]
        public void ArccotTest()
        {
            var builder = new Builder(3).Arccot();

            Assert.Equal(builder.Current, new Arccot(new Number(3)));
        }

        [Fact]
        public void ArcsecTest()
        {
            var builder = new Builder(3).Arcsec();

            Assert.Equal(builder.Current, new Arcsec(new Number(3)));
        }

        [Fact]
        public void ArccscTest()
        {
            var builder = new Builder(3).Arccsc();

            Assert.Equal(builder.Current, new Arccsc(new Number(3)));
        }

        #endregion Trigonometric

        #region Hyperbolic

        [Fact]
        public void SinhTest()
        {
            var builder = new Builder(3).Sinh();

            Assert.Equal(builder.Current, new Sinh(new Number(3)));
        }

        [Fact]
        public void CoshTest()
        {
            var builder = new Builder(3).Cosh();

            Assert.Equal(builder.Current, new Cosh(new Number(3)));
        }

        [Fact]
        public void TanhTest()
        {
            var builder = new Builder(3).Tanh();

            Assert.Equal(builder.Current, new Tanh(new Number(3)));
        }

        [Fact]
        public void CothTest()
        {
            var builder = new Builder(3).Coth();

            Assert.Equal(builder.Current, new Coth(new Number(3)));
        }

        [Fact]
        public void SechTest()
        {
            var builder = new Builder(3).Sech();

            Assert.Equal(builder.Current, new Sech(new Number(3)));
        }

        [Fact]
        public void CschTest()
        {
            var builder = new Builder(3).Csch();

            Assert.Equal(builder.Current, new Csch(new Number(3)));
        }

        [Fact]
        public void ArsinhTest()
        {
            var builder = new Builder(3).Arsinh();

            Assert.Equal(builder.Current, new Arsinh(new Number(3)));
        }

        [Fact]
        public void ArcoshTest()
        {
            var builder = new Builder(3).Arcosh();

            Assert.Equal(builder.Current, new Arcosh(new Number(3)));
        }

        [Fact]
        public void ArtanhTest()
        {
            var builder = new Builder(3).Artanh();

            Assert.Equal(builder.Current, new Artanh(new Number(3)));
        }

        [Fact]
        public void ArcothTest()
        {
            var builder = new Builder(3).Arcoth();

            Assert.Equal(builder.Current, new Arcoth(new Number(3)));
        }

        [Fact]
        public void ArsechTest()
        {
            var builder = new Builder(3).Arsech();

            Assert.Equal(builder.Current, new Arsech(new Number(3)));
        }

        [Fact]
        public void ArcschTest()
        {
            var builder = new Builder(3).Arcsch();

            Assert.Equal(builder.Current, new Arcsch(new Number(3)));
        }

        #endregion Hyperbolic
    }
}