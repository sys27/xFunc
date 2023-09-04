### Common

| Function (Operation)  | Class                                                                                             | Alternative input                       | Remarks                                                         |
|-----------------------|---------------------------------------------------------------------------------------------------|-----------------------------------------|-----------------------------------------------------------------|
| abs(x)                | [Abs.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Abs.cs)                  |                                         |                                                                 |
| x + y                 | [Add.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Add.cs)                  | add(x, y)                               |                                                                 |
| ceil(x)               | [Ceil.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Ceil.cs)                |                                         |                                                                 |
| name := value         | [Assign.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Assign.cs)            | f := (x) => sin(x), assign(name, value) |                                                                 |
| deriv(f*, var, point) | [Derivate.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Derivate.cs)        | derivative(f*, var, point)              | * - is required parameter. Defaults: var = "x", point = null.   |
| x / y                 | [Div.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Div.cs)                  | div(x, y)                               |                                                                 |
| exp(x)                | [Exp.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Exp.cs)                  | e^x                                     |                                                                 |
| x!                    | [Fact.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Fact.cs)                | fact(x)                                 |                                                                 |
| floor(x)              | [Floor.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Floor.cs)              |                                         |                                                                 |
| frac(x)               | [Frac.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Frac.cs)                |                                         |                                                                 |
| gcd(x, y)             | [GCD.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/GCD.cs)                  | gcf(x, y); hcf(x, y)                    | Also you can use more than 2 parameters. Example: gcd(4, 8, 16) |
| lb(x)                 | [Lb.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Lb.cs)                    | log2(x)                                 |                                                                 |
| lcm(x, y)             | [LCM.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/LCM.cs)                  | scm(x, y)                               | Also you can use more than 2 parameters. Example: lcm(4, 8, 16) |
| lg(x)                 | [Lg.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Lg.cs)                    |                                         |                                                                 |
| ln(x)                 | [Ln.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Ln.cs)                    |                                         |                                                                 |
| log(base, x)          | [Log.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Log.cs)                  |                                         |                                                                 |
| x % y                 | [Mod.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Mod.cs)                  | mod(x, y)                               |                                                                 |
| x * y                 | [Mul.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Mul.cs)                  | mul(x, y)                               |                                                                 |
| x ^ y                 | [Pow.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Pow.cs)                  | pow(x, y)                               |                                                                 |
| root(x)               | [Root.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Root.cs)                |                                         |                                                                 |
| round(x, y)           | [Round.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Round.cs)              |                                         | y - is optional                                                 |
| simplify(exp)         | [Simplify.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Simplify.cs)        |                                         |                                                                 |
| sqrt(x)               | [Sqrt.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Sqrt.cs)                |                                         |                                                                 |
| trunc(x)              | [Trunc.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trunc.cs)              | truncate(x)                             |                                                                 |
| x - y                 | [Sub.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Sub.cs)                  | sub(x, y)                               |                                                                 |
| -x                    | [UnaryMinus.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/UnaryMinus.cs)    |                                         |                                                                 |
| unassign(name)        | [Unassign.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Unassign.cs)        |                                         |                                                                 |
| sign(x)               | [Sign.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Sign.cs)                |                                         |                                                                 |
| tobin(x)              | [ToBin.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/ToBin.cs)              |                                         |                                                                 |
| tooct(x)              | [ToOct.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/ToOct.cs)              |                                         |                                                                 |
| tohex(x)              | [ToHex.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/ToHex.cs)              |                                         |                                                                 |
| convert(x, 'unit')    | [Convert.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Units/Convert.cs)    |                                         |                                                                 |
| tonumber(x)           | [ToNumber.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Angles/ToNumber.cs) |                                         | converts any unit to a number                                   |
| a // b                | [Rational.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Rational.cs)        |                                         |                                                                 |

### Angle

| Function  | Class                                                                                                   | Alternative input         |
|-----------|---------------------------------------------------------------------------------------------------------|---------------------------|
| x 'rad'   | [AngleNumber.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Angles/AngleNumber.cs) | x 'radian', x 'radians'   |
| x 'deg'   | [AngleNumber.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Angles/AngleNumber.cs) | x 'degree', x 'degrees'   |
| x 'grad'  | [AngleNumber.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Angles/AngleNumber.cs) | x 'gradian', x 'gradians' |
| todeg(x)  | [ToDegree.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Angles/ToDegree.cs)       | todegree(x)               |
| torad(x)  | [ToRadian.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Angles/ToRadian.cs)       | toradian(x)               |
| tograd(x) | [ToGradian.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Angles/ToGradian.cs)     | togradian(x)              |

### Trigonometric and inverse

