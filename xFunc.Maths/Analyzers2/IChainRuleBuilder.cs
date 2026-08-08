namespace xFunc.Maths.Analyzers2;

internal interface IChainRuleBuilder<out TExpression>
    where TExpression : IExpression
{
    IChainRuleBuilder<TExpression> WithNext(IRule<TExpression> next);
}