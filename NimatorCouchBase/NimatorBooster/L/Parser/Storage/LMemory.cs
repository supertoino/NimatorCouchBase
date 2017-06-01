// 
// NimatorCouchBase - NimatorCouchBase - LMemory.cs 
// CREATOR: antonio.silva - António Silva
// AT: 2017/05/30/14:55
// LAST HEADER UPDATE: 2017 /06/01/16:44
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
            var memorySlotKeySpplited = SplitMemorySlotKeyByClassMemberSeperator(pMemorySlotKey.Key);
            Dictionary<string, string> descentAcessor = CreateAccessDictionary(memorySlotKeySpplited);
            var firstAcessor = memorySlotKeySpplited[0];
            IList<IMemorySlot> result = GetListFromMemAux(firstAcessor, descentAcessor);
            return result;
        }

        private IList<IMemorySlot> GetListFromMemAux(string pMemorySlotKey, Dictionary<string, string> pAccessDictionary)
        {
            IList<IMemorySlot> result = new List<IMemorySlot>();

            var memorySlot = GetFromMemory(new MemorySlotKey(pMemorySlotKey));

            if (MemorySlotHasAnObject(pMemorySlotKey, pAccessDictionary, memorySlot))
            {
                var descentedForKey = pAccessDictionary[pMemorySlotKey];
                var nextMemoryKeytoSearch = AppendMemorySlotKeys(pMemorySlotKey, descentedForKey);
                result = result.Concat(GetListFromMemAux(nextMemoryKeytoSearch, pAccessDictionary)).ToList();
                return result;
            }
            if (MemorySlotStoresList(memorySlot))
            {
                var listMaxIndex = GetListMaxIndex(memorySlot);
                for (int i = 0; i <= listMaxIndex; i++)
                {
                    var slotKeyForListPosition = MemoryUtils.CreateSlotKeyNameForList(pMemorySlotKey, i);
                    if (MemorySlotKeyHasAccesses(pMemorySlotKey, pAccessDictionary))
                    {
                        AddAllAccessesForListField(pMemorySlotKey, pAccessDictionary, slotKeyForListPosition);
                        result = result.Concat(GetListFromMemAux(slotKeyForListPosition, pAccessDictionary)).ToList();
                    }
                    else //The list[i] is a value type
                    {
                        result.Add(GetFromMemory(new MemorySlotKey(slotKeyForListPosition)));
                    }
                }
            }
            else //Its a value type
            {
                result.Add(GetFromMemory(new MemorySlotKey(pMemorySlotKey)));
            }
            return result;
        }

        #region Get List from Mex Helper Methods


        /// <summary>
        /// Creates a Dictionary with accessors for each key
        /// Ex: for the query "Nodes.InterestingStats.CurrItems" the dictionary will be.
        ///     Nodes, InterestingStats
        ///     InterestingStats, CurrItems
        ///     Nodes.InterestingStats, CurrItems        
        /// It will be used to get List values
        /// </summary>
        /// <param name="pMemorySlotKeys"></param>
        /// <returns></returns>
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

        private static bool MemorySlotHasAnObject(string pMemorySlotKey, Dictionary<string, string> pAccessDictionary, IMemorySlot memorySlot)
        {
            return memorySlot.IsEmpty() && MemorySlotKeyHasAccesses(pMemorySlotKey, pAccessDictionary);
        }

        private static string GetAccessKeyToUntilCurrentKey(IReadOnlyList<string> pMemorySlotKeys, int pI)
        {
            var allAcessorsUntilCurrentKey = string.Join(CLASS_MEMBER_SEPARATOR,
                Enumerable.Range(0, pI).Select(pJ => pMemorySlotKeys[pJ]));
            return allAcessorsUntilCurrentKey;
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
                if (!MemorySlotKeyHasAccesses(appendMemorySlotKeys, pAccessDictionary))
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

        private static bool MemorySlotKeyHasAccesses(string pMemorySlotKey, Dictionary<string, string> pAccessDictionary)
        {
            return pAccessDictionary.ContainsKey(pMemorySlotKey);
        }

        private static string AppendMemorySlotKeys(string pMemorySlotKey, string pDescentedForKey)
        {
            return pMemorySlotKey + CLASS_MEMBER_SEPARATOR + pDescentedForKey;
        }

        private static bool MemorySlotStoresList(IMemorySlot pMemorySlot)
        {
            return pMemorySlot.ValueType.IsGenericType &&
                   pMemorySlot.ValueType.GetGenericTypeDefinition() == typeof (List<>);
        }

        private static List<string> SplitMemorySlotKeyByClassMemberSeperator(string pSlotKeyValue)
        {
            return pSlotKeyValue.Split('.').ToList();
        }

        #endregion
    }
}