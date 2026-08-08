namespace xFunc.Maths.Analyzers2.Rules;

public abstract class UnaryRule<TExpression> : Rule<TExpression>
    where TExpression : UnaryExpression
{
    protected override Result ExecuteInternal(TExpression expression, RuleContext context)
    {
        var argument = context.Analyze(expression.Argument);
        if (Equals(argument, expression.Argument))
            return NotHandled();

        expression = Create(argument);

        return Continue(expression);
    }

    protected abstract TExpression Create(IExpression argument);
}