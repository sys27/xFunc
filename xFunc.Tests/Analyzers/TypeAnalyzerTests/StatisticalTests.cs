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

using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Statistical;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class StatisticalTests : TypeAnalyzerBaseTests
    {
        [Fact]
        public void TestAvgUndefined()
        {
            var exp = new Avg(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAvgNumber()
        {
            var exp = new Avg(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestAvgVector()
        {
            var exp = new Avg(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestAvgOneException()
        {
            var exp = new Avg(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestAvgException()
        {
            var exp = new Avg(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestCountUndefined()
        {
            var exp = new Count(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestCountNumber()
        {
            var exp = new Count(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestCountVector()
        {
            var exp = new Count(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestCountOneException()
        {
            var exp = new Count(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestCountException()
        {
            var exp = new Count(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestMaxUndefined()
        {
            var exp = new Max(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestMaxNumber()
        {
            var exp = new Max(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestMaxVector()
        {
            var exp = new Max(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestMaxOneException()
        {
            var exp = new Max(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestMaxException()
        {
            var exp = new Max(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestMinUndefined()
        {
            var exp = new Min(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestMinNumber()
        {
            var exp = new Min(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestMinVector()
        {
            var exp = new Min(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestMinOneException()
        {
            var exp = new Min(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestMinException()
        {
            var exp = new Min(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestProductUndefined()
        {
            var exp = new Product(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestProductNumber()
        {
            var exp = new Product(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestProductVector()
        {
            var exp = new Product(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestProductOneException()
        {
            var exp = new Product(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestProductException()
        {
            var exp = new Product(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestStdevUndefined()
        {
            var exp = new Stdev(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestStdevNumber()
        {
            var exp = new Stdev(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestStdevVector()
        {
            var exp = new Stdev(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestStdevOneException()
        {
            var exp = new Stdev(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestStdevException()
        {
            var exp = new Stdev(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestStdevpUndefined()
        {
            var exp = new Stdevp(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestStdevpNumber()
        {
            var exp = new Stdevp(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestStdevpVector()
        {
            var exp = new Stdevp(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestStdevpOneException()
        {
            var exp = new Stdevp(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestStdevpException()
        {
            var exp = new Stdevp(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestSumUndefined()
        {
            var exp = new Sum(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestSumNumber()
        {
            var exp = new Sum(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestSumVector()
        {
            var exp = new Sum(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestSumOneException()
        {
            var exp = new Sum(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestSumException()
        {
            var exp = new Sum(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestVarUndefined()
        {
            var exp = new Var(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestVarNumber()
        {
            var exp = new Var(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestVarVector()
        {
            var exp = new Var(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestVarOneException()
        {
            var exp = new Var(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestVarException()
        {
            var exp = new Var(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestVarpUndefined()
        {
            var exp = new Varp(new IExpression[] { Variable.X, new Variable("y") });

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestVarpNumber()
        {
            var exp = new Varp(new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestVarpVector()
        {
            var exp = new Varp(new IExpression[] { new Vector(new IExpression[] { new Number(3), Number.Two }) });

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestVarpOneException()
        {
            var exp = new Varp(new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestVarpException()
        {
            var exp = new Varp(new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }
    }
}