using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;
using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Infix.Interfaces
{
    public interface IInfixParser
    {
        IExpression Parse(Parser pArser, IExpression pLeft, Token pToken);
        int Precedence{ get; }
}