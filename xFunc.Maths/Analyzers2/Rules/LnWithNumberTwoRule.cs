namespace xFunc.Maths.Analyzers2.Rules;

public class LnWithNumberTwoRule : Rule<Ln>
{
    protected override Result ExecuteInternal(Ln expression, RuleContext context)
        => expression.Argument switch
        {
            Variable("e") => Handled(Number.One),
            _ => NotHandled(),
        };

    public override string Name => "Natural Logarithm With Exponent Rule";
}