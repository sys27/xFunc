namespace xFunc.Maths.Analyzers2.Rules.AddRules;

public class AddTwoVariablesRule : Rule<Add>
{
    protected override Result ExecuteInternal(Add expression, RuleContext context)
        => expression switch
        {
            (Variable left, Variable right) when left.Name == right.Name
                => Handled(new Mul(Number.Two, left)),

            _ => NotHandled(),
        };

    public override string Name => "Add Same Variables";
}