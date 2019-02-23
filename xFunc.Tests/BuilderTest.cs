// Copyright 2012-2019 Dmitry Kischenko
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

        private Builder builder;

        public BuilderTest()
        {
            builder = new Builder();
        }

        [Fact]
        public void DefautlCtorTest()
        {
            var builder = new Builder();

            Assert.Null(builder.Current);
        }

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
            builder.Init(3).Expression(exp => new Sin(exp));

            Assert.Equal(builder.Current, new Sin(new Number(3)));
        }

        [Fact]
        public void ExecuteTest()
        {
            builder.Init(3).Add(2);

            Assert.Equal(5.0, builder.Execute());
        }

        [Fact]
        public void ExecuteWithParamsTest()
        {
            builder.Init(3).Add(2);

            Assert.Equal(5.0, builder.Execute(new ExpressionParameters()));
        }

        [Fact]
        public void NullParamTest()
        {
            Assert.Throws<ArgumentNullException>(() => builder.Init((IExpression)null).Add((IExpression)new Number(2)));
        }

        #region Standart

        [Fact]
        public void AddExpTest()
        {
            builder.Init(3).Add((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Add(new Number(3), new Number(2)));
        }

        [Fact]
        public void AddNumberTest()
        {
            builder.Init(3).Add(2);

            Assert.Equal(builder.Current, new Add(new Number(3), new Number(2)));
        }

        [Fact]
        public void AddVariableTest()
        {
            builder.Init(3).Add("x");

            Assert.Equal(builder.Current, new Add(new Number(3), Variable.X));
        }

        [Fact]
        public void SubExpTest()
        {
            builder.Init(3).Sub((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Sub(new Number(3), new Number(2)));
        }

        [Fact]
        public void SubNumberTest()
        {
            builder.Init(3).Sub(2);

            Assert.Equal(builder.Current, new Sub(new Number(3), new Number(2)));
        }

        [Fact]
        public void SubVariableTest()
        {
            builder.Init(3).Sub("x");

            Assert.Equal(builder.Current, new Sub(new Number(3), Variable.X));
        }

        [Fact]
        public void MulExpTest()
        {
            builder.Init(3).Mul((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Mul(new Number(3), new Number(2)));
        }

        [Fact]
        public void MulNumberTest()
        {
            builder.Init(3).Mul(2);

            Assert.Equal(builder.Current, new Mul(new Number(3), new Number(2)));
        }

        [Fact]
        public void MulVariableTest()
        {
            builder.Init(3).Mul("x");

            Assert.Equal(builder.Current, new Mul(new Number(3), Variable.X));
        }

        [Fact]
        public void DivExpTest()
        {
            builder.Init(3).Div((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Div(new Number(3), new Number(2)));
        }

        [Fact]
        public void DivNumberTest()
        {
            builder.Init(3).Div(2);

            Assert.Equal(builder.Current, new Div(new Number(3), new Number(2)));
        }

        [Fact]
        public void DivVariableTest()
        {
            builder.Init(3).Div("x");

            Assert.Equal(builder.Current, new Div(new Number(3), Variable.X));
        }

        [Fact]
        public void PowExpTest()
        {
            builder.Init(3).Pow((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Pow(new Number(3), new Number(2)));
        }

        [Fact]
        public void PowNumberTest()
        {
            builder.Init(3).Pow(2);

            Assert.Equal(builder.Current, new Pow(new Number(3), new Number(2)));
        }

        [Fact]
        public void PowVariableTest()
        {
            builder.Init(3).Pow("x");

            Assert.Equal(builder.Current, new Pow(new Number(3), Variable.X));
        }

        [Fact]
        public void SqrtNumberTest()
        {
            builder.Init(3).Sqrt();

            Assert.Equal(builder.Current, new Sqrt(new Number(3)));
        }

        [Fact]
        public void RootExpTest()
        {
            builder.Init(3).Root((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Root(new Number(3), new Number(2)));
        }

        [Fact]
        public void RootNumberTest()
        {
            builder.Init(3).Root(2);

            Assert.Equal(builder.Current, new Root(new Number(3), new Number(2)));
        }

        [Fact]
        public void RootVariableTest()
        {
            builder.Init(3).Root("x");

            Assert.Equal(builder.Current, new Root(new Number(3), Variable.X));
        }

        [Fact]
        public void AbsNumberTest()
        {
            builder.Init(3).Abs();

            Assert.Equal(builder.Current, new Abs(new Number(3)));
        }

        [Fact]
        public void LogExpTest()
        {
            builder.Init(3).Log((IExpression)new Number(2));

            Assert.Equal(builder.Current, new Log(new Number(3), new Number(2)));
        }

        [Fact]
        public void LogNumberTest()
        {
            builder.Init(3).Log(2);

            Assert.Equal(builder.Current, new Log(new Number(3), new Number(2)));
        }

        [Fact]
        public void LogVariableTest()
        {
            builder.Init(3).Log("x");

            Assert.Equal(builder.Current, new Log(new Number(3), Variable.X));
        }

        [Fact]
        public void LnNumberTest()
        {
            builder.Init(3).Ln();

            Assert.Equal(builder.Current, new Ln(new Number(3)));
        }

        [Fact]
        public void LgNumberTest()
        {
            builder.Init(3).Lg();

            Assert.Equal(builder.Current, new Lg(new Number(3)));
        }

        [Fact]
        public void LbNumberTest()
        {
            builder.Init(3).Lb();

            Assert.Equal(builder.Current, new Lb(new Number(3)));
        }

        #endregion Standart

        #region Trigonometric

        [Fact]
        public void SinTest()
        {
            builder.Init(3).Sin();

            Assert.Equal(builder.Current, new Sin(new Number(3)));
        }

        [Fact]
        public void CosTest()
        {
            builder.Init(3).Cos();

            Assert.Equal(builder.Current, new Cos(new Number(3)));
        }

        [Fact]
        public void TanTest()
        {
            builder.Init(3).Tan();

            Assert.Equal(builder.Current, new Tan(new Number(3)));
        }

        [Fact]
        public void CotTest()
        {
            builder.Init(3).Cot();

            Assert.Equal(builder.Current, new Cot(new Number(3)));
        }

        [Fact]
        public void SecTest()
        {
            builder.Init(3).Sec();

            Assert.Equal(builder.Current, new Sec(new Number(3)));
        }

        [Fact]
        public void CscTest()
        {
            builder.Init(3).Csc();

            Assert.Equal(builder.Current, new Csc(new Number(3)));
        }

        [Fact]
        public void ArcsinTest()
        {
            builder.Init(3).Arcsin();

            Assert.Equal(builder.Current, new Arcsin(new Number(3)));
        }

        [Fact]
        public void ArccosTest()
        {
            builder.Init(3).Arccos();

            Assert.Equal(builder.Current, new Arccos(new Number(3)));
        }

        [Fact]
        public void ArctanTest()
        {
            builder.Init(3).Arctan();

            Assert.Equal(builder.Current, new Arctan(new Number(3)));
        }

        [Fact]
        public void ArccotTest()
        {
            builder.Init(3).Arccot();

            Assert.Equal(builder.Current, new Arccot(new Number(3)));
        }

        [Fact]
        public void ArcsecTest()
        {
            builder.Init(3).Arcsec();

            Assert.Equal(builder.Current, new Arcsec(new Number(3)));
        }

        [Fact]
        public void ArccscTest()
        {
            builder.Init(3).Arccsc();

            Assert.Equal(builder.Current, new Arccsc(new Number(3)));
        }

        #endregion Trigonometric

        #region Hyperbolic

        [Fact]
        public void SinhTest()
        {
            builder.Init(3).Sinh();

            Assert.Equal(builder.Current, new Sinh(new Number(3)));
        }

        [Fact]
        public void CoshTest()
        {
            builder.Init(3).Cosh();

            Assert.Equal(builder.Current, new Cosh(new Number(3)));
        }

        [Fact]
        public void TanhTest()
        {
            builder.Init(3).Tanh();

            Assert.Equal(builder.Current, new Tanh(new Number(3)));
        }

        [Fact]
        public void CothTest()
        {
            builder.Init(3).Coth();

            Assert.Equal(builder.Current, new Coth(new Number(3)));
        }

        [Fact]
        public void SechTest()
        {
            builder.Init(3).Sech();

            Assert.Equal(builder.Current, new Sech(new Number(3)));
        }

        [Fact]
        public void CschTest()
        {
            builder.Init(3).Csch();

            Assert.Equal(builder.Current, new Csch(new Number(3)));
        }

        [Fact]
        public void ArsinhTest()
        {
            builder.Init(3).Arsinh();

            Assert.Equal(builder.Current, new Arsinh(new Number(3)));
        }

        [Fact]
        public void ArcoshTest()
        {
            builder.Init(3).Arcosh();

            Assert.Equal(builder.Current, new Arcosh(new Number(3)));
        }

        [Fact]
        public void ArtanhTest()
        {
            builder.Init(3).Artanh();

            Assert.Equal(builder.Current, new Artanh(new Number(3)));
        }

        [Fact]
        public void ArcothTest()
        {
            builder.Init(3).Arcoth();

            Assert.Equal(builder.Current, new Arcoth(new Number(3)));
        }

        [Fact]
        public void ArsechTest()
        {
            builder.Init(3).Arsech();

            Assert.Equal(builder.Current, new Arsech(new Number(3)));
        }

        [Fact]
        public void ArcschTest()
        {
            builder.Init(3).Arcsch();

            Assert.Equal(builder.Current, new Arcsch(new Number(3)));
        }

        #endregion Hyperbolic

    }

}
