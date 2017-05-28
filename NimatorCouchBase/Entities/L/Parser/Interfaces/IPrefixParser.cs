using NimatorCouchBase.Entities.L.Parser.Expressions.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Interfaces
{
    public interface IPrefixParser
    {
        IExpression Parse(Parser pParser, Token pToken);
    }
}