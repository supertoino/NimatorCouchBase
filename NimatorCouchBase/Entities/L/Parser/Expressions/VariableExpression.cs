using System.Text;

namespace NimatorCouchBase.Entities.L.Parser.Expressions
{
    public class VariableExpression : IExpression
    {
        private readonly string VariableName;

        public VariableExpression(string pVariableName)
        {
            VariableName = pVariableName;
        }

        public object Value { get; }

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append(VariableName);
        }
    }
}