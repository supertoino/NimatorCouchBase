using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class ArithmenticAdditionOperatorParser : IInfixParser
    {
        public ArithmenticAdditionOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public IExpression Parse(Parser pArser, IExpression pLeft, Token pToken)
        {
            IExpression right = pArser.ParseExpression(Precedence);
            return new ArithmeticAdditionExpression(pLeft, right);
        }

        public int Precedence { get; }       
    }
}