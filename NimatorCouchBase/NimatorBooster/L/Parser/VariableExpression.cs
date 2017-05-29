using System.Text;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;

namespace NimatorCouchBase.NimatorBooster.L.Parser
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