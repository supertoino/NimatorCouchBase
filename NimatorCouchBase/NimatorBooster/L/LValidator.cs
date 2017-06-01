using NimatorCouchBase.NimatorBooster.L.Lexical;
using NimatorCouchBase.NimatorBooster.L.Parser;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;

namespace NimatorCouchBase.NimatorBooster.L
{
    public class LValidator : ILValidator
    {
        public bool ValidateLExpression(string pLValidation)
        {
            LLexer lexer = new LLexer(pLValidation);
            IMemory lMemory = new LMemory();            
            LParser parser = new LParser(lexer, lMemory);
            return parser.Parse();
        }
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
