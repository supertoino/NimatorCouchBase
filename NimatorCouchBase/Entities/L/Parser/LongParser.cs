using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class LongParser : IPrefixParser
    {
        public IExpression Parse(Parser pParser, Token pToken)
        {
            return new LongExpression(pToken.Value);
        }
    }
}