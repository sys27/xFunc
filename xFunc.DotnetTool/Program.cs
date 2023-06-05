// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.DotnetTool.Options;
using xFunc.Maths.Expressions.Parameters;

namespace xFunc.DotnetTool;

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
        catch (ValueIsNotSupportedException vinse)
        {
            PrintError(options, vinse);
        }
        catch (UnitIsNotSupportedException uinse)
        {
            PrintError(options, uinse);
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
        => CommandLine.Parser.Default
            .ParseArguments<ParseOptions, SolveOptions, InteractiveOptions>(args)
            .WithParsed<ParseOptions>(Parse)
            .WithParsed<SolveOptions>(Solve)
            .WithParsed<InteractiveOptions>(Interactive);
}