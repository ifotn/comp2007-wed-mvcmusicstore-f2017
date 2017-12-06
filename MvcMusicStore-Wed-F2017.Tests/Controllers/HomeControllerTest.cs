using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMusicStore_Wed_F2017;
using MvcMusicStore_Wed_F2017.Controllers;

namespace MvcMusicStore_Wed_F2017.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        // make HomeController global in this test class
        HomeController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            controller = new HomeController();
        }

        [TestMethod]
        public void Index()
        {

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void AboutFails()
        {

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("This should fail", result.ViewBag.Message);
     
        }

        [TestMethod]
        public void Contact()
        {

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SumPass()
        {
            // arrange
            int x = 10;
            int y = 20;
            int expResult = 30;

            // act
            int result = controller.sum(x, y);

            // assert
            Assert.AreEqual(expResult, result);
        }

        [TestMethod]
        public void SumFail()
        {
            // arrange
            int x = 10;
            int y = 20;
            int expResult = 40;

            // act
            int result = controller.sum(x, y);

            // assert
            Assert.AreNotEqual(expResult, result);

        }
    }
}
