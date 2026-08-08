namespace xFunc.Maths.Analyzers2;

public interface IRule
{
    Result Execute(IExpression expression, RuleContext context);

    string Name { get; }
}