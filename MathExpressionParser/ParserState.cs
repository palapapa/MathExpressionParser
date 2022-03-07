using System;

namespace MathExpressionParser;

[Flags]
internal enum ParserState
{
    ExpectingNumber = 1 << 0,
    ExpectingConstant = 1 << 1,
    ExpectingFunctionalOperator = 1 << 2,
    ExpectingBinaryOperator = 1 << 3,
    ExpectingUnaryPrefixOperator = 1 << 4,
    ExpectingUnaryPostfixOperator = 1 << 5,
    ExpectingOpeningParenthesis = 1 << 6,
    ExpectingClosingParenthesis = 1 << 7,
    ExpectingComma = 1 << 8,
    ExpectingNewline = 1 << 9
}