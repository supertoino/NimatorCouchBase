using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class BinaryOperatorParselet : IInfixParselet
    {
        public IExpression Parse(Parser pArser, IExpression pLeft, Token pToken)
        {
            IExpression right = pArser.ParseExpression();
            return new OperatorExpression(pLeft, pToken.Type, right);
        }
    }
}