// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Cli.Options;

[Verb("run", HelpText = "Run all expressions from a file.")]
public class RunFileOptions : DebugInfoOptions
{
    [Value(0, Required = true, MetaName = "File", HelpText = "Path to a file.")]
    public string File { get; set; }

    [Usage(ApplicationAlias = "xfunc")]
    public static IEnumerable<Example> Examples
        => new List<Example>
        {
            new Example("Run all expressions from a file", new RunFileOptions { File = "./file.xf" })
        };
}