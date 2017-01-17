// Copyright 2012-2017 Dmitry Kischenko
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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions.Maths.LogicalAndBitwise
{
    
    public class XOrTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new XOr(new Number(1), new Number(2));

            Assert.Equal(3.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new XOr(new Number(1), new Number(2.5));

            Assert.Equal(2.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new XOr(new Bool(true), new Bool(true));

            Assert.Equal(false, exp.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = new XOr(new Bool(false), new Bool(true));

            Assert.Equal(true, exp.Execute());
        }

        [Fact]
        public void ResultTypeNumberNumberTest()
        {
            var exp = new XOr(new Number(2), new Number(4));

            Assert.Equal(ExpressionResultType.Number, exp.ResultType);
        }

        [Fact]
        public void ResultTypeBoolBoolTest()
        {
            var exp = new XOr(new Bool(true), new Bool(false));

            Assert.Equal(ExpressionResultType.Boolean, exp.ResultType);
        }

        [Fact]
        public void ResultTypeVarNumTest()
        {
            var exp = new XOr(new Variable("x"), new Number(1));

            Assert.Equal(ExpressionResultType.Number, exp.ResultType);
        }

        [Fact]
        public void ResultTypeNumVarTest()
        {
            var exp = new XOr(new Number(1), new Variable("x"));

            Assert.Equal(ExpressionResultType.Number, exp.ResultType);
        }

        [Fact]
        public void ResultTypeVarBoolTest()
        {
            var exp = new XOr(new Variable("x"), new Bool(true));

            Assert.Equal(ExpressionResultType.Boolean, exp.ResultType);
        }

        [Fact]
        public void ResultTypeBoolVarTest()
        {
            var exp = new XOr(new Bool(true), new Variable("x"));

            Assert.Equal(ExpressionResultType.Boolean, exp.ResultType);
        }

        [Fact]
        public void ResultTypeVerVarTest()
        {
            var exp = new XOr(new Variable("y"), new Variable("x"));

            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.Boolean, exp.ResultType);
        }

        [Fact]
        public void ResultTypeNumberBoolTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new XOr(new Number(2), new Bool(false)));
        }

        [Fact]
        public void ResultTypeBoolNumberTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new XOr(new Bool(true), new Number(2)));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new XOr(new Bool(true), new Bool(false));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
