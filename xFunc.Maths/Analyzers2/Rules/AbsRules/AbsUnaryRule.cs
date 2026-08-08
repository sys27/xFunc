namespace xFunc.Maths.Analyzers2.Rules.AbsRules;

public class AbsUnaryRule : UnaryRule<Abs>
{
    protected override Abs Create(IExpression argument)
        => new Abs(argument);

    public override string Name => "Abs Unary Rule";
}