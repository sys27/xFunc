namespace xFunc.Maths.Analyzers2.Rules;

public class FracUnaryRule : UnaryRule<Frac>
{
    protected override Frac Create(IExpression argument)
        => new Frac(argument);

    public override string Name => "Frac Unary Rule";
}