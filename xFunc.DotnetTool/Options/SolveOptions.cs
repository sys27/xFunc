using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace xFunc.DotnetTool.Options
{
    [Verb("solve", HelpText = "Calculate result of expression.")]
    public class SolveOptions : BaseOptions
    {
        public SolveOptions(string stringExpression) : base(stringExpression) { }

        [Usage(ApplicationAlias = "xfunc")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>
                {
                    new Example("Calculate string expression", new SolveOptions("1 + 1"))
                };
            }
        }
    }
}
