### Common

| Function (Operation)  | Class                                                          | Alternative input                       | Remarks                                                         |
| --------------------- | -------------------------------------------------------------- | --------------------------------------- | --------------------------------------------------------------- |
| abs(x)                | [Abs.cs](../api/xFunc.Maths.Expressions.Abs.yml)               |                                         |                                                                 |
| x + y                 | [Add.cs](../api/xFunc.Maths.Expressions.Add.yml)               | add(x, y)                               |                                                                 |
| ceil(x)               | [Ceil.cs](../api/xFunc.Maths.Expressions.Ceil.yml)             |                                         |                                                                 |
| name := value         | [Assign.cs](../api/xFunc.Maths.Expressions.Assign.yml)         | f := (x) => sin(x), assign(name, value) |                                                                 |
| deriv(f*, var, point) | [Derivate.cs](../api/xFunc.Maths.Expressions.Derivative.yml)   | derivative(f*, var, point)              | * - is required parameter. Defaults: var = "x", point = null.   |
| x / y                 | [Div.cs](../api/xFunc.Maths.Expressions.Div.yml)               | div(x, y)                               |                                                                 |
| exp(x)                | [Exp.cs](../api/xFunc.Maths.Expressions.Exp.yml)               | e^x                                     |                                                                 |
| x!                    | [Fact.cs](../api/xFunc.Maths.Expressions.Frac.yml)             | fact(x)                                 |                                                                 |
| floor(x)              | [Floor.cs](../api/xFunc.Maths.Expressions.Floor.yml)           |                                         |                                                                 |
| frac(x)               | [Frac.cs](../api/xFunc.Maths.Expressions.Frac.yml)             |                                         |                                                                 |
| gcd(x, y)             | [GCD.cs](../api/xFunc.Maths.Expressions.GCD.yml)               | gcf(x, y); hcf(x, y)                    | Also you can use more than 2 parameters. Example: gcd(4, 8, 16) |
| lb(x)                 | [Lb.cs](../api/xFunc.Maths.Expressions.Lb.yml)                 | log2(x)                                 |                                                                 |
| lcm(x, y)             | [LCM.cs](../api/xFunc.Maths.Expressions.LCM.yml)               | scm(x, y)                               | Also you can use more than 2 parameters. Example: lcm(4, 8, 16) |
| lg(x)                 | [Lg.cs](../api/xFunc.Maths.Expressions.Lg.yml)                 |                                         |                                                                 |
| ln(x)                 | [Ln.cs](../api/xFunc.Maths.Expressions.Ln.yml)                 |                                         |                                                                 |
| log(base, x)          | [Log.cs](../api/xFunc.Maths.Expressions.Log.yml)               |                                         |                                                                 |
| x % y                 | [Mod.cs](../api/xFunc.Maths.Expressions.Mod.yml)               | mod(x, y)                               |                                                                 |
| x * y                 | [Mul.cs](../api/xFunc.Maths.Expressions.Mul.yml)               | mul(x, y)                               |                                                                 |
| x ^ y                 | [Pow.cs](../api/xFunc.Maths.Expressions.Pow.yml)               | pow(x, y)                               |                                                                 |
| root(x)               | [Root.cs](../api/xFunc.Maths.Expressions.Root.yml)             |                                         |                                                                 |
| round(x, y)           | [Round.cs](../api/xFunc.Maths.Expressions.Round.yml)           |                                         | y - is optional                                                 |
| simplify(exp)         | [Simplify.cs](../api/xFunc.Maths.Expressions.Simplify.yml)     |                                         |                                                                 |
| sqrt(x)               | [Sqrt.cs](../api/xFunc.Maths.Expressions.Sqrt.yml)             |                                         |                                                                 |
| trunc(x)              | [Trunc.cs](../api/xFunc.Maths.Expressions.Trunc.yml)           | truncate(x)                             |                                                                 |
| x - y                 | [Sub.cs](../api/xFunc.Maths.Expressions.Sub.yml)               | sub(x, y)                               |                                                                 |
| -x                    | [UnaryMinus.cs](../api/xFunc.Maths.Expressions.UnaryMinus.yml) |                                         |                                                                 |
| unassign(name)        | [Unassign.cs](../api/xFunc.Maths.Expressions.Unassign.yml)     |                                         |                                                                 |
| sign(x)               | [Sign.cs](../api/xFunc.Maths.Expressions.Sign.yml)             |                                         |                                                                 |
| tobin(x)              | [ToBin.cs](../api/xFunc.Maths.Expressions.ToBin.yml)           |                                         |                                                                 |
| tooct(x)              | [ToOct.cs](../api/xFunc.Maths.Expressions.ToOct.yml)           |                                         |                                                                 |
| tohex(x)              | [ToHex.cs](../api/xFunc.Maths.Expressions.ToHex.yml)           |                                         |                                                                 |
| tonumber(x)           | [ToNumber.cs](../api/xFunc.Maths.Expressions.ToNumber.yml)     |                                         | converts any unit to a number                                   |
| a // b                | [Rational.cs](../api/xFunc.Maths.Expressions.ToRational.yml)   |                                         |                                                                 |

