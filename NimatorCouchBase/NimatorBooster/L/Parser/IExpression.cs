using System.Text;

namespace NimatorCouchBase.NimatorBooster.L.Parser
{
    public interface IExpression
    {
        object Value { get; }
        void Print(StringBuilder pBuilder);
    }
}