namespace xFunc.Maths.Analyzers2.Rules;

public class LbWithNumberTwoRule : Rule<Lb>
{
    protected override Result ExecuteInternal(Lb expression, RuleContext context)
        => expression.Argument switch
        {
            Number(var number) when number == 2 => Handled(Number.One),
            _ => NotHandled(),
        };

    public override string Name => "Binary Logarithm With Number Two Rule";
}