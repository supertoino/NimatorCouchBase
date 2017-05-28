using NimatorCouchBase.Entities.L.Tokens;

namespace NimatorCouchBase.Entities.L.Parser
{
    public class ArithmenticMultiplicationOperatorParser : IInfixParser
    {
        public ArithmenticMultiplicationOperatorParser(int pPrecedence)
        {
            Precedence = pPrecedence;
        }

        public IExpression Parse(Parser pArser, IExpression pLeft, Token pToken)
        {
            IExpression rightExpression = pArser.ParseExpression(Precedence);
            return new ArithmeticMultiplicationExpression(pLeft, rightExpression);
        }

        public int Precedence { get; }

    }
}