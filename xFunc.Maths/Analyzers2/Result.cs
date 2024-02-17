using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Analyzers2;

public readonly struct Result
{
    private readonly ResultKind kind;

    private Result(ResultKind kind, IExpression? value)
    {
        this.kind = kind;
        Value = value;
    }

    public static Result NotHandled()
        => new Result(ResultKind.NotHandled, default);

    public static Result Handled(IExpression value)
        => new Result(ResultKind.Handled, value);

    public static Result Continue(IExpression value)
        => new Result(ResultKind.Continue, value);

    public static Result ReAnalyze(IExpression value)
        => new Result(ResultKind.ReAnalyze, value);

    private bool IsKind(ResultKind kind)
        => this.kind == kind;

    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsNotHandled()
        => IsKind(ResultKind.NotHandled);

    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsHandled()
        => IsKind(ResultKind.Handled);

    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsContinue()
        => IsKind(ResultKind.Continue);

    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsReAnalyze()
        => IsKind(ResultKind.ReAnalyze);

    public IExpression? Value { get; }
}