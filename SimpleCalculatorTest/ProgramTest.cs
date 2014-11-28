using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleCalculator;

namespace SimpleCalculatorTest
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void CalculatorTest()
        {
            string arg1 = "5";
            string arg2 = "6";
            string op = "+";

            long expected = 11;
            ArgumentsParser argp = new ArgumentsParser();
            CalculationArgument carg = argp.Parse(arg1, arg2, op);
            Calculator calc= new Calculator();
            calc.Process(carg);
            Assert.AreEqual(expected, calc.Result);
        }
    }
}
