namespace xFunc.Maths.Analyzers2;

internal class RuleStorageBuilder
{
    private readonly Dictionary<Type, IRule> rules = new Dictionary<Type, IRule>();

    public RuleStorageBuilder WithRule<TExpression>(IRule<TExpression> rule)
        where TExpression : IExpression
    {
        rules[typeof(TExpression)] = rule;

        return this;
    }

    public RuleStorageBuilder WithChain<TExpression>(
        Action<IInitialChainRuleBuilder<TExpression>> builder)
        where TExpression : IExpression
    {
        var ruleBuilder = new ChainRuleBuilder<TExpression>();
        builder(ruleBuilder);
        var rule = ruleBuilder.GetRule();
        rules[typeof(TExpression)] = rule;

        return this;
    }

    public RuleStorage Build()
        => new RuleStorage(rules);
}