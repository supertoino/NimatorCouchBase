using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.Entities.L.Lexer;
using NimatorCouchBase.Entities.L.Parser;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLParserDoubles
    {
        [TestMethod]
        public void TestParser10Dot10SmallerEqualThanOneShouldReturnFalse()
        {
            Lexer lexer = new Lexer("10.10<=1");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }

        [TestMethod]
        public void TestParser10Dot10SmallerEqualThanOneDotFiveShouldReturnFalse()
        {
            Lexer lexer = new Lexer("10.10<=1.5");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsFalse((bool)result.Value);
        }
    }
}
