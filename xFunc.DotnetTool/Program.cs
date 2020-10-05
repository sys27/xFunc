// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using CommandLine;
using System;
using System.Collections.Generic;
using xFunc.DotnetTool.Options;
using xFunc.Maths;
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.DotnetTool
{
    public class Program
    {
        private static readonly Processor processor = new Processor();

        private static void Parse(ParseOptions options)
        {
            Run(options, () =>
            {
                var exp = processor.Parse(options.StringExpression);
                Console.WriteLine(exp);
            });
        }

        private static void Solve(SolveOptions options)
        {
            Run(options, () =>
            {
                var result = processor.Solve(options.StringExpression);
                Console.WriteLine(result);
            });
        }

        private static void Interactive(InteractiveOptions options)
        {
            while (true)
            {
                Console.Write("> ");

                var stringExpression = Console.ReadLine()?.ToLower();
                if (stringExpression is null ||
                    stringExpression == "#quit" ||
                    stringExpression == "#exit")
                {
                    break;
                }

                Run(options, () =>
                {
                    var result = processor.Solve(stringExpression);
                    Console.WriteLine($"> {result}");
                });
            }
        }

        private static void Run(DebugInfoOptions options, Action action)
        {
            try
            {
                action();
            }
            catch (TokenizeException mle)
            {
                PrintError(options, mle);
            }
            catch (ParseException mpe)
            {
                PrintError(options, mpe);
            }
            catch (ParameterIsReadOnlyException mpiroe)
            {
                PrintError(options, mpiroe);
            }
            catch (BinaryParameterTypeMismatchException bptme)
            {
                PrintError(options, bptme);
            }
            catch (DifferentParameterTypeMismatchException dptme)
            {
                PrintError(options, dptme);
            }
            catch (ParameterTypeMismatchException ptme)
            {
                PrintError(options, ptme);
            }
            catch (ResultIsNotSupportedException rinse)
            {
                PrintError(options, rinse);
            }
            catch (DivideByZeroException dbze)
            {
                PrintError(options, dbze);
            }
            catch (ArgumentNullException ane)
            {
                PrintError(options, ane);
            }
            catch (ArgumentException ae)
            {
                PrintError(options, ae);
            }
            catch (FormatException fe)
            {
                PrintError(options, fe);
            }
            catch (OverflowException oe)
            {
                PrintError(options, oe);
            }
            catch (KeyNotFoundException knfe)
            {
                PrintError(options, knfe);
            }
            catch (IndexOutOfRangeException ioofe)
            {
                PrintError(options, ioofe);
            }
            catch (InvalidOperationException ioe)
            {
                PrintError(options, ioe);
            }
            catch (NotSupportedException nse)
            {
                PrintError(options, nse);
            }
        }

        private static void PrintError(DebugInfoOptions options, Exception e)
        {
            if (options.Debug)
                Console.WriteLine($"> {e}");
            else
                Console.WriteLine($"> {e.Message}");
        }

        public static void Main(string[] args)
        {
            CommandLine.Parser.Default
                .ParseArguments<ParseOptions, SolveOptions, InteractiveOptions>(args)
                .WithParsed<ParseOptions>(Parse)
                .WithParsed<SolveOptions>(Solve)
                .WithParsed<InteractiveOptions>(Interactive);
        }
    }
}