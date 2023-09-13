// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Matrices;

public class InverseTest : BaseExpressionTests
{
    [Test]
    public void ExecuteMatrixTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3), new Number(7), Number.Two, new Number(5) }),
            new Vector(new IExpression[] { Number.One, new Number(8), new Number(4), Number.Two }),
            new Vector(new IExpression[] { Number.Two, Number.One, new Number(9), new Number(3) }),
            new Vector(new IExpression[] { new Number(5), new Number(4), new Number(7), Number.One })
        });
        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(0.0970873786407767), new NumberValue(-0.18270079435128), new NumberValue(-0.114739629302736), new NumberValue(0.224183583406884) },
            new NumberValue[] { new NumberValue(-0.0194174757281553), new NumberValue(0.145631067961165), new NumberValue(-0.0679611650485437), new NumberValue(0.00970873786407767) },
            new NumberValue[] { new NumberValue(-0.087378640776699), new NumberValue(0.0644307149161518), new NumberValue(0.103265666372463), new NumberValue(-0.00176522506619595) },
            new NumberValue[] { new NumberValue(0.203883495145631), new NumberValue(-0.120035304501324), new NumberValue(0.122683142100618), new NumberValue(-0.147396293027361) },
        });

        var exp = new Inverse(matrix);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteIsNotSquareTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new[] { new Number(3), new Number(7), Number.Two, new Number(5) }),
            new Vector(new[] { Number.One, new Number(8), new Number(4), Number.Two }),
            new Vector(new[] { Number.Two, Number.One, new Number(9), new Number(3) })
        });
        var exp = new Inverse(matrix);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteVectorTest()
    {
        var vector = new Vector(new[] { new Number(3), new Number(7), Number.Two, new Number(5) });
        var exp = new Inverse(vector);

        TestNotSupported(exp);
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Inverse(new Matrix(new[]
        {
            new Vector(new[] { Number.One, new Number(-2), new Number(3) }),
            new Vector(new[] { new Number(4), Number.Zero, new Number(6) }),
            new Vector(new[] { new Number(-7), new Number(8), new Number(9) })
        }));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}