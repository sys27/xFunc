namespace xFunc.Maths.Analyzers2.Rules.AddRules;

public class AddZeroRule : Rule<Add>
{
    protected override Result ExecuteInternal(Add expression, RuleContext context)
        => expression switch
        {
            (Number(var number), _) when number == 0
                => Handled(expression.Right),

            (_, Number(var number)) when number == 0
                => Handled(expression.Left),

            _ => NotHandled(),
        };

    public override string Name => "Add Zero";
}