using System;

namespace MathExpressionParser;

[Flags]
internal enum ParserState
{
    ExpectingNumber = 1 << 0,
    ExpectingConstant = 1 << 1,
    ExpectingFunctionalOperator = 1 << 2,
    ExpectingBinaryOperator = 1 << 3,
    ExpectingPrefixUnaryOperator = 1 << 4,
    ExpectingPostfixUnaryOperator = 1 << 5,
    ExpectingOpeningParenthesis = 1 << 6,
    ExpectingClosingParenthesis = 1 << 7,
    ExpectingComma = 1 << 8,
    ExpectingNewline = 1 << 9,
    InFunction = 1 << 10,
    ExpectingOperand = ExpectingNumber | ExpectingConstant | ExpectingFunctionalOperator | ExpectingOpeningParenthesis,
    Start = ExpectingOperand | ExpectingPrefixUnaryOperator,
    AfterNumber = ExpectingBinaryOperator | ExpectingPostfixUnaryOperator | ExpectingClosingParenthesis | ExpectingComma,
    AfterPostfix = AfterNumber,
    AfterClosingParenthesis = AfterNumber
}