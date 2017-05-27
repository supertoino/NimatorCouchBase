﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NimatorCouchBase.Entities.L;

namespace TestNimatorCouchBase
{
    [TestClass]
    public class TestLLexer
    {
        [TestMethod]
        public void TestLexerOneBiggerTwoOk()
        {
            string phrase = "1>2";
            Lexer lexer = new Lexer(phrase);
            List<Token> tokens = new List<Token>();
            while (lexer.MoveNext())
            {
                tokens.Add(lexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);            
            Assert.AreEqual(TokenType.Scalar, tokens[0].Type);            
            Assert.AreEqual("1", tokens[0].Value);            
            Assert.AreEqual(TokenType.Bigger, tokens[1].Type);            
            Assert.AreEqual(">", tokens[1].Value);            
            Assert.AreEqual(TokenType.Scalar, tokens[2].Type);            
            Assert.AreEqual("2", tokens[2].Value);            
        }

        [TestMethod]
        public void TestLexerOneEqualTwoOk()
        {
            string phrase = "1=2";
            Lexer lexer = new Lexer(phrase);
            List<Token> tokens = new List<Token>();
            while (lexer.MoveNext())
            {
                tokens.Add(lexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(TokenType.Scalar, tokens[0].Type);
            Assert.AreEqual("1", tokens[0].Value);
            Assert.AreEqual(TokenType.Equal, tokens[1].Type);
            Assert.AreEqual("=", tokens[1].Value);
            Assert.AreEqual(TokenType.Scalar, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerOneDot666EqualTwoOk()
        {
            string phrase = "1.666<2";
            Lexer lexer = new Lexer(phrase);
            List<Token> tokens = new List<Token>();
            while (lexer.MoveNext())
            {
                tokens.Add(lexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(TokenType.Scalar, tokens[0].Type);
            Assert.AreEqual("1.666", tokens[0].Value);
            Assert.AreEqual(TokenType.Smaller, tokens[1].Type);
            Assert.AreEqual("<", tokens[1].Value);
            Assert.AreEqual(TokenType.Scalar, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerTenBiggerThousandOk()
        {
            string phrase = "10>1000";
            Lexer lexer = new Lexer(phrase);
            List<Token> tokens = new List<Token>();
            while (lexer.MoveNext())
            {
                tokens.Add(lexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(TokenType.Scalar, tokens[0].Type);
            Assert.AreEqual("10", tokens[0].Value);
            Assert.AreEqual(TokenType.Bigger, tokens[1].Type);
            Assert.AreEqual(TokenType.Scalar, tokens[2].Type);
            Assert.AreEqual("1000", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerCpuEqualTwoOk()
        {
            string phrase = "Cpu=2";
            Lexer lexer = new Lexer(phrase);
            List<Token> tokens = new List<Token>();
            while (lexer.MoveNext())
            {
                tokens.Add(lexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(TokenType.Variable, tokens[0].Type);
            Assert.AreEqual("Cpu", tokens[0].Value);
            Assert.AreEqual(TokenType.Equal, tokens[1].Type);
            Assert.AreEqual(TokenType.Scalar, tokens[2].Type);
            Assert.AreEqual("2", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerTwoEqualCpuOk()
        {
            string phrase = "2=Cpu";
            Lexer lexer = new Lexer(phrase);
            List<Token> tokens = new List<Token>();
            while (lexer.MoveNext())
            {
                tokens.Add(lexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(TokenType.Scalar, tokens[0].Type);
            Assert.AreEqual("2", tokens[0].Value);
            Assert.AreEqual(TokenType.Equal, tokens[1].Type);
            Assert.AreEqual(TokenType.Variable, tokens[2].Type);
            Assert.AreEqual("Cpu", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerCpuBiggerSuperRamOk()
        {
            string phrase = "Cpu=SuperRam";
            Lexer lexer = new Lexer(phrase);
            List<Token> tokens = new List<Token>();
            while (lexer.MoveNext())
            {
                tokens.Add(lexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(TokenType.Variable, tokens[0].Type);
            Assert.AreEqual("Cpu", tokens[0].Value);
            Assert.AreEqual(TokenType.Equal, tokens[1].Type);
            Assert.AreEqual(TokenType.Variable, tokens[2].Type);
            Assert.AreEqual("SuperRam", tokens[2].Value);
        }

        [TestMethod]
        public void TestLexerCpuDotRateBiggerSuperRamOk()
        {
            string phrase = "Cpu.Rate=SuperRam";
            Lexer lexer = new Lexer(phrase);
            List<Token> tokens = new List<Token>();
            while (lexer.MoveNext())
            {
                tokens.Add(lexer.Current);
            }
            Assert.AreEqual(3, tokens.Count);
            Assert.AreEqual(TokenType.Variable, tokens[0].Type);
            Assert.AreEqual("Cpu.Rate", tokens[0].Value);
            Assert.AreEqual(TokenType.Equal, tokens[1].Type);
            Assert.AreEqual(TokenType.Variable, tokens[2].Type);
            Assert.AreEqual("SuperRam", tokens[2].Value);
        }
    }
}