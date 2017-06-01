// 
// NimatorCouchBase - NimatorCouchBase - LMemory.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/30/14:55
// LAST HEADER UPDATE: 2017 /06/01/13:54
// 

#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace NimatorCouchBase.NimatorBooster.L.Parser.Storage
{
    public class LMemory : IMemory
    {
        private const string CLASS_MEMBER_SEPARATOR = ".";
        private readonly Dictionary<IMemorySlotKey, IMemorySlot> MemoryData;

        public LMemory()
        {
            MemoryData = new Dictionary<IMemorySlotKey, IMemorySlot>(new MemorySlotKeyComparer());
        }

        public void AddToMemory<T>(T pObject) where T : IMemoryReady
        {
            if (pObject == null)
            {
                return;
            }
            var memorySlots = pObject.AvailableInMemoery();
            if (memorySlots == null)
            {
                return;
            }
            foreach (var memorySlot in memorySlots)
            {
                MemoryData.Add(memorySlot.Key, memorySlot);
            }
        }

        public IMemorySlot GetFromMemory(IMemorySlotKey pMemoryKey)
        {
            IMemorySlot value;
            MemoryData.TryGetValue(pMemoryKey, out value);
            return value ?? new MemorySlotEmpty();
        }

        public IList<IMemorySlot> GetListFromMemory(IMemorySlotKey pMemorySlotKey)
        {
            return GetListFromMem(pMemorySlotKey);
        }

        public void DumpMemory(StringBuilder pBuilder)
        {
            foreach (var key in MemoryData.Keys)
            {
                pBuilder.AppendLine($"{key.Key} - {MemoryData[key].ValueType} - {MemoryData[key].Value}");
            }
        }

        private IList<IMemorySlot> GetListFromMem(IMemorySlotKey pMemorySlotKey)
        {
            var memorySlotKeySpplited = SplitMemorySlotKeyValue(pMemorySlotKey.Key);
            Dictionary<string, string> descentAcessor = CreateAccessDictionary(memorySlotKeySpplited);
            var firstAcessor = memorySlotKeySpplited[0];
            IList<IMemorySlot> result = GetListFromMemAux(firstAcessor, descentAcessor);
            return result;
        }

        private static Dictionary<string, string> CreateAccessDictionary(IReadOnlyList<string> pMemorySlotKeys)
        {
            var acessDictionary = new Dictionary<string, string>();
            for (int i = 0; i < pMemorySlotKeys.Count - 1; i++)
            {
                var currentKey = pMemorySlotKeys[i];
                var nextKey = pMemorySlotKeys[i + 1];
                if (!acessDictionary.ContainsKey(currentKey))
                {
                    acessDictionary.Add(currentKey, nextKey);
                }
                if (i > 0)
                {
                    var acessKeyPath = GetAccessKeyToUntilCurrentKey(pMemorySlotKeys, i);
                    var fullPathForNextKey = AppendMemorySlotKeys(acessKeyPath, currentKey);
                    if (!acessDictionary.ContainsKey(fullPathForNextKey))
                    {
                        acessDictionary.Add(fullPathForNextKey, nextKey);
                    }
                }
            }
            return acessDictionary;
        }

        private static string GetAccessKeyToUntilCurrentKey(IReadOnlyList<string> pMemorySlotKeys, int i)
        {
            var allAcessorsUntilCurrentKey = string.Join(CLASS_MEMBER_SEPARATOR,
                Enumerable.Range(0, i).Select(pJ => pMemorySlotKeys[pJ]));
            return allAcessorsUntilCurrentKey;
        }

        private IList<IMemorySlot> GetListFromMemAux(string pMemorySlotKey, Dictionary<string, string> pAccessDictionary)
        {
            IList<IMemorySlot> result = new List<IMemorySlot>();

            var memorySlot = GetFromMemory(new MemorySlotKey(pMemorySlotKey));

            if (memorySlot.IsEmpty() && SlotKeyHasAccesses(pMemorySlotKey, pAccessDictionary))
            {
                var descentedForKey = pAccessDictionary[pMemorySlotKey];
                result =
                    result.Concat(GetListFromMemAux(AppendMemorySlotKeys(pMemorySlotKey, descentedForKey),
                        pAccessDictionary)).ToList();
                return result;
            }
            if (IsMemorySlotStoresList(memorySlot))
            {
                var listMaxIndex = GetListMaxIndex(memorySlot);
                for (int i = 0; i <= listMaxIndex; i++)
                {
                    var slotKeyNameForList = MemoryUtils.CreateSlotKeyNameForList(pMemorySlotKey, i);
                    if (SlotKeyHasAccesses(pMemorySlotKey, pAccessDictionary))
                    {
                        AddAllAccessesForListField(pMemorySlotKey, pAccessDictionary, slotKeyNameForList);
                        result = result.Concat(GetListFromMemAux(slotKeyNameForList, pAccessDictionary)).ToList();
                    }
                    else //The list[i] It's a value type
                    {
                        result.Add(GetFromMemory(new MemorySlotKey(slotKeyNameForList)));
                    }
                }
            }
            else //It's a value type
            {
                result.Add(GetFromMemory(new MemorySlotKey(pMemorySlotKey)));
            }
            return result;
        }

        private static void AddAllAccessesForListField(string pMemorySlotKey,
            Dictionary<string, string> pAccessDictionary, string pSlotKeyNameForList)
        {
            var descentedForKey = pAccessDictionary[pMemorySlotKey];
            pAccessDictionary.Add(pSlotKeyNameForList, descentedForKey);
            var otherDescentedForKey = descentedForKey;
            var otherSlotKeyNameForList = pSlotKeyNameForList;

            AddAllAccessPossibleForSlotKey(pAccessDictionary, otherDescentedForKey, otherSlotKeyNameForList,
                descentedForKey);
        }

        private static void AddAllAccessPossibleForSlotKey(Dictionary<string, string> pAccessDictionary,
            string pOtherDescentedForKey,
            string pOtherSlotKeyNameForList, string pDescentedForKey)
        {
            while (pAccessDictionary.ContainsKey(pOtherDescentedForKey))
            {
                var nextDescented = pAccessDictionary[pOtherDescentedForKey];
                var appendMemorySlotKeys = AppendMemorySlotKeys(pOtherSlotKeyNameForList, pDescentedForKey);
                if (!SlotKeyHasAccesses(appendMemorySlotKeys, pAccessDictionary))
                {
                    pAccessDictionary.Add(appendMemorySlotKeys, nextDescented);
                }
                pOtherSlotKeyNameForList = appendMemorySlotKeys;
                pOtherDescentedForKey = nextDescented;
            }
        }

        private static long GetListMaxIndex(IMemorySlot pMemorySlot)
        {
            return Convert.ToInt64(pMemorySlot.Value);
        }

        private static bool SlotKeyHasAccesses(string pMemorySlotKey, Dictionary<string, string> pAccessDictionary)
        {
            return pAccessDictionary.ContainsKey(pMemorySlotKey);
        }

        private static string AppendMemorySlotKeys(string pMemorySlotKey, string pDescentedForKey)
        {
            return pMemorySlotKey + CLASS_MEMBER_SEPARATOR + pDescentedForKey;
        }

        private static bool IsMemorySlotStoresList(IMemorySlot pMemorySlot)
        {
            return pMemorySlot.ValueType.IsGenericType &&
                   pMemorySlot.ValueType.GetGenericTypeDefinition() == typeof (List<>);
        }

        private static List<string> SplitMemorySlotKeyValue(string pSlotKeyValue)
        {
            return pSlotKeyValue.Split('.').ToList();
        }
    }
}