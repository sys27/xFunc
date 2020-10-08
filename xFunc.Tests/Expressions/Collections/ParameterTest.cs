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
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Matrices;
using Xunit;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Tests.Expressions.Collections
{
    public class ParameterTest
    {
        [Fact]
        public void DoubleCtor()
        {
            var value = 1.0;
            var x = new Parameter("x", value);
            var expected = new NumberValue(value);

            Assert.Equal(expected, x.Value);
        }

        [Fact]
        public void NumberCtor()
        {
            var value = new NumberValue(1.0);
            var x = new Parameter("x", value);

            Assert.Equal(value, x.Value);
        }

        [Fact]
        public void AngleValueCtor()
        {
            var value = AngleValue.Degree(1.0);
            var x = new Parameter("x", value);

            Assert.Equal(value, x.Value);
        }

        [Fact]
        public void ComplexCtor()
        {
            var value = new Complex(1, 2);
            var x = new Parameter("x", value);

            Assert.Equal(value, x.Value);
        }

        [Fact]
        public void BoolCtor()
        {
            var value = true;
            var x = new Parameter("x", value);

            Assert.Equal(value, x.Value);
        }

        [Fact]
        public void VectorCtor()
        {
            var value = new Vector(new IExpression[] { Number.One });
            var x = new Parameter("x", value);

            Assert.Equal(value, x.Value);
        }

        [Fact]
        public void MatrixCtor()
        {
            var value = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One })
            });
            var x = new Parameter("x", value);

            Assert.Equal(value, x.Value);
        }

        [Fact]
        public void NullEqual()
        {
            var parameter = new Parameter("x", 1);
            var isEqual = parameter.Equals(null);

            Assert.False(isEqual);
        }

        [Fact]
        public void RefEqualEqual()
        {
            var parameter = new Parameter("x", 1);
            var isEqual = parameter.Equals(parameter);

            Assert.True(isEqual);
        }

        [Fact]
        public void EqualObjectEqual()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("x", 1);
            var isEqual = parameter1.Equals((object)parameter2);

            Assert.True(isEqual);
        }

        [Fact]
        public void OtherType()
        {
            var parameter = new Parameter("x", 1);
            var obj = new object();
            var isEqual = parameter.Equals(obj);

            Assert.False(isEqual);
        }

        [Fact]
        public void EqualParameters()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("x", 1);
            var isEqual = parameter1.Equals(parameter2);

            Assert.True(isEqual);
        }

        [Fact]
        public void NotEqualKey()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("y", 1);
            var isEqual = parameter1.Equals(parameter2);

            Assert.False(isEqual);
        }

        [Fact]
        public void NotEqualValue()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("x", 2);
            var isEqual = parameter1.Equals(parameter2);

            Assert.False(isEqual);
        }

        [Fact]
        public void EqualOperatorTest()
        {
            var x = new Parameter("x", 1);
            var y = new Parameter("x", 1);

            Assert.True(x == y);
        }

        [Fact]
        public void NotEqualOperatorTest()
        {
            var x = new Parameter("x", 1);
            var y = new Parameter("x", 2);

            Assert.True(x != y);
        }

        [Fact]
        public void GreaterTest()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("a", 2);

            Assert.True(parameter1 > parameter2);
        }

        [Fact]
        public void GreaterLeftNullTest()
        {
            var parameter2 = new Parameter("a", 2);

            Assert.False(null > parameter2);
        }

        [Fact]
        public void GreaterRightNullTest()
        {
            var parameter1 = new Parameter("x", 1);

            Assert.False(parameter1 > null);
        }

        [Fact]
        public void LessTest()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("y", 2);

            Assert.True(parameter1 < parameter2);
        }

        [Fact]
        public void LessLeftNullTest()
        {
            var parameter2 = new Parameter("y", 2);

            Assert.False(null < parameter2);
        }

        [Fact]
        public void LessRightNullTest()
        {
            var parameter1 = new Parameter("x", 1);

            Assert.False(parameter1 < null);
        }

        [Fact]
        public void GreaterOrEqualTest()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("a", 2);

            Assert.True(parameter1 >= parameter2);
        }

        [Fact]
        public void LessOrEqualTest()
        {
            var parameter1 = new Parameter("x", 1);
            var parameter2 = new Parameter("y", 2);

            Assert.True(parameter1 <= parameter2);
        }

        [Fact]
        public void CompareToNullTest()
        {
            var parameter = new Parameter("x", 1);
            var result = parameter.CompareTo(null);

            Assert.Equal(1, result);
        }

        [Fact]
        public void EmptyKeyTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Parameter(string.Empty, 1.0));
        }

        [Fact]
        public void SetNullValueTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Parameter("x", (Matrix)null));
        }

        [Fact]
        public void EditConstantParameterTest()
        {
            var parameter = new Parameter("x", 1.0, ParameterType.Constant);

            Assert.Throws<ParameterIsReadOnlyException>(() => parameter.Value = 3.0);
        }

        [Fact]
        public void EditReadonlyParameterTest()
        {
            var parameter = new Parameter("x", 1.0, ParameterType.ReadOnly);

            Assert.Throws<ParameterIsReadOnlyException>(() => parameter.Value = 3.0);
        }

        [Fact]
        public void ToStringTest()
        {
            var parameter = new Parameter("x", 1, ParameterType.Constant);
            var str = parameter.ToString();
            var expected = "x: 1 (Constant)";

            Assert.Equal(expected, str);
        }
    }
}