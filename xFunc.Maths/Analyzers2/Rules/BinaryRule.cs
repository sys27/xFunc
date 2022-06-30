namespace xFunc.Maths.Analyzers2.Rules;

public abstract class BinaryRule<TExpression> : Rule<TExpression>
    where TExpression : BinaryExpression
{
    protected override Result ExecuteInternal(TExpression expression, RuleContext context)
    {
        var left = context.Analyze(expression.Left);
        var right = context.Analyze(expression.Right);
        if (ReferenceEquals(left, expression.Left) && ReferenceEquals(right, expression.Right))
            return NotHandled();

        expression = Create(left, right);

        return Continue(expression);
    }

    protected abstract TExpression Create(IExpression left, IExpression right);
}