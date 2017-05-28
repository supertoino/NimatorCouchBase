using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
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