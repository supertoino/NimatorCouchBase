using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.CouchBase.Statistics.Default;
using NimatorCouchBase.NimatorBooster.HttpCheckers.Callers;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLMemory
    {
        public class DClass : IMemoryReady
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public DClass(string pD1, string pD2, int pD3)
            {
                D1 = pD1;
                D2 = pD2;
                D3 = pD3;
                Inside = new List<DClass>();
            }

            public string D1 { get; set; }
            public string D2 { get; set; }
            public int D3 { get; set; }

            public List<DClass> Inside { get; set; } 
            public List<EClass> EClasses { get; set; } 
            public List<IMemorySlot> AvailableInMemoery()
            {
                return MemoryUtils.CreateMemorySlots(this);
            }
        }

        public class EClass
        {
            public int EA { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public EClass(int pEa)
            {
                EA = pEa;
            }
        }
        public class BClass : IMemoryReady
        {
            public string PublicBProperty { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public BClass()
            {
                Values = new List<int>();
                StringValues = new List<String>();
                DValues = new List<DClass>();
            }

            public List<int> Values { get; set; }
            public List<string> StringValues { get; set; }
            public List<DClass> DValues { get; set; }
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
            var memory = new LMemory();
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
            Assert.IsFalse(memory.GetFromMemory(new MemorySlotKey("B.Values")).IsEmpty());
            Assert.AreEqual("Antonio", memory.GetFromMemory(new MemorySlotKey("PublicProperty")).Value);            
        }

        [TestMethod]
        public void TestMemoryCanHaveListValues()
        {
            var memory = new LMemory();
            var aclass = new AClass(1, 2, "Antonio")
            {
                B =
                {
                    PublicBProperty = "Ola",
                    Values = new List<int>() {1, 2, 3},
                    StringValues = new List<string>() {"1", "2", "3", "4"},
                    DValues = new List<DClass>()
                    {
                        new DClass("1","2",3)
                        {
                            Inside = new List<DClass>()
                            {
                                new DClass("1","2",0)
                                {
                                    EClasses = new List<EClass>()
                                    {
                                        new EClass(0),
                                        new EClass(1),
                                        new EClass(2),
                                    }
                                    //Inside = new List<DClass>() { new DClass("0","0", 0) }
                                }
                            }
                        },
                        new DClass("1","2",4),
                        new DClass("1","2",5),
                        new DClass("1","2",6)
                    }                    
                }
            };

            memory.AddToMemory(aclass);

            StringBuilder sb = new StringBuilder();
            memory.DumpMemory(sb);
            Console.WriteLine(sb.ToString());

            Assert.IsFalse(memory.GetFromMemory(new MemorySlotKey("B.Values")).IsEmpty());
            Assert.IsFalse(memory.GetFromMemory(new MemorySlotKey("B.StringValues")).IsEmpty());
            Assert.IsFalse(memory.GetFromMemory(new MemorySlotKey("B.DValues")).IsEmpty());

            Assert.AreEqual(2,memory.GetFromMemory(new MemorySlotKey("B.Values")).Value);
            Assert.AreEqual(3,memory.GetFromMemory(new MemorySlotKey("B.StringValues")).Value);
            Assert.AreEqual(3,memory.GetFromMemory(new MemorySlotKey("B.DValues")).Value);

            var listFromMemory = memory.GetListFromMemory(new MemorySlotKey("B.Values"));
            Assert.AreEqual(3, listFromMemory.Count);
            Assert.AreEqual(1, listFromMemory[0].Value);
            Assert.AreEqual(2, listFromMemory[1].Value);
            Assert.AreEqual(3, listFromMemory[2].Value);

            listFromMemory = memory.GetListFromMemory(new MemorySlotKey("B.StringValues"));
            Assert.AreEqual(4, listFromMemory.Count);
            Assert.AreEqual("1", listFromMemory[0].Value);
            Assert.AreEqual("2", listFromMemory[1].Value);
            Assert.AreEqual("3", listFromMemory[2].Value);
            Assert.AreEqual("4", listFromMemory[3].Value);

            listFromMemory = memory.GetListFromMemory(new MemorySlotKey("B.DValues.D3"));
            Assert.AreEqual(4,listFromMemory.Count);
            Assert.AreEqual(3,listFromMemory[0].Value);
            Assert.AreEqual(4,listFromMemory[1].Value);
            Assert.AreEqual(5,listFromMemory[2].Value);
            Assert.AreEqual(6,listFromMemory[3].Value);

            listFromMemory = memory.GetListFromMemory(new MemorySlotKey("B.DValues.Inside.D1"));
            Assert.AreEqual(1, listFromMemory.Count);
            Assert.AreEqual("1", listFromMemory[0].Value);

            listFromMemory = memory.GetListFromMemory(new MemorySlotKey("B.DValues.Inside.EClasses.EA"));
            Assert.AreEqual(3, listFromMemory.Count);
            Assert.AreEqual(0, listFromMemory[0].Value);
            Assert.AreEqual(1, listFromMemory[1].Value);
            Assert.AreEqual(2, listFromMemory[2].Value);
        }

        [TestMethod]
        public void TestMemoryAccessCaseInsensitiveShouldReturnEmptyMemorySlots()
        {
            var memory = new LMemory();
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
            var memory = new LMemory();            
            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("PublicVar")).IsEmpty());
            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("A")).IsEmpty());
            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("A.B.C")).IsEmpty());

            Assert.AreEqual("",memory.GetFromMemory(new MemorySlotKey("A.B.C")).Value);
        }

        [TestMethod]
        public void TestMemoryAddNullObject()
        {
            var memory = new LMemory();
            memory.AddToMemory(new CClass());

            Assert.IsTrue(memory.GetFromMemory(new MemorySlotKey("C")).IsEmpty());
        }

        [TestMethod]
        public void TestMemoeryWithCouchBaseStats()
        {
            var defaultStatsHttpCallerParameters = new HttpCallerParameters("http://localhost:8091/pools/default", new HttpAuthenticationSettings("supertoino", "OcohoW*99"), HttpMethods.GET);

            var httpCallerParameters = defaultStatsHttpCallerParameters;

            var checkHttpCaller = new HttpCaller(httpCallerParameters);

            var stats = checkHttpCaller.DoHttpGetCall<CouchBaseDefaultStats>();

            LMemory lMemory = new LMemory();
            lMemory.AddToMemory(stats);

            StringBuilder sb = new StringBuilder();
            lMemory.DumpMemory(sb);
            Console.WriteLine(sb.ToString());

            Assert.AreNotEqual(stats, null);
            Assert.AreEqual("supertoino", stats.ClusterName);
        }
    }
}
