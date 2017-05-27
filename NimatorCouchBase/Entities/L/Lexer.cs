using System;
using System.Collections;
using System.Collections.Generic;

namespace NimatorCouchBase.Entities.L
{
    public class Lexer : IEnumerator<Token>
    {
        private int CurrentIndex;
        private readonly string TextToLex;
        private Token CurrrentToken;
        private readonly Dictionary<char, TokenType> Puctuators;

        public Lexer(string pText)
        {
            CurrentIndex = 0;
            TextToLex = pText;

            Puctuators = new Dictionary<char, TokenType>();
            RegisterTokenTypesPunctuators();
        }

        private void RegisterTokenTypesPunctuators()
        {
            foreach (TokenType type in Enum.GetValues(typeof(TokenType)))
            {
                var value = type.GetPunctuator();
                if (value != ' ')
                {
                    Puctuators.Add(value, type);
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            return;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public bool MoveNext()
        {
            if (++CurrentIndex < TextToLex.Length)
            {
                return false;
            }
            SetNextToken();
            return true;
        }

        private void SetNextToken()
        {
            char currentChar = TextToLex[CurrentIndex];

            if (CharIsPunctuator(currentChar))
            {
                CurrrentToken = new Token(TokenType.Asterisk, Convert.ToString(TextToLex[CurrentIndex]));
                return;
            }
            if (CharIsLetter(currentChar))
            {
                var variableName = GetVariableName();
                CurrrentToken = new Token(TokenType.Variable, variableName);
                return;
            }
            else
            {
                //Ignore (Whitespaces)
            }
            CurrrentToken = new Token(TokenType.Eof, string.Empty);
        }

        private string GetVariableName()
        {
            int nameStartIndex = CurrentIndex;
            while (CurrentIndex < TextToLex.Length)
            {
                if (!char.IsLetter(TextToLex[CurrentIndex]))
                {
                    break;
                }
                CurrentIndex++;
            }
            string name = TextToLex.Substring(nameStartIndex, CurrentIndex);
            return name;
        }

        private static bool CharIsLetter(char pCurrentChar)
        {
            return char.IsLetter(pCurrentChar);
        }

        private bool CharIsPunctuator(char pCurrentChar)
        {
            return Puctuators.ContainsKey(pCurrentChar);
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public void Reset()
        {
            CurrentIndex = -1;
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <returns>
        /// The element in the collection at the current position of the enumerator.
        /// </returns>
        public Token Current => CurrrentToken;

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>
        /// The current element in the collection.
        /// </returns>
        object IEnumerator.Current => Current;
    }
}