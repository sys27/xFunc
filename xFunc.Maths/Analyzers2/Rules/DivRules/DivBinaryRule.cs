namespace xFunc.Maths.Analyzers2.Rules.DivRules;

public class DivBinaryRule : BinaryRule<Div>
{
    protected override Div Create(IExpression left, IExpression right)
        => new Div(left, right);

    public override string Name => "Div Binary Rule";
}