### Angle

| Function  | Class                                                                           | Alternative input         |
| --------- | ------------------------------------------------------------------------------- | ------------------------- |
| x 'rad'   | [AngleValue.cs](../api/xFunc.Maths.Expressions.Units.AngleUnits.AngleValue.yml) | x 'radian', x 'radians'   |
| x 'deg'   | [AngleValue.cs](../api/xFunc.Maths.Expressions.Units.AngleUnits.AngleValue.yml) | x 'degree', x 'degrees'   |
| x 'grad'  | [AngleValue.cs](../api/xFunc.Maths.Expressions.Units.AngleUnits.AngleValue.yml) | x 'gradian', x 'gradians' |
| todeg(x)  | [ToDegree.cs](../api/xFunc.Maths.Expressions.Units.AngleUnits.ToDegree.yml)     | todegree(x)               |
| torad(x)  | [ToRadian.cs](../api/xFunc.Maths.Expressions.Units.AngleUnits.ToRadian.yml)     | toradian(x)               |
| tograd(x) | [ToGradian.cs](../api/xFunc.Maths.Expressions.Units.AngleUnits.ToGradian.yml)   | togradian(x)              |

### Trigonometric and inverse

| Function  | Class                                                                | Alternative input |
| --------- | -------------------------------------------------------------------- | ----------------- |
| sin(x)    | [Sin.cs](../api/xFunc.Maths.Expressions.Trigonometric.Sin.yml)       |                   |
| cos(x)    | [Cos.cs](../api/xFunc.Maths.Expressions.Trigonometric.Cos.yml)       |                   |
| tan(x)    | [Tan.cs](../api/xFunc.Maths.Expressions.Trigonometric.Tan.yml)       | tg(x)             |
| cot(x)    | [Cot.cs](../api/xFunc.Maths.Expressions.Trigonometric.Cot.yml)       | ctg(x)            |
| sec(x)    | [Sec.cs](../api/xFunc.Maths.Expressions.Trigonometric.Sec.yml)       |                   |
| csc(x)    | [Csc.cs](../api/xFunc.Maths.Expressions.Trigonometric.Csc.yml)       | cosec(x)          |
| arcsin(x) | [Arcsin.cs](../api/xFunc.Maths.Expressions.Trigonometric.Arcsin.yml) |                   |
| arccos(x) | [Arccos.cs](../api/xFunc.Maths.Expressions.Trigonometric.Arccos.yml) |                   |
| arctan(x) | [Arctan.cs](../api/xFunc.Maths.Expressions.Trigonometric.Arctan.yml) | arctg(x)          |
| arccot(x) | [Arccot.cs](../api/xFunc.Maths.Expressions.Trigonometric.Arccot.yml) | arcctg(x)         |
| arcsec(x) | [Arcsec.cs](../api/xFunc.Maths.Expressions.Trigonometric.Arcsec.yml) |                   |
| arccsc(x) | [Arccsc.cs](../api/xFunc.Maths.Expressions.Trigonometric.Arccsc.yml) | arccosec(x)       |

