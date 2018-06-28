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

    public class InverseTest
    {

        [Fact]
        public void ExecuteMatrixTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(3), new Number(7), new Number(2), new Number(5) }),
                new Vector(new[] { new Number(1), new Number(8), new Number(4), new Number(2) }),
                new Vector(new[] { new Number(2), new Number(1), new Number(9), new Number(3) }),
                new Vector(new[] { new Number(5), new Number(4), new Number(7), new Number(1) })
            });
            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(0.0970873786407767), new Number(-0.18270079435128), new Number(-0.114739629302736), new Number(0.224183583406884) }),
                new Vector(new[] { new Number(-0.0194174757281553), new Number(0.145631067961165), new Number(-0.0679611650485437), new Number(0.00970873786407767) }),
                new Vector(new[] { new Number(-0.087378640776699), new Number(0.0644307149161518), new Number(0.103265666372463), new Number(-0.00176522506619595) }),
                new Vector(new[] { new Number(0.203883495145631), new Number(-0.120035304501324), new Number(0.122683142100618), new Number(-0.147396293027361) })
            });

            var exp = new Inverse(matrix);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteEmptyTest()
        {
            var matrix = new Matrix(2, 2);
            var exp = new Inverse(matrix);

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteIsNotSquareTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(3), new Number(7), new Number(2), new Number(5) }),
                new Vector(new[] { new Number(1), new Number(8), new Number(4), new Number(2) }),
                new Vector(new[] { new Number(2), new Number(1), new Number(9), new Number(3) })
            });
            var exp = new Inverse(matrix);

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteVectorTest()
        {
            var vector = new Vector(new[] { new Number(3), new Number(7), new Number(2), new Number(5) });
            var exp = new Inverse(vector);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Inverse(new Matrix(new[]
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
