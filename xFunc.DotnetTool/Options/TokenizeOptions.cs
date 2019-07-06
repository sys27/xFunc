using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace xFunc.DotnetTool.Options
{
    [Verb("tokenize", HelpText = "Convert string expression to list of tokens.")]
    public class TokenizeOptions : BaseOptions
    {
        public TokenizeOptions(string stringExpression, bool debug) : base(stringExpression, debug) { }

        [Usage(ApplicationAlias = "xfunc")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>
                {
                    new Example("Tokenize string expression", new TokenizeOptions("1 + 1", false))
                };
            }
        }
    }
}
