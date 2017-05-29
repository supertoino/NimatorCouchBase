// 
// NimatorCouchBase - NimatorCouchBase - LLexer.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/28/00:29
// LAST HEADER UPDATE: 2017 /05/29/19:38
// 

#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
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
            CurrentIndex++;            
            if (!IndexIsWithinCodeBounds(CurrentIndex))
            {                
                return false;
            }            
            SetNextToken();
            return true;
        }

        private void SetNextToken()
        {
            char currentChar = TextToTokenize[CurrentIndex];            
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
            if (FunctionWithTwoCaracters(currentChar)) //Because of expressions using != (like 1!=2). Operations using two chars.
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
                CreateScalerToken();
                return;
            }
            throw new Exception($"Unexpected Character {currentChar} to parse.");          
        }
        
        private bool FunctionWithTwoCaracters(char pCurrentChar)
        {
            return NextPositionIsFunction(pCurrentChar);
        }

        private void CreateScalerToken()
        {
            var factorValue = GetScalarValue();
            CurrrentLToken = ScalarIsDecimal(factorValue)
                ? new LToken(LTokenType.Double, factorValue)
                : new LToken(LTokenType.Long, factorValue);
        }

        private void CreateVariableToken()
        {
            var variableName = GetVariableName();
            CurrrentLToken = new LToken(LTokenType.Variable, variableName);
        }

        private void CreateFunctionTokenWithOneCaracther(char pCurrentChar)
        {
            CurrrentLToken = new LToken(Functions[Convert.ToString(pCurrentChar)], Convert.ToString(pCurrentChar));
        }

        private void CreateFunctionTokenWithTwoCharacters(char pCurrentChar)
        {
            var function = pCurrentChar + TextToTokenize[CurrentIndex + 1].ToString();
            CurrrentLToken = new LToken(Functions[function], function);
            CurrentIndex++;
        }

        private bool NextPositionIsFunction(char pCurrentChar)
        {
            var nextIndex = CurrentIndex + 1;
            var nextPositionFunction = IndexIsWithinCodeBounds(nextIndex) &&
                                       CharIsFunction("" + pCurrentChar + TextToTokenize[nextIndex]);
            return nextPositionFunction;
        }

        private void SetNextTokenAsEofAndEndIteration()
        {
            CurrrentLToken = new LToken(LTokenType.Eof, string.Empty);
            CurrentIndex = TextToTokenize.Length;
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
            CurrentIndex = searchIndex;
            return scalarValue;
        }

        private static string GetSubstring(int pInitialIndex, int pCharsToTake, string pTextToSubstring)
        {
            return pTextToSubstring.Substring(pInitialIndex, pCharsToTake);
        }

        private string GetScalarValue()
        {
            return GetCharactersUntil(CharIsNotNumber, CurrentIndex, TextToTokenize);
        }

        private string GetVariableName()
        {
            return GetCharactersUntil(CharIsNotLetterNorNumber, CurrentIndex, TextToTokenize);
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
    }
}