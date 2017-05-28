using NimatorCouchBase.Entities.L.Parser.Expressions.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Interfaces
{
    public interface IInfixParser
    {
        IExpression Parse(Parser pArser, IExpression pLeft, Token pToken);
    }
}