namespace xFunc.Maths.Analyzers2.Rules;

public class LgWithNumberTwoRule : Rule<Lg>
{
    protected override Result ExecuteInternal(Lg expression, RuleContext context)
        => expression.Argument switch
        {
            Number(var number) when number == 10 => Handled(Number.One),
            _ => NotHandled(),
        };

    public override string Name => "Common Logarithm With Number Ten Rule";
}