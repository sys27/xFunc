namespace xFunc.Maths.Analyzers2;

public abstract class Rule<TExpression> : IRule<TExpression>
    where TExpression : IExpression
{
    Result IRule.Execute(IExpression expression, RuleContext context)
        => Execute((TExpression)expression, context);

    public Result Execute(TExpression expression, RuleContext context)
    {
        var result = ExecuteInternal(expression, context);
        if (result.IsHandled() || result.IsContinue() || result.IsReAnalyze())
            context.AddStep(this, expression, result.Value);

        return result;
    }

    protected abstract Result ExecuteInternal(TExpression expression, RuleContext context);

    protected static Result Handled(IExpression value)
        => Result.Handled(value);

    protected static Result Continue(IExpression value)
        => Result.Continue(value);

    protected static Result ReAnalyze(IExpression value)
        => Result.ReAnalyze(value);

    protected static Result NotHandled()
        => Result.NotHandled();

    public virtual string Name => GetType().Name;
}