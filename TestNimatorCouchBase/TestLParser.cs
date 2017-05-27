using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.Entities.L;
using NimatorCouchBase.Entities.L.Expressions;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLParser
    {
        [TestMethod]
        public void TestParserTwoBiggerOneOk()
        {
            Lexer lexer = new Lexer("2>1");
            Parser parser = new LParser(lexer);
            IExpression result = parser.ParseExpression();
            StringBuilder stringBuilder = new StringBuilder();
            result.Print(stringBuilder);
            Console.WriteLine(stringBuilder);
            Assert.IsTrue(true);
        }
    }
}
