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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions.Matrices
{

    public class DeterminantTest
    {

        [Fact]
        public void ExecuteTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) }),
                new Vector(new[] { new Number(-7), new Number(8), new Number(9) })
            });

            var det = new Determinant(matrix);

            Assert.Equal(204.0, det.Execute());
        }

        [Fact]
        public void ExecuteIsNotSquareTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) })
            });

            var det = new Determinant(matrix);

            Assert.Throws<ArgumentException>(() => det.Execute());
        }

        [Fact]
        public void ExecuteVectorTest()
        {
            var vector = new Vector(new[] { new Number(1), new Number(-2), new Number(3) });
            var det = new Determinant(vector);

            Assert.Throws<ResultIsNotSupportedException>(() => det.Execute());
        }

        [Fact]
        public void ExecuteEmptyMatrixTest()
        {
            var matrix = new Matrix(2, 2);
            var det = new Determinant(matrix);

            Assert.Throws<ArgumentException>(() => det.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Transpose(new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) }),
                new Vector(new[] { new Number(-7), new Number(8), new Number(9) })
            }));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
