using CommandLine;

namespace xFunc.DotnetTool.Options
{
    public abstract class DebugInfoOptions
    {
        protected DebugInfoOptions(bool debug)
        {
            Debug = debug;
        }

        [Option('d', "debug", Required = false, HelpText = "Show stack trace.")]
        public bool Debug { get; }
    }
}
