// 
// NimatorCouchBase - NimatorCouchBase - Lexer.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/27/10:15
// LAST HEADER UPDATE: 2017 /05/27/23:16
// 

#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
using NimatorCouchBase.Entities.L.Tokens;

#endregion

namespace NimatorCouchBase.Entities.L.Lexer
{
    public class Lexer : IEnumerator<Token>
    {
        private readonly Dictionary<string, TokenType> Functions;
        private readonly string TextToTokenize;
        private int CurrentIndex;
        private Token CurrrentToken;

        public Lexer(string pText)
        {
            Reset();
            TextToTokenize = CleanTextToTokenize(pText);

            Functions = new Dictionary<string, TokenType>();
            RegisterTokenTypesPunctuators();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        ///     true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of
        ///     the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public bool MoveNext()
        {
            if (++CurrentIndex > (TextToTokenize.Length - 1))
            {
                return false;
            }
            SetNextToken();
            return true;
        }

        /// <summary>
        ///     Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public void Reset()
        {
            CurrentIndex = -1;
        }

        /// <summary>
        ///     Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <returns>
        ///     The element in the collection at the current position of the enumerator.
        /// </returns>
        public Token Current => CurrrentToken;

        /// <summary>
        ///     Gets the current element in the collection.
        /// </summary>
        /// <returns>
        ///     The current element in the collection.
        /// </returns>
        object IEnumerator.Current => Current;

        private static string CleanTextToTokenize(string pCode)
        {            
            var cleanCode = pCode.Replace(" ",string.Empty);
            return cleanCode;
        }

        private void RegisterTokenTypesPunctuators()
        {
            foreach (TokenType type in Enum.GetValues(typeof (TokenType)))
            {
                var value = type.GetPunctuator();
                if (value != " ")
                {
                    Functions.Add(value, type);
                }
            }
        }

        private void SetNextToken()
        {
            char currentChar = TextToTokenize[CurrentIndex];            
            if (CharIsPunctuator(currentChar))
            {
                if (CurrentIndex + 1 < TextToTokenize.Length)
                {
                    if (CharIsPunctuator("" + currentChar + TextToTokenize[CurrentIndex + 1]))
                    {
                        var functionId = currentChar.ToString() + TextToTokenize[CurrentIndex + 1].ToString();
                        CurrrentToken = new Token(Functions[functionId], functionId);
                        CurrentIndex++;
                        return;
                    }
                }
                CurrrentToken = new Token(Functions[Convert.ToString(currentChar)], Convert.ToString(currentChar));
                return;
            }
            if (CurrentIndex + 1 < TextToTokenize.Length)
            {
                if (CharIsPunctuator("" + currentChar + TextToTokenize[CurrentIndex + 1]))
                {
                    var functionId = currentChar.ToString() + TextToTokenize[CurrentIndex + 1].ToString();
                    CurrrentToken = new Token(Functions[functionId], functionId);
                    CurrentIndex++;
                    return;
                }
            }
            if (CharIsLetter(currentChar))
            {
                var variableName = GetVariableName();
                CurrrentToken = new Token(TokenType.Variable, variableName);
                return;
            }
            if (CharIsNumber(currentChar))
            {
                var factorValue = GetScalarValue();
                if (NumberFactorIsDecimal(factorValue))
                {
                    CurrrentToken = new Token(TokenType.Double, factorValue);
                }
                else
                {
                    CurrrentToken = new Token(TokenType.Long, factorValue);
                }
                
                return;
            }            
            CurrrentToken = new Token(TokenType.Eof, string.Empty);
        }

        private static bool NumberFactorIsDecimal(string pFactorValue)
        {
            return pFactorValue.Contains(".");
        }

        private static bool CharIsNumber(char pCurrentChar)
        {
            return char.IsDigit(pCurrentChar);
        }

        private static bool CharIsNotNumber(char pCurrentChar)
        {
            return !char.IsDigit(pCurrentChar) && pCurrentChar != '.';
        }

        private static bool CharIsNotLetter(char pCurrentChar)
        {
            return !char.IsLetter(pCurrentChar) && pCurrentChar != '.';
        }

        private string GetCharactersUntil(Func<char, bool> pStopFunction)
        {
            int valueStartIndex = CurrentIndex;
            int searchIndex = valueStartIndex;
            var charsToTake = 1;
            while (searchIndex < TextToTokenize.Length - 1)
            {
                var currentChar = TextToTokenize[searchIndex];
                if (pStopFunction(currentChar))
                {
                    //currentIndex belongs to stop function. We want the previous index
                    charsToTake--;
                    searchIndex--;
                    break;
                }
                charsToTake++;
                searchIndex++;
            }
            string scalarValue = GetSubstring(valueStartIndex, charsToTake, TextToTokenize);
            CurrentIndex = searchIndex;
            return scalarValue;
        }

        private static string GetSubstring(int pInitialIndex, int pCharsToTake, string pTextToSubstring)
        {
            return pTextToSubstring.Substring(pInitialIndex, pCharsToTake);
        }

        private string GetScalarValue()
        {
            return GetCharactersUntil(CharIsNotNumber);
        }

        private string GetVariableName()
        {
            return GetCharactersUntil(CharIsNotLetter);
        }

        private static bool CharIsLetter(char pCurrentChar)
        {
            return char.IsLetter(pCurrentChar);
        }

        private bool CharIsPunctuator(char pCurrentChar)
        {
            return Functions.ContainsKey(Convert.ToString(pCurrentChar));
        }

        private bool CharIsPunctuator(string pCurrentChar)
        {
            return Functions.ContainsKey(pCurrentChar);
        }
    }
}