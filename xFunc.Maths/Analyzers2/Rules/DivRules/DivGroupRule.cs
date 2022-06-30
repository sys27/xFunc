namespace xFunc.Maths.Analyzers2.Rules.DivRules;

public class DivGroupRule : Rule<Div>
{
    protected override Result ExecuteInternal(Div expression, RuleContext context)
        => expression switch
        {
            // (2 * x) / 2
            // (x * 2) / 2
            (Mul(Number left, var right), Number number)
                => ReAnalyze(new Div(right, new Number(number.Value / left.Value))),

            // 2 / (2 * x)
            // 2 / (x * 2)
            (Number number, Mul(Number left, var right))
                => ReAnalyze(new Div(new Number(number.Value / left.Value), right)),

            // (2 / x) / 2
            (Div(Number left, var right), Number number)
                => ReAnalyze(new Div(new Number(left.Value / number.Value), right)),

            // (x / 2) / 2
            (Div(var left, Number right), Number number)
                => ReAnalyze(new Div(left, new Number(right.Value * number.Value))),

            // 2 / (2 / x)
            (Number number, Div(Number left, var right))
                => ReAnalyze(new Mul(new Number(number.Value / left.Value), right)),

            // 2 / (x / 2)
            (Number number, Div(var left, Number right))
                => ReAnalyze(new Div(new Number(number.Value * right.Value), left)),

            _ => NotHandled(),
        };

    public override string Name => "Div Group Like Terms";
}