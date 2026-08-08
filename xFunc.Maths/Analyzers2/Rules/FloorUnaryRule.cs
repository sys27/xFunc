namespace xFunc.Maths.Analyzers2.Rules;

public class FloorUnaryRule : UnaryRule<Floor>
{
    protected override Floor Create(IExpression argument)
        => new Floor(argument);

    public override string Name => "Floor Unary Rule";
}