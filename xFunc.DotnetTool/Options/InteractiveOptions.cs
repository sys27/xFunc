using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace xFunc.DotnetTool.Options
{
    [Verb("interactive", HelpText = "Run interactive mode.")]
    public class InteractiveOptions : DebugInfoOptions
    {
        public InteractiveOptions(bool debug) : base(debug) { }

        [Usage(ApplicationAlias = "xfunc")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>
                {
                    new Example("Run iteractive mode", new InteractiveOptions(false))
                };
            }
        }
    }
}