### Hyperbolic and inverse

| Function  | Class                                                             | Alternative input |
| --------- | ----------------------------------------------------------------- | ----------------- |
| sinh(x)   | [Sinh.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Sinh.yml)     | sh(x)             |
| cosh(x)   | [Cosh.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Cosh.yml)     | ch(x)             |
| tanh(x)   | [Tanh.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Tanh.yml)     | th(x)             |
| coth(x)   | [Coth.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Coth.yml)     | cth(x)            |
| sech(x)   | [Sech.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Sech.yml)     |                   |
| csch(x)   | [Csch.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Csch.yml)     |                   |
| arsinh(x) | [Arsinh.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Arsinh.yml) | arsh(x)           |
| arcosh(x) | [Arcosh.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Arcosh.yml) | arch(x)           |
| artanh(x) | [Artanh.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Artanh.yml) | arth(x)           |
| arcoth(x) | [Arcoth.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Arcoth.yml) | arcth(x)          |
| arsech(x) | [Arsech.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Arsech.yml) |                   |
| arcsch(x) | [Arcsch.cs](../api/xFunc.Maths.Expressions.Hyperbolic.Arcsch.yml) |                   |

### Complex Numbers

| Function (Operation) | Class                                                                               | Alternative input |
| -------------------- | ----------------------------------------------------------------------------------- | ----------------- |
| a + bi               | [ComplexNumber.cs](../api/xFunc.Maths.Expressions.ComplexNumbers.ComplexNumber.yml) | ∠a + b°           |
| conjugate(x)         | [Conjugate.cs](../api/xFunc.Maths.Expressions.ComplexNumbers.Conjugate.yml)         |                   |
| im(x)                | [Im.cs](../api/xFunc.Maths.Expressions.ComplexNumbers.Im.yml)                       | imaginary(x)      |
| phase(x)             | [Phase.cs](../api/xFunc.Maths.Expressions.ComplexNumbers.Phase.yml)                 |                   |
| re(x)                | [Re.cs](../api/xFunc.Maths.Expressions.ComplexNumbers.Re.yml)                       | real(x)           |
| reciprocal(x)        | [Reciprocal.cs](../api/xFunc.Maths.Expressions.ComplexNumbers.Reciprocal.yml)       |                   |
| tocomplex(x)         | [ToComplex.cs](../api/xFunc.Maths.Expressions.ComplexNumbers.ToComplex.yml)         |                   |

### Matrix

| Function                 | Class                                                                     | Alternative input |
| ------------------------ | ------------------------------------------------------------------------- | ----------------- |
| {exp, exp}               | [Vector.cs](../api/xFunc.Maths.Expressions.Matrices.Vector.yml)           |                   |
| {{exp, exp}, {exp, exp}} | [Matrix.cs](../api/xFunc.Maths.Expressions.Matrices.Matrix.yml)           |                   |
| determinant(matrix)      | [Determinant.cs](../api/xFunc.Maths.Expressions.Matrices.Determinant.yml) | det(matrix)       |
| transpose(matrix/vector) | [Transpose.cs](../api/xFunc.Maths.Expressions.Matrices.Transpose.yml)     |                   |
| inverse(matrix)          | [Inverse.cs](../api/xFunc.Maths.Expressions.Matrices.Inverse.yml)         |                   |

### Statistical

