using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLMemory
    {
        public class BClass : IMemoryReady
        {
            public string PublicBProperty { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public BClass()
            {
                Values = new List<int>();
            }

            public List<int> Values { get; set; }
            public List<IMemorySlot> AvailableInMemoery()
            {
                return MemoryUtils.CreateMemorySlots(this);
            }
        }
        public class AClass : IMemoryReady
        {
            private int PrivateVar;
            public int PublicVariable;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public AClass(int pPrivateVar, int pPublicVariable, string pPublicProperty)
            {
                B = new BClass();
                PrivateVar = pPrivateVar;
                PublicVariable = pPublicVariable;
                PublicProperty = pPublicProperty;
            }
           
            public string PublicProperty { get; set; }
            public BClass B { get; set; }

            public void AFunction()
            {
                
            }

            public List<IMemorySlot> AvailableInMemoery()
            {                
                return MemoryUtils.CreateMemorySlots(this);
            }
        }

        public class CClass : IMemoryReady
        {
            public string C { get; set; }
            public List<IMemorySlot> AvailableInMemoery()
            {
                return null;
            }
        }

        [TestMethod]
        public void TestMemoryAccessOk()
        {
            var memory = new Memory();
            var aclass = new AClass(1, 2, "Antonio")
            {
                B =
                {
                    PublicBProperty = "Ola",
                    Values = new List<int>() {1, 2, 3}
                }
            };

            memory.AddToMemory(aclass);
            
            Assert.AreEqual("Ola", memory.GetFromMemory(new MemorySlotKey("B.PublicBProperty")).Value);
            Assert.AreEqual("Antonio", memory.GetFromMemory(new MemorySlotKey("PublicProperty")).Value);            
        }

        [TestMethod]
        public void TestMemoryAccessCaseInsensitiveShouldReturnEmptyMemorySlots()
        {
            var memory = new Memory();
            var aclass = new AClass(1, 2, "Antonio")
            {
                B =
                {
                    PublicBProperty = "Ola",
                    Values = new List<int>() {1, 2, 3}
                }
            };

            memory.AddToMemory(aclass);

            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("b.PublicBProperty")).IsEmpty());
            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("Publicproperty")).IsEmpty());
        }

        [TestMethod]
        public void TestMemoryAcessOkNullSlots()
        {
            var memory = new Memory();            
            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("PublicVar")).IsEmpty());
            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("A")).IsEmpty());
            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("A.B.C")).IsEmpty());

            Assert.AreEqual("",memory.GetFromMemory(new MemorySlotKey("A.B.C")).Value);
        }

        [TestMethod]
        public void TestMemoryAddNullObject()
        {
            var memory = new Memory();
            memory.AddToMemory(new CClass());

            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("C")).IsEmpty());
        }
    }
}
