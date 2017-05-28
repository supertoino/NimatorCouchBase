using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public interface IInfixParser
    {
        IExpression Parse(Parser pArser, IExpression pLeft, Token pToken);
        int Precedence { get; }
    }
}