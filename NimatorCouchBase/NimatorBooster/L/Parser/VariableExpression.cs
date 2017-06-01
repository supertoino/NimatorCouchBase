using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;

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

        public object Value
        {
            get
            {
                MemorySlotKey memorySlotKey = new MemorySlotKey(VariableName);

                var variableList = CheckIfVariableIsList(memorySlotKey);
                IMemorySlot variable = variableList.Any() ?
                    SumAllValuesFromListInMemory(variableList, memorySlotKey) :
                    GetSingleValueFromMemory(memorySlotKey);
                return variable;
            }
        }

        private IMemorySlot GetSingleValueFromMemory(IMemorySlotKey pMemorySlotKey)
        {
            return Memory.GetFromMemory(pMemorySlotKey);
        }

        private IList<IMemorySlot> CheckIfVariableIsList(IMemorySlotKey pMemorySlotKey)
        {
            return Memory.GetListFromMemory(pMemorySlotKey);
        }

        private static IMemorySlot SumAllValuesFromListInMemory(IList<IMemorySlot> pVariableList, IMemorySlotKey pMemorySlotKey)
        {
            var arrayValues = pVariableList;
            var sum = arrayValues.Sum(pArrayValue => Convert.ToDouble(pArrayValue.Value));
            IMemorySlot variable = new MemorySlot(pMemorySlotKey, typeof (double), sum);
            return variable;
        }

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append(VariableName);
        }
    }
}