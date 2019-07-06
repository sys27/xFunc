using CommandLine;

namespace xFunc.DotnetTool.Options
{
    public abstract class BaseOptions : DebugInfoOptions
    {
        protected BaseOptions(string stringExpression, bool debug) : base(debug)
        {
            StringExpression = stringExpression;
        }

        [Value(0, Required = true, MetaName = "String Expression", HelpText = "The string expression.")]
        public string StringExpression { get; }
    }
}
