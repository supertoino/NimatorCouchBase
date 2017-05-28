using System.Text;

namespace NimatorCouchBase.Entities.L.Parser
{
    public interface IExpression
    {
        object Value { get; }
        void Print(StringBuilder pBuilder);
    }
}