namespace xFunc.Maths.Analyzers2.Rules.AbsRules;

public class AbsNestedRule : Rule<Abs>
{
    protected override Result ExecuteInternal(Abs expression, RuleContext context)
        => expression.Argument switch
        {
            Abs abs => Handled(abs),
            _ => NotHandled(),
        };

    public override string Name => "Abs Nested Rule";
}