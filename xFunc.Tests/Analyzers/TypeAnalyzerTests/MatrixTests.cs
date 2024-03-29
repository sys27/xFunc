// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class MatrixTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestVectorUndefined()
    {
        var exp = new Vector(new IExpression[] { new Number(10), Variable.X });

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestVectorNumber()
    {
        var exp = new Vector(new IExpression[] { new Number(10), new Number(10), new Number(10) });

        Test(exp, ResultTypes.Vector);
    }

    [Test]
    public void TestVectorException()
    {
        var exp = new Vector(new IExpression[] { new ComplexNumber(10), new Number(10) });

        TestDiffParamException(exp);
    }

    [Test]
    public void TestMatrixVector()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One }),
            new Vector(new IExpression[] { Number.One })
        });

        Test(exp, ResultTypes.Matrix);
    }

    [Test]
    public void TestMatrixVectorUndefined()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { Variable.X }),
        });

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestDeterminantUndefined()
    {
        var exp = new Determinant(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestDeterminantMatrix()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) }),
        });
        var exp = new Determinant(matrix);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestDeterminantException()
    {
        var exp = new Determinant(new ComplexNumber(2, 2));

        TestException(exp);
    }

    [Test]
    public void TestDeterminantsException2()
    {
        var exp = new Determinant(
            new Vector(new IExpression[]
            {
                new Number(3),
                new Number(7),
                Number.Two,
                new Number(5)
            }));

        TestException(exp);
    }

    [Test]
    public void TestInverseUndefined()
    {
        var exp = new Inverse(Variable.X);

        Test(exp, ResultTypes.Matrix);
    }

    [Test]
    public void TestInverseMatrix()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) }),
        });
        var exp = new Inverse(matrix);

        Test(exp, ResultTypes.Matrix);
    }

    [Test]
    public void TestInverseException()
    {
        var exp = new Inverse(new ComplexNumber(2, 2));

        TestException(exp);
    }

    [Test]
    public void TestInverseException2()
    {
        var exp = new Inverse(
            new Vector(new IExpression[]
            {
                new Number(3),
                new Number(7),
                Number.Two,
                new Number(5)
            }));

        TestException(exp);
    }

    [Test]
    public void DotProductUndefined()
    {
        var exp = new DotProduct(Variable.X, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void DotProductLeftUndefined()
    {
        var exp = new DotProduct(Variable.X, new Vector(new IExpression[] { Number.One }));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void DotProductRightUndefined()
    {
        var exp = new DotProduct(new Vector(new IExpression[] { Number.One }), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void DotProduct()
    {
        var exp = new DotProduct(
            new Vector(new IExpression[] { Number.One }),
            new Vector(new IExpression[] { Number.Two }));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void DotProductLeftException()
    {
        var exp = new DotProduct(
            Number.One,
            new Vector(new IExpression[] { Number.Two }));

        TestBinaryException(exp);
    }

    [Test]
    public void DotProductRightException()
    {
        var exp = new DotProduct(
            new Vector(new IExpression[] { Number.Two }),
            Number.One);

        TestBinaryException(exp);
    }

    [Test]
    public void CrossProductUndefined()
    {
        var exp = new CrossProduct(Variable.X, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void CrossProductLeftUndefined()
    {
        var exp = new CrossProduct(Variable.X, new Vector(new IExpression[] { Number.One }));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void CrossProductRightUndefined()
    {
        var exp = new CrossProduct(new Vector(new IExpression[] { Number.One }), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void CrossProduct()
    {
        var exp = new CrossProduct(
            new Vector(new IExpression[] { Number.One }),
            new Vector(new IExpression[] { Number.Two }));

        Test(exp, ResultTypes.Vector);
    }

    [Test]
    public void CrossProductLeftException()
    {
        var exp = new CrossProduct(
            Number.One,
            new Vector(new IExpression[] { Number.Two }));

        TestBinaryException(exp);
    }

    [Test]
    public void CrossProductRightException()
    {
        var exp = new CrossProduct(
            new Vector(new IExpression[] { Number.Two }),
            Number.One);

        TestBinaryException(exp);
    }

    [Test]
    public void TestTransposeUndefined()
    {
        var exp = new Transpose(Variable.X);

        Test(exp, ResultTypes.Matrix);
    }

    [Test]
    public void TestTransposeMatrix()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) }),
        });
        var exp = new Transpose(matrix);

        Test(exp, ResultTypes.Matrix);
    }

    [Test]
    public void TestTransposeException()
    {
        var exp = new Transpose(new ComplexNumber(2, 2));

        TestException(exp);
    }
}