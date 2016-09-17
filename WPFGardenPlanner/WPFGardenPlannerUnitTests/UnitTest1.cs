using System;
using GardenPlanner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace WPFGardenPlannerUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GettLinkToImage()
        {
            int plantId = 2;
            string expected = "pack://application:,,,/Pictures/Plants/Artichoke.png";
            Database db = new Database();
            PlantsLookup p = new PlantsLookup();
            p = db.GetPlantById(plantId);
            string actual = p.PictureSource;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetGardenBeds()
        {
            Database db = new Database();
            List<Bed> b = new List<Bed>();
            b = db.GetBedByGardenId(4);
            Assert.AreNotEqual(b, null);
        }

        [TestMethod]
        public void GetGardenPlant()
        {
            Database db = new Database();
            PlantsLookup b = new PlantsLookup();
            b = db.GetPlantById(2);
            Assert.AreNotEqual(b, null);
        }
    }
}
