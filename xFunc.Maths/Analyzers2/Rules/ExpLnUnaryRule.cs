namespace xFunc.Maths.Analyzers2.Rules;

public class ExpLnUnaryRule : Rule<Exp>
{
    protected override Result ExecuteInternal(Exp expression, RuleContext context)
        => expression.Argument switch
        {
            Ln ln => Handled(ln.Argument),
            _ => NotHandled(),
        };

    public override string Name => "Exponential Ln Rule";
}