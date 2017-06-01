using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage
{
    public static class MemoryUtils
    {
        public static List<IMemorySlot> CreateMemorySlots(object pObj)
        {
            return CreateMemorySlotsAux(pObj);
        }
        private static List<IMemorySlot> CreateMemorySlotsAux(object pObj, string pPreviousName = "")
        {
            List<IMemorySlot> result = new List<IMemorySlot>();
            if (pObj == null) { return result; }
            Type objType = pObj.GetType();

            if (TypeIsPrimitive(objType))
            {
                MemorySlot memorySlot = CreateMemorySlot(pPreviousName, objType, pObj);
                result.Add(memorySlot);
                return result;
            }
            PropertyInfo[] properties = GetObjectProperties(objType);
            foreach (PropertyInfo property in properties)
            {
                var currentName = GetPropertyNameForMemorySlot(pPreviousName, property);
                object propValue = property.GetValue(pObj, null);
                if (PropertyIsCollection(propValue))
                {
                    var elems = (IList)propValue;

                    AddToMemoryListMaxIndex(elems, currentName, property, result);

                    int listCurrentIndex = 0;
                    foreach (var item in elems)
                    {
                        result.AddRange(CreateMemorySlotsAux(item, CreateSlotKeyNameForList(currentName, listCurrentIndex)));
                        listCurrentIndex++;
                    }
                }
                else if (PropertyIsObject(property, objType))
                {
                    result.AddRange(CreateMemorySlotsAux(propValue, currentName));
                }
                else //Assume is Primitive value
                {
                    MemorySlot memorySlot = CreateMemorySlot(currentName, property.PropertyType, propValue);
                    result.Add(memorySlot);
                }

            }
            return result;
        }

        public static string CreateSlotKeyNameForList(string pCurrentName, long pListCurrentIndex)
        {
            return pCurrentName + $"[{pListCurrentIndex}]";
        }

        private static void AddToMemoryListMaxIndex(IList pElems, string pCurrentName, PropertyInfo pRoperty, List<IMemorySlot> pResult)
        {
            var slotValue = pElems.Count - 1;
            MemorySlot memorySlotForListLength = CreateMemorySlot(pCurrentName, pRoperty.PropertyType, slotValue);
            pResult.Add(memorySlotForListLength);
        }

        private static string GetPropertyNameForMemorySlot(string pPreviousName, PropertyInfo pRoperty)
        {
            return pPreviousName == string.Empty ? pRoperty.Name : pPreviousName + "." + pRoperty.Name;
        }

        private static bool PropertyIsObject(PropertyInfo pRoperty, Type pObjType)
        {
            return pRoperty.PropertyType.Assembly == pObjType.Assembly;
        }

        private static bool PropertyIsCollection(object pRopValue)
        {
            var elems = pRopValue as IList;
            return elems != null;
        }

        private static PropertyInfo[] GetObjectProperties(Type pObjType)
        {
            return pObjType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private static bool TypeIsPrimitive(Type pObjType)
        {
            return pObjType.IsPrimitive || pObjType == typeof(decimal) || pObjType == typeof(string) || pObjType == typeof(DateTime);
        }

        private static MemorySlot CreateMemorySlot(string pSlotKey, Type pSlotType, object pSlotValue)
        {
            MemorySlotKey memorySlotKey = new MemorySlotKey(pSlotKey);
            MemorySlot memorySlot = new MemorySlot(memorySlotKey, pSlotType, pSlotValue);
            return memorySlot;
        }
    }
}