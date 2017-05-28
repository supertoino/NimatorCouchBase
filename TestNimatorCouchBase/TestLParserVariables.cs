using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.Entities.L.Lexer;
using NimatorCouchBase.Entities.L.Memory;
using NimatorCouchBase.Entities.L.Memory.Interfaces;
using NimatorCouchBase.Entities.L.Parser;
using NimatorCouchBase.Entities.L.Parser.Expressions.Interfaces;

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
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("TotalGoals=TotalPenalties");
            Parser parser = new LParser(lexer, memory);
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
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("TotalGoals=TotalPenalties");
            Parser parser = new LParser(lexer, memory);
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
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("TotalGoals>TotalPenalties");
            Parser parser = new LParser(lexer, memory);
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
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("TotalGoalsPerMatch=MatchersOver2Goals");
            Parser parser = new LParser(lexer, memory);
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
            IMemory memory = new Memory();
            memory.AddToMemory(total);
            Lexer lexer = new Lexer("SubTotal.SumOfGoals=100");
            Parser parser = new LParser(lexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }        
    }
}
