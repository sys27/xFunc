namespace xFunc.Maths.Analyzers2.Rules.AddRules;

public class AddConstFoldingRule : Rule<Add>
{
    protected override Result ExecuteInternal(Add expression, RuleContext context)
        => expression switch
        {
            (Number left, Number right)
                => Handled(new Number(left.Value + right.Value)),

            (Number left, Angle right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Angle left, Number right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Angle left, Angle right)
                => Handled((left.Value + right.Value).AsExpression()),

            (Number left, Area right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Area left, Number right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Area left, Area right)
                => Handled((left.Value + right.Value).AsExpression()),

            (Number left, Length right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Length left, Number right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Length left, Length right)
                => Handled((left.Value + right.Value).AsExpression()),

            (Number left, Mass right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Mass left, Number right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Mass left, Mass right)
                => Handled((left.Value + right.Value).AsExpression()),

            (Number left, Power right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Power left, Number right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Power left, Power right)
                => Handled((left.Value + right.Value).AsExpression()),

            (Number left, Temperature right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Temperature left, Number right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Temperature left, Temperature right)
                => Handled((left.Value + right.Value).AsExpression()),

            (Number left, Time right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Time left, Number right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Time left, Time right)
                => Handled((left.Value + right.Value).AsExpression()),

            (Number left, Volume right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Volume left, Number right)
                => Handled((left.Value + right.Value).AsExpression()),
            (Volume left, Volume right)
                => Handled((left.Value + right.Value).AsExpression()),

            _ => NotHandled(),
        };

    public override string Name => "Add Constants Folding";
}