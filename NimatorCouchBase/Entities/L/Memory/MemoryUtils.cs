using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NimatorCouchBase.Entities.L.Memory.Interfaces;

namespace NimatorCouchBase.Entities.L.Memory
{
    public static class MemoryUtils
    {
        public static List<IMemorySlot> CreateMemorySlots(object pObj, string pPreviousName = "")
        {
            List<IMemorySlot> result = new List<IMemorySlot>();
            if (pObj == null)
            {
                return result;
            }            
            Type objType = pObj.GetType();
            PropertyInfo[] properties = objType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(pObj, null);
                var elems = propValue as IList;
                if (elems != null)
                {
                    foreach (var item in elems)
                    {
                        result.AddRange(CreateMemorySlots(item));
                    }
                }
                else
                {
                    // This will not cut-off System.Collections because of the first check
                    if (property.PropertyType.Assembly == objType.Assembly)
                    {
                        result.AddRange(CreateMemorySlots(propValue, property.Name));
                    }
                    else
                    {
                        var previousName = pPreviousName == string.Empty ? string.Empty : pPreviousName + ".";
                        MemorySlotKey memorySlotKey = new MemorySlotKey(previousName + property.Name);
                        MemorySlot memorySlot = new MemorySlot(memorySlotKey, property.PropertyType, propValue);
                        result.Add(memorySlot);
                    }
                }
            }
            return result;
        }
    }
}