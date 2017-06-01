// 
// NimatorCouchBase - NimatorCouchBase - LLexer.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/30/14:55
// LAST HEADER UPDATE: 2017 /06/01/15:52
// 

#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
using NimatorCouchBase.NimatorBooster.L.Parser;
using NimatorCouchBase.NimatorBooster.L.Tokens;

#endregion

namespace NimatorCouchBase.NimatorBooster.L.Lexical
{
    public class LLexer : IEnumerator<LToken>
    {
        private readonly Dictionary<string, LTokenType> Functions;
        private readonly string TextToTokenize;
        private int CurrentIndex;
        private LToken CurrrentLToken;

        public LLexer(string pText)
        {
            Reset();
            TextToTokenize = CleanTextToTokenize(pText);

            Functions = new Dictionary<string, LTokenType>();
            RegisterTokenTypesFunctions();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
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
        public LToken Current => CurrrentLToken;

        /// <summary>
        ///     Gets the current element in the collection.
        /// </summary>
        /// <returns>
        ///     The current element in the collection.
        /// </returns>
        object IEnumerator.Current => Current;

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
            AdvanceCurrentIndex();
            if (!IndexIsWithinCodeBounds(GetCurrentIndex()))
            {
                return false;
            }
            SetNextToken();
            return true;
        }

        private void SetNextToken()
        {
            char currentChar = TextToTokenize[GetCurrentIndex()];
            if (CharIsFunction(currentChar))
            {
                if (NextPositionIsFunction(currentChar)) //Because of expressions using >= and <=
                {
                    CreateFunctionTokenWithTwoCharacters(currentChar);
                    return;
                }
                CreateFunctionTokenWithOneCaracther(currentChar);
                return;
            }
            if (FunctionWithTwoCaracters(currentChar))
            //Because of expressions using != (like 1!=2). Operations using two chars.
            {
                CreateFunctionTokenWithTwoCharacters(currentChar);
                return;
            }
            if (CharIsLetter(currentChar))
            {
                CreateVariableToken();
                return;
            }
            if (CharIsNumber(currentChar))
            {
                CreateScalarToken();
                return;
            }
            throw new UnableToParseLTokenTypeException($"Unexpected Character {currentChar} to parse.");
        }

        private bool IndexIsWithinCodeBounds(int pCurrentIndex)
        {
            return pCurrentIndex < (TextToTokenize.Length);
        }

        private static string CleanTextToTokenize(string pCode)
        {
            var cleanCode = pCode.Replace(" ", string.Empty);
            return cleanCode;
        }

        private void RegisterTokenTypesFunctions()
        {
            foreach (LTokenType type in Enum.GetValues(typeof (LTokenType)))
            {
                var value = type.GetFunctionSyntax();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Functions.Add(value, type);
                }
            }
        }
                                     
        #region L Token Creators

        private void CreateScalarToken()
        {
            var factorValue = GetScalarValue();
            SetCurrentLToken(ScalarIsDecimal(factorValue)
                ? new LToken(LTokenType.Double, factorValue)
                : new LToken(LTokenType.Long, factorValue));
        }

        private void CreateVariableToken()
        {
            var variableName = GetVariableName();
            SetCurrentLToken(new LToken(LTokenType.Variable, variableName));
        }

        private void CreateFunctionTokenWithOneCaracther(char pCurrentChar)
        {
            var value = Convert.ToString(pCurrentChar);
            SetCurrentLToken(new LToken(Functions[value], value));
        }

        private void CreateFunctionTokenWithTwoCharacters(char pCurrentChar)
        {
            var function = pCurrentChar + TextToTokenize[GetCurrentIndex() + 1].ToString();
            SetCurrentLToken(new LToken(Functions[function], function));
            AdvanceCurrentIndex();
        }

        #endregion

        #region Lexer Char Helpers 

        private string GetCharactersUntil(Func<char, bool> pStopFunction, int pValueStartIndex, string pTextToSubstring)
        {
            int valueStartIndex = pValueStartIndex;
            int searchIndex = valueStartIndex;
            var charsToTake = 1;
            while (searchIndex < pTextToSubstring.Length - 1)
            {
                var currentChar = pTextToSubstring[searchIndex];
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
            string scalarValue = GetSubstring(valueStartIndex, charsToTake, pTextToSubstring);
            SetCurrentIndex(searchIndex);
            return scalarValue;
        }

        private static string GetSubstring(int pInitialIndex, int pCharsToTake, string pTextToSubstring)
        {
            return pTextToSubstring.Substring(pInitialIndex, pCharsToTake);
        }

        private string GetScalarValue()
        {
            return GetCharactersUntil(CharIsNotNumber, GetCurrentIndex(), TextToTokenize);
        }

        private string GetVariableName()
        {
            return GetCharactersUntil(CharIsNotLetterNorNumber, GetCurrentIndex(), TextToTokenize);
        }

        private bool FunctionWithTwoCaracters(char pCurrentChar)
        {
            return NextPositionIsFunction(pCurrentChar);
        }

        private bool NextPositionIsFunction(char pCurrentChar)
        {
            var nextIndex = GetCurrentIndex() + 1;
            var nextPositionFunction = IndexIsWithinCodeBounds(nextIndex) &&
                                       CharIsFunction("" + pCurrentChar + TextToTokenize[nextIndex]);
            return nextPositionFunction;
        }

        private static bool CharIsLetter(char pCurrentChar)
        {
            return char.IsLetter(pCurrentChar);
        }

        private bool CharIsFunction(char pCurrentChar)
        {
            return Functions.ContainsKey(Convert.ToString(pCurrentChar));
        }

        private bool CharIsFunction(string pCurrentChar)
        {
            return Functions.ContainsKey(pCurrentChar);
        }

        private static bool ScalarIsDecimal(string pFactorValue)
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

        private static bool CharIsNotLetterNorNumber(char pCurrentChar)
        {
            return !char.IsLetter(pCurrentChar) && pCurrentChar != '.' && !char.IsDigit(pCurrentChar);
        }

        #endregion

        #region Iterator Variables Action Functions

        private int GetCurrentIndex()
        {
            return CurrentIndex;
        }

        public void AdvanceCurrentIndex()
        {
            CurrentIndex++;
        }

        public void SetCurrentIndex(int pValue)
        {
            CurrentIndex = pValue;
        }

        public void SetCurrentLToken(LToken pNewLToken)
        {
            CurrrentLToken = pNewLToken;
        }

        #endregion
    }
}