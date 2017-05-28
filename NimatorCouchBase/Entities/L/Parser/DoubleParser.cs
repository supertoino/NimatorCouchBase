using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class DoubleParser : IPrefixParser
    {
        public IExpression Parse(Parser pParser, Token pToken)
        {
            return new DoubleExpression(pToken.Value);
        }
    }
}