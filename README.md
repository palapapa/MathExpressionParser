# MathExpressionParser

[![Build](https://img.shields.io/travis/com/palapapa/MathExpressionParser/master?style=for-the-badge)](https://travis-ci.com/github/palapapa/MathExpressionParser)

A lightweight customizable math expression parsing library that supports custom functions and variables, with a complete error handling system(`ParserException`).

## Getting Started

### Evaluating a expression

```csharp
MathExpression expr = new("1 * (2 + 3) - (4 * 5 * (6 + 7))");
double value = expr.Evaluate(); // value is -255
```

```csharp
MathExpression expr = new("1! * 2! * 3! * 4!");
double value = expr.Evaluate(); // value is 288
```

```csharp
MathExpression expr = new("sin(90 torad)");
double value = expr.Evaluate(); // value is 1
```

```csharp
MathExpression expr = new("sqrt(1 + sin(pi / 2) * 3)");
double value = expr.Evaluate(); // value is 2
```

```csharp
MathExpression expr = new("log(log(3.2e+2, -(-1e1)), sqrt(1E-4))");
double value = expr.Evaluate(); // value is -0.19941686566
```

### Supported built-in operators, functions, and constants

The number in the parentheses is the number of arguments that function takes.

All trigonometric functions are in radians. Use torad to convert degrees to radians.

Type          | Name  | Description
--------------|-------|-----------------------------------------------------------------------
Binary        | +     | Addition
Binary        | -     | Subtraction
Prefix unary  | -     | Negation
Binary        | *     | Multiplication
Binary        | /     | Division
Binary        | ^     | Power
Binary        | %     | Modulo
Constant      | pi    | π
Constant      | e     | Euler's number
Function(1)   | sqrt  | Square root
Function(1)   | sin   | Sine
Function(1)   | asin  | Arc sine
Function(1)   | cos   | Cosine
Function(1)   | acos  | Arc cosine
Function(1)   | tan   | Tangent
Function(1)   | atan  | Arc tangent
Function(1)   | csc   | Cosecant
Function(1)   | acsc  | Arc cosecant
Function(1)   | sec   | Secant
Function(1)   | asec  | Arc secant
Function(1)   | cot   | Cotangent
Function(1)   | acot  | Arc cotangent
Function(1)   | sinh  | Hyperbolic sine
Function(1)   | asinh | Hyperbolic arc sine
Function(1)   | cosh  | Hyperbolic cosine
Function(1)   | acosh | Hyperbolic arc cosine
Function(1)   | tanh  | Hyperbolic tangent
Function(1)   | atanh | Hyperbolic arc tangent
Function(1)   | csch  | Hyperbolic cosecant
Function(1)   | acsch | Hyperbolic arc cosecant
Function(1)   | sech  | Hyperbolic secant
Function(1)   | asech | Hyperbolic arc secant
Function(1)   | coth  | Hyperbolic cotangent
Function(1)   | acoth | Hyperbolic arc cotangent
Function(2)   | P     | Permutation
Function(2)   | C     | Combination
Function(2)   | H     | H(x, y) = C(x + y - 1, x - 1) = C(x + y - 1, y)
Function(2)   | log   | Logarithm. The second argument is the base.
Function(1)   | log10 | Logarithm base 10
Function(1)   | log2  | Logarithm base 2
Function(1)   | ln    | Natural logarithm
Function(1)   | ceil  | Least integer not less than
Function(1)   | floor | Greatest integer not greater than
Function(1)   | round | Round decimal places
Function(1)   | abs   | Absolute value
Function(any) | min   | Smallest value of the arguments. If no arguments are given, returns 0.
Function(any) | max   | Largest value of the arguments. If no arguments are given, returns 0.
Postfix unary | !     | Factorial
Postfix unary | torad | Convert degrees to radians
Postfix unary | todeg | Convert radians to degrees

### Adding custom functions and constants

```csharp
MathExpression expr = new("f(x)");
expr.CustomFunctions.Add(new FunctionalOperator("f", x => x[0] * 2, 1));
expr.CustomConstants.Add(new ConstantOperator("x", 100));
double value = expr.Evaluate(); // value is 200
```

### Error handling

```csharp
MathExpression expr = new("sin(1,)");
Console.WriteLine(expr.Validate().Context); // prints ParserExceptionContext { Position = 6, Type = UnexpectedClosingParenthesis }
```

### All error types

Type                           | Description
-------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
InvalidNumberFormat            | An error where a number with an invalid format was found in a `MathExpression`.
IncorrectArgumentCount         | An error where either too many or too few arguments were passed to a `FunctionalOperator`.
InvalidCustomFunctionName      | An error where some of the custom functions provided have names that either start with a number, are empty, or contain characters that are not alphanumeric or are not underscores.
NullCustomFunction             | An error where `MathExpression.CustomFunctions` contains a null element.
InvalidCustomConstantName      | An error where some of the custom constants provided have names that either start with a number, are empty, or contain characters that are not alphanumeric or are not underscores.
NullCustomConstant             | An error where `MathExpression.CustomConstants` contain a mull element.
NaNConstant                    | An error where `MathExpression.CustomConstants` have a `ConstantOperator.Value` of `double.NaN`.
ConflictingNames               | An error where two `Operator`s in a `MathExpression` share the same name.
UnexpectedBinaryOperator       | An error where a `BinaryOperator` is used incorrectly.
TooManyOpeningParentheses      | An error where a opening parenthesiis is used without a corresponding opening parenthesis.
UnexpectedClosingParenthesis   | An error where a closing parenthesis is used incorrectly, or where a closing parenthesis is used without a corresponding opening parenthesis.
UnexpectedComma                | An error where a comma is used incorrectly.
UnexpectedConstantOperator     | An error where a `ConstantOperator` is used incorrectly.
UnexpectedFunctionalOperator   | An error where a `FunctionalOperator` is used incorrectly.
UnexpectedNumber               | An error where a number is used incorrectly.
UnexpectedOpeningParenthesis   | An error where a opening parenthesis is used incorrectly.
UnexpectedPostfixUnaryOperator | An error where a `PostfixUnaryOperator` is used incorrectly.
UnexpectedPrefixUnaryOperator  | An error where a `PrefixUnaryOperator` is used incorrectly.
UnknownOperator                | An error where an unknown `Operator` is used in a `MathExpression`.
UnexpectedNewline              | An error where a `MathExpression` ended unexpectedly.

## Complete Docs

For the complete auto-generated documentation, see [here](https://github.com/palapapa/MathExpressionParser/blob/master/Docs/MathExpressionParser.md). (The docs generation tool Vsxmd currently doesn't work for &lt;inheritdoc&gt; tags and will just leave them empty or write "Inherited from parent.")
