using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public interface IPrefixParser
    {
        IExpression Parse(Parser pParser, Token pToken);
    }
}