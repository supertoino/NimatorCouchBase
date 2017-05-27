using System.Text;

namespace NimatorCouchBase.Entities.L
{
    public class VariableExpression : IExpression
    {
        private readonly string VariableName;

        public VariableExpression(string pVariableName)
        {
            VariableName = pVariableName;
        }

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append(VariableName);
        }
    }
}