| Function  | Class                                                                                                | Alternative input |
|-----------|------------------------------------------------------------------------------------------------------|-------------------|
| sin(x)    | [Sin.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Sin.cs)       |                   |
| cos(x)    | [Cos.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Cos.cs)       |                   |
| tan(x)    | [Tan.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Tan.cs)       | tg(x)             |
| cot(x)    | [Cot.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Cot.cs)       | ctg(x)            |
| sec(x)    | [Sec.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Sec.cs)       |                   |
| csc(x)    | [Csc.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Csc.cs)       | cosec(x)          |
| arcsin(x) | [Arcsin.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Arcsin.cs) |                   |
| arccos(x) | [Arccos.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Arccos.cs) |                   |
| arctan(x) | [Arctan.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Arctan.cs) | arctg(x)          |
| arccot(x) | [Arccot.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Arccot.cs) | arcctg(x)         |
| arcsec(x) | [Arcsec.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Arcsec.cs) |                   |
| arccsc(x) | [Arccsc.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Trigonometric/Arccsc.cs) | arccosec(x)       |

### Hyperbolic and inverse

| Function  | Class                                                                                              | Alternative input |
|-----------|----------------------------------------------------------------------------------------------------|-------------------|
| sinh(x)   | [Sinh.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Sinh.cs)      | sh(x)             |
| cosh(x)   | [Cosh.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Cosh.cs)      | ch(x)             |
| tanh(x)   | [Tanh.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Tanh.cs)      | th(x)             |
| coth(x)   | [Coth.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Coth.cs)      | cth(x)            |
| sech(x)   | [Sech.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Sech.cs)      |                   |
| csch(x)   | [Csch.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Csch.cs)      |                   |
| arsinh(x) | [Arcsinh.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Arsinh.cs) | arsh(x)           |
| arcosh(x) | [Arccosh.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Arcosh.cs) | arch(x)           |
| artanh(x) | [Arctanh.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Artanh.cs) | arth(x)           |
| arcoth(x) | [Arccoth.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Arcoth.cs) | arcth(x)          |
| arsech(x) | [Arcsech.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Arsech.cs) |                   |
| arcsch(x) | [Arccsch.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Hyperbolic/Arcsch.cs) |                   |

### Complex Numbers

| Function (Operation) | Class                                                                                                         | Alternative input |
|----------------------|---------------------------------------------------------------------------------------------------------------|-------------------|
| a + bi               | [ComplexNumber.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/ComplexNumbers/ComplexNumber.cs)       | ∠a + b°           |
| conjugate(x)         | [Conjugate.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/ComplexNumbers/Conjugate.cs)   |                   |
| im(x)                | [Im.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/ComplexNumbers/Im.cs)                 | imaginary(x)      |
| phase(x)             | [Phase.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/ComplexNumbers/Phase.cs)           |                   |
| re(x)                | [Re.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/ComplexNumbers/Re.cs)                 | real(x)           |
| reciprocal(x)        | [Reciprocal.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/ComplexNumbers/Reciprocal.cs) |                   |
| tocomplex(x)         | [ToComplex.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/ComplexNumbers/ToComplex.cs)   |                   |

### Matrix

| Function                 | Class                                                                                                     | Alternative input |
|--------------------------|-----------------------------------------------------------------------------------------------------------|-------------------|
| {exp, exp}               | [Vector.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Matrices/Vector.cs)           |                   |
| {{exp, exp}, {exp, exp}} | [Matrix.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Matrices/Matrix.cs)           |                   |
| determinant(matrix)      | [Determinant.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Matrices/Determinant.cs) | det(matrix)       |
| transpose(matrix/vector) | [Transpose.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Matrices/Transpose.cs)     |                   |
| inverse(matrix)          | [Inverse.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Matrices/Inverse.cs)         |                   |

### Statistical

| Function (Operation) | Class                                                                                                | Alternative input |
|----------------------|------------------------------------------------------------------------------------------------------|-------------------|
| avg(params...)       | [Avg.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Statistical/Avg.cs)         | avg({vector})     |
| min(params...)       | [Min.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Statistical/Min.cs)         | min({vector})     |
| max(params...)       | [Max.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Statistical/Max.cs)         | max({vector})     |
| product(params...)   | [Product.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Statistical/Product.cs) | product({vector}) |
| stdev(params...)     | [Stdev.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Statistical/Stdev.cs)     | stdev({vector})   |
| stdevp(params...)    | [Stdevp.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Statistical/Stdevp.cs)   | stdevp({vector})  |
| sum(params...)       | [Sum.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Statistical/Sum.cs)         | sum({vector})     |
| var(params...)       | [Var.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Statistical/Var.cs)         | var({vector})     |
| varp(params...)      | [Varp.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Statistical/Varp.cs)       | varp({vector})    |

### Bitwise And Logical

