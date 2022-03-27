# MathExpressionParser

[![](https://img.shields.io/travis/com/palapapa/MathExpressionParser/master?style=for-the-badge)](https://travis-ci.com/github/palapapa/MathExpressionParser) <br>
A lightweight customizable math expression parsing library that supports custom functions and variables, with a complete error handling system(`ParserException`).

## Getting Started

### Evaluating a expression

```csharp
MathExpression expr = new("log(log(3.2e+2, -(-1e1)), sqrt(1E-4))");
double value = expr.Evaluate(); // value is -0.19941686566
```

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
For all types of error that can happen, see [here](#parserexceptiontype-type).

### Supported built-in operators, functions, and constants

- \+ (binary operator)
- \- (binary operator)
- \* (binary operator)
- / (binary operator)
- ^ (binary operator)
- % (binary operator)
- pi (constant)
- e (constant)
- sqrt (function)
- sin (function)
- asin (function)
- cos (function)
- acos (function)
- tan (function)
- atan (function)
- csc (function)
- acsc (function)
- sec (function)
- asec (function)
- cot (function)
- acot (function)
- sinh (function)
- asinh (function)
- cosh (function)
- acosh (function)
- tanh (function)
- atanh (function)
- csch (function)
- acsch (function)
- sech (function)
- asech (function)
- coth (function)
- acoth (function)
- P (function)
- C (function)
- H (function)
- log (function)
- log10 (function)
- log2 (function)
- ln (function)
- ceil (function)
- floor (function)
- round (function)
- abs (function)
- min (function)
- max (function)
- ! (postfix unary operator)
- torad (postfix unary operator)
- todeg (postfix unary operator)

# Complete Docs

<a name='assembly'></a>

## Contents

- [BinaryOperator](#T-MathExpressionParser-BinaryOperator 'MathExpressionParser.BinaryOperator')
  - [Precedence](#P-MathExpressionParser-BinaryOperator-Precedence 'MathExpressionParser.BinaryOperator.Precedence')
- [BinaryOperatorDelegate](#T-MathExpressionParser-BinaryOperatorDelegate 'MathExpressionParser.BinaryOperatorDelegate')
- [ConstantOperator](#T-MathExpressionParser-ConstantOperator 'MathExpressionParser.ConstantOperator')
  - [#ctor(name,value)](#M-MathExpressionParser-ConstantOperator-#ctor-System-String,System-Double- 'MathExpressionParser.ConstantOperator.#ctor(System.String,System.Double)')
  - [Value](#P-MathExpressionParser-ConstantOperator-Value 'MathExpressionParser.ConstantOperator.Value')
- [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator')
  - [#ctor(name,calculate,argumentCounts)](#M-MathExpressionParser-FunctionalOperator-#ctor-System-String,MathExpressionParser-FunctionalOperatorDelegate,System-Int32[]- 'MathExpressionParser.FunctionalOperator.#ctor(System.String,MathExpressionParser.FunctionalOperatorDelegate,System.Int32[])')
  - [ArgumentCounts](#P-MathExpressionParser-FunctionalOperator-ArgumentCounts 'MathExpressionParser.FunctionalOperator.ArgumentCounts')
  - [Calculate](#P-MathExpressionParser-FunctionalOperator-Calculate 'MathExpressionParser.FunctionalOperator.Calculate')
- [FunctionalOperatorDelegate](#T-MathExpressionParser-FunctionalOperatorDelegate 'MathExpressionParser.FunctionalOperatorDelegate')
- [IMathExpression](#T-MathExpressionParser-IMathExpression 'MathExpressionParser.IMathExpression')
  - [Expression](#P-MathExpressionParser-IMathExpression-Expression 'MathExpressionParser.IMathExpression.Expression')
  - [Evaluate()](#M-MathExpressionParser-IMathExpression-Evaluate 'MathExpressionParser.IMathExpression.Evaluate')
- [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression')
  - [#ctor()](#M-MathExpressionParser-MathExpression-#ctor 'MathExpressionParser.MathExpression.#ctor')
  - [#ctor(expression)](#M-MathExpressionParser-MathExpression-#ctor-System-String- 'MathExpressionParser.MathExpression.#ctor(System.String)')
  - [#ctor(customFunctions,customConstants)](#M-MathExpressionParser-MathExpression-#ctor-System-Collections-Generic-IList{MathExpressionParser-FunctionalOperator},System-Collections-Generic-IList{MathExpressionParser-ConstantOperator}- 'MathExpressionParser.MathExpression.#ctor(System.Collections.Generic.IList{MathExpressionParser.FunctionalOperator},System.Collections.Generic.IList{MathExpressionParser.ConstantOperator})')
  - [#ctor(expression,customFunctions,customConstants)](#M-MathExpressionParser-MathExpression-#ctor-System-String,System-Collections-Generic-IList{MathExpressionParser-FunctionalOperator},System-Collections-Generic-IList{MathExpressionParser-ConstantOperator}- 'MathExpressionParser.MathExpression.#ctor(System.String,System.Collections.Generic.IList{MathExpressionParser.FunctionalOperator},System.Collections.Generic.IList{MathExpressionParser.ConstantOperator})')
  - [#ctor(mathExpression)](#M-MathExpressionParser-MathExpression-#ctor-MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.#ctor(MathExpressionParser.MathExpression)')
  - [CustomConstants](#P-MathExpressionParser-MathExpression-CustomConstants 'MathExpressionParser.MathExpression.CustomConstants')
  - [CustomFunctions](#P-MathExpressionParser-MathExpression-CustomFunctions 'MathExpressionParser.MathExpression.CustomFunctions')
  - [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression')
  - [CompareTo(other)](#M-MathExpressionParser-MathExpression-CompareTo-MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.CompareTo(MathExpressionParser.MathExpression)')
  - [Equals(other)](#M-MathExpressionParser-MathExpression-Equals-MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.Equals(MathExpressionParser.MathExpression)')
  - [Equals(obj)](#M-MathExpressionParser-MathExpression-Equals-System-Object- 'MathExpressionParser.MathExpression.Equals(System.Object)')
  - [Evaluate()](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate')
  - [GetArgumentCount(tokens)](#M-MathExpressionParser-MathExpression-GetArgumentCount-System-Collections-Generic-List{MathExpressionParser-Token}- 'MathExpressionParser.MathExpression.GetArgumentCount(System.Collections.Generic.List{MathExpressionParser.Token})')
  - [GetHashCode()](#M-MathExpressionParser-MathExpression-GetHashCode 'MathExpressionParser.MathExpression.GetHashCode')
  - [ToString()](#M-MathExpressionParser-MathExpression-ToString 'MathExpressionParser.MathExpression.ToString')
  - [TryEvaluate(result)](#M-MathExpressionParser-MathExpression-TryEvaluate-System-Double@- 'MathExpressionParser.MathExpression.TryEvaluate(System.Double@)')
  - [Validate()](#M-MathExpressionParser-MathExpression-Validate 'MathExpressionParser.MathExpression.Validate')
  - [op_Addition(left,right)](#M-MathExpressionParser-MathExpression-op_Addition-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_Addition(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
  - [op_Division(left,right)](#M-MathExpressionParser-MathExpression-op_Division-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_Division(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
  - [op_Equality(left,right)](#M-MathExpressionParser-MathExpression-op_Equality-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_Equality(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
  - [op_Explicit(mathExpression)](#M-MathExpressionParser-MathExpression-op_Explicit-MathExpressionParser-MathExpression-~System-String 'MathExpressionParser.MathExpression.op_Explicit(MathExpressionParser.MathExpression)~System.String')
  - [op_Explicit(mathExpression)](#M-MathExpressionParser-MathExpression-op_Explicit-MathExpressionParser-MathExpression-~System-Double 'MathExpressionParser.MathExpression.op_Explicit(MathExpressionParser.MathExpression)~System.Double')
  - [op_GreaterThan(left,right)](#M-MathExpressionParser-MathExpression-op_GreaterThan-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_GreaterThan(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
  - [op_GreaterThanOrEqual(left,right)](#M-MathExpressionParser-MathExpression-op_GreaterThanOrEqual-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_GreaterThanOrEqual(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
  - [op_Inequality(left,right)](#M-MathExpressionParser-MathExpression-op_Inequality-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_Inequality(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
  - [op_LessThan(left,right)](#M-MathExpressionParser-MathExpression-op_LessThan-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_LessThan(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
  - [op_LessThanOrEqual(left,right)](#M-MathExpressionParser-MathExpression-op_LessThanOrEqual-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_LessThanOrEqual(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
  - [op_Multiply(left,right)](#M-MathExpressionParser-MathExpression-op_Multiply-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_Multiply(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
  - [op_Subtraction(left,right)](#M-MathExpressionParser-MathExpression-op_Subtraction-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression- 'MathExpressionParser.MathExpression.op_Subtraction(MathExpressionParser.MathExpression,MathExpressionParser.MathExpression)')
- [Operator](#T-MathExpressionParser-Operator 'MathExpressionParser.Operator')
  - [#ctor(name)](#M-MathExpressionParser-Operator-#ctor-System-String- 'MathExpressionParser.Operator.#ctor(System.String)')
  - [Name](#P-MathExpressionParser-Operator-Name 'MathExpressionParser.Operator.Name')
- [OperatorAssociativity](#T-MathExpressionParser-OperatorAssociativity 'MathExpressionParser.OperatorAssociativity')
  - [Left](#F-MathExpressionParser-OperatorAssociativity-Left 'MathExpressionParser.OperatorAssociativity.Left')
  - [Right](#F-MathExpressionParser-OperatorAssociativity-Right 'MathExpressionParser.OperatorAssociativity.Right')
- [OperatorPrecedence](#T-MathExpressionParser-OperatorPrecedence 'MathExpressionParser.OperatorPrecedence')
  - [Additive](#F-MathExpressionParser-OperatorPrecedence-Additive 'MathExpressionParser.OperatorPrecedence.Additive')
  - [Exponentiation](#F-MathExpressionParser-OperatorPrecedence-Exponentiation 'MathExpressionParser.OperatorPrecedence.Exponentiation')
  - [Multiplicative](#F-MathExpressionParser-OperatorPrecedence-Multiplicative 'MathExpressionParser.OperatorPrecedence.Multiplicative')
  - [Unary](#F-MathExpressionParser-OperatorPrecedence-Unary 'MathExpressionParser.OperatorPrecedence.Unary')
- [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException')
  - [#ctor()](#M-MathExpressionParser-ParserException-#ctor 'MathExpressionParser.ParserException.#ctor')
  - [#ctor(message)](#M-MathExpressionParser-ParserException-#ctor-System-String- 'MathExpressionParser.ParserException.#ctor(System.String)')
  - [#ctor(message,innerException)](#M-MathExpressionParser-ParserException-#ctor-System-String,System-Exception- 'MathExpressionParser.ParserException.#ctor(System.String,System.Exception)')
  - [#ctor(message,context)](#M-MathExpressionParser-ParserException-#ctor-System-String,MathExpressionParser-ParserExceptionContext- 'MathExpressionParser.ParserException.#ctor(System.String,MathExpressionParser.ParserExceptionContext)')
  - [#ctor(message,context,innerException)](#M-MathExpressionParser-ParserException-#ctor-System-String,MathExpressionParser-ParserExceptionContext,System-Exception- 'MathExpressionParser.ParserException.#ctor(System.String,MathExpressionParser.ParserExceptionContext,System.Exception)')
  - [#ctor()](#M-MathExpressionParser-ParserException-#ctor-System-Runtime-Serialization-SerializationInfo,System-Runtime-Serialization-StreamingContext- 'MathExpressionParser.ParserException.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)')
  - [Context](#P-MathExpressionParser-ParserException-Context 'MathExpressionParser.ParserException.Context')
  - [GetObjectData()](#M-MathExpressionParser-ParserException-GetObjectData-System-Runtime-Serialization-SerializationInfo,System-Runtime-Serialization-StreamingContext- 'MathExpressionParser.ParserException.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)')
- [ParserExceptionContext](#T-MathExpressionParser-ParserExceptionContext 'MathExpressionParser.ParserExceptionContext')
  - [#ctor(Position,Type)](#M-MathExpressionParser-ParserExceptionContext-#ctor-System-Int32,MathExpressionParser-ParserExceptionType- 'MathExpressionParser.ParserExceptionContext.#ctor(System.Int32,MathExpressionParser.ParserExceptionType)')
  - [Position](#P-MathExpressionParser-ParserExceptionContext-Position 'MathExpressionParser.ParserExceptionContext.Position')
  - [Type](#P-MathExpressionParser-ParserExceptionContext-Type 'MathExpressionParser.ParserExceptionContext.Type')
- [ParserExceptionType](#T-MathExpressionParser-ParserExceptionType 'MathExpressionParser.ParserExceptionType')
  - [ConflictingNames](#F-MathExpressionParser-ParserExceptionType-ConflictingNames 'MathExpressionParser.ParserExceptionType.ConflictingNames')
  - [IncorrectArgumentCount](#F-MathExpressionParser-ParserExceptionType-IncorrectArgumentCount 'MathExpressionParser.ParserExceptionType.IncorrectArgumentCount')
  - [InvalidCustomConstantName](#F-MathExpressionParser-ParserExceptionType-InvalidCustomConstantName 'MathExpressionParser.ParserExceptionType.InvalidCustomConstantName')
  - [InvalidCustomFunctionName](#F-MathExpressionParser-ParserExceptionType-InvalidCustomFunctionName 'MathExpressionParser.ParserExceptionType.InvalidCustomFunctionName')
  - [InvalidNumberFormat](#F-MathExpressionParser-ParserExceptionType-InvalidNumberFormat 'MathExpressionParser.ParserExceptionType.InvalidNumberFormat')
  - [NaNConstant](#F-MathExpressionParser-ParserExceptionType-NaNConstant 'MathExpressionParser.ParserExceptionType.NaNConstant')
  - [NullCustomConstant](#F-MathExpressionParser-ParserExceptionType-NullCustomConstant 'MathExpressionParser.ParserExceptionType.NullCustomConstant')
  - [NullCustomFunction](#F-MathExpressionParser-ParserExceptionType-NullCustomFunction 'MathExpressionParser.ParserExceptionType.NullCustomFunction')
  - [TooManyOpeningParentheses](#F-MathExpressionParser-ParserExceptionType-TooManyOpeningParentheses 'MathExpressionParser.ParserExceptionType.TooManyOpeningParentheses')
  - [UnexpectedBinaryOperator](#F-MathExpressionParser-ParserExceptionType-UnexpectedBinaryOperator 'MathExpressionParser.ParserExceptionType.UnexpectedBinaryOperator')
  - [UnexpectedClosingParenthesis](#F-MathExpressionParser-ParserExceptionType-UnexpectedClosingParenthesis 'MathExpressionParser.ParserExceptionType.UnexpectedClosingParenthesis')
  - [UnexpectedComma](#F-MathExpressionParser-ParserExceptionType-UnexpectedComma 'MathExpressionParser.ParserExceptionType.UnexpectedComma')
  - [UnexpectedConstantOperator](#F-MathExpressionParser-ParserExceptionType-UnexpectedConstantOperator 'MathExpressionParser.ParserExceptionType.UnexpectedConstantOperator')
  - [UnexpectedFunctionalOperator](#F-MathExpressionParser-ParserExceptionType-UnexpectedFunctionalOperator 'MathExpressionParser.ParserExceptionType.UnexpectedFunctionalOperator')
  - [UnexpectedNewline](#F-MathExpressionParser-ParserExceptionType-UnexpectedNewline 'MathExpressionParser.ParserExceptionType.UnexpectedNewline')
  - [UnexpectedNumber](#F-MathExpressionParser-ParserExceptionType-UnexpectedNumber 'MathExpressionParser.ParserExceptionType.UnexpectedNumber')
  - [UnexpectedOpeningParenthesis](#F-MathExpressionParser-ParserExceptionType-UnexpectedOpeningParenthesis 'MathExpressionParser.ParserExceptionType.UnexpectedOpeningParenthesis')
  - [UnexpectedPostfixUnaryOperator](#F-MathExpressionParser-ParserExceptionType-UnexpectedPostfixUnaryOperator 'MathExpressionParser.ParserExceptionType.UnexpectedPostfixUnaryOperator')
  - [UnexpectedPrefixUnaryOperator](#F-MathExpressionParser-ParserExceptionType-UnexpectedPrefixUnaryOperator 'MathExpressionParser.ParserExceptionType.UnexpectedPrefixUnaryOperator')
  - [UnknownOperator](#F-MathExpressionParser-ParserExceptionType-UnknownOperator 'MathExpressionParser.ParserExceptionType.UnknownOperator')

<a name='T-MathExpressionParser-BinaryOperator'></a>

## BinaryOperator `type`

##### Namespace

MathExpressionParser

##### Summary

Represents a operator that takes the left and right tokens as its operands.

<a name='P-MathExpressionParser-BinaryOperator-Precedence'></a>

### Precedence `property`

##### Summary

The order in which this [Operator](#T-MathExpressionParser-Operator 'MathExpressionParser.Operator') will be parsed.

<a name='T-MathExpressionParser-BinaryOperatorDelegate'></a>

## BinaryOperatorDelegate `type`

##### Namespace

MathExpressionParser

##### Summary

A delegate used to compute the result of a [BinaryOperator](#T-MathExpressionParser-BinaryOperator 'MathExpressionParser.BinaryOperator').

##### Returns

The computed result of a [BinaryOperator](#T-MathExpressionParser-BinaryOperator 'MathExpressionParser.BinaryOperator').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [T:MathExpressionParser.BinaryOperatorDelegate](#T-T-MathExpressionParser-BinaryOperatorDelegate 'T:MathExpressionParser.BinaryOperatorDelegate') | The left operand of a [BinaryOperator](#T-MathExpressionParser-BinaryOperator 'MathExpressionParser.BinaryOperator'). |

<a name='T-MathExpressionParser-ConstantOperator'></a>

## ConstantOperator `type`

##### Namespace

MathExpressionParser

##### Summary

Represents a constant in a [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression').

<a name='M-MathExpressionParser-ConstantOperator-#ctor-System-String,System-Double-'></a>

### #ctor(name,value) `constructor`

##### Summary

Initailizes a new instance of [ConstantOperator](#T-MathExpressionParser-ConstantOperator 'MathExpressionParser.ConstantOperator').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| value | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The value of this [ConstantOperator](#T-MathExpressionParser-ConstantOperator 'MathExpressionParser.ConstantOperator'). |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | When `name` is null. |

<a name='P-MathExpressionParser-ConstantOperator-Value'></a>

### Value `property`

##### Summary

The value this [ConstantOperator](#T-MathExpressionParser-ConstantOperator 'MathExpressionParser.ConstantOperator') holds.

<a name='T-MathExpressionParser-FunctionalOperator'></a>

## FunctionalOperator `type`

##### Namespace

MathExpressionParser

##### Summary

Represents a function in a [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression').

<a name='M-MathExpressionParser-FunctionalOperator-#ctor-System-String,MathExpressionParser-FunctionalOperatorDelegate,System-Int32[]-'></a>

### #ctor(name,calculate,argumentCounts) `constructor`

##### Summary

Initializes a new instance of [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| calculate | [MathExpressionParser.FunctionalOperatorDelegate](#T-MathExpressionParser-FunctionalOperatorDelegate 'MathExpressionParser.FunctionalOperatorDelegate') |  |
| argumentCounts | [System.Int32[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32[] 'System.Int32[]') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | When either `name` or `calculate` is null. |

<a name='P-MathExpressionParser-FunctionalOperator-ArgumentCounts'></a>

### ArgumentCounts `property`

##### Summary

The possible number of arguments this [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator') can take. If this has 0 elements, this [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator') can take any number of arguments.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | When this is set to `null`. |

<a name='P-MathExpressionParser-FunctionalOperator-Calculate'></a>

### Calculate `property`

##### Summary

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | When this is set to `null`. |

<a name='T-MathExpressionParser-FunctionalOperatorDelegate'></a>

## FunctionalOperatorDelegate `type`

##### Namespace

MathExpressionParser

##### Summary

A `delegate` used to calculate the value of a [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator').

##### Returns

The value of a [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| arguments | [T:MathExpressionParser.FunctionalOperatorDelegate](#T-T-MathExpressionParser-FunctionalOperatorDelegate 'T:MathExpressionParser.FunctionalOperatorDelegate') | An [IList\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList`1 'System.Collections.Generic.IList`1') of arguments passed to a [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator') in a [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression'). |

<a name='T-MathExpressionParser-IMathExpression'></a>

## IMathExpression `type`

##### Namespace

MathExpressionParser

##### Summary

Exposes common properties and methods of mathematical expressions.

<a name='P-MathExpressionParser-IMathExpression-Expression'></a>

### Expression `property`

##### Summary

Represents the math expression.

<a name='M-MathExpressionParser-IMathExpression-Evaluate'></a>

### Evaluate() `method`

##### Summary

Evaluates the [Expression](#P-MathExpressionParser-IMathExpression-Expression 'MathExpressionParser.IMathExpression.Expression') and returns a value.

##### Returns

The value of [Expression](#P-MathExpressionParser-IMathExpression-Expression 'MathExpressionParser.IMathExpression.Expression').

##### Parameters

This method has no parameters.

<a name='T-MathExpressionParser-MathExpression'></a>

## MathExpression `type`

##### Namespace

MathExpressionParser

##### Summary

Represents a mathematical expression.

<a name='M-MathExpressionParser-MathExpression-#ctor'></a>

### #ctor() `constructor`

##### Summary

Initializes a new instance of [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') with [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression') set to an empty [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String').

##### Parameters

This constructor has no parameters.

<a name='M-MathExpressionParser-MathExpression-#ctor-System-String-'></a>

### #ctor(expression) `constructor`

##### Summary

Initializes a new instance of [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') with [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression') set to `expression`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The math expression to use. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | When `expression` is `null`. |

<a name='M-MathExpressionParser-MathExpression-#ctor-System-Collections-Generic-IList{MathExpressionParser-FunctionalOperator},System-Collections-Generic-IList{MathExpressionParser-ConstantOperator}-'></a>

### #ctor(customFunctions,customConstants) `constructor`

##### Summary

Initializes a new instance of [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') with [CustomFunctions](#P-MathExpressionParser-MathExpression-CustomFunctions 'MathExpressionParser.MathExpression.CustomFunctions') set to `customFunctions`, [CustomConstants](#P-MathExpressionParser-MathExpression-CustomConstants 'MathExpressionParser.MathExpression.CustomConstants') set to `customConstants`, and [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression') set to an empty [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String').
If `customFunctions` or `customConstants` is `null`, it will remain as an empty [IList\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList`1 'System.Collections.Generic.IList`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| customFunctions | [System.Collections.Generic.IList{MathExpressionParser.FunctionalOperator}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{MathExpressionParser.FunctionalOperator}') | The [IList\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList`1 'System.Collections.Generic.IList`1') to set [CustomFunctions](#P-MathExpressionParser-MathExpression-CustomFunctions 'MathExpressionParser.MathExpression.CustomFunctions') to, or `null` if you wish to leave it empty. |
| customConstants | [System.Collections.Generic.IList{MathExpressionParser.ConstantOperator}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{MathExpressionParser.ConstantOperator}') | The [IList\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList`1 'System.Collections.Generic.IList`1') to set [CustomConstants](#P-MathExpressionParser-MathExpression-CustomConstants 'MathExpressionParser.MathExpression.CustomConstants') to, or `null` if you wish to leave it empty. |

<a name='M-MathExpressionParser-MathExpression-#ctor-System-String,System-Collections-Generic-IList{MathExpressionParser-FunctionalOperator},System-Collections-Generic-IList{MathExpressionParser-ConstantOperator}-'></a>

### #ctor(expression,customFunctions,customConstants) `constructor`

##### Summary

Initializes a new instance of [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') with [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression') set to `expression`, [CustomFunctions](#P-MathExpressionParser-MathExpression-CustomFunctions 'MathExpressionParser.MathExpression.CustomFunctions') set to `customFunctions` and [CustomConstants](#P-MathExpressionParser-MathExpression-CustomConstants 'MathExpressionParser.MathExpression.CustomConstants') set to `customConstants`.
If `customFunctions` or `customConstants` is `null`, it will remain as an empty [IList\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList`1 'System.Collections.Generic.IList`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| customFunctions | [System.Collections.Generic.IList{MathExpressionParser.FunctionalOperator}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{MathExpressionParser.FunctionalOperator}') |  |
| customConstants | [System.Collections.Generic.IList{MathExpressionParser.ConstantOperator}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{MathExpressionParser.ConstantOperator}') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') |  |

<a name='M-MathExpressionParser-MathExpression-#ctor-MathExpressionParser-MathExpression-'></a>

### #ctor(mathExpression) `constructor`

##### Summary

Initializes a new instance of [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') and copies each member of `mathExpression` into the new instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| mathExpression | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') | The [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') to copy. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | If `mathExpression` is `null`. |

<a name='P-MathExpressionParser-MathExpression-CustomConstants'></a>

### CustomConstants `property`

##### Summary

Any [ConstantOperator](#T-MathExpressionParser-ConstantOperator 'MathExpressionParser.ConstantOperator') in this [IList\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList`1 'System.Collections.Generic.IList`1') will be used when [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') is called.

<a name='P-MathExpressionParser-MathExpression-CustomFunctions'></a>

### CustomFunctions `property`

##### Summary

Any [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator') in this [IList\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList`1 'System.Collections.Generic.IList`1') will be used when [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') is called.

<a name='P-MathExpressionParser-MathExpression-Expression'></a>

### Expression `property`

##### Summary

The [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') representation of the math expression this [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') holds.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | When this is set to `null`. |

<a name='M-MathExpressionParser-MathExpression-CompareTo-MathExpressionParser-MathExpression-'></a>

### CompareTo(other) `method`

##### Summary

Compares the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of this instance to that of `other`.

##### Returns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') | If either this instance or `other` throws [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') on calling its [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate'). |

<a name='M-MathExpressionParser-MathExpression-Equals-MathExpressionParser-MathExpression-'></a>

### Equals(other) `method`

##### Summary

Check if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of this instance is equal to that of `other`.

##### Returns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='M-MathExpressionParser-MathExpression-Equals-System-Object-'></a>

### Equals(obj) `method`

##### Summary

##### Returns

If `obj` is not a [Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') or an [IMathExpression](#T-MathExpressionParser-IMathExpression 'MathExpressionParser.IMathExpression'), this always returns `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | A [Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') or a [IMathExpression](#T-MathExpressionParser-IMathExpression 'MathExpressionParser.IMathExpression'). |

<a name='M-MathExpressionParser-MathExpression-Evaluate'></a>

### Evaluate() `method`

##### Summary

Evaluates [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression').

##### Returns

The value of the [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression').

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') | If [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression') is not a valid math expression. |

<a name='M-MathExpressionParser-MathExpression-GetArgumentCount-System-Collections-Generic-List{MathExpressionParser-Token}-'></a>

### GetArgumentCount(tokens) `method`

##### Summary

Returns the same [List\`1](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List`1 'System.Collections.Generic.List`1') as `tokens` but with every [ArgumentCount](#P-MathExpressionParser-FunctionalOperatorToken-ArgumentCount 'MathExpressionParser.FunctionalOperatorToken.ArgumentCount') set to the correct number.
This method assumes the provided `tokens` represent a valid expression.

##### Returns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tokens | [System.Collections.Generic.List{MathExpressionParser.Token}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{MathExpressionParser.Token}') |  |

<a name='M-MathExpressionParser-MathExpression-GetHashCode'></a>

### GetHashCode() `method`

##### Summary

##### Returns

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') | If this instance throws [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') on calling its [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate'). |

<a name='M-MathExpressionParser-MathExpression-ToString'></a>

### ToString() `method`

##### Summary

Converts this [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') instance to a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String').

##### Returns

[Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression').

##### Parameters

This method has no parameters.

<a name='M-MathExpressionParser-MathExpression-TryEvaluate-System-Double@-'></a>

### TryEvaluate(result) `method`

##### Summary

Tries to evaluate [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression'). This can still throw an exception if any [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator') used throws an exception itself.

##### Returns

A [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') instance containing information about the error if `result` is not a valid math expression, or `null` if it is.
If a non-`null`[ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') is returned, `result` will be set to [NaN](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.NaN 'System.Double.NaN').
If [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression') is whitespace or empty, `result` will be set to [NaN](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.NaN 'System.Double.NaN').
Otherwise, `result` will be set to the result of the evaluation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| result | [System.Double@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double@ 'System.Double@') | The result of the evaluation if it succeeds. |

<a name='M-MathExpressionParser-MathExpression-Validate'></a>

### Validate() `method`

##### Summary

Checks if [Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression') is a valid math expression.

##### Returns

A [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') instance that contains information about the error, or `null` if the expression is valid.

##### Parameters

This method has no parameters.

<a name='M-MathExpressionParser-MathExpression-op_Addition-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_Addition(left,right) `method`

##### Summary

Adds the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of the two [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') together.

##### Returns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') | When either of the arguments throws [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') on calling its [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate'). |

<a name='M-MathExpressionParser-MathExpression-op_Division-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_Division(left,right) `method`

##### Summary

Divide the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` by that of `right`.

##### Returns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='M-MathExpressionParser-MathExpression-op_Equality-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_Equality(left,right) `method`

##### Summary

Checks if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is equal to that of `right`.

##### Returns

`true` if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is equal to that of `right`; otherwise, `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='M-MathExpressionParser-MathExpression-op_Explicit-MathExpressionParser-MathExpression-~System-String'></a>

### op_Explicit(mathExpression) `method`

##### Summary

Converts this [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') instance to a [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String').

##### Returns

[Expression](#P-MathExpressionParser-MathExpression-Expression 'MathExpressionParser.MathExpression.Expression') or `null` if `mathExpression` is null.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| mathExpression | [MathExpressionParser.MathExpression)~System.String](#T-MathExpressionParser-MathExpression-~System-String 'MathExpressionParser.MathExpression)~System.String') | The [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') to convert. |

<a name='M-MathExpressionParser-MathExpression-op_Explicit-MathExpressionParser-MathExpression-~System-Double'></a>

### op_Explicit(mathExpression) `method`

##### Summary

Converts `mathExpression` to a [Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') with the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `mathExpression`.

##### Returns

Value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `mathExpression`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| mathExpression | [MathExpressionParser.MathExpression)~System.Double](#T-MathExpressionParser-MathExpression-~System-Double 'MathExpressionParser.MathExpression)~System.Double') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') | If `mathExpression` throws [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') on calling its [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate'). |

<a name='M-MathExpressionParser-MathExpression-op_GreaterThan-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_GreaterThan(left,right) `method`

##### Summary

Checks if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is larger than that of `right`.

##### Returns

`true` if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is larger than that of `right`; otherwise, `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='M-MathExpressionParser-MathExpression-op_GreaterThanOrEqual-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_GreaterThanOrEqual(left,right) `method`

##### Summary

Checks if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is larger than or equal to that of `right`.

##### Returns

`true` if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is larger than or equal to that of `right`; otherwise, `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='M-MathExpressionParser-MathExpression-op_Inequality-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_Inequality(left,right) `method`

##### Summary

Checks if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is not equal to that of `right`.

##### Returns

`true` if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is not equal to that of `right`; otherwise, `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='M-MathExpressionParser-MathExpression-op_LessThan-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_LessThan(left,right) `method`

##### Summary

Checks if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is smaller than that of `right`.

##### Returns

`true` if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is smaller than that of `right`; otherwise, `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='M-MathExpressionParser-MathExpression-op_LessThanOrEqual-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_LessThanOrEqual(left,right) `method`

##### Summary

Checks if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is smaller than or equal to that of `right`.

##### Returns

`true` if the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `left` is smaller or equal to than that of `right`; otherwise, `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='M-MathExpressionParser-MathExpression-op_Multiply-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_Multiply(left,right) `method`

##### Summary

Multiplies the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of the two [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') together.

##### Returns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='M-MathExpressionParser-MathExpression-op_Subtraction-MathExpressionParser-MathExpression,MathExpressionParser-MathExpression-'></a>

### op_Subtraction(left,right) `method`

##### Summary

Substracts the value of [Evaluate](#M-MathExpressionParser-MathExpression-Evaluate 'MathExpressionParser.MathExpression.Evaluate') of `right` from that of `left`.

##### Returns

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |
| right | [MathExpressionParser.MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [MathExpressionParser.ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') |  |

<a name='T-MathExpressionParser-Operator'></a>

## Operator `type`

##### Namespace

MathExpressionParser

##### Summary

Base class for different types of operators in an [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression').

<a name='M-MathExpressionParser-Operator-#ctor-System-String-'></a>

### #ctor(name) `constructor`

##### Summary

Base constructor for all derived [Operator](#T-MathExpressionParser-Operator 'MathExpressionParser.Operator')s.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | When `name` is `null`. |

<a name='P-MathExpressionParser-Operator-Name'></a>

### Name `property`

##### Summary

The [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') representation of the this [Operator](#T-MathExpressionParser-Operator 'MathExpressionParser.Operator').

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | When this is set to `null`. |

<a name='T-MathExpressionParser-OperatorAssociativity'></a>

## OperatorAssociativity `type`

##### Namespace

MathExpressionParser

##### Summary

Represents the associativity of an [Operator](#T-MathExpressionParser-Operator 'MathExpressionParser.Operator').

<a name='F-MathExpressionParser-OperatorAssociativity-Left'></a>

### Left `constants`

##### Summary

Left-to-right associativity.

<a name='F-MathExpressionParser-OperatorAssociativity-Right'></a>

### Right `constants`

##### Summary

Right-to-left associativity.

<a name='T-MathExpressionParser-OperatorPrecedence'></a>

## OperatorPrecedence `type`

##### Namespace

MathExpressionParser

##### Summary

Represents the precedence of an [Operator](#T-MathExpressionParser-Operator 'MathExpressionParser.Operator'). An `enum` option with a higher value is parsed first.

<a name='F-MathExpressionParser-OperatorPrecedence-Additive'></a>

### Additive `constants`

##### Summary

This precedence level is parsed fourth.

<a name='F-MathExpressionParser-OperatorPrecedence-Exponentiation'></a>

### Exponentiation `constants`

##### Summary

This precedence level is parsed first.

<a name='F-MathExpressionParser-OperatorPrecedence-Multiplicative'></a>

### Multiplicative `constants`

##### Summary

This precedence level is parsed third.

<a name='F-MathExpressionParser-OperatorPrecedence-Unary'></a>

### Unary `constants`

##### Summary

This precedence level is parsed second.

<a name='T-MathExpressionParser-ParserException'></a>

## ParserException `type`

##### Namespace

MathExpressionParser

##### Summary

An [Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') that is thrown when [Evaluate](#M-MathExpressionParser-IMathExpression-Evaluate 'MathExpressionParser.IMathExpression.Evaluate') encounters an unexpected situation.

<a name='M-MathExpressionParser-ParserException-#ctor'></a>

### #ctor() `constructor`

##### Summary

Initializes a new instance of [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException').

##### Parameters

This constructor has no parameters.

<a name='M-MathExpressionParser-ParserException-#ctor-System-String-'></a>

### #ctor(message) `constructor`

##### Summary

Initializes a new instance of [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') with a specified error message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The reason why this [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') was thrown. |

<a name='M-MathExpressionParser-ParserException-#ctor-System-String,System-Exception-'></a>

### #ctor(message,innerException) `constructor`

##### Summary

Initializes a new instance of the [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') with a specified error message and a reference to the inner exception that is the cause of this exception.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| innerException | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |

<a name='M-MathExpressionParser-ParserException-#ctor-System-String,MathExpressionParser-ParserExceptionContext-'></a>

### #ctor(message,context) `constructor`

##### Summary

Initializes a new instance of the [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') with a specified error message and a [ParserExceptionContext](#T-MathExpressionParser-ParserExceptionContext 'MathExpressionParser.ParserExceptionContext') with information about this exception.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| context | [MathExpressionParser.ParserExceptionContext](#T-MathExpressionParser-ParserExceptionContext 'MathExpressionParser.ParserExceptionContext') | A [ParserExceptionContext](#T-MathExpressionParser-ParserExceptionContext 'MathExpressionParser.ParserExceptionContext') with information about this exception. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | When `context` is `null`. |

<a name='M-MathExpressionParser-ParserException-#ctor-System-String,MathExpressionParser-ParserExceptionContext,System-Exception-'></a>

### #ctor(message,context,innerException) `constructor`

##### Summary

Initializes a new instance of the [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') with a specified error message, a [ParserExceptionContext](#T-MathExpressionParser-ParserExceptionContext 'MathExpressionParser.ParserExceptionContext') with information about this exception,
and a reference to the inner exception that is the cause of this exception.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| context | [MathExpressionParser.ParserExceptionContext](#T-MathExpressionParser-ParserExceptionContext 'MathExpressionParser.ParserExceptionContext') |  |
| innerException | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') |  |

<a name='M-MathExpressionParser-ParserException-#ctor-System-Runtime-Serialization-SerializationInfo,System-Runtime-Serialization-StreamingContext-'></a>

### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='P-MathExpressionParser-ParserException-Context'></a>

### Context `property`

##### Summary

Represents extra information regarding this instance of [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException').

<a name='M-MathExpressionParser-ParserException-GetObjectData-System-Runtime-Serialization-SerializationInfo,System-Runtime-Serialization-StreamingContext-'></a>

### GetObjectData() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-MathExpressionParser-ParserExceptionContext'></a>

## ParserExceptionContext `type`

##### Namespace

MathExpressionParser

##### Summary

A record class that contains additional information regarding a [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Position | [T:MathExpressionParser.ParserExceptionContext](#T-T-MathExpressionParser-ParserExceptionContext 'T:MathExpressionParser.ParserExceptionContext') | The position in the [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') where the [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') occurs. Set this to a value less than zero if it is not applicable. |

<a name='M-MathExpressionParser-ParserExceptionContext-#ctor-System-Int32,MathExpressionParser-ParserExceptionType-'></a>

### #ctor(Position,Type) `constructor`

##### Summary

A record class that contains additional information regarding a [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| Position | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The position in the [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') where the [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') occurs. Set this to a value less than zero if it is not applicable. |
| Type | [MathExpressionParser.ParserExceptionType](#T-MathExpressionParser-ParserExceptionType 'MathExpressionParser.ParserExceptionType') | The type of error that happened. |

<a name='P-MathExpressionParser-ParserExceptionContext-Position'></a>

### Position `property`

##### Summary

The position in the [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') where the [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') occurs. If this is not applicable, it will be less than zero.

<a name='P-MathExpressionParser-ParserExceptionContext-Type'></a>

### Type `property`

##### Summary

The kind of error that caused a [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') to be thrown.

<a name='T-MathExpressionParser-ParserExceptionType'></a>

## ParserExceptionType `type`

##### Namespace

MathExpressionParser

##### Summary

Represents the kind of error that caused a [ParserException](#T-MathExpressionParser-ParserException 'MathExpressionParser.ParserException') to be thrown.

<a name='F-MathExpressionParser-ParserExceptionType-ConflictingNames'></a>

### ConflictingNames `constants`

##### Summary

An error where two [Operator](#T-MathExpressionParser-Operator 'MathExpressionParser.Operator')s in a [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') share the same name.

<a name='F-MathExpressionParser-ParserExceptionType-IncorrectArgumentCount'></a>

### IncorrectArgumentCount `constants`

##### Summary

An error where either too many or too few arguments were passed to a [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator').

<a name='F-MathExpressionParser-ParserExceptionType-InvalidCustomConstantName'></a>

### InvalidCustomConstantName `constants`

##### Summary

An error where some of the custom constants provided have names that either start with a number, are empty, or contain characters that are not alphanumeric or are not underscores.

<a name='F-MathExpressionParser-ParserExceptionType-InvalidCustomFunctionName'></a>

### InvalidCustomFunctionName `constants`

##### Summary

An error where some of the custom functions provided have names that either start with a number, are empty, or contain characters that are not alphanumeric or are not underscores.

<a name='F-MathExpressionParser-ParserExceptionType-InvalidNumberFormat'></a>

### InvalidNumberFormat `constants`

##### Summary

An error where a number with an invalid format was found in a [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression').

<a name='F-MathExpressionParser-ParserExceptionType-NaNConstant'></a>

### NaNConstant `constants`

##### Summary

An error where [CustomConstants](#P-MathExpressionParser-MathExpression-CustomConstants 'MathExpressionParser.MathExpression.CustomConstants') have a [Value](#P-MathExpressionParser-ConstantOperator-Value 'MathExpressionParser.ConstantOperator.Value') of [NaN](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double.NaN 'System.Double.NaN').

<a name='F-MathExpressionParser-ParserExceptionType-NullCustomConstant'></a>

### NullCustomConstant `constants`

##### Summary

An error where [CustomConstants](#P-MathExpressionParser-MathExpression-CustomConstants 'MathExpressionParser.MathExpression.CustomConstants') contain a `null` element.

<a name='F-MathExpressionParser-ParserExceptionType-NullCustomFunction'></a>

### NullCustomFunction `constants`

##### Summary

An error where [CustomFunctions](#P-MathExpressionParser-MathExpression-CustomFunctions 'MathExpressionParser.MathExpression.CustomFunctions') contains a `null` element.

<a name='F-MathExpressionParser-ParserExceptionType-TooManyOpeningParentheses'></a>

### TooManyOpeningParentheses `constants`

##### Summary

An error where a opening parenthesiis is used without a corresponding opening parenthesis.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedBinaryOperator'></a>

### UnexpectedBinaryOperator `constants`

##### Summary

An error where a [BinaryOperator](#T-MathExpressionParser-BinaryOperator 'MathExpressionParser.BinaryOperator') is used incorrectly.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedClosingParenthesis'></a>

### UnexpectedClosingParenthesis `constants`

##### Summary

An error where a closing parenthesis is used incorrectly, or where a closing parenthesis is used without a corresponding opening parenthesis.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedComma'></a>

### UnexpectedComma `constants`

##### Summary

An error where a comma is used incorrectly.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedConstantOperator'></a>

### UnexpectedConstantOperator `constants`

##### Summary

An error where a [ConstantOperator](#T-MathExpressionParser-ConstantOperator 'MathExpressionParser.ConstantOperator') is used incorrectly.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedFunctionalOperator'></a>

### UnexpectedFunctionalOperator `constants`

##### Summary

An error where a [FunctionalOperator](#T-MathExpressionParser-FunctionalOperator 'MathExpressionParser.FunctionalOperator') is used incorrectly.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedNewline'></a>

### UnexpectedNewline `constants`

##### Summary

An error where a [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') ended unexpectedly.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedNumber'></a>

### UnexpectedNumber `constants`

##### Summary

An error where a number is used incorrectly.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedOpeningParenthesis'></a>

### UnexpectedOpeningParenthesis `constants`

##### Summary

An error where a opening parenthesis is used incorrectly.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedPostfixUnaryOperator'></a>

### UnexpectedPostfixUnaryOperator `constants`

##### Summary

An error where a [PostfixUnaryOperator](#T-MathExpressionParser-PostfixUnaryOperator 'MathExpressionParser.PostfixUnaryOperator') is used incorrectly.

<a name='F-MathExpressionParser-ParserExceptionType-UnexpectedPrefixUnaryOperator'></a>

### UnexpectedPrefixUnaryOperator `constants`

##### Summary

An error where a [PrefixUnaryOperator](#T-MathExpressionParser-PrefixUnaryOperator 'MathExpressionParser.PrefixUnaryOperator') is used incorrectly.

<a name='F-MathExpressionParser-ParserExceptionType-UnknownOperator'></a>

### UnknownOperator `constants`

##### Summary

An error where a [Operator](#T-MathExpressionParser-Operator 'MathExpressionParser.Operator') in a [MathExpression](#T-MathExpressionParser-MathExpression 'MathExpressionParser.MathExpression') is not found.
