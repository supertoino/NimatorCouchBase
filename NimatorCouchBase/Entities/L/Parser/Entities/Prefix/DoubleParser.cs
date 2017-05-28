using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Expressions;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Prefix
{
    public class DoubleParser : IPrefixParser
    {
        public IExpression Parse(Parser pParser, Token pToken)
        {
            return new DoubleExpression(pToken.Value);
        }
    }
}