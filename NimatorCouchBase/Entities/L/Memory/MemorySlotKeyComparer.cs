using System;
using System.Collections.Generic;
using NimatorCouchBase.Entities.L.Memory.Interfaces;

namespace NimatorCouchBase.Entities.L.Memory
{
    public class MemorySlotKeyComparer : IEqualityComparer<IMemorySlotKey>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="pX">The first object of type <paramref name="T"/> to compare.</param><param name="pY">The second object of type <paramref name="T"/> to compare.</param>
        public bool Equals(IMemorySlotKey pX, IMemorySlotKey pY)
        {
            if (pX == null | pY == null)
            {
                return false;
            }
            return string.Equals(pY.Key, pX.Key, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="pObj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="pObj"/> is a reference type and <paramref name="pObj"/> is null.</exception>
        public int GetHashCode(IMemorySlotKey pObj)
        {
            return pObj.Key.GetHashCode() * 17;
        }
    }
}