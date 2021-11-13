// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.DotnetTool.Options;

public abstract class BaseOptions : DebugInfoOptions
{
    [Value(0, Required = true, MetaName = "String Expression", HelpText = "The string expression.")]
    public string StringExpression { get; set; }
}