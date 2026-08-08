namespace xFunc.Maths.Analyzers2.Rules;

public class LgUnaryRule : UnaryRule<Lg>
{
    protected override Lg Create(IExpression argument)
        => new Lg(argument);

    public override string Name => "Common Logarithm Unary Rule";
}