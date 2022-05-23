using xFunc.Maths;
using xFunc.Maths.Results;

var processor = new Processor();
var result = processor.Solve<NumberResult>("sin(90)");

Console.WriteLine(result.Result);