namespace NimatorCouchBase.Entities.L.Tokens
{
    public static class UtilsTokenType
    {
        public static string GetPunctuator(this TokenType pTokenType)
        {
            switch (pTokenType)
            {                
                case TokenType.Bigger:
                    return ">";
                case TokenType.Smaller:
                    return "<";
                case TokenType.Equal:
                    return "=";    
                case TokenType.Different:
                    return "!=";
                case TokenType.BiggerEqual:
                    return ">=";
                case TokenType.SmallerEqual:
                    return "<=";
                default:
                    return " ";
            }
        }
    }
}