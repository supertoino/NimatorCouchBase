namespace NimatorCouchBase.NimatorBooster.L.Tokens
{
    public class LToken
    {
        public LToken(LTokenType pType, string pValue)
        {
            Type = pType;
            Value = pValue;
        }

        public LTokenType Type { get; }
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