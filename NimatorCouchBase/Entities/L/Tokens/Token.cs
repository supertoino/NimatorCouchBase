namespace NimatorCouchBase.Entities.L.Tokens
{
    public class Token
    {
        public Token(TokenType pType, string pValue)
        {
            Type = pType;
            Value = pValue;
        }

        public TokenType Type { get; }
        public string Value { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Value;
        }
    }
}