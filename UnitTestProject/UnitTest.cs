using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethodCheckObjective()
        {
            //Vez do player e ele não possui mais de 20 territorios
            Assert.AreEqual(TerritoryControllerTest.checkObjective(true, 15, 20), false);

            //Vez do player e ele possui 20 ou mais territorios
            Assert.AreEqual(TerritoryControllerTest.checkObjective(true, 20, 15), true);

            //Vez da IA e ela possui menos de 20 territorios
            Assert.AreEqual(TerritoryControllerTest.checkObjective(false, 20, 15), false);

            //Vez da IA e ela possui 20 ou mais territorios
            Assert.AreEqual(TerritoryControllerTest.checkObjective(false, 15, 20), true);
        }
    }
}
