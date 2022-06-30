namespace xFunc.Maths.Analyzers2.Rules.AddRules;

public class AddUnaryMinusRule : Rule<Add>
{
    protected override Result ExecuteInternal(Add expression, RuleContext context)
        => expression switch
        {
            (UnaryMinus minus, var right)
                => ReAnalyze(new Sub(right, minus.Argument)),

            (var left, UnaryMinus minus)
                => ReAnalyze(new Sub(left, minus.Argument)),

            _ => NotHandled(),
        };

    public override string Name => "Add Unary Minus";
}