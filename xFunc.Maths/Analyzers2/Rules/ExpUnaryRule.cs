namespace xFunc.Maths.Analyzers2.Rules;

public class ExpUnaryRule : UnaryRule<Exp>
{
    protected override Exp Create(IExpression argument)
        => new Exp(argument);

    public override string Name => "Exponential Unary Rule";
}