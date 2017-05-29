using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;
using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public class VariableParser : IPrefixParser
    {
        public void SetMemory(IMemory pMemory)
        {
            Memory = pMemory;
        }

        public IMemory Memory { get; private set; }

        public IExpression Parse(BaseParser pParser, LToken pLToken)
        {
            return new VariableExpression(pLToken.Value, Memory);
        }
    }
}