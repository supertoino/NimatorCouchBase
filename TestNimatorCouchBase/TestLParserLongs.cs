using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.NimatorBooster.L.Lexical;
using NimatorCouchBase.NimatorBooster.L.Parser;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLParserLongs
    {
        [TestMethod]
        public void TestParserTwoBiggerThanOneShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("2>1");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParserOneBiggerThanTwoShouldReturnFalse()
        {
            LLexer lLexer = new LLexer("1>2");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        [TestMethod]
        public void TestParserOneSmallerThanTwoShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("1<2");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParserTwoEqualsTwoShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("2=2");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParserOneDiferentThanTwoShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("1!=2");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParserOneDiferentThanOneShouldReturnFalse()
        {
            LLexer lLexer = new LLexer("1!=1");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        [TestMethod]
        public void TestParser10BiggerEqualThanOneShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("10>=1");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue((bool)result.Value);
        }

        [TestMethod]
        public void TestParser10SmallerEqualThanOneShouldReturnFalse()
        {
            LLexer lLexer = new LLexer("10<=1");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

         [TestMethod]
        public void TestParser10Plus1BiggerThan10ShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("10+1>10");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void TestTenPlusOnePlus10SmallerThen100ShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("10+1+10<100");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test10Times2Equals20ShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("10*2=20");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test10Times2Plus5BiggerEqualThan25ShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("10*2+5>=25");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test10Times2Plus5Equals15ShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("10*2-5=15");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test100Div10Equals10ShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("100/10=10");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [TestMethod]
        public void Test10Times2Plus5BiggerThan1ShouldReturnTrue()
        {
            LLexer lLexer = new LLexer("10*2+5>1");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }
    }
}
