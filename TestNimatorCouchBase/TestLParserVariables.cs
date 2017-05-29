using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.NimatorBooster.L.Lexical;
using NimatorCouchBase.NimatorBooster.L.Parser;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLParserVariables
    {
        public class Totals : IMemoryReady
        {
            public int TotalGoals { get; set; }
            public int TotalPenalties { get; set; }

            public double MatchersOver2Goals { get; set; }
            public double TotalGoalsPerMatch { get; set; }

            public SubTotals SubTotal { get; set; }

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
        public void TestTotalGoalsEqualsTotalPenaltiesShouldReturnTrue()
        {
            var total = new Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("TotalGoals=TotalPenalties");
            BaseParser parser = new LParser(lLexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestTotalGoalsEqualsTotalPenaltiesShouldReturnFalse()
        {
            var total = new Totals
            {
                TotalGoals = 10,
                TotalPenalties = 12
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("TotalGoals=TotalPenalties");
            BaseParser parser = new LParser(lLexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        [TestMethod]
        public void TestTotalGoalsBiggerTotalPenaltiesShouldReturnTrue()
        {
            var total = new Totals
            {
                TotalGoals = 12,
                TotalPenalties = 10
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("TotalGoals>TotalPenalties");
            BaseParser parser = new LParser(lLexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestTotalGoalsPerMatchEqualToMatchersOver2GoalsShouldReturnTrue()
        {
            var total = new Totals
            {
                TotalGoalsPerMatch = 12.11,
                MatchersOver2Goals = 12.11
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("TotalGoalsPerMatch=MatchersOver2Goals");
            BaseParser parser = new LParser(lLexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestSubTotalDocsSumOfGoalsEquals100ShouldReturnTrue()
        {
            var total = new Totals
            {
                SubTotal = new Totals.SubTotals()
                {
                    SumOfGoals = 100
                }                
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("SubTotal.SumOfGoals=100");
            BaseParser parser = new LParser(lLexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }        
    }
}
