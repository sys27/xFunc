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
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.Expressions.Matrices
{

    public class VectorTest
    {

        [Fact]
        public void MulByNumberVectorTest()
        {
            var vector = new Vector(new[] { new Number(2), new Number(3) });
            var number = new Number(5);

            var expected = new Vector(new[] { new Number(10), new Number(15) });
            var result = vector.Mul(number);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddVectorsTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1) });

            var expected = new Vector(new[] { new Number(9), new Number(4) });
            var result = vector1.Add(vector2);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddVectorsDiffSizeTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1), new Number(3) });

            Assert.Throws<ArgumentException>(() => vector1.Add(vector2));
        }

        [Fact]
        public void SubVectorsTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1) });

            var expected = new Vector(new[] { new Number(-5), new Number(2) });
            var result = vector1.Sub(vector2);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SubVectorsDiffSizeTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1), new Number(3) });

            Assert.Throws<ArgumentException>(() => vector1.Sub(vector2));
        }

        [Fact]
        public void TransposeVectorTest()
        {
            var vector = new Vector(new[] { new Number(1), new Number(2) });

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(1) }),
                new Vector(new[] { new Number(2) })
            });
            var result = vector.Transpose();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void VectorMulMatrixTest()
        {
            var vector = new Vector(new[] { new Number(-2), new Number(1) });
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(3) }),
                new Vector(new[] { new Number(-1) })
            });

            var expected = new Matrix(new[] { new Vector(new[] { new Number(-7) }) });
            var result = vector.Mul(matrix);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MultiOpMulAdd()
        {
            // ({1, 2, 3} * 4) + {2, 3, 4}
            var vector1 = new Vector(new[] { new Number(1), new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(2), new Number(3), new Number(4) });
            var mul = new Mul(vector1, new Number(4));
            var add = new Add(mul, vector2);

            var expected = new Vector(new[] { new Number(6), new Number(11), new Number(16) });
            var result = add.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MultiOpMulSub()
        {
            // ({1, 2, 3} * 4) - {2, 3, 4}
            var vector1 = new Vector(new[] { new Number(1), new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(2), new Number(3), new Number(4) });
            var mul = new Mul(vector1, new Number(4));
            var sub = new Sub(mul, vector2);

            var expected = new Vector(new[] { new Number(2), new Number(5), new Number(8) });
            var result = sub.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MultiOpSubMul()
        {
            // ({2, 3, 4} - {1, 2, 3}) * 4
            var vector1 = new Vector(new[] { new Number(1), new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(2), new Number(3), new Number(4) });
            var sub = new Sub(vector2, vector1);
            var mul = new Mul(sub, new Number(4));

            var expected = new Vector(new[] { new Number(4), new Number(4), new Number(4) });
            var result = mul.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MultiOpAddMul()
        {
            // ({2, 3, 4} + {1, 2, 3}) * 4
            var vector1 = new Vector(new[] { new Number(1), new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(2), new Number(3), new Number(4) });
            var add = new Add(vector2, vector1);
            var mul = new Mul(add, new Number(4));

            var expected = new Vector(new[] { new Number(12), new Number(20), new Number(28) });
            var result = mul.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MultiOpMulMul()
        {
            // ({1, 2, 3} * 2) * 4
            var vector = new Vector(new[] { new Number(1), new Number(2), new Number(3) });
            var mul1 = new Mul(vector, new Number(2));
            var mul2 = new Mul(mul1, new Number(4));

            var expected = new Vector(new[] { new Number(8), new Number(16), new Number(24) });
            var result = mul2.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Vector(new[] { new Number(1), new Number(2), new Number(3) });
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
