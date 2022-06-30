namespace xFunc.Maths.Analyzers2.Rules;

public class FactUnaryRule : UnaryRule<Fact>
{
    protected override Fact Create(IExpression argument)
        => new Fact(argument);

    public override string Name => "Fact Unary Rule";
}