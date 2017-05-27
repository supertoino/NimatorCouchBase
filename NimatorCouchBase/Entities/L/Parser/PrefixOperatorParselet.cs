using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class PrefixOperatorParselet : IPrefixParselet
    {
        public IExpression Parse(Parser pArser, Token pToken)
        {
            IExpression operand = pArser.ParseExpression();
            return new PrefixExpression(pToken.Type, operand);
        }
    }
}