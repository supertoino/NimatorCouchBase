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

#endregion

namespace NimatorCouchBase.Entities.L
{
    public class Lexer : IEnumerator<Token>
    {
        private readonly Dictionary<char, TokenType> Puctuators;
        private readonly string TextToLex;
        private int CurrentIndex;
        private Token CurrrentToken;

        public Lexer(string pText)
        {
            Reset();
            TextToLex = pText;

            Puctuators = new Dictionary<char, TokenType>();
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
            if (++CurrentIndex > (TextToLex.Length - 1))
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

        private void RegisterTokenTypesPunctuators()
        {
            foreach (TokenType type in Enum.GetValues(typeof (TokenType)))
            {
                var value = type.GetPunctuator();
                if (value != ' ')
                {
                    Puctuators.Add(value, type);
                }
            }
        }

        private void SetNextToken()
        {
            char currentChar = TextToLex[CurrentIndex];

            if (CharIsPunctuator(currentChar))
            {
                CurrrentToken = new Token(Puctuators[currentChar], Convert.ToString(TextToLex[CurrentIndex]));
                return;
            }
            if (CharIsLetter(currentChar))
            {
                var variableName = GetVariableName();
                CurrrentToken = new Token(TokenType.Variable, variableName);
                return;
            }
            if (CharIsNumber(currentChar))
            {
                var scalarValue = GetScalarValue();
                CurrrentToken = new Token(TokenType.Scalar, scalarValue);
                return;
            }
            if (CharIsWhitespace(currentChar))
            {
                //Ignore
            }
            CurrrentToken = new Token(TokenType.Eof, string.Empty);
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

        private static bool CharIsWhitespace(char pCurrentChar)
        {
            return char.IsWhiteSpace(pCurrentChar);
        }

        private string GetCharactersUntil(Func<char, bool> pStopFunction)
        {
            int valueStartIndex = CurrentIndex;
            int searchIndex = valueStartIndex;
            var charsToTake = 1;
            while (searchIndex < TextToLex.Length - 1)
            {
                var currentChar = TextToLex[searchIndex];
                if (pStopFunction(currentChar))
                {
                    charsToTake--;
                    searchIndex--;
                    break;
                }
                charsToTake++;
                searchIndex++;
            }
            string scalarValue = GetSubstring(valueStartIndex, charsToTake, TextToLex);
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
            return Puctuators.ContainsKey(pCurrentChar);
        }
    }
}