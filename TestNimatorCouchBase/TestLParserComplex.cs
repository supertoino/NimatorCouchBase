using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.Entities.L.Lexer;
using NimatorCouchBase.Entities.L.Memory;
using NimatorCouchBase.Entities.L.Memory.Interfaces;
using NimatorCouchBase.Entities.L.Parser;
using NimatorCouchBase.Entities.L.Parser.Entities.Interfaces;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLParserComplex
    {
        public class Totals : IMemoryReady
        {
            public int TotalGoals { get; set; }
            public int TotalPenalties { get; set; }

            public double MatchersOver2Goals { get; set; }
            public double TotalGoalsPerMatch { get; set; }

            public TestLParserVariables.Totals.SubTotals SubTotal { get; set; }

            public List<IMemorySlot> AvailableInMemoery()
            {
                return MemoryUtils.CreateMemorySlots(this);
            }

            public class SubTotals : IMemoryReady
            {
                public int SumOfGoals { get; set; }
                public List<IMemorySlot> AvailableInMemoery()
                {
                    return MemoryUtils.CreateMemorySlots(this);
                }
            }
        }

        [TestMethod]
        public void TestOk()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("TotalGoals*10>TotalPenalties");
            Parser parser = new LParser(lexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestOk2()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("TotalGoals*10<TotalPenalties");
            Parser parser = new LParser(lexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        [TestMethod]
        public void TestOk3()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("TotalGoals*0.5+1.2<TotalPenalties");
            Parser parser = new LParser(lexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestOk4()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("TotalGoals*0.5+1.2=TotalPenalties*0.5+1.2");
            Parser parser = new LParser(lexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestOk5()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("TotalGoals*0.5+1.2!=TotalPenalties*0.5+1.21");
            Parser parser = new LParser(lexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }
    }
}
