namespace xFunc.Maths.Analyzers2.Rules.DivRules;

public class DivBySameExpression : Rule<Div>
{
    protected override Result ExecuteInternal(Div expression, RuleContext context)
        => expression switch
        {
            // x / x
            (Variable left, Variable right) when left.Equals(right)
                => Handled(Number.One),

            _ => NotHandled(),
        };

    public override string Name => "Div By Same Expression";
}