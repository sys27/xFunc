namespace xFunc.Maths.Analyzers2;

internal class ChainRuleBuilder<TExpression> :
    IInitialChainRuleBuilder<TExpression>,
    IChainRuleBuilder<TExpression>
    where TExpression : IExpression
{
    private ChainRule initialRule;
    private ChainRule currentRule;

    public IChainRuleBuilder<TExpression> WithRule(IRule<TExpression> rule)
    {
        initialRule = currentRule = new ChainRule(rule);

        return this;
    }

    public IChainRuleBuilder<TExpression> WithNext(IRule<TExpression> next)
    {
        var chain = new ChainRule(next);
        currentRule.SetNext(chain);
        currentRule = chain;

        return this;
    }

    public IRule GetRule()
        => initialRule.UnwrapIfEmpty();

    private sealed class ChainRule : IRule<TExpression>
    {
        private readonly IRule<TExpression> current;
        private IRule<TExpression>? next;

        public ChainRule(IRule<TExpression> rule)
            => current = rule ?? throw new ArgumentNullException(nameof(rule));

        public void SetNext(IRule<TExpression> rule)
            => next = rule ?? throw new ArgumentNullException(nameof(rule));

        public IRule UnwrapIfEmpty()
            => next is null ? current : this;

        public Result Execute(IExpression expression, RuleContext context)
        {
            var result = current.Execute(expression, context);
            if (result.IsHandled() || result.IsReAnalyze())
                return result;

            if (result.IsContinue())
                expression = result.Value;

            if (next is not null)
                return next.Execute(expression, context);

            return Result.NotHandled();
        }

        public Result Execute(TExpression expression, RuleContext context)
            => Execute(expression as IExpression, context);

        public string Name => "Chain Rule";
    }
}