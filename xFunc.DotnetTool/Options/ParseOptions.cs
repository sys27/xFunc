using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace xFunc.DotnetTool.Options
{
    [Verb("parse", HelpText = "Parse string expression.")]
    public class ParseOptions : BaseOptions
    {
        public ParseOptions(string stringExpression, bool debug) : base(stringExpression, debug) { }

        [Usage(ApplicationAlias = "xfunc")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>
                {
                    new Example("Parse string expression", new ParseOptions("1 + 1", false))
                };
            }
        }
    }
}
