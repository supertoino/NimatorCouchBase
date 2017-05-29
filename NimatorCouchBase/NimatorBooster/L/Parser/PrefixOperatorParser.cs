using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class PrefixOperatorParser : IPrefixParser
    {
        public IExpression Parse(BaseParser pParser, LToken pLToken)
        {
            IExpression operand = pParser.ParseExpression();
            return new PrefixExpression(pLToken.Type, operand);
        }
    }
}