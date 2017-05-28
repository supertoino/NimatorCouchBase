using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Interfaces
{
    public interface IPrefixParser
    {
        IExpression Parse(Parser pParser, Token pToken);
    }
}