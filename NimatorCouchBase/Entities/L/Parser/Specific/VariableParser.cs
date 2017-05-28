using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Parser.Expressions.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Specific
{
    public class VariableParser : IPrefixParser
    {
        public IExpression Parse(Parser pParser, Token pToken)
        {
            return new VariableExpression(pToken.Value);
        }
    }
}