using System.Text;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Interfaces
{
    public interface IExpression
    {
        object Value { get; }
        void Print(StringBuilder pBuilder);
    }
}