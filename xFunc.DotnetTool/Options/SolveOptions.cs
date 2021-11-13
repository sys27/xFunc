// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.DotnetTool.Options;

[Verb("solve", HelpText = "Calculate result of expression.")]
public class SolveOptions : BaseOptions
{
    [Usage(ApplicationAlias = "xfunc")]
    public static IEnumerable<Example> Examples
        => new List<Example>
        {
            new Example(
                "Calculate string expression",
                new SolveOptions { StringExpression = "1 + 1" }),
        };
}