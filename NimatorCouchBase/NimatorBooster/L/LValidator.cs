using NimatorCouchBase.NimatorBooster.L.Lexical;
using NimatorCouchBase.NimatorBooster.L.Parser;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;

namespace NimatorCouchBase.NimatorBooster.L
{
    public class LValidator : ILValidator
    {
        public bool ValidateLExpression(string pLValidation, IMemoryReady pObject)
        {
            LLexer lexer = new LLexer(pLValidation);
            IMemory lMemory = new LMemory();            
            lMemory.AddToMemory(pObject);                        
            LParser parser = new LParser(lexer, lMemory);
            return parser.Parse();
        }
    }
}
