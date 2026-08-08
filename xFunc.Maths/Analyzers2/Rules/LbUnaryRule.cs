namespace xFunc.Maths.Analyzers2.Rules;

public class LbUnaryRule : UnaryRule<Lb>
{
    protected override Lb Create(IExpression argument)
        => new Lb(argument);

    public override string Name => "Binary Logarithm Unary Rule";
}