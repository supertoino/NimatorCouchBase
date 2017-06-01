using System;
using System.Collections.Generic;
using System.Linq;
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

        public object Value
        {
            get
            {
                MemorySlotKey memorySlotKey = new MemorySlotKey(VariableName);
                IMemorySlot variable;
                var variableList = Memory.GetListFromMemory(memorySlotKey);
                if (variableList.Any())
                {
                    var arrayValues = variableList;
                    var sum = arrayValues.Sum(pArrayValue => Convert.ToDouble(pArrayValue.Value));
                    variable = new MemorySlot(memorySlotKey, typeof (double), sum);
                }
                else
                {
                    variable = Memory.GetFromMemory(memorySlotKey);
                }
                return variable;
            }
        } 

        public void Print(StringBuilder pBuilder)
        {
            pBuilder.Append(VariableName);
        }
    }
}