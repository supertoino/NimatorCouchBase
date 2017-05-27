namespace NimatorCouchBase.Entities.L
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
                case TokenType.LeftParam:
                    break;
                case TokenType.RigthParam:
                    break;
                case TokenType.Comma:
                    break;
                case TokenType.Not:
                    break;                              
                default:
                    return ' ';
            }
            return ' ';
        }
    }
}