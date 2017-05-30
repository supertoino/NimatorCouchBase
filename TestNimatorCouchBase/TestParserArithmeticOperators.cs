using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.NimatorBooster.L;
using NimatorCouchBase.NimatorBooster.L.Tokens;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestParserArithmeticOperators
    {
        [TestMethod]
        public void TestAllArithmeticOperations()
        {
            LValidator validator = new LValidator();
            
            string code = $"1{LTokenType.Plus.GetFunctionSyntax()}1=0.5*2+0.5*2";
            Assert.IsTrue(validator.ValidateLExpression(code), code);

            code = $"1{LTokenType.Asterisk.GetFunctionSyntax()}1=0.5*0+0.5*2";
            Assert.IsTrue(validator.ValidateLExpression(code), code);

            code = $"1{LTokenType.Divide.GetFunctionSyntax()}1=0.5+0.5*1";
            Assert.IsTrue(validator.ValidateLExpression(code), code);

            code = $"2{LTokenType.Minus.GetFunctionSyntax()}1=0.5+0.5";
            Assert.IsTrue(validator.ValidateLExpression(code), code);
        }

        [TestMethod]        
        public void TestMultiplyByZero()
        {
            LValidator validator = new LValidator();
            string code = $"1{LTokenType.Asterisk.GetFunctionSyntax()}0=0";
            Assert.IsTrue(validator.ValidateLExpression(code), code);
        }

        [TestMethod]
        public void TestZero()
        {
            LValidator validator = new LValidator();
            string code = $"0{LTokenType.Asterisk.GetFunctionSyntax()}0=0";
            Assert.IsTrue(validator.ValidateLExpression(code), code);

            code = $"0{LTokenType.Minus.GetFunctionSyntax()}0=0";
            Assert.IsTrue(validator.ValidateLExpression(code), code);

            code = $"0{LTokenType.Plus.GetFunctionSyntax()}0=0";
            Assert.IsTrue(validator.ValidateLExpression(code), code);

        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivisionDivideByZero()
        {
            LValidator validator = new LValidator();
            string code = $"1{LTokenType.Divide.GetFunctionSyntax()}0=0.5*2+0.5*2";
            Assert.IsFalse(validator.ValidateLExpression(code), code);
        }

        [TestMethod]
        public void TestDivisionDivideZero()
        {
            LValidator validator = new LValidator();
            string code = $"0{LTokenType.Divide.GetFunctionSyntax()}1!=0.5*2+0.5*2";
            Assert.IsTrue(validator.ValidateLExpression(code), code);
        }
    }
}
