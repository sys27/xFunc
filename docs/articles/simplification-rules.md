Simplification Rules
===

## [Addition](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Add.cs)

 Example                 | Result        
-------------------------|---------------
 2 + x                   | x + 2         
 2 + (x + 2)             | (x + 2) + 2   
 x + ax                  | ax + x        
 x + 0                   | x             
 0 + x                   | x             
 x + y                   | result of sum 
 x + x                   | 2x            
 -y + x                  | x - y         
 x + (-y)                | x - y         
 _const_ + (_const_ + x) | x + _sum_     
 _const_ + (x + _const_) | x + _sum_     
 (_const_ + x) + _const_ | x + _sum_     
 (x + _const_) + _const_ | x + _sum_     
 _const_ + (_const_ - x) | _sum_ - x     
 _const_ + (x - _const_) | _diff_ + x    
 (_const_ - x) + _const_ | _sum_ - x     
 (x - _const_) + _const_ | _diff_ + x    
 x + xb                  | _(b + 1)_ * x 
 x + bx                  | _(b + 1)_ * x 
 ax + x                  | _(a + 1)_ * x 
 xa + x                  | _(a + 1)_ * x 
 ax + bx                 | _(a + b)_ * x 
 ax + xb                 | _(a + b)_ * x 
 xa + bx                 | _(a + b)_ * x 
 xa + xb                 | _(a + b)_ * x 

Note: _italic_ result is precalculated.

## [Subtraction](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Sub.cs)

 Example                 | Result        
-------------------------|---------------
 0 - x                   | -x            
 x - 0                   | x             
 const - const           | _diff_        
 x - x                   | 0             
 x - (-y)                | x + y         
 (_const_ + x) - _const_ | x + _diff_    
 (x + _const_) - _const_ | x + _diff_    
 _const_ - (_const_ + x) | _diff_ - x    
 _const_ - (x + _const_) | _diff_ - x    
 (_const_ - x) - _const_ | _diff_ - x    
 (x - _const_) - _const_ | x - _sum_     
 _const_ - (_const_ - x) | _diff_ - x    
 _const_ - (x - _const_) | _sum_ - x     
 x - xb                  | _(1 - b)_ * x 
 x - bx                  | _(1 - b)_ * x 
 ax - x                  | _(a - 1)_ * x 
 xa - x                  | _(a - 1)_ * x 
 ax - bx                 | _(a - b)_ * x 
 ax - xb                 | _(a - b)_ * x 
 xa - bx                 | _(a - b)_ * x 
 xa - xb                 | _(a - b)_ * x 

Note: _italic_ result is precalculated.

## [Multiplication](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Mul.cs)

 Example                 | Result            
-------------------------|-------------------
 xa                      | ax                
 x * ax                  | ax * x            
 2 * (2 * x)             | (2 * x) * 2       
 0 * x                   | 0                 
 x * 0                   | 0                 
 1 * x                   | x                 
 x * 1                   | x                 
 -1 * x                  | -x                
 x * -1                  | -x                
 _const_ * _const_       | result of mul     
 x * -y                  | -(x * y)          
 x * x                   | 2x                
 _const_ * (_const_ * x) | _product_ * x     
 _const_ * (x * _const_) | _product_ * x     
 (_const_ * x) * _const_ | _product_ * x     
 (x * _const_) * _const_ | _product_ * x     
 _const_ * (_const_ / x) | _product_ / x     
 _const_ * (x / _const_) | _factor_ * x      
 (_const_ / x) * _const_ | _product_ / x     
 (x / _const_) * _const_ | _factor_ * x      
 x * xb                  | b * x ^ 2         
 x * bx                  | b * x ^ 2         
 ax * x                  | a * x ^ 2         
 xa * x                  | a * x ^ 2         
 ax + bx                 | _(a * b)_ * x ^ 2 
 ax + xb                 | _(a * b)_ * x ^ 2 
 xa + bx                 | _(a * b)_ * x ^ 2 
 xa + xb                 | _(a * b)_ * x ^ 2 
 x * (1 / x)             | 1                 
 (2 * x) * (1 / x)       | 2                 
 (x * 2) * (1 / x)       | 2                 

Note: _italic_ result is precalculated.