| Function (Operation) | Class                                                                | Alternative input |
| -------------------- | -------------------------------------------------------------------- | ----------------- |
| avg(params...)       | [Avg.cs](../api/xFunc.Maths.Expressions.Statistical.Avg.yml)         | avg({vector})     |
| min(params...)       | [Min.cs](../api/xFunc.Maths.Expressions.Statistical.Min.yml)         | min({vector})     |
| max(params...)       | [Max.cs](../api/xFunc.Maths.Expressions.Statistical.Max.yml)         | max({vector})     |
| product(params...)   | [Product.cs](../api/xFunc.Maths.Expressions.Statistical.Product.yml) | product({vector}) |
| stdev(params...)     | [Stdev.cs](../api/xFunc.Maths.Expressions.Statistical.Stdev.yml)     | stdev({vector})   |
| stdevp(params...)    | [Stdevp.cs](../api/xFunc.Maths.Expressions.Statistical.Stdevp.yml)   | stdevp({vector})  |
| sum(params...)       | [Sum.cs](../api/xFunc.Maths.Expressions.Statistical.Sum.yml)         | sum({vector})     |
| var(params...)       | [Var.cs](../api/xFunc.Maths.Expressions.Statistical.Var.yml)         | var({vector})     |
| varp(params...)      | [Varp.cs](../api/xFunc.Maths.Expressions.Statistical.Varp.yml)       | varp({vector})    |

### Bitwise And Logical

| Function (Operation) | Class                                                                              | Alternative input |
| -------------------- | ---------------------------------------------------------------------------------- | ----------------- |
| not(x)               | [Not.cs](../api/xFunc.Maths.Expressions.LogicalAndBitwise.Not.yml)                 | ~x                |
| x and y              | [And.cs](../api/xFunc.Maths.Expressions.LogicalAndBitwise.And.yml)                 | x & y             |
| x or y               | [Or.cs](../api/xFunc.Maths.Expressions.LogicalAndBitwise.Or.yml)                   | x &#124; y        |
| x xor y              | [XOr.cs](../api/xFunc.Maths.Expressions.LogicalAndBitwise.XOr.yml)                 |                   |
| x eq y               | [Equality.cs](../api/xFunc.Maths.Expressions.LogicalAndBitwise.Equality.yml)       |                   |
| x impl y             | [Implication.cs](../api/xFunc.Maths.Expressions.LogicalAndBitwise.Implication.yml) |                   |
| x nand y             | [NAnd.cs](../api/xFunc.Maths.Expressions.LogicalAndBitwise.NAnd.yml)               |                   |
| x nor y              | [NOr.cs](../api/xFunc.Maths.Expressions.LogicalAndBitwise.NOr.yml)                 |                   |

### Programming

