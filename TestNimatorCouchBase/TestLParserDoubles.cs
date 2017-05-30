using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.NimatorBooster.L;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLParserDoubles
    {
        [TestMethod]
        public void TestParser10Dot10SmallerEqualThanOneShouldReturnFalse()
        {
            LLexer lLexer = new LLexer("10.10<=1");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        [TestMethod]
        public void TestParser10Dot10SmallerEqualThanOneDotFiveShouldReturnFalse()
        {
            LLexer lLexer = new LLexer("10.10<=1.5");
            BaseParser parser = new LParser(lLexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }
    }
}
