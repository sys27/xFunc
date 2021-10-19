// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests
{
    public class GreaterOrEqualTests : TypeAnalyzerBaseTests
    {
        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestUndefined(Type type)
        {
            var exp = CreateBinary(type, Variable.X, Variable.X);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestNumberUndefined(Type type)
        {
            var exp = CreateBinary(type, Number.One, Variable.X);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestUndefinedNumber(Type type)
        {
            var exp = CreateBinary(type, Variable.X, Number.One);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestAngleUndefined(Type type)
        {
            var exp = CreateBinary(type, AngleValue.Degree(1).AsExpression(), Variable.X);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestGUndefinedAngle(Type type)
        {
            var exp = CreateBinary(type, Variable.X, AngleValue.Degree(1).AsExpression());

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestNumber(Type type)
        {
            var exp = CreateBinary(type, new Number(10), new Number(10));

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestAngle(Type type)
        {
            var exp = CreateBinary(type,
                AngleValue.Degree(10).AsExpression(),
                AngleValue.Degree(12).AsExpression()
            );

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestPower(Type type)
        {
            var exp = CreateBinary(type,
                PowerValue.Watt(10).AsExpression(),
                PowerValue.Watt(12).AsExpression()
            );

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestBoolNumberException(Type type)
        {
            var exp = CreateBinary(type, Bool.True, new Number(10));

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestNumberBoolException(Type type)
        {
            var exp = CreateBinary(type, new Number(10), Bool.True);

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestBoolAngle(Type type)
        {
            var exp = CreateBinary(type,
                Bool.True,
                AngleValue.Degree(12).AsExpression()
            );

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestAngleBool(Type type)
        {
            var exp = CreateBinary(type,
                AngleValue.Degree(12).AsExpression(),
                Bool.True
            );

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestBoolPower(Type type)
        {
            var exp = CreateBinary(type,
                Bool.True,
                PowerValue.Watt(12).AsExpression()
            );

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestPowerBool(Type type)
        {
            var exp = CreateBinary(type,
                PowerValue.Watt(12).AsExpression(),
                Bool.True
            );

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(GreaterThan))]
        [InlineData(typeof(GreaterOrEqual))]
        [InlineData(typeof(LessThan))]
        [InlineData(typeof(LessOrEqual))]
        public void TestRelationalOperatorException(Type type)
        {
            var exp = CreateBinary(type, Bool.True, Bool.False);

            TestException(exp);
        }
    }
}