using xFunc.Maths.Analyzers2.Rules;
using xFunc.Maths.Analyzers2.Rules.AbsRules;
using xFunc.Maths.Analyzers2.Rules.AddRules;
using xFunc.Maths.Analyzers2.Rules.DivRules;
using xFunc.Maths.Analyzers2.Rules.SubRules;

namespace xFunc.Maths.Analyzers2;

public class Simplifier2 : IAnalyzer2
{
    private readonly RuleStorage rules;

    public Simplifier2()
    {
        rules = new RuleStorageBuilder()
            .WithChain<Abs>(builder => builder
                .WithRule(new AbsUnaryRule())
                .WithNext(new AbsUnaryMinusRule())
                .WithNext(new AbsNestedRule()))
            .WithChain<Add>(builder => builder
                .WithRule(new AddBinaryRule())
                .WithNext(new AddOrderingRule())
                .WithNext(new AddZeroRule())
                .WithNext(new AddConstFoldingRule())
                .WithNext(new AddTwoVariablesRule())
                .WithNext(new AddUnaryMinusRule())
                .WithNext(new AddGroupRule()))
            .WithRule(new CeilUnaryRule())
            // .WithRule<Define>()
            // .WithRule<Del>()
            .WithChain<Div>(builder => builder
                .WithRule(new DivBinaryRule())
                .WithNext(new DivZeroRule())
                .WithNext(new DivByOneRule())
                .WithNext(new DivConstFoldingRule())
                .WithNext(new DivBySameExpression())
                .WithNext(new DivGroupRule()))
            .WithChain<Exp>(builder => builder
                .WithRule(new ExpUnaryRule())
                .WithNext(new ExpLnUnaryRule()))
            .WithRule(new FactUnaryRule())
            .WithRule(new FloorUnaryRule())
            .WithRule(new TruncUnaryRule())
            .WithRule(new FracUnaryRule())
            // .WithRule<GCD>()
            // .WithRule<LCM>()
            .WithChain<Lb>(builder => builder
                .WithRule(new LbUnaryRule())
                .WithNext(new LbWithNumberTwoRule()))
            .WithChain<Lg>(builder => builder
                .WithRule(new LgUnaryRule())
                .WithNext(new LgWithNumberTwoRule()))
            .WithChain<Ln>(builder => builder
                .WithRule(new LnUnaryRule())
                .WithNext(new LnWithNumberTwoRule()))
            .WithChain<Sub>(builder => builder
                .WithRule(new SubBinaryRule())
                .WithNext(new SubZeroRule())
                .WithNext(new SubConstFoldingRule())
                .WithNext(new SubTwoVariablesRule())
                .WithNext(new SubUnaryMinusRule())
                .WithNext(new SubGroupRule()))
            .Build();
    }

    public IExpression Analyze(IExpression expression)
    {
        var context = new RuleContext(this);
        var result = Analyze(expression, context);

        return result;
    }

    public IExpression Analyze(IExpression expression, RuleContext context)
    {
        var rule = rules.GetRule(expression.GetType());
        if (rule is null)
            return expression;

        var result = rule.Execute(expression, context);

        if (result.IsReAnalyze())
            return Analyze(result.Value, context);

        if (result.IsHandled() || result.IsContinue())
            return result.Value;

        return expression;
    }
}