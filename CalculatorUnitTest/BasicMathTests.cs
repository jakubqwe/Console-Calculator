using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using calculator;
using OperatorsDLL;

namespace CalculatorUnitTest
{
    [TestClass]
    public class BasicMathTests
    {
        [TestMethod]
        public void Add4And3Return7()
        {
            var addingOperator = OperatorFactory.Create<Addition>();
            var result = addingOperator.CalculateOperator(3, 4);
            Assert.AreEqual(7, result);
        }
        [TestMethod]
        public void Subtract100And8Return92()
        {
            var subtractingOperator = OperatorFactory.Create<Subtraction>();
            var result = subtractingOperator.CalculateOperator(100, 8);
            Assert.AreEqual(92, result);
        }
        [TestMethod]
        public void Multiply8And2Point5Return20()
        {
            var multiplyingOperator = OperatorFactory.Create<Multiplication>();
            var result = multiplyingOperator.CalculateOperator(8, (decimal)2.5);
            Assert.AreEqual(20, result);
        }
        [TestMethod]
        public void Divide5By2Return2Point5()
        {
            var divisionOperator = OperatorFactory.Create<Division>();
            var result = divisionOperator.CalculateOperator(5, 2);
            Assert.AreEqual((decimal)2.5, result);
        }
        [TestMethod]
        public void Power2And10Return1024()
        {
            var powerOperator = OperatorFactory.Create<Power>();
            var result = powerOperator.CalculateOperator(2, 10);
            Assert.AreEqual(1024, result);
        }
        [TestMethod]
        public void Mod3And2Return1()
        {
            var moduloOperator = OperatorFactory.Create<Modulo>();
            var result = moduloOperator.CalculateOperator(3, 2);
            Assert.AreEqual(1, result);
        }
    }
}
