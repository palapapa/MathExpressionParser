using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEP;

public class MathExpressionParser
{
    protected static List<string> Tokenize(string input)
    {
        if (input is null)
        {
            throw new ArgumentNullException($"{nameof(input)}");
        }
        List<string> tokens = new();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i].IsDigit())
            {
                bool hasEAppeared = false;
                for (int j = i; j < input.Length; j++)
                {
                    if (input[j] is 'E' or 'e')
                    {
                        if (hasEAppeared)
                        {
                            throw new FormatException($"Unexpected scientific notation at position {j}.");
                        }
                        hasEAppeared = true;
                    }
                    if (input[j] is ' ' or ')' or ',')
                    {
                        tokens.Add(input[i..j]);
                        i = j - 1;
                        break;
                    }
                    else if (!input[j].IsDigit() && input[j] != '.' && input[j] != 'E' && input[j] != 'e')
                    {
                        throw new FormatException($"Invalid number format at position {i}. If you are trying to use a {nameof(PostfixUnaryMathOperator)}, please put a space between the number and the {nameof(MathOperator)}.");
                    }
                    else if (j == input.Length - 1)
                    {
                        tokens.Add(input[i..(j + 1)]);
                        i = j;
                        break;
                    }
                }
                try
                {
                    double.Parse(tokens.Last());
                }
                catch (FormatException)
                {
                    throw new FormatException($"Invalid number format at position {i}");
                }
            }
            else if (input[i].IsLetter() || input[i] == '_')
            {
                for (int j = i; j < input.Length; j++)
                {
                    if (!input[j].IsDigit() && !input[j].IsLetter() && input[j] != '_')
                    {
                        tokens.Add(input[i..j]);
                        i = j - 1;
                        break;
                    }
                    else if (j == input.Length - 1)
                    {
                        tokens.Add(input[i..(j + 1)]);
                        i = j;
                        break;
                    }
                }
            }
            else // whitespaces or symbols
            {
                tokens.Add(input[i].ToString());
            }
        }
        return tokens;
    }

    protected static string UnpackTokens(List<string> tokens, string delimiter = "")
    {
        if (tokens is null)
        {
            throw new ArgumentNullException($"{nameof(tokens)}");
        }
        if (delimiter is null)
        {
            throw new ArgumentNullException($"{nameof(delimiter)}");
        }
        StringBuilder result = new();
        for (int i = 0; i < tokens.Count; i++)
        {
            result.Append(tokens[i]);
            if (i != tokens.Count - 1)
            {
                result.Append(delimiter);
            }
        }
        return result.ToString();
    }

    /// <summary>
    /// Get the index of the <paramref name="index"/>-th <see langword="char"/> in the <paramref name="token"/>-th token as if the tokens were merged into a single string.
    /// </summary>
    /// <param name="tokens">The target tokens.</param>
    /// <param name="token">The index of the token to get the index from.</param>
    /// <param name="index">The index of the <see langword="char"/> in the token to get the index from.</param>
    /// <param name="delimiterLength">The length of the delimiter to add to the string of the unpacked <paramref name="tokens"/> between each tokens before the result is computed.</param>
    /// <returns>The index of the <paramref name="index"/>-th <see langword="char"/> in the <paramref name="token"/>-th token as if the tokens were merged into a single string.</returns>
    protected static int GetIndexInStringFromTokens(List<string> tokens, int token, int index, int delimiterLength = 0)
    {
        if (tokens is null)
        {
            throw new ArgumentNullException($"{nameof(tokens)}");
        }
        if (token < 0 || token >= tokens.Count)
        {
            throw new ArgumentOutOfRangeException($"{nameof(token)} = {token} is out of range");
        }
        if (index < 0 || index >= tokens[token].Length)
        {
            throw new ArgumentOutOfRangeException($"{nameof(index)} = {index} is out of range");
        }
        int result = 0;
        for (int i = 0; i < token; i++)
        {
            result += tokens[i].Length + delimiterLength;
        }
        return result + index;
    }

    /// <summary>
    /// Finds the index of the corrosponding closing parenthesis of the specified opening parenthesis at index <paramref name="start"/>.
    /// </summary>
    /// <param name="tokens"></param>
    /// <param name="start"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns></returns>
    protected static int GetClosingParenthesisIndex(List<string> tokens, int start)
    {
        if (tokens is null)
        {
            throw new ArgumentNullException($"{nameof(tokens)}");
        }
        if (tokens[start] != "(")
        {
            throw new ArgumentException("Specified start index is not an open parenthesis", nameof(start));
        }
        int open = 0;
        for (int i = start; i < tokens.Count; i++)
        {
            if (tokens[i] == "(")
            {
                open++;
            }
            else if (tokens[i] == ")")
            {
                open--;
            }
            if (open == 0)
            {
                return i;
            }
            if (i == tokens.Count - 1 && open != 0)
            {
                throw new UnmatchedParenthesisException("Too many opening parentheses", nameof(tokens));
            }
        }
        throw new Exception("This exception should never be thrown");
    }

    protected static int GetOpeningParenthesisIndex(List<string> tokens, int end)
    {
        if (tokens is null)
        {
            throw new ArgumentNullException($"{nameof(tokens)}");
        }
        if (tokens[end] != ")")
        {
            throw new ArgumentException("Specified start index is not an closing parenthesis", nameof(end));
        }
        int close = 0;
        for (int i = end; i >= 0; i--)
        {
            if (tokens[i] == ")")
            {
                close++;
            }
            else if (tokens[i] == "(")
            {
                close--;
            }
            if (close == 0)
            {
                return i;
            }
            if (i == 0 && close != 0)
            {
                throw new UnmatchedParenthesisException("Too many closing parentheses", nameof(tokens));
            }
        }
        throw new Exception("This exception should never be thrown");
    }

    public static string Parse(string input)
    {
        return Parse(input, MathOperator.GetDefaultOperators());
    }

    public static string Parse(string input, IEnumerable<MathOperator> operators)
    {
        if (input is null)
        {
            throw new ArgumentNullException($"{nameof(input)}");
        }
        if (operators is null)
        {
            throw new ArgumentNullException($"{nameof(operators)}");
        }
        if (operators.Any(o => o is null))
        {
            throw new ArgumentException($"Null element in {nameof(operators)}");
        }
        List<string> invalidOperators = MathOperator.GetInvalidOperators(operators).Select(o => o.Name).ToList();
        StringBuilder invalidNameError = new();
        if (invalidOperators.Count != 0)
        {
            invalidNameError.AppendJoin(',', invalidOperators);
            invalidNameError = new($"Invalid {nameof(MathOperator)} names: {invalidNameError}");
            throw new InvalidMathOperatorNameException(invalidNameError.ToString(), nameof(operators));
        }
        StringBuilder duplicateError = new();
        // If there are two operators whose types and names are the same at the same time(operators with the same name but different types are allowed)
        List<string> duplicateOperators = operators.GroupBy(o => o.Name)
            .Where(grouping => grouping.Count() > 1 && grouping.GroupBy(o => o.GetType()).Any(typeGrouping => typeGrouping.Count() > 1))
            .Select(grouping => grouping.Key)
            .ToList();
        if (duplicateOperators.Count != 0)
        {
            duplicateError.AppendJoin(',', duplicateOperators);
            duplicateError = new($"Duplicate {nameof(MathOperator)}s: {duplicateError}");
            throw new DuplicateMathOperatorException(duplicateError.ToString(), nameof(operators));
        }
        StringBuilder constantsWithOverloadsError = new();
        List<string> constantsWithOverloads = operators.GroupBy(o => o.Name)
            .Where(grouping => grouping.Any(o => o.GetType() == typeof(ConstantMathOperator)) && grouping.Count() > 1)
            .Select(grouping => grouping.Key)
            .ToList();
        if (constantsWithOverloads.Count != 0)
        {
            constantsWithOverloadsError.AppendJoin(',', constantsWithOverloads);
            constantsWithOverloadsError = new($"{nameof(ConstantMathOperator)}s cannot have other overloads: {constantsWithOverloadsError}");
            throw new DuplicateMathOperatorException(constantsWithOverloadsError.ToString(), nameof(operators));
        }
        if (string.IsNullOrWhiteSpace(input))
        {
            return "";
        }
        List<string> tokens = Tokenize(input);
        List<string> notFoundOperators = new();
        foreach (string token in tokens)
        {
            if (!MathOperator.IsValidOperatorName(token))
            {
                continue;
            }
            else if (!operators.Any(o => o.Name == token))
            {
                notFoundOperators.Add(token);
            }
        }
        if (notFoundOperators.Count != 0)
        {
            notFoundOperators = notFoundOperators.GroupBy(s => s).Select(grouping => grouping.Key).ToList();
            throw new MathOperatorNotFoundException($"Operators {string.Join(',', notFoundOperators)} were not found.");
        }
        List<int> checkedIndices = new(); // checked closing parenthesis indices
        for (int i = 0; i < tokens.Count; i++)
        {
            // throws an exception if a matching parenthesis isn't found, indicating that input has unmatching parentheses
            if (tokens[i] == "(")
            {
                try
                {
                    int closing = GetClosingParenthesisIndex(tokens, i);
                    checkedIndices.Add(closing);
                }
                catch (UnmatchedParenthesisException)
                {
                    throw new UnmatchedParenthesisException($"\"(\" at position {GetIndexInStringFromTokens(tokens, i, 0)} is an unmatched opening parenthesis.");
                }
            }
            else if (tokens[i] == ")" && !checkedIndices.Any(index => index == i))
            {
                throw new UnmatchedParenthesisException($"\")\" at position {GetIndexInStringFromTokens(tokens, i, 0)} is an unmatched closing parenthesis.");
            }
        }
        List<MathOperator> operatorsList = operators.ToList();
        for (int i = 0; i < tokens.Count; i++)
        {
            if (!MathOperator.IsValidOperatorName(tokens[i]))
            {
                continue;
            }
            Type suitableMathOperatorType = GetSuitableMathOperatorType(tokens, i, operatorsList);
            if (!operators.Any(o => o.Name == tokens[i] && o.GetType() == suitableMathOperatorType))
            {
                throw new NoMatchingMathOperatorException($"No suitable overload of \"{tokens[i]}\" at position {GetIndexInStringFromTokens(tokens, i, 0)} was found. It requires a {suitableMathOperatorType}.");
            }
            if (suitableMathOperatorType == typeof(FunctionMathOperator))
            {
                if (i == tokens.Count - 1 || tokens[i + 1] != "(")
                {
                    throw new OperandNotFoundException($"Function \"{tokens[i]}\" at position {GetIndexInStringFromTokens(tokens, i, 0)} doesn't have a argument list.");
                }
                int opening = i + 1, closing = GetClosingParenthesisIndex(tokens, i + 1);
                if (tokens[opening + 1] == ",")
                {
                    throw new OperandNotFoundException($"Expected an argument at position {GetIndexInStringFromTokens(tokens, opening + 1, 0)}.");
                }
                if (tokens[closing - 1] == ",")
                {
                    throw new OperandNotFoundException($"Expected an argument at position {GetIndexInStringFromTokens(tokens, closing - 1, 0)}.");
                }
                for (int j = opening + 1; j < closing; j++)
                {
                    if (j != 0 && tokens[j] == "," && tokens[j - 1] == ",")
                    {
                        throw new OperandNotFoundException($"Expected an argument at position {GetIndexInStringFromTokens(tokens, j, 0)}.");
                    }
                }
            }
        }
        tokens.RemoveAll(s => s.IsWhitespace());
        return RealParse(tokens.ToList());

        string RealParse(List<string> tokens)
        {
            if (tokens.Count == 0)
            {
                return "";
            }
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == "(" && !operators.Any(@operator => @operator is FunctionMathOperator && @operator.Name == tokens.BoundElememtAt(i - 1))) // if the previous token isn't a function operator
                {
                    int closing = GetClosingParenthesisIndex(tokens, i);
                    tokens = tokens.ReplaceRange
                    (
                        RealParse
                        (
                            tokens.GetRange // starting from the opening parenthesis + 1 to the closing parenthesis - 1
                            (
                                i + 1,
                                closing - (i + 1)
                            )
                        ),
                        i,
                        closing - i + 1
                    );
                }
            }
            if (tokens.All(token => token.Length == 0))
            {
                return "";
            }
            /*
             * At this point, we can be certain of a few properties of the expression we are about to parse:
             * - All parentheses(excluding the ones belonging to a function) have been evaluated.
             * - No unmatched parentheses.
             * - No invalid operators.
             * - No operators that do not exist in the operators argument.
             * - No functions with an invalid argument list.
             */
            MathOperatorPrecedence[] precedences = typeof(MathOperatorPrecedence).GetEnumValues().Cast<MathOperatorPrecedence>().Reverse().ToArray();
            foreach (MathOperatorPrecedence currentPrecedence in precedences) // one pass for each precedence
            {
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (!MathOperator.IsValidOperatorName(tokens[i]))
                    {
                        continue;
                    }
                    Type suitableMathOperatorType = GetSuitableMathOperatorType(tokens, i, operatorsList);
                    MathOperator matchingOperator = null;
                    try
                    {
                        matchingOperator = operators.First(o => o.Name == tokens[i] && o.Precedence == currentPrecedence); // I use First here because it's guaranteed that there will only be exactly one match
                    }
                    catch (InvalidOperationException)
                    {
                        continue;
                    }
                    if (matchingOperator is BinaryMathOperator bmo && suitableMathOperatorType == typeof(BinaryMathOperator))
                    {
                        tokens = tokens.ReplaceRange(bmo.Calculate(tokens[i - 1], tokens[i + 1]), i - 1, 3);
                        i--;
                    }
                    else if (matchingOperator is ConstantMathOperator cmo && suitableMathOperatorType == typeof(ConstantMathOperator))
                    {
                        tokens[i] = cmo.Value;
                    }
                    else if (matchingOperator is FunctionMathOperator fmo && suitableMathOperatorType == typeof(FunctionMathOperator))
                    {
                        int opening = i + 1, closing = GetClosingParenthesisIndex(tokens, i + 1);
                        string[] arguments = string.Join(null, tokens.GetRange(opening + 1, closing - opening - 1)).Split(',', StringSplitOptions.TrimEntries);
                        for (int j = 0; j < arguments.Length; j++)
                        {
                            arguments[j] = Parse(arguments[j]);
                        }
                        tokens = tokens.ReplaceRange(fmo.Calculate(arguments), i, closing - i + 1);
                    }
                    else if (matchingOperator is PrefixUnaryMathOperator pumo && suitableMathOperatorType == typeof(PrefixUnaryMathOperator))
                    {
                        tokens = tokens.ReplaceRange(pumo.Calculate(tokens[i + 1]), i, 2);
                    }
                    else if (matchingOperator is PostfixUnaryMathOperator sumo && suitableMathOperatorType == typeof(PostfixUnaryMathOperator))
                    {
                        tokens = tokens.ReplaceRange(sumo.Calculate(tokens[i - 1]), i - 1, 2);
                        i--;
                    }
                    else
                    {
                        throw new NotSupportedException($"{matchingOperator.GetType()} is not a supported {nameof(MathOperator)} type.");
                    }
                    break; // if no exception was thrown
                }
            }
            if (tokens.Count != 1)
            {
                throw new InvalidExpressionException($"There are missing operators somewhere in the expression causing it to be unresolvable.(Stuck at {string.Join("", tokens)})", nameof(input));
            }
            else
            {
                return tokens[0];
            }
        }
    }
    protected static Type GetSuitableMathOperatorType(List<string> tokens, int index, List<MathOperator> operators)
    {
        if (tokens is null)
        {
            throw new ArgumentNullException(nameof(tokens));
        }
        if (operators is null)
        {
            throw new ArgumentNullException(nameof(operators));
        }
        if (index < 0 || index >= tokens.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        if (!MathOperator.IsValidOperatorName(tokens[index]))
        {
            return null;
        }
        if (operators.Any(o => o.Name == tokens[index] && o.GetType() == typeof(ConstantMathOperator)))
        {
            return typeof(ConstantMathOperator);
        }
        tokens = tokens.ToList();
        string lastToken = null, nextToken = null;
        for (int i = index - 1; i >= 0; i--)
        {
            if (!tokens[i].IsWhitespace())
            {
                lastToken = tokens[i];
                break;
            }
        }
        for (int i = index + 1; i < tokens.Count; i++)
        {
            if (!tokens[i].IsWhitespace())
            {
                nextToken = tokens[i];
                break;
            }
        }
        bool isLastTokenOperator = MathOperator.IsValidOperatorName(lastToken ?? "0"),
            isLastTokenOutOfBound = lastToken is null,
            isLastTokenOpeningParenthesis = lastToken is "(",
            isLastTokenClosingParenthesis = lastToken is ")",
            isLastTokenNumber = operators.Any(o => o.Name == lastToken && o.GetType() == typeof(ConstantMathOperator)) || (lastToken?.IsDouble() ?? false);
        bool isNextTokenOperator = MathOperator.IsValidOperatorName(nextToken ?? "0"),
            isNextTokenOutOfBound = nextToken is null,
            isNextTokenOpeningParenthesis = nextToken is "(",
            isNextTokenClosingParenthesis = nextToken is ")",
            isNextTokenNumber = operators.Any(o => o.Name == nextToken && o.GetType() == typeof(ConstantMathOperator)) || (nextToken?.IsDouble() ?? false);
        if ((isLastTokenNumber && isNextTokenNumber) ||
            (isLastTokenNumber && isNextTokenOpeningParenthesis) ||
            (isLastTokenClosingParenthesis && isNextTokenOperator) ||
            (isLastTokenClosingParenthesis && isNextTokenNumber) ||
            (isLastTokenClosingParenthesis && isNextTokenOpeningParenthesis))
        {
            return typeof(BinaryMathOperator);
        }
        else if ((isLastTokenOperator && isNextTokenOpeningParenthesis) ||
            (isLastTokenOpeningParenthesis && isNextTokenOpeningParenthesis))
        {
            return typeof(FunctionMathOperator);
        }
        else if ((isLastTokenOperator && isNextTokenNumber) ||
            (isLastTokenOpeningParenthesis && isNextTokenOperator) ||
            (isLastTokenOpeningParenthesis && isNextTokenNumber) ||
            (isLastTokenOutOfBound && isNextTokenOperator) ||
            (isLastTokenOutOfBound && isNextTokenNumber) ||
            (isLastTokenOutOfBound && isNextTokenOpeningParenthesis))
        {
            return typeof(PrefixUnaryMathOperator);
        }
        else if ((isLastTokenNumber && isNextTokenOperator) ||
            (isLastTokenNumber && isNextTokenClosingParenthesis) ||
            (isLastTokenNumber && isNextTokenOutOfBound) ||
            (isLastTokenClosingParenthesis && isNextTokenClosingParenthesis) ||
            (isLastTokenClosingParenthesis && isNextTokenOutOfBound))
        {
            return typeof(PostfixUnaryMathOperator);
        }
        else if ((isLastTokenOperator && isNextTokenOperator) ||
            (isLastTokenOperator && isNextTokenClosingParenthesis) ||
            (isLastTokenOperator && isNextTokenOutOfBound) ||
            (isLastTokenOpeningParenthesis && isNextTokenClosingParenthesis) ||
            (isLastTokenOutOfBound && isNextTokenOutOfBound))
        {
            return typeof(ConstantMathOperator);
        }
        else
        {
            return null;
        }
    }
}
