using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class LongParser : IPrefixParser
    {
        public IExpression Parse(BaseParser pParser, LToken pLToken)
        {
            return new LongExpression(pLToken.Value);
        }
    }
}