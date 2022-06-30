namespace xFunc.Maths.Analyzers2.Rules.SubRules;

public class SubGroupRule : Rule<Sub>
{
    protected override Result ExecuteInternal(Sub expression, RuleContext context)
        => expression switch
        {
            // (2 + x) - 2
            // (x + 2) - 2
            (Add(var left, Number right), Number number)
                => ReAnalyze(new Add(left, new Number(number.Value - right.Value))),

            // 2 - (2 + x)
            // 2 - (x + 2)
            (Number number, Add(var left, Number right))
                => ReAnalyze(new Sub(new Number(number.Value - right.Value), left)),

            // (2 - x) - 2
            (Sub(Number left, var right), Number number)
                => ReAnalyze(new Sub(new Number(number.Value - left.Value), right)),

            // (x - 2) - 2
            (Sub(var left, Number right), Number number)
                => ReAnalyze(new Sub(left, new Number(number.Value + right.Value))),

            // 2 - (2 - x)
            (Number number, Sub(Number left, var right))
                => ReAnalyze(new Add(new Number(number.Value - left.Value), right)),

            // 2 - (x - 2)
            (Number number, Sub(var left, Number right))
                => ReAnalyze(new Sub(new Number(number.Value + right.Value), left)),

            // x - bx
            // x - xb
            (var x1, Mul(Number b, var x2)) when x1.Equals(x2)
                => ReAnalyze(new Mul(new Number(1 - b.Value), x1)),

            // ax - x
            // xa - x
            (Mul(Number a, var x1), var x2) when x1.Equals(x2)
                => ReAnalyze(new Mul(new Number(a.Value - 1), x1)),

            // ax - bx
            // ax - xb
            // xa - bx
            // xa - xb
            (Mul(Number a, var x1), Mul(Number b, var x2)) when x1.Equals(x2)
                => ReAnalyze(new Mul(new Number(a.Value - b.Value), x1)),

            _ => NotHandled(),
        };

    public override string Name => "Sub Group Like Terms";
}