namespace xFunc.Maths.Analyzers2;

internal interface IInitialChainRuleBuilder<out TExpression>
    where TExpression : IExpression
{
    IChainRuleBuilder<TExpression> WithRule(IRule<TExpression> rule);
}