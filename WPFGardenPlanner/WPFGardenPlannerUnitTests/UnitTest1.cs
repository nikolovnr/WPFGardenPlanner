using System;
using GardenPlanner;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPFGardenPlannerUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int plantId = 2;
            string expected = "ddd";
            Database db = new Database();
            PlantsLookup p = new PlantsLookup();
            p = db.GetPlantById(plantId);
            string actual = p.PictureSource;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            
        }
    }
}
