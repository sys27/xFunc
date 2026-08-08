namespace xFunc.Maths.Analyzers2.Rules.SubRules;

public class SubTwoVariablesRule : Rule<Sub>
{
    protected override Result ExecuteInternal(Sub expression, RuleContext context)
        => expression switch
        {
            (Variable left, Variable right) when left.Name == right.Name
                => Handled(Number.Zero),

            _ => NotHandled(),
        };

    public override string Name => "Sub Two Variables";
}