namespace xFunc.Maths.Analyzers2.Rules;

public class TruncUnaryRule : UnaryRule<Trunc>
{
    protected override Trunc Create(IExpression argument)
        => new Trunc(argument);

    public override string Name => "Trunc Unary Rule";
}