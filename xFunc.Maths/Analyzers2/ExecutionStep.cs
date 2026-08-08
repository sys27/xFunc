namespace xFunc.Maths.Analyzers2;

public readonly struct ExecutionStep
{
    public ExecutionStep(IRule rule, IExpression before, IExpression after)
    {
        Name = rule.Name;
        Before = before;
        After = after;
    }

    public string Name { get; }

    public IExpression Before { get; }

    public IExpression After { get; }
}