using CommandLine;

namespace xFunc.DotnetTool.Options
{
    public abstract class BaseOptions
    {
        protected BaseOptions(string stringExpression)
        {
            StringExpression = stringExpression;
        }

        [Value(0, Required = true, MetaName = "String Expression", HelpText = "The string expression.")]
        public string StringExpression { get; }
    }
}
