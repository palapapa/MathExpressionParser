using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEP
{
    public class MathExpressionParser
    {
        protected static IList<string> Tokenize(string input)
        {
            IList<string> tokens = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].IsDigit())
                {
                    for (int j = i; j < input.Length; j++)
                    {
                        if (!input[j].IsDigit() && input[j] != '.' && input[j] != 'E')
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
                else if (input[i].IsWhiteSpace())
                {
                    continue;
                }
                else
                {
                    tokens.Add(input[i].ToString());
                }
            }
            return tokens;
        }

        protected static string UnpackTokens(IList<string> tokens, string delimiter = "")
        {
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
        protected static int GetIndexInStringFromTokens(IList<string> tokens, int token, int index, int delimiterLength = 0)
        {
            int result = 0;
            for (int i = 0; i <= token; i++)
            {
                result += tokens[i].Length;
                if (i != token)
                {
                    result += delimiterLength;
                }
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
        protected static int GetClosingParenthesisIndex(IList<string> tokens, int start)
        {
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
                    throw new ArgumentException("Too many opening parentheses", nameof(tokens));
                }
            }
            throw new Exception("This exception should never be thrown");
        }

        protected static int GetOpeningParenthesisIndex(IList<string> tokens, int end)
        {
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
                    throw new ArgumentException("Too many closing parentheses", nameof(tokens));
                }
            }
            throw new Exception("This exception should never be thrown");
        }

        public static string Parse(string input)
        {
            return Parse(input, MathOperator.GetDefaultOperators());
        }

        public static string Parse(string input, IList<MathOperator> operators)
        {
            string RealParse(IList<string> tokens)
            {
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i] == "(")
                    {
                        int closeParenthesisIndex = GetClosingParenthesisIndex(tokens, i);
                        if
                        (
                            operators.Any
                            (
                                (MathOperator @operator) =>
                                {
                                    return @operator is FunctionMathOperator && @operator.Name == tokens[i == 0 ? 0 : i - 1]; // if the previous token before the parenthesis is a function
                                }
                            )
                        )
                        {
                            if (i == 1) // if the function is the first token
                            {
                                int totalCommas = 0, commas = 0;
                                for (int j = 2, start = 2; j < tokens.Count - 1; j++) // extract arguments
                                {
                                    if (tokens[j] == ",")
                                    {
                                        commas++;
                                        start = j + 1;
                                    }
                                    if ((tokens[j] == "," && commas > totalCommas) || j == tokens.Count - 2)
                                    {
                                        tokens = tokens.ReplaceRange
                                        (
                                            RealParse
                                            (
                                                tokens.GetRange
                                                (
                                                    start,
                                                    j == tokens.Count - 2 ? j - start + 1 : j - start
                                                )
                                            ),
                                            start,
                                            j == tokens.Count - 2 ? j - start + 1 : j - start
                                        );
                                        j = 1;
                                        start = 2; // reset to start over because the indices have now changed
                                        if (totalCommas < commas)
                                        {
                                            totalCommas = commas;
                                        }
                                        commas = 0;
                                    }
                                }
                            }
                            else
                            {
                                tokens = tokens.ReplaceRange
                                (
                                    RealParse
                                    (
                                        tokens.GetRange // starting from the function operator to the closing parenthesis
                                        (
                                            i - 1,
                                            closeParenthesisIndex - (i - 1) + 1
                                        )
                                    ),
                                    i - 1,
                                    closeParenthesisIndex - (i - 1) + 1
                                );
                            }
                        }
                        else // normal parenthesis
                        {
                            tokens = tokens.ReplaceRange
                            (
                                RealParse
                                (
                                    tokens.GetRange // starting from the opening parenthesis + 1 to the closing parenthesis - 1
                                    (
                                        i + 1,
                                        closeParenthesisIndex - (i + 1)
                                    )
                                ),
                                i,
                                closeParenthesisIndex - i + 1
                            );
                        }
                    }
                }
                /*
                 * At this point, after all the recursive algorithm above, we can be certain of a few properties of the expression we are about to parse:
                 * - If it contains a function, the function must be the first token in the expression and there are no other functions in the expression.
                 * - All parentheses(excluding the ones belonging to a function) have been resolved.
                 * - No nested functions.
                 * - All function arguments have been evaluated, meaning that each argument contains only one token and it is a number.
                 */
                MathOperatorPrecedence[] precedences = typeof(MathOperatorPrecedence).GetEnumValues().Cast<MathOperatorPrecedence>().Reverse().ToArray();
                foreach (MathOperatorPrecedence precedence in precedences)
                {
                    for (int i = 0; i < tokens.Count; i++)
                    {
                        if (tokens[i].IsDouble() || tokens[i].IsParenthesis())
                        {
                            continue;
                        }
                        else
                        {
                            if (!MathOperator.IsValidOperatorName(tokens[i]))
                            {
                                throw new ArgumentException($"\"{tokens[i]}\" is not a valid operator name", nameof(input));
                            }
                            else
                            {
                                
                            }
                        }
                    }
                }
                if (tokens.Count != 1)
                {
                    throw new ArgumentException("There are missing operators somewhere in the expression causing it to be unresolvable", nameof(input));
                }
                else
                {
                    return tokens[0];
                }
            }

            if (input is null)
            {
                throw new ArgumentNullException($"{nameof(input)}", "Is null");
            }
            else if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }
            IList<string> tokens = Tokenize(input);
            for (int i = 0; i < tokens.Count; i++)
            {
                // throws an exception if a matching parenthesis isn't found, indicating that input has unmatching parentheses
                if (tokens[i] == "(")
                {
                    GetClosingParenthesisIndex(tokens, i);
                }
                else if (tokens[i] == ")")
                {
                    GetOpeningParenthesisIndex(tokens, i);
                }
            }
            IList<MathOperator> invalidOperators = MathOperator.GetInvalidOperators(operators);
            if (invalidOperators.Count != 0)
            {
                string invalidNames = string.Empty;
                foreach (MathOperator @operator in invalidOperators)
                {
                    invalidNames += $"{@operator.Name} ";
                }
                throw new ArgumentException($"Invalid {nameof(MathOperator)} names: {invalidNames}", nameof(operators));
            }
            return RealParse(tokens);
        }
    }
}