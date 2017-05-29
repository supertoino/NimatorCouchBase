using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public interface IPrefixParser
    {
        IExpression Parse(BaseParser pParser, LToken pLToken);
    }    
}