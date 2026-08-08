namespace xFunc.Maths.Analyzers2.Rules;

public class LnUnaryRule : UnaryRule<Ln>
{
    protected override Ln Create(IExpression argument)
        => new Ln(argument);

    public override string Name => "Natural Logarithm Unary Rule";
}