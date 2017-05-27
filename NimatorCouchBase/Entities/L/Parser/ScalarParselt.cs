using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class ScalarParselt : IPrefixParselet
    {
        public IExpression Parse(Parser pArser, Token pToken)
        {
            return new ScalarExpression(pToken.Value);
        }
    }
}