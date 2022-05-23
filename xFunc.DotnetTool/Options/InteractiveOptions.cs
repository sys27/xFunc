// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.DotnetTool.Options;

[Verb("interactive", HelpText = "Run interactive mode.")]
public class InteractiveOptions : DebugInfoOptions
{
    [Usage(ApplicationAlias = "xfunc")]
    public static IEnumerable<Example> Examples
        => new List<Example>
        {
            new Example("Run iteractive mode", new InteractiveOptions())
        };
}