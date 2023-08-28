using xFunc.Maths.Expressions.Domains;

namespace xFunc.Tests.Expressions.Domains;

public class DomainBuilderTests
{
    [Test]
    public void AddRangeExceeded()
    {
        var domain = new DomainBuilder(1)
            .AddRange(r => r.Start(NumberValue.NegativeInfinity).End(NumberValue.PositiveInfinity));

        Assert.Throws<InvalidOperationException>(() =>
            domain.AddRange(r => r.Start(NumberValue.NegativeInfinity).End(NumberValue.PositiveInfinity)));
    }
}