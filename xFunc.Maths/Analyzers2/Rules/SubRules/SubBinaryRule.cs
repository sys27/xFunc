namespace xFunc.Maths.Analyzers2.Rules.SubRules;

public class SubBinaryRule : BinaryRule<Sub>
{
    protected override Sub Create(IExpression left, IExpression right)
        => new Sub(left, right);

    public override string Name => "Sub Binary Rule";
}