## [Division](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Div.cs)

 Example                 | Result                
-------------------------|-----------------------
 0 / 0                   | NaN                   
 0 / x                   | 0                     
 x / 0                   | DivideByZeroException 
 x / 1                   | x                     
 const / const           | result of div         
 x / x                   | 1                     
 (_const_ * x) / _const_ | x / _fraction_        
 (x * _const_) / _const_ | x / _fraction_        
 _const_ / (_const_ * x) | _fraction_ / x        
 _const_ / (x * _const_) | _fraction_ / x        
 (_const_ / x) / _const_ | _fraction_ / x        
 (x / _const_) / _const_ | x / _mul_             
 _const_ / (_const_ / x) | _mul_ / x             
 _const_ / (x / _const_) | _mul_ / x             

Note: _italic_ result is precalculated.

## [Power](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Pow.cs)

 Example       | Result 
---------------|--------
 x^0           | 1      
 0^x           | 0      
 x^1           | x      
 x ^ log(x, y) | y      
 e ^ ln(y)     | y      
 10 ^ lg(y)    | y      
 2 ^ lb(y)     | y      

## [Root](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Root.cs)

| Example    | Result |
|------------|--------|
| root(x, 1) | x      |

## [Exp](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Exp.cs)

| Example    | Result |
|------------|--------|
| exp(ln(x)) | x      |

## Logarithm [Lb](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Lb.cs), [Lg](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Lg.cs), [Ln](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Ln.cs), [Log](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Log.cs)

| Example   | Result |
|-----------|--------|
| lb(2)     | 1      |
| lg(10)    | 1      |
| ln(e)     | 1      |
| log(x, x) | 1      |

## [Unary Minus](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/UnaryMinus.cs)

| Example   | Result  |
|-----------|---------|
| -(-x)     | x       |
| -(number) | -number |

## [Trigonometric](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Trigonometric)

 Example        | Result 
----------------|--------
 arccos(cos(x)) | x      
 arccot(cot(x)) | x      
 arccsc(csc(x)) | x      
 arcsec(sec(x)) | x      
 arcsin(sin(x)) | x      
 arctan(tan(x)) | x      
 cos(arccos(x)) | x      
 cos(0)         | 1      
 cot(arccot(x)) | x      
 cot(0)         | +∞     
 csc(arccsc(x)) | x      
 csc(0)         | +∞     
 sec(arcsec(x)) | x      
 sec(0)         | 1      
 sin(arcsin(x)) | x      
 sin(0)         | 0      
 tan(arctan(x)) | x      
 tan(0)         | 0      

## [Hyperbolic](https://github.com/sys27/xFunc/tree/master/xFunc.Maths/Expressions/Hyperbolic)

 Example         | Result 
-----------------|--------
 arcosh(cosh(x)) | x      
 arcoth(coth(x)) | x      
 arcsch(csch(x)) | x      
 arsech(sech(x)) | x      
 arsinh(sinh(x)) | x      
 artanh(tanh(x)) | x      
 cosh(accosh(x)) | x      
 coth(accoth(x)) | x      
 csch(accsch(x)) | x      
 sech(acsech(x)) | x      
 sinh(acsinh(x)) | x      
 tanh(actanh(x)) | x      

## [ToDegree](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/ToDegree.cs)

| Example                | Result   |
|------------------------|----------|
| todegree(number)       | x degree |
| todegree(x degree)     | x degree |
| todegree(angle number) | x degree |

## [ToRadian](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/ToRadian.cs)

| Example                | Result   |
|------------------------|----------|
| toradian(number)       | x radian |
| toradian(x radian)     | x radian |
| toradian(angle number) | x radian |

## [ToGradian](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/ToGradian.cs)

| Example                 | Result    |
|-------------------------|-----------|
| togradian(number)       | x gradian |
| togradian(x gradian)    | x gradian |
| togradian(angle number) | x gradian |

## [ToNumber](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/ToNumber.cs)

| Example                | Result |
|------------------------|--------|
| tonumber(angle number) | x      |

## [Abs](https://github.com/sys27/xFunc/blob/master/xFunc.Maths/Expressions/Abs.cs)

| Example     | Result |
|-------------|--------|
| abs(-x)     | x      |
| abs(abs(x)) | abs(x) |
