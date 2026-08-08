namespace xFunc.Maths.Analyzers2.Rules.DivRules;

public class DivByOneRule : Rule<Div>
{
    protected override Result ExecuteInternal(Div expression, RuleContext context)
        => expression switch
        {
            // x / 1
            (var left, Number(var number)) when number == 1
                => Handled(left),

            _ => NotHandled(),
        };

    public override string Name => "Div By One Rule";
}