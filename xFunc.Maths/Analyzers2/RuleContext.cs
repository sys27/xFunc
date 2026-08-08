namespace xFunc.Maths.Analyzers2;

public class RuleContext
{
    private readonly IAnalyzer2 analyzer;
    private readonly List<ExecutionStep> steps;

    public RuleContext(IAnalyzer2 analyzer)
    {
        this.analyzer = analyzer;
        this.steps = new List<ExecutionStep>();
    }

    public IExpression Analyze(IExpression expression)
        => analyzer.Analyze(expression, this);

    public void AddStep(IRule rule, IExpression before, IExpression after)
        => steps.Add(new ExecutionStep(rule, before, after));

    public IEnumerable<ExecutionStep> Steps => steps;
}