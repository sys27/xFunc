namespace xFunc.Maths.Analyzers2.Rules.AddRules;

public class AddOrderingRule : Rule<Add>
{
    protected override Result ExecuteInternal(Add expression, RuleContext context)
        => expression switch
        {
            // 2 + x -> x + 2
            (Number number, Variable variable)
                => ReAnalyze(new Add(variable, number)),

            // 2 + (x + 2) -> (x + 2) + 2
            (Number number, Add(Variable, Number) add)
                => ReAnalyze(new Add(add, number)),

            // x + ax -> ax + x
            (Variable variable, Mul(Number, Variable) mul)
                => ReAnalyze(new Add(mul, variable)),

            _ => NotHandled(),
        };

    public override string Name => "Add Ordering";
}