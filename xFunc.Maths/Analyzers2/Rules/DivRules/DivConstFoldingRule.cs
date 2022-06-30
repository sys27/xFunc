namespace xFunc.Maths.Analyzers2.Rules.DivRules;

public class DivConstFoldingRule : Rule<Div>
{
    protected override Result ExecuteInternal(Div expression, RuleContext context)
        => expression switch
        {
            (Number left, Number right)
                => Handled(new Number(left.Value / right.Value)),

            (Angle left, Number right)
                => Handled((left.Value / right.Value).AsExpression()),
            (Power left, Number right)
                => Handled((left.Value / right.Value).AsExpression()),
            (Temperature left, Number right)
                => Handled((left.Value / right.Value).AsExpression()),
            (Mass left, Number right)
                => Handled((left.Value / right.Value).AsExpression()),
            (Length left, Number right)
                => Handled((left.Value / right.Value).AsExpression()),
            (Time left, Number right)
                => Handled((left.Value / right.Value).AsExpression()),
            (Area left, Number right)
                => Handled((left.Value / right.Value).AsExpression()),
            (Volume left, Number right)
                => Handled((left.Value / right.Value).AsExpression()),

            _ => NotHandled(),
        };

    public override string Name => "Div Unit Constants";
}