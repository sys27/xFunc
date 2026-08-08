namespace xFunc.Maths.Analyzers2.Rules.AbsRules;

public class AbsUnaryMinusRule : Rule<Abs>
{
    protected override Result ExecuteInternal(Abs expression, RuleContext context)
        => expression.Argument switch
        {
            UnaryMinus minus => Handled(minus.Argument),
            _ => NotHandled(),
        };

    public override string Name => "Abs Unary Minus";
}