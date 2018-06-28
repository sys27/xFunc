// Copyright 2012-2018 Dmitry Kischenko
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
using System.Numerics;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions.Trigonometric
{
    
    public class ArcsecTest
    {
        
        [Fact]
        public void CalculateRadianTest()
        {
            var exp = new Arcsec(new Number(1));

            Assert.Equal(MathExtensions.Asec(1), exp.Execute(AngleMeasurement.Radian));
        }

        [Fact]
        public void CalculateDegreeTest()
        {
            var exp = new Arcsec(new Number(1));

            Assert.Equal(MathExtensions.Asec(1) / Math.PI * 180, exp.Execute(AngleMeasurement.Degree));
        }

        [Fact]
        public void CalculateGradianTest()
        {
            var exp = new Arcsec(new Number(1));

            Assert.Equal(MathExtensions.Asec(1) / Math.PI * 200, exp.Execute(AngleMeasurement.Gradian));
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Arcsec(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(ComplexExtensions.Asec(complex), exp.Execute());
            Assert.Equal(1.3408334244176887, result.Real, 15);
            Assert.Equal(0.15735549884498545, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
        {
            var exp = new Arcsec(new Bool(false));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Arcsec(new Number(1));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
