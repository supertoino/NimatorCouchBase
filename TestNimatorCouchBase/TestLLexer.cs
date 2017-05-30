using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.NimatorBooster.L.Lexical;
using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLLexer
    {
        [TestMethod]
        public void TestLexerOneBiggerTwoOk()
        {
            string phrase = "1>2";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);            
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);            
            Assert.AreEqual("1", tokens[0].Value);            
            Assert.AreEqual(LTokenType.Bigger, tokens[1].Type);            
            Assert.AreEqual(">", tokens[1].Value);            
            Assert.AreEqual(LTokenType.Long, tokens[2].Type);            
            Assert.AreEqual("2", tokens[2].Value);            
        }

        [TestMethod]
        public void TestLexerOneBiggerTwoWithWhiteSpacesOk()
        {
            string phrase = "1 > 2 ";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("1", tokens[0].Value);
            Assert.AreEqual(LTokenType.Bigger, tokens[1].Type);
            Assert.AreEqual(">", tokens[1].Value);
            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerOneEqualTwoOk()
        {
            string phrase = "1=2";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("1", tokens[0].Value);
            Assert.AreEqual(LTokenType.Equal, tokens[1].Type);
            Assert.AreEqual("=", tokens[1].Value);
            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerOneDot666SmallerTwoOk()
        {
            string phrase = "1.666<2";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Double, tokens[0].Type);
            Assert.AreEqual("1.666", tokens[0].Value);
            Assert.AreEqual(LTokenType.Smaller, tokens[1].Type);
            Assert.AreEqual("<", tokens[1].Value);
            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerTwoBiggerOneDot666Ok()
        {
            string phrase = "2>1.666";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("2", tokens[0].Value);
            Assert.AreEqual(LTokenType.Bigger, tokens[1].Type);
            Assert.AreEqual(">", tokens[1].Value);
            Assert.AreEqual(LTokenType.Double, tokens[2].Type);
            Assert.AreEqual("1.666", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerTenBiggerThousandOk()
        {
            string phrase = "10>1000";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("10", tokens[0].Value);
            Assert.AreEqual(LTokenType.Bigger, tokens[1].Type);
            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("1000", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerCpuEqualTwoOk()
        {
            string phrase = "Cpu=2";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Variable, tokens[0].Type);
            Assert.AreEqual("Cpu", tokens[0].Value);
            Assert.AreEqual(LTokenType.Equal, tokens[1].Type);
            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerTwoEqualCpuOk()
        {
            string phrase = "2=Cpu";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("2", tokens[0].Value);
            Assert.AreEqual(LTokenType.Equal, tokens[1].Type);
            Assert.AreEqual(LTokenType.Variable, tokens[2].Type);
            Assert.AreEqual("Cpu", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerCpuEqualSuperRamOk()
        {
            string phrase = "Cpu=SuperRam";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Variable, tokens[0].Type);
            Assert.AreEqual("Cpu", tokens[0].Value);
            Assert.AreEqual(LTokenType.Equal, tokens[1].Type);
            Assert.AreEqual(LTokenType.Variable, tokens[2].Type);
            Assert.AreEqual("SuperRam", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerCpuDotRateEqualSuperRamOk()
        {
            string phrase = "Cpu.Rate=SuperRam";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Variable, tokens[0].Type);
            Assert.AreEqual("Cpu.Rate", tokens[0].Value);
            Assert.AreEqual(LTokenType.Equal, tokens[1].Type);
            Assert.AreEqual(LTokenType.Variable, tokens[2].Type);
            Assert.AreEqual("SuperRam", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerCpu2RateEqualSuperRamOk()
        {
            string phrase = "Cpu2Rate=SuperRam";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Variable, tokens[0].Type);
            Assert.AreEqual("Cpu2Rate", tokens[0].Value);
            Assert.AreEqual(LTokenType.Equal, tokens[1].Type);
            Assert.AreEqual(LTokenType.Variable, tokens[2].Type);
            Assert.AreEqual("SuperRam", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexer1NotEqual2Ok()
        {
            string phrase = "1!=2";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("1", tokens[0].Value);
            Assert.AreEqual(LTokenType.Different, tokens[1].Type);
            Assert.AreEqual("!=", tokens[1].Value);
            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexer1BiggerEqualThen2Ok()
        {
            string phrase = "1>=2";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("1", tokens[0].Value);
            Assert.AreEqual(LTokenType.BiggerEqual, tokens[1].Type);
            Assert.AreEqual(">=", tokens[1].Value);
            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexer1SmallerEqualThen2Ok()
        {
            string phrase = "1<=2";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("1", tokens[0].Value);
            Assert.AreEqual(LTokenType.SmallerEqual, tokens[1].Type);
            Assert.AreEqual("<=", tokens[1].Value);
            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerEmpty()
        {
            string phrase = "";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(0, tokens.Count);
        }

        [TestMethod]
        public void TestLexerAllWhiteSpaces()
        {
            string phrase = "    ";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(0, tokens.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestLexerUnknowCaracthersSpacesShouldThrowException()
        {
            string phrase = "#@€&%{";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(0, tokens.Count);
        }

        [TestMethod]
        public void TestLexerWithSums()
        {
            string phrase = "1+2>2";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(5, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("1", tokens[0].Value);

            Assert.AreEqual(LTokenType.Plus, tokens[1].Type);
            Assert.AreEqual("+", tokens[1].Value);

            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);

            Assert.AreEqual(LTokenType.Bigger, tokens[3].Type);
            Assert.AreEqual(">", tokens[3].Value);

            Assert.AreEqual(LTokenType.Long, tokens[4].Type);
            Assert.AreEqual("2", tokens[4].Value);
        }

        [TestMethod]
        public void TestLexerWithMultiplication()
        {
            string phrase = "1*2>2";
            LLexer lLexer = new LLexer(phrase);
            List<LToken> tokens = new List<LToken>();
            while (lLexer.MoveNext())
            {
                tokens.Add(lLexer.Current);
            }
            Assert.AreEqual(5, tokens.Count);
            Assert.AreEqual(LTokenType.Long, tokens[0].Type);
            Assert.AreEqual("1", tokens[0].Value);

            Assert.AreEqual(LTokenType.Asterisk, tokens[1].Type);
            Assert.AreEqual("*", tokens[1].Value);

            Assert.AreEqual(LTokenType.Long, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);

            Assert.AreEqual(LTokenType.Bigger, tokens[3].Type);
            Assert.AreEqual(">", tokens[3].Value);

            Assert.AreEqual(LTokenType.Long, tokens[4].Type);
            Assert.AreEqual("2", tokens[4].Value);
        }
    }
}
