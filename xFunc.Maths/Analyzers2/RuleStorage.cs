namespace xFunc.Maths.Analyzers2;

internal class RuleStorage
{
    private readonly IReadOnlyDictionary<Type, IRule> rules;

    public RuleStorage(IReadOnlyDictionary<Type, IRule> rules)
        => this.rules = rules;

    public IRule? GetRule(Type type)
    {
        rules.TryGetValue(type, out var rule);

        return rule;
    }
}