using System.Text;
using NimatorCouchBase.Entities.L.Memory;
using NimatorCouchBase.Entities.L.Memory.Interfaces;
using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;

namespace NimatorCouchBase.Entities.L.Parser.Entities.Prefix.Expressions
{
    public class VariableExpression : IExpression
    {
        private readonly string VariableName;
        private readonly IMemory Memory;

        public VariableExpression(string pVariableName, IMemory pMemory)
        {
            VariableName = pVariableName;
            Memory = pMemory;
        }

        public object Value => Memory.GetFromMemory(new MemorySlotKey(VariableName));

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append(VariableName);
        }
    }
}