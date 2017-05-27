using System;
using System.Text;

namespace NimatorCouchBase.Entities.L.Parser.Expressions
{
    public interface IExpression
    {
        Object Value { get; }
        void Print(StringBuilder pBuilder);
    }
}