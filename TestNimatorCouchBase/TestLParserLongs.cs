using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.Entities.L;
using NimatorCouchBase.Entities.L.Lexer;
using NimatorCouchBase.Entities.L.Parser;
using NimatorCouchBase.Entities.L.Parser.Expressions;
using NimatorCouchBase.Entities.L.Parser.Expressions.Interfaces;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLParserLongs
    {
        [TestMethod]
        public void TestParserTwoBiggerOneShouldReturnTrue()
        {
            Lexer lexer = new Lexer("2>1");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParserOneBiggerTwoShouldReturnFalse()
        {
            Lexer lexer = new Lexer("1>2");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        [TestMethod]
        public void TestParserOneSmallerTwoShouldReturnTrue()
        {
            Lexer lexer = new Lexer("1<2");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParserTwoEqualsTwoShouldReturnTrue()
        {
            Lexer lexer = new Lexer("2=2");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParserOneDiferentThanTwoShouldReturnTrue()
        {
            Lexer lexer = new Lexer("1!=2");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParserOneDiferentThanOneShouldReturnFalse()
        {
            Lexer lexer = new Lexer("1!=1");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        [TestMethod]
        public void TestParser10BiggerEqualThanOneShouldReturnTrue()
        {
            Lexer lexer = new Lexer("10>=1");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParser10SmallerEqualThanOneShouldReturnFalse()
        {
            Lexer lexer = new Lexer("10<=1");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        
    }
}
