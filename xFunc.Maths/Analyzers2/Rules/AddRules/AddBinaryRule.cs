namespace xFunc.Maths.Analyzers2.Rules.AddRules;

public class AddBinaryRule : BinaryRule<Add>
{
    protected override Add Create(IExpression left, IExpression right)
        => new Add(left, right);

    public override string Name => "Add Binary Rule";
}