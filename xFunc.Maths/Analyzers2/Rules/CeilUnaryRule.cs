namespace xFunc.Maths.Analyzers2.Rules;

public class CeilUnaryRule : UnaryRule<Ceil>
{
    protected override Ceil Create(IExpression argument)
        => new Ceil(argument);

    public override string Name => "Ceil Unary Rule";
}