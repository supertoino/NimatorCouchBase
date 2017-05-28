namespace NimatorCouchBase.Entities.L.Tokens
{
    public static class UtilsTokenType
    {
        public static string GetFunctionSyntax(this TokenType pTokenType)
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
                case TokenType.Plus:
                    return "+";
                case TokenType.Divide:
                    return "/";
                case TokenType.Minus:
                    return "-";
                case TokenType.Asterisk:
                    return "*";
                default:
                    return " ";
            }
        }
    }
}