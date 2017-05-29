using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{    
    public interface IInfixParser
    {
        IExpression Parse(BaseParser pArser, IExpression pLeft, LToken pLToken);
        int Precedence { get; }
    }
}