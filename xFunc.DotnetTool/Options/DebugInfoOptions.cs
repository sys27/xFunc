// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.DotnetTool.Options;

public abstract class DebugInfoOptions
{
    [Option('d', "debug", Default = false, Required = false, HelpText = "Show stack trace.")]
    public bool Debug { get; set; }
}