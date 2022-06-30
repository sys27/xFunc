namespace xFunc.Maths.Analyzers2.Rules.SubRules;

public class SubUnaryMinusRule : Rule<Sub>
{
    protected override Result ExecuteInternal(Sub expression, RuleContext context)
        => expression switch
        {
            (var left, UnaryMinus minus)
                => Handled(new Add(left, minus.Argument)),

            _ => NotHandled(),
        };

    public override string Name => "Sub Unary Minus";
}