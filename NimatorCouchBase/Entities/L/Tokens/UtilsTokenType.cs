namespace NimatorCouchBase.Entities.L.Tokens
{
    public static class UtilsTokenType
    {
        public static char GetPunctuator(this TokenType pTokenType)
        {
            switch (pTokenType)
            {
                case TokenType.Asterisk:
                    return '*';
                case TokenType.Plus:
                    return '+';
                case TokenType.Minus:
                    return '-';
                case TokenType.Bigger:
                    return '>';
                case TokenType.Smaller:
                    return '<';
                case TokenType.Equal:
                    return '=';                            
                default:
                    return ' ';
            }
        }
    }
}