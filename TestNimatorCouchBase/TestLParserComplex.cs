using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.NimatorBooster.L;
using NimatorCouchBase.NimatorBooster.L.Lexical;
using NimatorCouchBase.NimatorBooster.L.Parser;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage;
using NimatorCouchBase.NimatorBooster.L.Parser.Storage.Interfaces;

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
        public void TestTotalGoalsTimes10BiggerThanTotalPenaltiesShouldReturnTrue()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("TotalGoals*10>TotalPenalties");
            BaseParser parser = new LParser(lLexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestTotalGoalsTimes10SmallerThanTotalPenaltiesShouldReturnFalse()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("TotalGoals*10<TotalPenalties");
            BaseParser parser = new LParser(lLexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        [TestMethod]
        public void TestTotalGoalsTimes0_5Plus1_2SmallerThanTotalPenaltiesShouldReturnTrue()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("TotalGoals*0.5+1.2<TotalPenalties");
            BaseParser parser = new LParser(lLexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestTotalGoalsTime0_5Plus1_2EqualsTotalPenaltiesTimes0_5Plus1_2ShouldReturnTrue()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("TotalGoals*0.5+1.2=TotalPenalties*0.5+1.2");
            BaseParser parser = new LParser(lLexer, memory);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestTotalGoalsTime0_5Plus1_2DifferentThenTotalPenaltiesTimes0_5Plus1_21ShouldReturnTrue()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            IMemory memory = new LMemory();
            memory.AddToMemory(total);
            LLexer lLexer = new LLexer("TotalGoals*0.5+1.2!=TotalPenalties*0.5+1.21");
            LParser parser = new LParser(lLexer, memory);
            var result = parser.Parse();
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(UnableToParseLTokenTypeException))]
        public void TestWrongLValidationCodeWithMemory()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            LValidator lValidator = new LValidator();
            var result = lValidator.ValidateLExpression("0.5+1.2!=0.5+1.21AAAA", total);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(UnableToParseLTokenTypeException))]
        public void TestWrongLValidationCode()
        {
            var total = new TestLParserVariables.Totals
            {
                TotalGoals = 10,
                TotalPenalties = 10
            };
            LValidator lValidator = new LValidator();
            var result = lValidator.ValidateLExpression("0.5+1.2!=0.5+1.21AAAA");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(UnableToParseLTokenTypeException))]
        public void TestWrongLvalidationCodeNoFunction()
        {
            LValidator lValidator = new LValidator();
            var result = lValidator.ValidateLExpression("0.6+1.2A0.5+1.21");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(UnableToValidateExpressionException))]
        public void TestWrongLvalidationTwoFunctionsUnableToValidate()
        {
            LValidator lValidator = new LValidator();
            var result = lValidator.ValidateLExpression("0.6+1.2>0.5>1.21");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(UnableToParseLTokenTypeException))]
        public void TestWrongLvalidationTwoFunctionsUnableToParse()
        {
            LValidator lValidator = new LValidator();
            var result = lValidator.ValidateLExpression("0.6+1.2><0.51.21");
            Assert.IsTrue(result);
        }
    }
}
