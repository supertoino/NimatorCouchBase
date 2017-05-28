using System.Text;

namespace NimatorCouchBase.Entities.L.Parser.Expressions.Interfaces
{
    public interface IExpression
    {
        object Value { get; }
        void Print(StringBuilder pBuilder);
    }
}