using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public interface IPrefixParselet
    {
        IExpression Parse(Parser pArser, Token pToken);
    }
}