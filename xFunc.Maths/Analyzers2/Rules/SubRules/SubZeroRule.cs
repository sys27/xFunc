namespace xFunc.Maths.Analyzers2.Rules.SubRules;

public class SubZeroRule : Rule<Sub>
{
    protected override Result ExecuteInternal(Sub expression, RuleContext context)
        => expression switch
        {
            // plus zero
            (Number(var number), var right) when number == 0
                => ReAnalyze(new UnaryMinus(right)),
            (var left, Number(var number)) when number == 0
                => Handled(left),

            _ => NotHandled(),
        };

    public override string Name => "Sub Zero Rule";
}