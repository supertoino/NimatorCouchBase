using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Expressions;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Prefix
{
    public class PrefixOperatorParser : IPrefixParser
    {
        public IExpression Parse(Parser pParser, Token pToken)
        {
            IExpression operand = pParser.ParseExpression();
            return new PrefixExpression(pToken.Type, operand);
        }
    }
}