| Function (Operation)        | Class                                                                                  | Alternative input |
| --------------------------- | -------------------------------------------------------------------------------------- | ----------------- |
| +=                          | [AddAssign.cs](../api/xFunc.Maths.Expressions.Programming.AddAssign.yml)               |                   |
| &amp;&amp;                  | [ConditionalAnd.cs](../api/xFunc.Maths.Expressions.Programming.ConditionalAnd.yml)     |                   |
| --                          | [Dec.cs](../api/xFunc.Maths.Expressions.Programming.Dec.yml)                           |                   |
| /=                          | [DivAssign.cs](../api/xFunc.Maths.Expressions.Programming.DivAssign.yml)               |                   |
| ==                          | [Equal.cs](../api/xFunc.Maths.Expressions.Programming.Equal.yml)                       |                   |
| for(body, init, cond, iter) | [For.cs](../api/xFunc.Maths.Expressions.Programming.For.yml)                           |                   |
| &gt;=                       | [GreaterOrEqual.cs](../api/xFunc.Maths.Expressions.Programming.GreaterOrEqual.yml)     |                   |
| &gt;                        | [GreaterThan.cs](../api/xFunc.Maths.Expressions.Programming.GreaterThan.yml)           |                   |
| if(cond, than, else)        | [If.cs](../api/xFunc.Maths.Expressions.Programming.If.yml)                             | if(cond, than)    |
| ++                          | [Inc.cs](../api/xFunc.Maths.Expressions.Programming.Inc.yml)                           |                   |
| &lt;=                       | [LessOrEqual.cs](../api/xFunc.Maths.Expressions.Programming.LessOrEqual.yml)           |                   |
| &lt;                        | [LessThan.cs](../api/xFunc.Maths.Expressions.Programming.LessThan.yml)                 |                   |
| *=                          | [MulAssign.cs](../api/xFunc.Maths.Expressions.Programming.MulAssign.yml)               |                   |
| !=                          | [NotEqual.cs](../api/xFunc.Maths.Expressions.Programming.NotEqual.yml)                 |                   |
| &#124;&#124;                | [ConditionalOr.cs](../api/xFunc.Maths.Expressions.Programming.ConditionalOr.yml)       |                   |
| -=                          | [SubAssign.cs](../api/xFunc.Maths.Expressions.Programming.SubAssign.yml)               |                   |
| while(body, cond)           | [While.cs](../api/xFunc.Maths.Expressions.Programming.While.yml)                       |                   |
| x << y                      | [LeftShift.cs](../api/xFunc.Maths.Expressions.Programming.LeftShift.yml)               |                   |
| x >> y                      | [RightShift.cs](../api/xFunc.Maths.Expressions.Programming.RightShift.yml)             |                   |
| x <<= y                     | [LeftShiftAssign.cs](../api/xFunc.Maths.Expressions.Programming.LeftShiftAssign.yml)   |                   |
| x >>= y                     | [RightShiftAssign.cs](../api/xFunc.Maths.Expressions.Programming.RightShiftAssign.yml) |                   |

### Units

| Name        | Class                                                                           | Example                                                                                              |
| ----------- | ------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------- |
| length      | [LengthUnits/*](../api/xFunc.Maths.Expressions.Units.LengthUnits.yml)           | `nm`, `µm`, `mm`, `cm`, `m`, `km`, `in`, `ft`, `yd`, `mi`, `nmi`, `chains`, `rods`, `au`, `ly`, `pc` |
| area        | [AreaUnits/*](../api/xFunc.Maths.Expressions.Units.AreaUnits.yml)               | `1 mm^2`, `1 cm^2`, `1 m^2`, `1 km^2`, `1 ha`, `1 in^2`, `1 ft^2`, `1 yd^2`, `1 ac^2`, `1 mi^2`      |
| volume      | [VolumeUnits/*](../api/xFunc.Maths.Expressions.Units.VolumeUnits.yml)           | `cm³`, `m³`, `l`, `in³`, `gal`, `ft³`, `yd³`                                                         |
| mass        | [MassUnits/*](../api/xFunc.Maths.Expressions.Units.MassUnits.yml)               | `mg`, `g`, `kg`, `lb`, `t`, `oz`                                                                     |
| power       | [PowerUnits/*](../api/xFunc.Maths.Expressions.Units.PowerUnits.yml)             | `W`, `kW`, `hp`                                                                                      |
| time        | [TimeUnits/*](../api/xFunc.Maths.Expressions.Units.TimeUnits.yml)               | `μs`, `ms`, `s`, `min`, `hr`, `days`, `weeks`, `years`, `ns`                                         |
| temperature | [TemperatureUnits/*](../api/xFunc.Maths.Expressions.Units.TemperatureUnits.yml) | `°C`, `°F`, `K`                                                                                      |
| convert     | [Convert.cs](../api/xFunc.Maths.Expressions.Units.Convert.yml)                  | `convert(10km, 'm')`                                                                                 |

### Constants

| Name | Value                | Alternative name |
| ---- | -------------------- | ---------------- |
| pi   | Math.Pi              | π                |
| e    | Math.E               | exp(x)           |
| i    | Complex.ImaginaryOne |                  |