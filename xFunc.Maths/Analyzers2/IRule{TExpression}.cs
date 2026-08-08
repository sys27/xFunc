namespace xFunc.Maths.Analyzers2;

public interface IRule<in TExpression> : IRule
{
    Result Execute(TExpression expression, RuleContext context);
}