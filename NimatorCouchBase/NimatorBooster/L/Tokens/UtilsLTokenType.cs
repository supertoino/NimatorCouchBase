namespace NimatorCouchBase.NimatorBooster.L.Tokens
{
    public static class UtilsLTokenType
    {
        public static string GetFunctionSyntax(this LTokenType pLTokenType)
        {
            switch (pLTokenType)
            {                
                case LTokenType.Bigger:
                    return ">";
                case LTokenType.Smaller:
                    return "<";
                case LTokenType.Equal:
                    return "=";    
                case LTokenType.Different:
                    return "!=";
                case LTokenType.BiggerEqual:
                    return ">=";
                case LTokenType.SmallerEqual:
                    return "<=";
                case LTokenType.Plus:
                    return "+";
                case LTokenType.Divide:
                    return "/";
                case LTokenType.Minus:
                    return "-";
                case LTokenType.Asterisk:
                    return "*";
                default:
                    return " ";
            }
        }
    }
}