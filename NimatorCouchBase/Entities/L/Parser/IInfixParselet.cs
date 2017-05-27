using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public interface IInfixParselet
    {
        IExpression Parse(Parser pArser, IExpression pLeft, Token pToken);
    }
}