| Function (Operation) | Class                                                                                             | Alternative input |
|----------------------|---------------------------------------------------------------------------------------------------|-------------------|
| not(x)               | [Not.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Bitwise/Not.cs)          | ~x                |
| x and y              | [And.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Bitwise/And.cs)          | x & y             |
| x or y               | [Or.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Bitwise/Or.cs)            | x &#124; y        |
| x xor y              | [XOr.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Bitwise/XOr.cs)          |                   |
| x eq y               | [Equality.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Logics/Expressions/Equality.cs)       |                   |
| x impl y             | [Implication.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Logics/Expressions/Implication.cs) |                   |
| x nand y             | [NAnd.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Logics/Expressions/NAnd.cs)               |                   |
| x nor y              | [NOr.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Logics/Expressions/NOr.cs)                 |                   |

### Programming

| Function (Operation)        | Class                                                                                                                     | Alternative input |
|-----------------------------|---------------------------------------------------------------------------------------------------------------------------|-------------------|
| +=                          | [AddAssign.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/AddAssign.cs)               |                   |
| &amp;&amp;                  | [And.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/And.cs)                           |                   |
| --                          | [Dec.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/Dec.cs)                           |                   |
| /=                          | [DivAssign.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/DivAssign.cs)               |                   |
| ==                          | [Equal.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/Equal.cs)                       |                   |
| for(body, init, cond, iter) | [For.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/For.cs)                           |                   |
| &gt;=                       | [GreaterOrEqual.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/GreaterOrEqual.cs)     |                   |
| &gt;                        | [GreaterThan.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/GreaterThan.cs)           |                   |
| if(cond, than, else)        | [If.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/If.cs)                             | if(cond, than)    |
| ++                          | [Inc.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/Inc.cs)                           |                   |
| &lt;=                       | [LessOrEqual.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/LessOrEqual.cs)           |                   |
| &lt;                        | [LessThan.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/LessThan.cs)                 |                   |
| *=                          | [MulAssign.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/MulAssign.cs)               |                   |
| !=                          | [NotEqual.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/NotEqual.cs)                 |                   |
| &#124;&#124;                | [Or.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/Or.cs)                             |                   |
| -=                          | [SubAssign.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/SubAssign.cs)               |                   |
| while(body, cond)           | [While.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/While.cs)                       |                   |
| x << y                      | [LeftShift.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/LeftShift.cs)               |                   |
| x >> y                      | [RightShift.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/RightShift.cs)             |                   |
| x <<= y                     | [LeftShiftAssign.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/LeftShiftAssign.cs)   |                   |
| x >>= y                     | [RightShiftAssign.cs](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Programming/RightShiftAssign.cs) |                   |

### Units

| Name        | Class                                                                                                        | Example                                                                                              |
|-------------|--------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------|
| length      | [LengthUnits/*](https://github.com/sys27/xFunc/tree/dev/xFunc.Maths/Expressions/Units/LengthUnits)           | `nm`, `µm`, `mm`, `cm`, `m`, `km`, `in`, `ft`, `yd`, `mi`, `nmi`, `chains`, `rods`, `au`, `ly`, `pc` |
| area        | [AreaUnits/*](https://github.com/sys27/xFunc/tree/dev/xFunc.Maths/Expressions/Units/AreaUnits)               | `1 mm^2`, `1 cm^2`, `1 m^2`, `1 km^2`, `1 ha`, `1 in^2`, `1 ft^2`, `1 yd^2`, `1 ac^2`, `1 mi^2`      |
| volume      | [VolumeUnits/*](https://github.com/sys27/xFunc/tree/dev/xFunc.Maths/Expressions/Units/VolumeUnits)           | `cm³`, `m³`, `l`, `in³`, `gal`, `ft³`, `yd³`                                                         |
| mass        | [MassUnits/*](https://github.com/sys27/xFunc/tree/dev/xFunc.Maths/Expressions/Units/MassUnits)               | `mg`, `g`, `kg`, `lb`, `t`, `oz`                                                                     |
| power       | [PowerUnits/*](https://github.com/sys27/xFunc/tree/dev/xFunc.Maths/Expressions/Units/PowerUnits)             | `W`, `kW`, `hp`                                                                                      |
| time        | [TimeUnits/*](https://github.com/sys27/xFunc/tree/dev/xFunc.Maths/Expressions/Units/TimeUnits)               | `μs`, `ms`, `s`, `min`, `hr`, `days`, `weeks`, `years`, `ns`                                         |
| temperature | [TemperatureUnits/*](https://github.com/sys27/xFunc/tree/dev/xFunc.Maths/Expressions/Units/TemperatureUnits) | `°C`, `°F`, `K`                                                                                      |
| convert     | [Convert.cs](https://github.com/sys27/xFunc/blob/dev/xFunc.Maths/Expressions/Units/Convert.cs)               | `convert(10km, 'm')`                                                                                 |

### Constants

| Name | Value                     | Alternative name |
|------|---------------------------|------------------|
| pi   | Math.Pi                   | π                |
| e    | Math.E                    | exp(x)           |
| i    | Complex.ImaginaryOne      |                  |