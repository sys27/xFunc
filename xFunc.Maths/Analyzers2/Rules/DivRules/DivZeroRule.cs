namespace xFunc.Maths.Analyzers2.Rules.DivRules;

public class DivZeroRule : Rule<Div>
{
    protected override Result ExecuteInternal(Div expression, RuleContext context)
        => expression switch
        {
            // 0 / 0
            (Number(var left), Number(var right)) when left == 0 && right == 0
                => Handled(new Number(double.NaN)),

            // 0 / x
            (Number(var number), _) when number == 0
                => Handled(Number.Zero),

            // x / 0
            (_, Number(var number)) when number == 0
                => throw new DivideByZeroException(),

            _ => NotHandled(),
        };

    public override string Name => "Div Zero Rule";
}