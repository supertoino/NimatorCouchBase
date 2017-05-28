using NimatorCouchBase.Entities.L.Memory.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Expressions;
using NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Prefix
{
    public class VariableParser : IPrefixParser
    {
        public void SetMemory(IMemory pMemory)
        {
            Memory = pMemory;
        }

        public IMemory Memory { get; private set; }

        public IExpression Parse(Parser pParser, Token pToken)
        {
            return new VariableExpression(pToken.Value, Memory);
        }
    }
}