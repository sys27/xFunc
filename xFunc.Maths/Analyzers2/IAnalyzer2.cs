namespace xFunc.Maths.Analyzers2;

public interface IAnalyzer2
{
    IExpression Analyze(IExpression expression);

    IExpression Analyze(IExpression expression, RuleContext context);
}