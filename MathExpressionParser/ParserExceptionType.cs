namespace MathExpressionParser;

/// <summary>
/// Represents the kind of error that caused a <see cref="ParserException"/> to be thrown.
/// </summary>
public enum ParserExceptionType
{
    /// <summary>
    /// An error where a number with an invalid format was found in a <see cref="MathExpression"/>.
    /// </summary>
    InvalidNumberFormat,

    /// <summary>
    /// An error where either too many or too few arguments were passed to a <see cref="FunctionalOperator"/>.
    /// </summary>
    IncorrectArgumentCount,

    /// <summary>
    /// An error where some of the custom functions provided have names that either start with a number, are empty, or contain characters that are not alphanumeric or are not underscores.
    /// </summary>
    InvalidCustomFunctionName,

    /// <summary>
    /// An error where <see cref="MathExpression.CustomFunctions"/> contains a <see langword="null"/> element.
    /// </summary>
    NullCustomFunction,

    /// <summary>
    /// An error where some of the custom constants provided have names that either start with a number, are empty, or contain characters that are not alphanumeric or are not underscores.
    /// </summary>
    InvalidCustomConstantName,

    /// <summary>
    /// An error where <see cref="MathExpression.CustomConstants"/> contains a <see langword="null"/> element.
    /// </summary>
    NullCustomConstant,

    /// <summary>
    /// An error where <see cref="MathExpression.CustomFunctions"/> contains <see cref="FunctionalOperator"/>s with the same name.
    /// </summary>
    DuplicateCustomFunctions,

    /// <summary>
    /// An error where <see cref="MathExpression.CustomConstants"/> contains <see cref="ConstantOperator"/>s with the same name.
    /// </summary>
    DuplicateCustomConstants,

    /// <summary>
    /// An error where a <see cref="BinaryOperator"/> is used incorrectly.
    /// </summary>
    UnexpectedBinaryOperator,

    /// <summary>
    /// An error where a opening parenthesiis is used without a corresponding opening parenthesis.
    /// </summary>
    TooManyOpeningParentheses,

    /// <summary>
    /// An error where a closing parenthesis is used incorrectly, or where a closing parenthesis is used without a corresponding opening parenthesis.
    /// </summary>
    UnexpectedClosingParenthesis,

    /// <summary>
    /// An error where a comma is used incorrectly.
    /// </summary>
    UnexpectedComma,

    /// <summary>
    /// An error where a <see cref="ConstantOperator"/> is used incorrectly.
    /// </summary>
    UnexpectedConstantOperator,

    /// <summary>
    /// An error where a <see cref="FunctionalOperator"/> is used incorrectly.
    /// </summary>
    UnexpectedFunctionalOperator,

    /// <summary>
    /// An error where a number is used incorrectly.
    /// </summary>
    UnexpectedNumber,

    /// <summary>
    /// An error where a opening parenthesis is used incorrectly.
    /// </summary>
    UnexpectedOpeningParenthesis,

    /// <summary>
    /// An error where a <see cref="PostfixUnaryOperator"/> is used incorrectly.
    /// </summary>
    UnexpectedPostfixUnaryOperator,

    /// <summary>
    /// An error where a <see cref="PrefixUnaryOperator"/> is used incorrectly.
    /// </summary>
    UnexpectedPrefixUnaryOperator,

    /// <summary>
    /// An error where a <see cref="Operator"/> in a <see cref="MathExpression"/> is not found.
    /// </summary>
    UnknownOperator,

    /// <summary>
    /// An error where a <see cref="MathExpression"/> ended unexpectedly.
    /// </summary>
    UnexpectedNewline
}
