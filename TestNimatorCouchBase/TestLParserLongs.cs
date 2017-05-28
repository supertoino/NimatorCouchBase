using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.Entities.L;
using NimatorCouchBase.Entities.L.Lexer;
using NimatorCouchBase.Entities.L.Parser;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLParserLongs
    {
        [TestMethod]
        public void TestParserTwoBiggerThanOneShouldReturnTrue()
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
        public void TestParserOneBiggerThanTwoShouldReturnFalse()
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
        public void TestParserOneSmallerThanTwoShouldReturnTrue()
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

         [TestMethod]
        public void TestParser10Plus1BiggerThan10ShouldReturnTrue()
        {
            Lexer lexer = new Lexer("10+1>10");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void TestTenPlusOnePlus10SmallerThen100ShouldReturnTrue()
        {
            Lexer lexer = new Lexer("10+1+10<100");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test10Times2Equals20ShouldReturnTrue()
        {
            Lexer lexer = new Lexer("10*2=20");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test10Times2Plus5BiggerEqualThan25ShouldReturnTrue()
        {
            Lexer lexer = new Lexer("10*2+5>=25");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test10Times2Plus5Equals15ShouldReturnTrue()
        {
            Lexer lexer = new Lexer("10*2-5=15");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test100Div10Equals10ShouldReturnTrue()
        {
            Lexer lexer = new Lexer("100/10=10");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test10Times2Plus5BiggerThan1ShouldReturnTrue()
        {
            Lexer lexer = new Lexer("10*2+5>1");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }
    }
}
