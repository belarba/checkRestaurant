using Microsoft.VisualStudio.TestTools.UnitTesting;
using Daniel.Controllers;
using System.Collections.Generic;
using System;

namespace RestaurantTest
{
    [TestClass]
    public class TestTrataHora
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new HomeController();

            string horario = "10:59";
            System.TimeSpan expected = new System.TimeSpan(10, 59, 0);

            System.TimeSpan actual = controller.TrataHora(horario);

            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void TestMethod2()
        {
            var controller = new HomeController();

            List<string> listResult = controller.availableHours("../../../Data/restaurant-hours.csv","00:00");

            Assert.AreEqual(0, listResult.Count);

        }

        [TestMethod]
        public void TestMethod3()
        {
            var controller = new HomeController();

            List<string> listResult = controller.availableHours("../../../Data/restaurant-hours.csv", "12:00");

            Assert.IsNotNull(listResult.Count);

        }
    }
}
