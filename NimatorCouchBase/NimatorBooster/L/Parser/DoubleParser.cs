using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class DoubleParser : IPrefixParser
    {
        public IExpression Parse(BaseParser pParser, LToken pLToken)
        {
            return new DoubleExpression(pLToken.Value);
        }
    }
}